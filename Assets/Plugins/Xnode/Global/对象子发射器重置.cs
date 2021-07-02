using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using XNode;
namespace 基础事件.全局
{
	public class 对象子发射器重置 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public GameObject 指定对象;

		[Output ] public FunctionProgress 退出节点;
        // Use this for initialization
        public override void FunctionDo(string PortName, List<object> param = null)
        {
			指定对象 = GetInputValue("指定对象", 指定对象);
			foreach (var a in 指定对象.GetComponentsInChildren<Shooting>())
			{
				a.ResetCountTime();
				a.enabled = true;
			}
			ConnectDo("退出节点");
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
		{

			return null; // Replace this
		}
	}
}