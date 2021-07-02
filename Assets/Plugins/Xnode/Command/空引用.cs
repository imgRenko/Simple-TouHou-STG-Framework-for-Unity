using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 流程控制
{
public class 空引用 : Node {
	[Input] public Anything 任意对象;
	[Output] public bool Null结果;
	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		object 任意对象 = GetInputValue<object>("任意对象");
		return 任意对象 == null; // Replace this
	}
}}