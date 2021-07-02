using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.子弹
{
	public class 子弹瞄准自机 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public Bullet 子弹;
	
		[Output] public FunctionProgress 退出节点;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName, List<object> param = null)
		{
			子弹 = GetInputValue("子弹", 子弹);
			if (子弹 != null)
				 子弹.AimToPlayerObject();
			ConnectDo("退出节点");
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return null;
		}
	}
}