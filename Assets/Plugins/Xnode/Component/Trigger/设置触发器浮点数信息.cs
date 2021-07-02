using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.触发器
{
    public class 设置触发器浮点数信息 : Node
    {
        [Input] public FunctionProgress 进入节点;[Input] public float 目的值;[Input] public Trigger 触发器;
        [Output] public FunctionProgress 退出节点;
        public enum TriggerData
        {
            子弹半径 = 0,
            触发器速度 = 1,
            触发器加速度 = 2,
            触发器角速度 = 3,
            组件年龄 = 4
        }
        public TriggerData 触发器属性;
        public override void FunctionDo(string PortName, List<object> param = null)
        {
            触发器 = GetInputValue<Trigger>("触发器", null); if (触发器 == null) return; 目的值 = GetInputValue<float>("目的值", 目的值);
         //   Debug.Log(目的值);
            switch (触发器属性)
             
            {
                case TriggerData.子弹半径:
                    触发器.Radius = 目的值;
                    break;
                case TriggerData.触发器速度:
                    触发器.TriggerSpeed = 目的值;
                    break;
                case TriggerData.触发器加速度:
                    触发器.TriggerAcceleratedSpeed = 目的值;
                    break;
                case TriggerData.触发器角速度:
                    触发器.TriggerAccelerRotation = 目的值;
                    break;
                case TriggerData.组件年龄:
                    触发器.TotalLiveFrame = 目的值;
                    break;
            }
            ConnectDo("退出节点");
        }
    }
}