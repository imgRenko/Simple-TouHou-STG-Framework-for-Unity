using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 数学
{
	public class 修改曲线 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public AnimationCurve 曲线;
		[Input] public int 修改关键帧序号;
		[Input] public bool 修改时间 = false;
		[ShowIf("修改时间")]
		[Input] public float 时间 = -1;
		[Input] public bool 修改值 = false;
		[ShowIf("修改值")]
		[Input] public float 值 = -1;
		[Output] public AnimationCurve 最终曲线;
		[Output] public FunctionProgress 退出节点;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
        public override void FunctionDo(string PortName,List<object> param = null) 
        {
			
			曲线 = GetInputValue<AnimationCurve>("曲线", 曲线);
			if (曲线.keys.Length == 0)
				return;
			修改关键帧序号 = GetInputValue<int>("修改关键帧序号", 修改关键帧序号);
			时间 = GetInputValue<float>("时间", 时间);
			值 = GetInputValue<float>("值", 值);
			修改时间 = GetInputValue("修改时间", 修改时间);
			修改值 = GetInputValue("修改值", 修改值);

			float t = 时间;
			float v = 值;
			float ot = 曲线.keys[修改关键帧序号].time;
			float ov = 曲线.keys[修改关键帧序号].value;
			曲线.RemoveKey(修改关键帧序号);
			曲线.AddKey(修改时间 ? t : ot, 修改值 ? v : ov);
			最终曲线 = 曲线;
			ConnectDo("退出节点");
		}
        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
		{
			
			return 最终曲线; // Replace this
		}
	}
}