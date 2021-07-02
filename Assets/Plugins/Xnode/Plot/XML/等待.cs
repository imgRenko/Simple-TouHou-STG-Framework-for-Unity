using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.新剧情系统.XML生成
{
	public class 等待 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public float 等待时长;
		[Output] public FunctionProgress 退出节点;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName, List<object> param = null)
		{
			DialogSystemInit.createdXMLFileContent += "\r\n";
			等待时长 = GetInputValue("等待时长", 等待时长);
			string Content = "<Wait time=\""+ 等待时长.ToString()+"\"/>";
			DialogSystemInit.createdXMLFileContent += Content;
			ConnectDo("退出节点");
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}
	}
}