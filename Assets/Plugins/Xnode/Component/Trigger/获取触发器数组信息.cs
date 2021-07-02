using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.触发器 
{public class 获取触发器数组信息:Node 
 {[Input] public Trigger 触发器; 
[Output] public object 结果;
public enum TriggerData { 

受影响的额外检测子弹=2
}
public TriggerData 触发器属性;
public override object GetValue(NodePort port) 
{触发器 = GetInputValue<Trigger>("触发器", null);if (触发器 == null){ return 0;} 
switch(触发器属性) 
 {
case TriggerData.受影响的额外检测子弹:
结果=触发器.Extra;
break;
}return 结果;}}}