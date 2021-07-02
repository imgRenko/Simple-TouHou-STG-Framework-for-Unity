using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.新剧情系统.XML生成
{
	public class 控制Live2D相机缩放 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public float 缩放大小 = 0.6f;
		[Output] public FunctionProgress 退出节点;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName, List<object> param = null)
		{
			缩放大小 = GetInputValue("缩放大小", 缩放大小);
			DialogSystemInit.createdXMLFileContent += "\r\n";
			string Content = "<Zoom zoom=\"" + 缩放大小.ToString() + "\"/>";
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