using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.新剧情系统.XML生成
{
	public class 角色图片强调 : Node
	{
		
		[Input] public FunctionProgress 进入节点;
		[Input] public bool 强调;
		[Input] public int 角色图片序号 = 0;
		[Output] public FunctionProgress 退出节点;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName, List<object> param = null)
		{
			DialogSystemInit.createdXMLFileContent += "\r\n";
			强调 =   GetInputValue("强调", 强调);
			角色图片序号 = GetInputValue("角色图片序号", 角色图片序号);
			string Content = string.Empty;
			if (强调)
				Content = "<CharacterLighted index=\"" + 角色图片序号.ToString() + "\"/>";
			else
				Content = "<CharacterDarked index=\"" + 角色图片序号.ToString() + "\"/>";
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