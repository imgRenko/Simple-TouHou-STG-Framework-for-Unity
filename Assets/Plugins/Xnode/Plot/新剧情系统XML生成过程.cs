using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Sirenix.OdinInspector;
namespace 基础事件.新剧情系统
{
	public class 新剧情系统XML生成过程 : Node
	{
		[InfoBox("此事件允许你动态生成XML数据来动态生成剧情文本。剧情文本一旦生成，就不可再次更改，除非你使用“重构”函数。重构函数会保留玩家的剧情阅读进度，并重新按照此事件后连接的图表进行剧情生成，以此达到剧情动态化的需求，但重构函数仅能在玩家阅读剧情事件里使用。")]
		[Output] public FunctionProgress 继续;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
        public override void FunctionDo(string PortName, List<object> param = null)
        {
			string Content = "<Event>";
			DialogSystemInit.createdXMLFileContent += Content;
			ConnectDo("继续");
        }
        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}
	}
}