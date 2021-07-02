using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 流程控制
{
    public class 限制重复性命令中转结点 : Node
    {
        [Input] public FunctionProgress 进入节点;

        [Output] public FunctionProgress 退出节点;
        // Use this for initialization
        public override void FunctionDo(string PortName,List<object> param = null) 
        {
            ConnectDo("退出节点");
        }
    }
}