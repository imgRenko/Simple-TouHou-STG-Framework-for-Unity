using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace 数学.向量{
public class 两点距离 : Node {
	[Input] public Vector3 a;
		[Input] public Vector3 b;
	[Output] public float 结果;
	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		a = GetInputValue("a",a);
a = GetInputValue("a",b);
		return (a-b).magnitude;
	}
}}