using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.触发器 
{public class 获取触发器浮点数信息:Node 
 {[Input] public Trigger 触发器; 
[Output] public float 结果;
public enum TriggerData { 
子弹半径=0,
触发器速度=1,
触发器加速度=2,
触发器角速度=3,
组件年龄=4
}
public TriggerData 触发器属性;
public override object GetValue(NodePort port) 
{触发器 = GetInputValue<Trigger>("触发器", null);if (触发器 == null){ return 0;} 
switch(触发器属性) 
 {case TriggerData.子弹半径:
结果=触发器.Radius;
break;
case TriggerData.触发器速度:
结果=触发器.TriggerSpeed;
break;
case TriggerData.触发器加速度:
结果=触发器.TriggerAcceleratedSpeed;
break;
case TriggerData.触发器角速度:
结果=触发器.TriggerAccelerRotation;
break;
case TriggerData.组件年龄:
结果=触发器.TotalLiveFrame;
break;
}return 结果;}}}