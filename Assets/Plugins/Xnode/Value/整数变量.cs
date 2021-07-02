using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Sirenix.OdinInspector;
namespace 变量
{
	public class 整数变量 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public string 名称;
		[Input] public int 变量初始值;
		[Output] public FunctionProgress 退出节点;
		[Output] public IntValue 变量值;
		[HideInInspector]
		public int NodeIndex = 0;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();
			NodeIndex = 0;
			名称 = GetInputValue<string>("名称", 名称);
			
			foreach (var a in graph.nodes) {
				if (this == a)
					break;
				NodeIndex++;
			}
			
				graph.valueNode.Add(名称, NodeIndex);
			Debug.Log("变量初始化完成,Index:"+ NodeIndex.ToString());
		}
       
        // Return the correct value of an output port when requested
        public override void FunctionDo(string PortName,List<object> param = null) 
		{
			变量初始值 = GetInputValue<int>("变量初始值", 变量初始值);
			名称 = GetInputValue<string>("名称", 名称);
			变量值 = new IntValue() {
				Name = 名称,
				Value = 变量初始值
				
			};

			ConnectDo("退出节点");
		}
        public override object GetValue(NodePort port)
        {
			if (变量值 != null)
				return 变量值;
			return null;

        }
    }
	[System.Serializable]
	public class IntValue {
		public string Name;
		public int Value;
		
	}
}