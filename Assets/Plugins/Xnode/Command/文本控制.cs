using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 流程控制
{
	public class 文本控制 : Node
	{
		[Multiline]
		[Input] public string A;
		[Multiline]
		[Input] public string B;
		[Multiline]
		[Input] public string C;
		[Multiline]
		[Input] public string D;
		[Output] public string 结果;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			A = GetInputValue<string>("A", this.A);
			B = GetInputValue<string>("B", this.B);
			C = GetInputValue<string>("C", this.C);
			D = GetInputValue<string>("D", this.D);
			结果 = A + B + C + D;
			return 结果; // Replace this
		}
	}
}