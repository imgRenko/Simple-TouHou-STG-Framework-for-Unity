using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 数学.布尔
{
	public class 浮点数整数比较 : Node
	{
		[Input] public float a;
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
			float a = GetInputValue("a", this.a);
			int b = GetInputValue("b", this.b);

			switch (method)
			{
				case Method.AEqualB:
					result = (int)a == b;
					break;
				case Method.ALessB:
					result = (int)a < b; break;
				case Method.ALessEqualB:
					result = (int)a <= b; break;
				case Method.AMoreB:
					result = (int)a > b; break;
				case Method.AMoreEqualB:
					result = (int)a >= b; break;
			}

			return result;
		}
	}
}