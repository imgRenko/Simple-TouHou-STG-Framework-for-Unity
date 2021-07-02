using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.奖励
{
	public class 掉落奖励 : Node
	{
		[Input] public FunctionProgress 进入节点;

		[Input] public int 数量;
	
		[Input]  public Vector3 指定位置;
		public Bouns.BounsType 奖励类型;
		[Output] public FunctionProgress 退出节点;
        // Use this for initialization
        public override void FunctionDo(string PortName, List<object> param = null)
        {
			数量 = GetInputValue("数量", 数量);
			指定位置 = GetInputValue("指定位置", 指定位置);
			Bouns.SetBouns(数量, 指定位置, 奖励类型);
			ConnectDo("退出节点");
		}

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}
	}
}