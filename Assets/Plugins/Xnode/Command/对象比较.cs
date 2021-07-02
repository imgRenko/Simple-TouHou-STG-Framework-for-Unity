using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 流程控制
{
	public class 对象比较 : Node
	{
		[Input] public Anything A;
		[Input] public Anything B;
		[Output] public bool 结果;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			object A = GetInputValue<object>("A");
			object B = GetInputValue<object>("B");
			if (A != null && B != null)
				return object.Equals(A, B);
			return false; // Replace this
		}
	}
}