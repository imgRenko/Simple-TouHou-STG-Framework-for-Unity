using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 数学.布尔
{
	public class 整数比较 : Node
	{
		[Input] public int a;
		[Input] public int b;
		public Method method = Method.AMoreB;
		public enum Method
		{
			AMoreB = 0,
			AMoreEqualB = 1,
			AEqualB = 2,
			ALessEqualB = 3,
			ALessB = 4

		}
		[Output] public bool result;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			int a = GetInputValue("a", this.a);
			int b = GetInputValue("b", this.b);

			switch (method)
			{
				case Method.AEqualB:
					result = a == b;
					break;
				case Method.ALessB:
					result = a < b; break;
				case Method.ALessEqualB:
					result = a <= b; break;
				case Method.AMoreB:
					result = a > b; break;
				case Method.AMoreEqualB:
					result = a >= b; break;
			}

			return result;
		}
	}
}