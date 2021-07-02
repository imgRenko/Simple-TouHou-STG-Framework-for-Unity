using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 类型转换.转换
{
	public class 浮点转整数Node : Node
	{
		[Input] public float 浮点;
		[Output] public int 整数;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			浮点 = GetInputValue<float>("浮点", 0);
			整数 = (int)浮点;
			return 整数; // Replace this
		}
	}
}