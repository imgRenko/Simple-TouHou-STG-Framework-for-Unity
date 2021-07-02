using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.物理
{
	public class 子弹触发器反弹 : Node
	{
		[Input] public FunctionProgress 进入节点;
		
		[Input] public Bullet 子弹;
		[Input] public Trigger 触发器;
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
			子弹.Rebound(子弹, 触发器);
			ConnectDo("退出节点");
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}
	}
}