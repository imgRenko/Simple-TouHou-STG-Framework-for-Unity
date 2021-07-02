using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 变量.获得
{
	public class 获得曲线变量 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public AnimationCurveValue 想要操作的变量;

		[Output] public FunctionProgress 退出节点;
		[Output] public AnimationCurve 变量值;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName,List<object> param = null) 
		{

			ConnectDo("退出节点");
		}
		public override object GetValue(NodePort port)
		{
			想要操作的变量 = GetInputValue("想要操作的变量", 想要操作的变量);

			变量值 = 想要操作的变量.Value;
			return 变量值;
		}
	}
}