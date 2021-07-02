using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.发射器事件
{
	
	public class 发射器发射后 : Node
	{
		
		[Output] public FunctionProgress 下一步;
		[Output]
		public Shooting 发射器;

		Shooting shooting;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName,List<object> param = null) 
		{
		
			shooting = (Shooting)param[0];
			ConnectDo("下一步");
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			if (shooting != null)
				return shooting; // Replace this
			return null;
		}
	}
}