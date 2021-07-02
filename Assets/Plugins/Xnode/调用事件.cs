using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class 调用事件 : Node {
	[Input] public FunctionProgress 进入节点;
	public string 事件名称;
	Node nodeGot;
	[Output] public FunctionProgress 退出节点;
	// Use this for initialization
	protected override void Init() {
		base.Init();
		foreach (var a in graph.nodes)
		{
			if (a ==null)
				continue;
			if (a.name == 事件名称) {
				nodeGot = a;
				break;
			}
		}
	}
    public override void FunctionDo(string PortName, List<object> param = null)
    {
		if(nodeGot != null)
		nodeGot.ConnectDo("继续");
		ConnectDo("退出节点");
    }
    // Return the correct value of an output port when requested
    public override object GetValue(NodePort port) {
		return null; // Replace this
	}
}