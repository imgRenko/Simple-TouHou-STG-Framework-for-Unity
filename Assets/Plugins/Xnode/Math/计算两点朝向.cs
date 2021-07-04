using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using XNode;
namespace 数学
{
	public class 计算两点朝向 : Node
	{
		[Input] public Vector3 A;
		[Input] public Vector3 B;
		[Output] public float 朝向;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			
			return Math2D.GetAimToTargetRotation(A, B); ; // Replace this
		}
	}
}