using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.子弹事件
{
	
	public class 子弹改变父级时事件Node : Node
	{
		

		[Output] public FunctionProgress 下一步;
		[Output]
		public Bullet 子弹;
		[Output]
		public Bullet 父级子弹;
		Bullet bullet;
		Bullet parentBullet;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName,List<object> param = null) 
		{
			
			bullet = (Bullet)param[0];
			parentBullet = (Bullet)param[1];
			ConnectDo("下一步");
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			if (port.fieldName == "子弹")
			{
				if (bullet != null)
					return bullet;
			}
			if (port.fieldName == "父级子弹")
			{
				if (parentBullet != null)
					return parentBullet;
			}
			return null;
		}
	}
}