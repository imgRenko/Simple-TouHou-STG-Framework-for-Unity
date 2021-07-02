using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.触发器事件
{
	
	public class 额外子弹在触发器内时事件 : Node
	{
		

		[Output] public FunctionProgress 下一步;
		[Output]
		public Trigger 触发器;
			[Output]
		public Bullet 子弹;
				[Output]
		public Bullet 触发器子弹;
			
		Trigger trigger;
		Bullet bullet;
Bullet bullet2;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName,List<object> param = null) 
		{
		
			trigger = (Trigger)param[0];
				bullet = (Bullet )param[1];
	bullet2 = (Bullet )param[2];
			ConnectDo("下一步");
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
				if(port.fieldName =="触发器")
				if (trigger != null)
					return trigger; // Replace this
			if (port.fieldName == "子弹")
				if (bullet != null)
				return bullet;
	if (port.fieldName == "触发器子弹")
				if (bullet2 != null)
				return bullet2;
			return null;
		}
	}
}