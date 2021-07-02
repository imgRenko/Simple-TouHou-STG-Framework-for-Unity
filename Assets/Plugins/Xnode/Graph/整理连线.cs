using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class 整理连线 : Node {
	[Input] public Anything 输入;
	[Output] public Anything 输出;
	NodePort 输入Port;
	// Use this for initialization
	protected override void Init() {
		base.Init();
		输入Port = GetPort("输入");


	}

    // Return the correct value of an output port when requested
    public override object GetValue(NodePort port) {
		return 输入Port.GetInputValue<object>() ;
	}
}