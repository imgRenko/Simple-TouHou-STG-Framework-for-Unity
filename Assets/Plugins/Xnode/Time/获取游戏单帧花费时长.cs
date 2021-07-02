using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.时间
{
	public class 获取游戏单帧花费时长 : Node
	{
		[Output] public float 时间;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return	Time.deltaTime; // Replace this
		}
	}
}