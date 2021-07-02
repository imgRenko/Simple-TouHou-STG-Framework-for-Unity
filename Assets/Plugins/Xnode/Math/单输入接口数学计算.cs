using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using XNode;
namespace 数学
{
	public class 单输入接口数学计算 : Node
	{
		[Input] public float A;
		public enum Method { 
			上取整 = 0,
			下取整 = 1,
			四舍五入 = 2,
			最近指数 = 3,
			绝对值 =4,
			下一个2的幂值 =5,
			上一个2的幂值 = 6,
			符号 = 7
		}
		public Method 计算方式;
		[Output] public Anything Result;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();
			
		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			A = GetInputValue("A", A);
			switch (计算方式)
			{
				case Method.上取整:
					return Mathf.Ceil(A);
				case Method.下取整:
					return Mathf.Floor(A);
				case Method.四舍五入:
					return Mathf.Round(A);
				case Method.最近指数:
					return Mathf.Exp(A);
				case Method.绝对值:
					return Mathf.Abs(A);
				case Method.下一个2的幂值:
					return Mathf.NextPowerOfTwo((int)A);
				case Method.上一个2的幂值:
					return Mathf.ClosestPowerOfTwo((int)A);
				case Method.符号:
					return Mathf.Sign((int)A);

			}
			return null;
		}
	}
}