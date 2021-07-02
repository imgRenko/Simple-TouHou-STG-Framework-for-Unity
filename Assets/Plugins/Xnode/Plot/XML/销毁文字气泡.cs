using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.新剧情系统.XML生成
{
	public class 销毁文字气泡 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public int 要销毁气泡的序号;
		[Output] public FunctionProgress 退出节点;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName, List<object> param = null)
		{
			要销毁气泡的序号 = GetInputValue("要销毁气泡的序号", 要销毁气泡的序号);
			DialogSystemInit.createdXMLFileContent += "\r\n";
			string Content = "<DestroyText index=\"" + 要销毁气泡的序号.ToString() + "\"/>";
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