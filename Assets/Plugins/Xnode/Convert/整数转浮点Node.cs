using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 类型转换.转换
{
	public class 整数转浮点Node : Node
	{
		[Input] public int 整数;
		[Output] public float 浮点;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			整数 = GetInputValue<int>("整数", 0);
			浮点 = (float)整数;
			return 浮点; // Replace this
		}
	}
}