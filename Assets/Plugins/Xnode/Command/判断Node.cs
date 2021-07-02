using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using XNode;
namespace 流程控制
{
	public class 判断Node : Node
	{

		[Input] public FunctionProgress 进入节点;
		[Input( connectionType = ConnectionType.Override)] public bool 条件;

		[Output] public FunctionProgress 条件为True时;
		[Output] public FunctionProgress 条件为False时;
		NodePort 判断Port;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();
			判断Port = GetPort("条件");
		}

		public override void FunctionDo(string PortName,List<object> param = null) 
		{

			条件 = 判断Port.GetInputValue<bool>();
			//Debug.Log(条件);
			if (条件)
				ConnectDo("条件为True时", null);
			else
				ConnectDo("条件为False时", null);
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}
	}
}