using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 流程控制
{
	public class 循环计数 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public FunctionProgress 新循环;
		[Input] public int 循环次数;
		[Output] public FunctionProgress 循环体;
		[Output] public FunctionProgress 循环结束;
		[Output] public int 计数;
		private int i = 0;

		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName, List<object> param = null)
		{
			if (PortName == "进入节点")
				i = 0;
			if (循环次数 <= 0)
				ConnectDo("循环结束");
			循环次数 = GetInputValue("循环次数", 循环次数);
			++i;
			if (i > 循环次数)
			{
				ConnectDo("循环结束");
				i = 0;
			}
			else
			{
				List<object> portname = new List<object>();
				portname.Add("循环体");
				ConnectDo("循环体");
			}

		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			计数 = i;
			return 计数; // Replace this
		}
	}
}