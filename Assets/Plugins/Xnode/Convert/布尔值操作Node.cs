using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 类型转换.布尔
{
	public class 布尔值操作Node : Node
	{
		[Input] public bool 布尔值A;
		[Input] public bool 布尔值B;
		public enum BoolMethod
		{
			或 = 0,
			且 = 1,
			取反 = 2
		}
		public BoolMethod 操作方式;
		[Output] public bool 输出值;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			布尔值A = GetInputValue<bool>("布尔值A", 布尔值A);
			布尔值B = GetInputValue<bool>("布尔值B", 布尔值B);
			switch (操作方式)
			{
				case BoolMethod.且:
					输出值 = 布尔值A && 布尔值B;
					break;
				case BoolMethod.或:
					输出值 = 布尔值A || 布尔值B;
					break;
				case BoolMethod.取反:
					输出值 = !布尔值A;
					break;
			}

			return 输出值; // Replace this
		}
	}
}