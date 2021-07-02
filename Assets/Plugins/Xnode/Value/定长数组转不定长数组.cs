using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 变量
{
	public class 定长数组转不定长数组 : Node
	{
		[Input] public Anything 定长数组;
		[Output] public Anything 不定长数组;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			object r = GetInputValue<object>("定长数组", 定长数组);
	
			//List<r.GetType()>
			return null; // Replace this
		}
	}
}