using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.全局.符卡
{
	public class 得到当前符卡 : Node
	{
		[Output] public SpellCard 符卡;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			if (Global.SpellCardNow != null)
				return Global.SpellCardNow;
			return null; // Replace this
		}
	}
}