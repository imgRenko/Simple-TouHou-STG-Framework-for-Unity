using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.新剧情系统.XML生成
{
	public class 文字气泡 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public Vector2 位置;
		[Input] public int 序号;
		[Input] public string 气泡图片 = "balloon_1";
		[Multiline]
		[Input] public string 文本;
		[Input] public bool 翻转气泡;
		public bool 需要回车键继续 = true; 
		[Output] public FunctionProgress 退出节点;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName, List<object> param = null)
		{
			位置 = GetInputValue("原位置", 位置);
		
			序号 = GetInputValue("序号", 序号);
			文本 = GetInputValue("文本", 文本);
			翻转气泡 = GetInputValue("翻转气泡", 翻转气泡);
			气泡图片 = GetInputValue("气泡图片", 气泡图片);
			DialogSystemInit.createdXMLFileContent += "\r\n";
			string Content = "<Text src=\"" + 气泡图片 + "\" index=\"" + 序号.ToString() + "\" zlayout=\"-1\" filp=\"" + 翻转气泡.ToString().ToLower() + "\" pos=\"" + 位置.x.ToString() + "," + 位置.y.ToString() + "\" text=\"" + 文本 + "\"/>" + (需要回车键继续 ? "\r\n<WaitInput /> ": "");
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