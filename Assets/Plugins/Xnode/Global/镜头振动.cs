using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.全局
{
	public class 镜头振动 : Node
	{
		[Input] public FunctionProgress 进入节点;
		 public float  振幅 = 0.2f;
		public float 震动总值 = 0.7f;
		public float 震动衰退速度 = 1;
		[Output] public FunctionProgress 退出节点;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
        public override void FunctionDo(string PortName, List<object> param = null)
        {
			CameraShake.shakeAmount = 震动总值;
			CameraShake.shake = 振幅;
			CameraShake.decreaseFactor = 震动衰退速度;
			ConnectDo("退出节点");
		}
        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}
	}
}