using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
//using UnityEngine;
namespace 数学.向量{
public class 向量线性插值 : Node {
	[Input] public Vector3 a;
	[Input] public Vector3 b;
	[Input] public float t;
	[Output] public Vector3 结果;
	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		a = GetInputValue("a",a);
	b = GetInputValue("b",b);
	t = GetInputValue("t",t);
		return Vector3.Lerp(a,b,t);
	}
}}