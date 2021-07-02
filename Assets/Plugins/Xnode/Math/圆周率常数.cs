using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 数学
{
	public class 圆周率常数 : Node
	{
		[Output] public float Pi;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return Mathf.PI; // Replace this
		}
	}
}