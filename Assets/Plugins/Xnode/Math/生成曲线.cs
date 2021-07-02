using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 数学
{
	public class 生成曲线 : Node
	{
		public AnimationCurve 曲线;
		[Output]public AnimationCurve 输出;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return 曲线; // Replace this
		}
	}
}