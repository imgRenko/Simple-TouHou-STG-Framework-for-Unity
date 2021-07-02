using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.新剧情系统.XML生成
{
	public class 播放Live2D模型动作 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public int 模型序号;
		[Input] public float 过渡时间;
		[Input] public string 动作名称;
		[Output] public FunctionProgress 退出节点;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName, List<object> param = null)
		{
			模型序号 = GetInputValue("模型序号", 模型序号);
			过渡时间 = GetInputValue("过渡时间", 过渡时间);
			动作名称 = GetInputValue("动作名称", 动作名称);
			DialogSystemInit.createdXMLFileContent += "\r\n";
			string Content = "<PlayMotion index=\"" + 模型序号.ToString() + "\" motion=\""+ 动作名称 + "\" fade=\""+ 过渡时间 .ToString()+ "\"/>";
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