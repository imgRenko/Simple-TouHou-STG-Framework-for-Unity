using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.时间
{
	public class 获取符卡时间 : Node
	{
		[Output] public float 秒;
		[Output] public float 毫秒;
		// Use this for initialization


		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			if (port.fieldName == "秒")
				return Timer.Second;
			if (port.fieldName == "毫秒")
				return Timer.MinTime;
			return 0;

		}
	}
}