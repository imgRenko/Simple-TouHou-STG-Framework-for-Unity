using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
//using UnityEngine;
namespace 数学{
public class 三角函数 : Node {
	[Input] public float 角度;
	[Input] public bool 弧度制;
	[Input] public bool 反的;

		public enum Type{
		Sin = 0,
		Cos = 1,
		Tan = 2
		
	}
	public Type 计算函数种类;
	[Output] public float 结果;
	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
	//	float a = 0;
		角度 = GetInputValue("角度",角度);
		弧度制 = GetInputValue("弧度制",弧度制);
			反的 = GetInputValue("反的", 反的);
			float an = 角度 * (弧度制 ?1: Mathf.Deg2Rad);
			switch (计算函数种类){
			case Type.Sin:
				return 反的 ? (Mathf.Asin(an)) :(Mathf.Sin(an));
			case Type.Cos:
				return 反的 ? (Mathf.Acos(an)) : (Mathf.Cos(an));
				case Type.Tan:
					return 反的 ? (Mathf.Atan(an)) : (Mathf.Tan(an));
			}
		return 0;
	}
}}