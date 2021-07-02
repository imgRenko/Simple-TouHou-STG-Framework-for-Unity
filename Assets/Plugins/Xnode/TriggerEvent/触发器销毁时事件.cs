using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.触发器事件
{
	
	public class 触发器销毁时事件 : Node
	{
		

		[Output] public FunctionProgress 下一步;
		[Output]
		public Trigger 触发器;

			
		Trigger trigger;


		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName,List<object> param = null) 
		{
		
			trigger = (Trigger)param[0];
		
	
			ConnectDo("下一步");
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			
				if (trigger != null)
					return trigger; // Replace this

	
			return null;
		}
	}
}