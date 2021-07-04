using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.激光.跟随式激光
{
	public class 设置跟随式激光 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Output] public FunctionProgress 退出节点;
		[Input]  public List<GameObject> 追踪对象 = new List<GameObject>();
		[Input] public int 最大使用时长 = 200;
		[Input] public AnimationCurve 动画曲线;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName, List<object> param = null)
		{
			最大使用时长 = GetInputValue<int>("最大使用时长", 最大使用时长);
			追踪对象 = GetInputValue<List<GameObject>>("追踪对象", 追踪对象);
			动画曲线 = GetInputValue<AnimationCurve>("动画曲线", 动画曲线);
			FollowLaserManager.managerInstance.ApplyNewProcessor(追踪对象, 动画曲线, 最大使用时长);

			ConnectDo("退出节点");
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{

			return null; // Replace this
		}
	}
}