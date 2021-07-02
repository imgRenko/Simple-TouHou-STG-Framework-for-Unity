using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.对象
{
	public class 游戏对象活跃度设置 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public GameObject 游戏对象;
		public bool 活跃度;
		[Output] public FunctionProgress 退出节点;
		
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
        public override void FunctionDo(string PortName, List<object> param = null)
        {
			游戏对象 = GetInputValue("游戏对象", 游戏对象);
			if (游戏对象 != null)
				游戏对象.SetActive(活跃度);
			ConnectDo("退出节点");
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}
	}
}