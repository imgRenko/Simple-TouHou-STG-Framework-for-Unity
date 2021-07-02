using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.全局.符卡
{
	public class 获取符卡收取状态 : Node
	{
		[Output] public bool 是否收取符卡;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();
			
		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return Global.SpellFailed; // Replace this
		}
	}
}