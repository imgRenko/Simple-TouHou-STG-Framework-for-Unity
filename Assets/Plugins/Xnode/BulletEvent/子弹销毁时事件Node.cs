using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.子弹事件
{
	
	public class 子弹销毁时事件Node : Node
	{
		

		[Output] public FunctionProgress 下一步;
		[Output]
		public Bullet 子弹;
		Bullet bullet;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName,List<object> param = null) 
		{
			
			bullet = (Bullet)param[0];
			ConnectDo("下一步");
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			if (bullet != null)
				return bullet; // Replace this
			return null;
		}
	}
}