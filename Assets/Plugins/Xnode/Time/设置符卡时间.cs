using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.时间
{
	public class 设置符卡时间 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public float 秒;

		[Input] public float 毫秒;
		[Output] public FunctionProgress 退出节点;
		public override void FunctionDo(string PortName, List<object> param = null)
		{
			float sec = GetInputValue("秒", 秒);
			float minSec = GetInputValue("毫秒", 毫秒);
			Timer.Second = sec;
			Timer.MinTime = minSec;
			ConnectDo("退出节点");
		}
	}
}