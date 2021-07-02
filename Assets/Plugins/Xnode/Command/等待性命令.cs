using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 流程控制
{
    [NodeWidth(240)]
    public class 等待性命令 : Node
    {
        [Input] public FunctionProgress 进入节点;
        public float 秒数周期 =5;
        [Output] public FunctionProgress 退出节点;
       
        private float i = 0;
       // private float a = 0;
        protected override void Init()
        {
            i = Time.time;
        }
        public override void FunctionDo(string PortName,List<object> param = null) 
        {
            GameObject a = new GameObject();
            a.name = "Coroutine Processor";
            NullStub p = a.AddComponent<NullStub>();
            p.maxTime = 秒数周期 * 60;
            p.node = this;
            


        }
      
      
       
    }
}