using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 流程控制
{
    [NodeWidth(240)]
    public class 限制重复性命令 : Node
    {
        [Input] public FunctionProgress 进入节点;
        public int 计数超过n时不执行 =1;
        [Output] public FunctionProgress 要执行部分;
        [Output] public FunctionProgress 不符合条件执行;
      
        private int i = 0;
        protected override void Init()
        {
            
        }
        public override void FunctionDo(string PortName,List<object> param = null) 
        {
            i++;

            if (i < 计数超过n时不执行)
            {
           
                ConnectDo("要执行部分");
            }
            else
                ConnectDo("不符合条件执行");

        }
    }
}