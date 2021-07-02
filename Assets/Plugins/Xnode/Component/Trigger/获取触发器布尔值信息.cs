using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.触发器 
{public class 获取触发器布尔值信息:Node 
 {[Input] public Trigger 触发器; 
[Output] public bool 结果;
public enum TriggerData { 
使用触发器=0,
检测玩家=1,
触发器新循环使用开始事件=2,
调用停留事件=3,
额外检查子弹=4,
可复用=5,
只使用一次=6,
用自身组件更新=7
}
public TriggerData 触发器属性;
public override object GetValue(NodePort port) 
{触发器 = GetInputValue<Trigger>("触发器", null);if (触发器 == null){ return 0;} 
switch(触发器属性) 
 {case TriggerData.使用触发器:
结果=触发器.Use;
break;
case TriggerData.检测玩家:
结果=触发器.checkPlayer;
break;
case TriggerData.触发器新循环使用开始事件:
结果=触发器.loopStartEvent;
break;
case TriggerData.调用停留事件:
结果=触发器.AllowStayEvent;
break;
case TriggerData.额外检查子弹:
结果=触发器.CheckExtraBullet;
break;
case TriggerData.可复用:
结果=触发器.reusable;
break;
case TriggerData.只使用一次:
结果=触发器.OnceTime;
break;
case TriggerData.用自身组件更新:
结果=触发器.UpdateWithSelfComponent;
break;
}return 结果;}}}