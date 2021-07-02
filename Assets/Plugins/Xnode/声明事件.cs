using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class 声明事件 : Node {
	[Output]public FunctionProgress 继续;
	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}
    public override void FunctionDo(string PortName, List<object> param = null)
    {
		ConnectDo("继续");
    }
    // Return the correct value of an output port when requested
    public override object GetValue(NodePort port) {
		return null; // Replace this
	}
}