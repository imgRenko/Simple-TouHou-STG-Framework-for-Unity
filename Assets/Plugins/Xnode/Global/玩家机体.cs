using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.全局.玩家
{
public class 玩家机体 : Node {
	[Output] public Character 机体;
	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
			if (Global.PlayerObject == null)
				return null;
			return Global.PlayerObject.GetComponent<Character>(); // Replace this
	}
}}