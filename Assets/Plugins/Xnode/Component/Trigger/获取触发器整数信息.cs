using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.触发器 
{public class 获取触发器整数信息:Node 
 {[Input] public Trigger 触发器; 
[Output] public int 结果;
public enum TriggerData { 
最大使用寿命=0,
最大使用次数 =1
}
public TriggerData 触发器属性;
public override object GetValue(NodePort port) 
{触发器 = GetInputValue<Trigger>("触发器", null);if (触发器 == null){ return 0;} 
switch(触发器属性) 
 {case TriggerData.最大使用寿命:
结果=触发器.MaxLiveFrame;
break;
                case TriggerData.最大使用次数:
                    结果 = 触发器.MaxUseTime;
                    break;

            }
            return 结果;}}}