using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.新剧情系统.XML生成
{
	public class 展示图像 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public Vector2 绝对位置;
		[Input] public Vector2 目的绝对位置;
		[Input] public int 序号;
		[Input] public int 层次级别;
		[Input] public string 角色图片 = "balloon_1";
		[Input] public string 遮罩图片 = "";
		[Input] public bool 使用遮罩 = true;
		[Input] public Vector2 原始缩放 = Vector2.one;
		[Input] public Vector2 目的缩放 = Vector2.one;
		[Input] public Vector2 角色相对位置;
		[Input] public Color 遮罩颜色;
	

		[Output] public FunctionProgress 退出节点;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName, List<object> param = null)
		{
			//Sample <Live2D src="object_live2d_046_101.asset" original-pos ="0,0" target-pos="-2.45,1" transform - speed = "0.1" original - alpha = "0" target - alpha = "1" index = "1" zlayout = "2" />
			DialogSystemInit.createdXMLFileContent += "\r\n";
			Vector2 originalPos = GetInputValue("绝对位置", 绝对位置);
			Vector2 tarPos = GetInputValue("目的绝对位置", 目的绝对位置);
			角色相对位置 = GetInputValue("角色相对位置", 角色相对位置);
			原始缩放 = GetInputValue("原始缩放", 原始缩放);
			目的缩放 = GetInputValue("目的缩放", 目的缩放);
			角色图片 = GetInputValue("角色图片", 角色图片);
			遮罩图片 = GetInputValue("遮罩图片", 遮罩图片);
			使用遮罩 = GetInputValue("使用遮罩", 使用遮罩);
			遮罩颜色 = GetInputValue("遮罩颜色", 遮罩颜色);
			序号 = GetInputValue("序号", 序号);
			层次级别 = GetInputValue("层次级别", 层次级别);
			string Content = "<CharacterImage img-src=\"" + 角色图片 + "\" original-pos=\"" + originalPos.x.ToString() + "," + originalPos.y.ToString()  + "\" char-pos=\"" + 角色相对位置.x.ToString() + "," + 角色相对位置.y.ToString() + "\" target-pos=\"" + tarPos.x.ToString() + "," + tarPos.y.ToString() + "\" transform-speed=\"0.2\" original-scale=\"" + 原始缩放.x.ToString() + "," + 原始缩放.y.ToString() + "\" target-scale=\"" + 目的缩放.x.ToString() + "," + 目的缩放.y.ToString() + "\" index=\"" + 序号.ToString() + "\" zlayout=\"" + 层次级别.ToString() + "\" masked=\"" + 使用遮罩.ToString().ToLower() + "\" cover-src=\"" + 遮罩图片 + "\" mask-color=\"" + 遮罩颜色.r.ToString() + "," + 遮罩颜色.g.ToString() + "," + 遮罩颜色.b.ToString() +"," + 遮罩颜色.a.ToString() + "\"/>";
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