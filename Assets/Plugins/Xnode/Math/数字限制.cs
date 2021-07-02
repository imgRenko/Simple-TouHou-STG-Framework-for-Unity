using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
//using UnityEngine;
namespace 数学{
public class 数字限制 : Node {
	[Input] public float 当前值;
	[Input] public float 最大值;
	[Input] public float 最小值;
	[Output] public float 结果;
	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
	当前值 = GetInputValue("当前值",当前值);
	最小值 = GetInputValue("最小值",最小值);
	最大值 = GetInputValue("最大值",最大值);
		return Mathf.Clamp(当前值,最小值,最大值);
	}
}}