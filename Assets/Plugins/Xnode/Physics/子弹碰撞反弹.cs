using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
public class BulletPhycicsInfo
{
	public float Xspd, Yspd;
	public float X, Y;
	public float Radius;
	public Bullet TarBullet;

	public void SetBulletSpeed() {
		TarBullet.SetSpeedVector(new Vector2(Xspd, Yspd));
	}
	public void SetInfo(Bullet bullet) {
		
		Radius = bullet.Radius;
		TarBullet = bullet;
	}
	public void UpdateInfo() {
		Vector2 BulletSpd = TarBullet.GetSpeedVector();
		Xspd = BulletSpd.x;
		Yspd = BulletSpd.y;
		X = TarBullet.BulletTransform.position.x;
		Y = TarBullet.BulletTransform.position.y;
	}
}
namespace 基础事件.物理
{

	public class 子弹碰撞反弹 : Node
	{
		[Sirenix.OdinInspector.InfoBox("这项功能一般在“基本事件->触发器事件->额外子弹进入触发器事件”里使用，若需要精确计算，需要触发器的判定方式为圆形，碰撞半径和子弹的碰撞半径相同。此外，因为需要两个子弹参与互动，必须要让触发器来检测额外子弹，所以发射子弹的发射器，必须要让子弹携带触发器额外探查指针，否则额外子弹进入触发器事件无法触发。")]
		[Input] public FunctionProgress 进入节点;
		
		[Input] public Trigger 触发器;
		[Input] public Bullet 子弹;
		[Output] public FunctionProgress 退出节点;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName, List<object> param = null)
		{
			子弹 = GetInputValue<Bullet>("子弹");
			触发器 = GetInputValue<Trigger>("触发器");
			BulletPhycicsInfo bullet1 = new BulletPhycicsInfo();
			bullet1.SetInfo(子弹);
			bullet1.UpdateInfo();
			List<BulletPhycicsInfo> infos = new List<BulletPhycicsInfo>();
			foreach (var a in 触发器.Extra)
			{
				BulletPhycicsInfo bullet = new BulletPhycicsInfo();
				if (a.bullet == 子弹)
					continue;
				bullet.SetInfo(a.bullet);
				bullet.UpdateInfo();
				infos.Add(bullet);
			
			}
		

			Collision(bullet1, infos);

			bullet1.SetBulletSpeed();
			foreach (var r in infos)
			{
				r.SetBulletSpeed();
			}
			ConnectDo("退出节点");
		}

		public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}

		Vector2 DoBound(float x1, float y1, float x2, float y2)
		{
			// v反弹 = v - 2 * (V * N) * N
			Vector2 v1, v2;

			v1 = new Vector2(x1, y1);
			v2 = new Vector2(x2, y2);
			v2 = v2.normalized;

			Vector2 v3 = v2;

			float Dot = Vector2.Dot(v1, v3);
			float x_Value = x1 - 2 * Dot * v3.x;
			float y_Value = y1 - 2 * Dot * v3.y;

			return (new Vector2(x_Value, y_Value));

		}

		bool isMistaking(BulletPhycicsInfo b1, BulletPhycicsInfo b2)
		{
			Vector2 b1Vec = new Vector2(b1.Xspd, b1.Yspd);
			Vector2 cenVec = new Vector2(b2.X - b1.X, b2.Y - b1.Y);
			if (Vector2.Dot(b1Vec, cenVec) < 0)
				return true;
			else
				return false;
		}
		// 碰撞
		void Collision(BulletPhycicsInfo Bullet, List<BulletPhycicsInfo> CollisedBullets)
		{
			for (int i = 0; i != CollisedBullets.Count; ++i)
			{
				BulletPhycicsInfo CollisedBullet = CollisedBullets[i];
				if (!Object.Equals(CollisedBullet, Bullet))
				{
					float dis = Vector2.Distance(new Vector2(Bullet.X, Bullet.Y), new Vector2(CollisedBullet.X, CollisedBullet.Y));

					if (!isMistaking(Bullet, CollisedBullet))
					{
						
						Vector2 rel1 = DoBound(Bullet.Xspd, Bullet.Yspd, Bullet.X - CollisedBullet.X, Bullet.Y - CollisedBullet.Y);

						Bullet.Xspd = rel1.x;
						Bullet.Yspd = rel1.y;

						rel1 = DoBound(CollisedBullet.Xspd, CollisedBullet.Yspd, Bullet.X - CollisedBullet.X, Bullet.Y - CollisedBullet.Y);

						CollisedBullet.Xspd = rel1.x;
						CollisedBullet.Yspd = rel1.y;

					}
				}


			}
		}
	}
}