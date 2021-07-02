using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using XNode;
namespace 变量.设置
{
	public class 设置长整数变量 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public LongValue 想要操作的变量;

		[Input] public long 变量值;
		[Output] public FunctionProgress 退出节点;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
        public override void FunctionDo(string PortName,List<object> param = null) 
        {
			想要操作的变量 = GetInputValue<LongValue>("想要操作的变量", 想要操作的变量);
			变量值 = GetInputValue<long>("变量值", 变量值);
		
				
				想要操作的变量.Value = 变量值;
			ConnectDo("退出节点");
		}

        // Return the correct value of an output port when requested
     
	}
}