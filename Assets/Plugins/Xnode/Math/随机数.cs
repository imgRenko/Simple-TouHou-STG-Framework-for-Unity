using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 数学{
public class 随机数 : Node {
	[Input] public float 最小随机数值;
	[Input] public float 最大随机数值;
	[Output] public float 随机数结果;
	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return Random.Range(最小随机数值,最大随机数值);
	}
}}