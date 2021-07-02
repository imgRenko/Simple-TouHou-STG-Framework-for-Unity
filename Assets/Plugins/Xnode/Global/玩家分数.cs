using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using XNode;
namespace 基础事件.全局.玩家{
public class 玩家分数: Node {
	[Output] public long 分数;
		[ShowIf("返回最大分数")]
		[Output] public long 最大分数;
		public bool 返回最大分数;
		// Use this for initialization
		protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return 返回最大分数 ? Global.MaxScore : Global.Score; // Replace this
	}
}}