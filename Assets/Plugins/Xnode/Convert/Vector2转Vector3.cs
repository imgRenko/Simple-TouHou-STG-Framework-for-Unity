using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 类型转换.转换
{
	public class Vector2转Vector3 : Node
	{
		[Input] public Vector2 二维向量;
		[Output] public Vector3 三维向量;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			二维向量 = GetInputValue("二维向量", 二维向量);
			
			return (Vector3)二维向量; // Replace this
		}
	}
}