using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
//using UnityEngine;
namespace 数学{
public class 数字线性插值 : Node {
	[Input] public float a;
	[Input] public float b;
	[Input] public float t;
	[Output] public float 结果;
	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
	
		return Mathf.Lerp(a,b,t);
	}
}}