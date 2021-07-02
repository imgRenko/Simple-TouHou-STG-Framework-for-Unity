using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.全局.玩家
{
public class 玩家位置 : Node {
	[Output] public Vector3 位置;
	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
			if (Global.PlayerObject == null)
				return Vector3.zero;
		return Global.PlayerObject.transform.position; // Replace this
	}
}}