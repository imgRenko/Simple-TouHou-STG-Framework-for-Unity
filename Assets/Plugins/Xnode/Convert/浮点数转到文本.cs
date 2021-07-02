using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using XNode;
namespace 类型转换.转换{
	public class 浮点数转到文本 : Node {
		[Input] public float 浮点数;
		[Output] public string 字符串;
		// Use this for initialization
		protected override void Init() {
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port) {
			浮点数 = GetInputValue<float>("浮点数", this.浮点数);
				字符串 = System.Convert.ToString(浮点数);
			return 字符串;
		}
	} }