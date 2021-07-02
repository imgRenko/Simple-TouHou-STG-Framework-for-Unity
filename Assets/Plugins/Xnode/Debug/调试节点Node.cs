using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 调试机制.输出
{
	public class 调试节点Node : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Output] public FunctionProgress 退出节点;

		[Input] public Anything 输出内容;
		public enum Level
		{
			Info = 0,
			Warning = 1,
			Error = 2
		}
		public Level 输出等级;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName,List<object> param = null) 
		{
			object 输出内容 = GetInputValue<object>("输出内容");
			switch (输出等级)
			{
				case Level.Error:
					Debug.LogError(输出内容);
					break;
				case Level.Info:
					Debug.Log(输出内容);
					break;
				case Level.Warning:
					Debug.LogWarning(输出内容);
					break;

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