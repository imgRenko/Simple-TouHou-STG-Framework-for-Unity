using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.全局.玩家{
public class 玩家符卡数 : Node {
	[Output] public int 符卡数;
		[ShowIf("返回碎片")]
		[Output] public int 符卡碎片;
		public bool 返回碎片;
		// Use this for initialization
		protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
			return !返回碎片 ? Global.SpellCard : Global.SpellCardPrice; // Replace this
	}
}}