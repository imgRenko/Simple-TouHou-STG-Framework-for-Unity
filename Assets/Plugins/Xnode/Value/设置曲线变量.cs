using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 变量.设置
{
	public class 设置曲线变量 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public AnimationCurveValue 想要操作的变量;

		[Input] public AnimationCurve 变量值;
		[Output] public FunctionProgress 退出节点;
		public override void FunctionDo(string PortName,List<object> param = null) 
		{
			想要操作的变量 = GetInputValue("想要操作的变量", 想要操作的变量);
			变量值 = GetInputValue("变量值", 变量值);


			想要操作的变量.Value = 变量值;
			ConnectDo("退出节点");
		}
	}
}