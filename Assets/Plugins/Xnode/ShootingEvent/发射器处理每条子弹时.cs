using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.发射器事件
{
	
	public class 发射器处理每条子弹时 : Node
	{
	
		[Output] public FunctionProgress 下一步;
		[Output]
		public Shooting 发射器;
		[Output]
		public Bullet 子弹;
		Shooting shooting;
		Bullet bullet;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName,List<object> param = null) 
		{
		
			shooting = (Shooting)param[0];
			bullet = (Bullet)param[1];
			ConnectDo("下一步");
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			if(port.fieldName =="发射器")
				if (shooting != null)
					return shooting; // Replace this
			if (port.fieldName == "子弹")
				if (bullet != null)
				return bullet;
			return null;
		}
	}
}