using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 变量
{
	public class 曲线 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public string 名称;
		[Input] public AnimationCurve 原曲线;
		[Output] public FunctionProgress 退出节点;
		[Output] public AnimationCurveValue 最终曲线;
		[HideInInspector]
		public int NodeIndex = 0;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();
			名称 = GetInputValue<string>("名称", 名称);
			NodeIndex = 0;
			foreach (var a in graph.nodes)
			{
				if (this == a)
					break;
				NodeIndex++;
			}

			graph.valueNode.Add(名称, NodeIndex);
			Debug.Log("变量初始化完成,Index:" + NodeIndex.ToString());
		}

		// Return the correct value of an output port when requested
		public override void FunctionDo(string PortName,List<object> param = null) 
		{
			原曲线 = GetInputValue("原曲线", 原曲线);
			名称 = GetInputValue<string>("名称", 名称);
			最终曲线 = new AnimationCurveValue()
			{
				Name = 名称,
				Value = 原曲线

			};

			ConnectDo("退出节点");
		}
		public override object GetValue(NodePort port)
		{
			if (最终曲线 != null)
				return 最终曲线;
			return null;

		}
		
	}
}
[System.Serializable]
public class AnimationCurveValue
{
	public string Name;
	public AnimationCurve Value;

}