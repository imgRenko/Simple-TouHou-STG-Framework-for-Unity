using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using 变量;
namespace 变量
{
	public class 数组类型处理 : Node
	{
		[Sirenix.OdinInspector.InfoBox("这个不是一个节点，但它是部分数组节点的基类。")]
		public void AddDmyOutputPort(数组.ValueType 变量类型, string a)
		{
			switch (变量类型)
			{
				case 数组.ValueType.双精度浮点:
					AddDynamicOutput(typeof(List<double>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.浮点数:
					AddDynamicOutput(typeof(List<float>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.整数:
					AddDynamicOutput(typeof(List<int>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.长整数:
					AddDynamicOutput(typeof(List<long>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.短整数:
					AddDynamicOutput(typeof(List<short>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.布尔值:
					AddDynamicOutput(typeof(List<bool>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.文本:
					AddDynamicOutput(typeof(List<string>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.发射器:
					AddDynamicOutput(typeof(List<Shooting>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.力场:
					AddDynamicOutput(typeof(List<Force>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.发射器曲线事件:
					AddDynamicOutput(typeof(List<BasedEvent_ShootingLocomotion>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.向量:
					AddDynamicOutput(typeof(List<Vector3>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.子弹曲线事件:
					AddDynamicOutput(typeof(List<BasedEvent_BulletLocomotion>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.敌人:
					AddDynamicOutput(typeof(List<Enemy>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				
				case 数组.ValueType.自绘图案:
					AddDynamicOutput(typeof(List<CustomedPattern>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.角色:
					AddDynamicOutput(typeof(List<Character>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.触发器:
					AddDynamicOutput(typeof(List<Trigger>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.贝塞尔曲线:
					AddDynamicOutput(typeof(List<BezierDrawer>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.颜色:
					AddDynamicOutput(typeof(List<Color>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;

				case 数组.ValueType.精灵纹理:
					AddDynamicOutput(typeof(List<Sprite>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.关卡流程控制器:
					AddDynamicOutput(typeof(List<StageControl>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.动画播放系统:
					AddDynamicOutput(typeof(List<Animator>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.声音播放系统:
					AddDynamicOutput(typeof(List<AudioSource>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.弹幕连接器:
					AddDynamicOutput(typeof(List<Link>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.新版剧情系统:
					AddDynamicOutput(typeof(List<DialogSystemInit>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.旧版剧情系统:
					AddDynamicOutput(typeof(List<WrittenControlNew>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.符卡系统:
					AddDynamicOutput(typeof(List<SpellCard>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;


			}


		}
		public void AddDmyOutputPortNotList(数组.ValueType 变量类型, string a)
		{
			switch (变量类型)
			{
				case 数组.ValueType.双精度浮点:
					AddDynamicOutput(typeof(double), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.浮点数:
					AddDynamicOutput(typeof(float), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.整数:
					AddDynamicOutput(typeof(int), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.长整数:
					AddDynamicOutput(typeof(long), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.短整数:
					AddDynamicOutput(typeof(short), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.布尔值:
					AddDynamicOutput(typeof(bool), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.文本:
					AddDynamicOutput(typeof(string), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.发射器:
					AddDynamicOutput(typeof(Shooting), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.力场:
					AddDynamicOutput(typeof(Force), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.发射器曲线事件:
					AddDynamicOutput(typeof(BasedEvent_ShootingLocomotion), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.向量:
					AddDynamicOutput(typeof(Vector3), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.子弹曲线事件:
					AddDynamicOutput(typeof(BasedEvent_BulletLocomotion), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.敌人:
					AddDynamicOutput(typeof(Enemy), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				
				case 数组.ValueType.自绘图案:
					AddDynamicOutput(typeof(CustomedPattern), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.角色:
					AddDynamicOutput(typeof(Character), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.触发器:
					AddDynamicOutput(typeof(Trigger), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.贝塞尔曲线:
					AddDynamicOutput(typeof(BezierDrawer), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.颜色:
					AddDynamicOutput(typeof(Color), ConnectionType.Multiple, TypeConstraint.None, a);
					break;

				case 数组.ValueType.精灵纹理:
					AddDynamicOutput(typeof(Sprite), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.关卡流程控制器:
					AddDynamicOutput(typeof(StageControl), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.动画播放系统:
					AddDynamicOutput(typeof(Animator), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.声音播放系统:
					AddDynamicOutput(typeof(AudioSource), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.弹幕连接器:
					AddDynamicOutput(typeof(Link), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.新版剧情系统:
					AddDynamicOutput(typeof(DialogSystemInit), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.旧版剧情系统:
					AddDynamicOutput(typeof(WrittenControlNew), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.符卡系统:
					AddDynamicOutput(typeof(SpellCard), ConnectionType.Multiple, TypeConstraint.None, a);
					break;


			}


		}
		public void AddDmyInputPort(数组.ValueType 变量类型, string a)
		{
			switch (变量类型)
			{
				case 数组.ValueType.双精度浮点:
					AddDynamicInput(typeof(List<double>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.浮点数:
					AddDynamicInput(typeof(List<float>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.整数:
					AddDynamicInput(typeof(List<int>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.长整数:
					AddDynamicInput(typeof(List<long>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.短整数:
					AddDynamicInput(typeof(List<short>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.布尔值:
					AddDynamicInput(typeof(List<bool>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.文本:
					AddDynamicInput(typeof(List<string>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.发射器:
					AddDynamicInput(typeof(List<Shooting>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.力场:
					AddDynamicInput(typeof(List<Force>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.发射器曲线事件:
					AddDynamicInput(typeof(List<BasedEvent_ShootingLocomotion>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.向量:
					AddDynamicInput(typeof(List<Vector3>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.子弹曲线事件:
					AddDynamicInput(typeof(List<BasedEvent_BulletLocomotion>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.敌人:
					AddDynamicInput(typeof(List<Enemy>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
			
				case 数组.ValueType.自绘图案:
					AddDynamicInput(typeof(List<CustomedPattern>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.角色:
					AddDynamicInput(typeof(List<Character>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.触发器:
					AddDynamicInput(typeof(List<Trigger>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.贝塞尔曲线:
					AddDynamicInput(typeof(List<BezierDrawer>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.颜色:
					AddDynamicInput(typeof(List<Color>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;

				case 数组.ValueType.精灵纹理:
					AddDynamicInput(typeof(List<Sprite>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.关卡流程控制器:
					AddDynamicInput(typeof(List<StageControl>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.动画播放系统:
					AddDynamicInput(typeof(List<Animator>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.声音播放系统:
					AddDynamicInput(typeof(List<AudioSource>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.弹幕连接器:
					AddDynamicInput(typeof(List<Link>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.新版剧情系统:
					AddDynamicInput(typeof(List<DialogSystemInit>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.旧版剧情系统:
					AddDynamicInput(typeof(List<WrittenControlNew>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.符卡系统:
					AddDynamicInput(typeof(List<SpellCard>), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
			}

		}
		public void AddDmyInputNotListPort(数组.ValueType 变量类型, string a)
		{
			switch (变量类型)
			{
				case 数组.ValueType.双精度浮点:
					AddDynamicInput(typeof(double), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.浮点数:
					AddDynamicInput(typeof(float), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.整数:
					AddDynamicInput(typeof(int), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.长整数:
					AddDynamicInput(typeof(long), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.短整数:
					AddDynamicInput(typeof(short), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.布尔值:
					AddDynamicInput(typeof(bool), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.文本:
					AddDynamicInput(typeof(string), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.发射器:
					AddDynamicInput(typeof(Shooting), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.力场:
					AddDynamicInput(typeof(Force), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.发射器曲线事件:
					AddDynamicInput(typeof(BasedEvent_ShootingLocomotion), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.向量:
					AddDynamicInput(typeof(Vector3), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.子弹曲线事件:
					AddDynamicInput(typeof(BasedEvent_BulletLocomotion), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.敌人:
					AddDynamicInput(typeof(Enemy), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				
				case 数组.ValueType.自绘图案:
					AddDynamicInput(typeof(CustomedPattern), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.角色:
					AddDynamicInput(typeof(Character), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.触发器:
					AddDynamicInput(typeof(Trigger), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.贝塞尔曲线:
					AddDynamicInput(typeof(BezierDrawer), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.颜色:
					AddDynamicInput(typeof(Color), ConnectionType.Multiple, TypeConstraint.None, a);
					break;

				case 数组.ValueType.精灵纹理:
					AddDynamicInput(typeof(Sprite), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.关卡流程控制器:
					AddDynamicInput(typeof(StageControl), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.动画播放系统:
					AddDynamicInput(typeof(Animator), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.声音播放系统:
					AddDynamicInput(typeof(AudioSource), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.弹幕连接器:
					AddDynamicInput(typeof(Link), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.新版剧情系统:
					AddDynamicInput(typeof(DialogSystemInit), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.旧版剧情系统:
					AddDynamicInput(typeof(WrittenControlNew), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
				case 数组.ValueType.符卡系统:
					AddDynamicInput(typeof(SpellCard), ConnectionType.Multiple, TypeConstraint.None, a);
					break;
			}

		}
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}
	}
}