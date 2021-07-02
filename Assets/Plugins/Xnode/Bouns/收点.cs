using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.奖励
{
	public class 收点 : Node
	{
		[Input] public FunctionProgress 进入节点;

		[Output] public FunctionProgress 退出节点;
        // Use this for initialization
        public override void FunctionDo(string PortName, List<object> param = null)
        {
			Global.GameObjectPool_A.PlayerGetAllBouns();
			ConnectDo("退出节点");
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}
	}
}