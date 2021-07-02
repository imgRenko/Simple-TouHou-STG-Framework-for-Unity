using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 数学
{
	public class 曲线计算 : Node
	{

		[Input] public float 自变量;
		[Input] public AnimationCurve 曲线;
		[Output] public float 计算结果;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			曲线 = GetInputValue<AnimationCurve>("曲线", 曲线);
			自变量 = GetInputValue<float>("自变量", 自变量);
			计算结果 = 曲线.Evaluate(自变量);
			return 计算结果; // Replace this
		}
	}
}