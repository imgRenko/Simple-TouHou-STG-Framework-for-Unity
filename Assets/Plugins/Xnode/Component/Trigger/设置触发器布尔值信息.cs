using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.触发器 
{public class 设置触发器布尔值信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public bool 目的值;[Input] public Trigger 触发器; 
[Output] public FunctionProgress 退出节点;
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
 public override void FunctionDo(string PortName,List<object> param = null) {
 触发器 = GetInputValue<Trigger>("触发器", null);if (触发器 == null) return;目的值 = GetInputValue<bool>("目的值", 目的值); switch(触发器属性) 
 {case TriggerData.使用触发器:
触发器.Use=目的值;
break;
case TriggerData.检测玩家:
触发器.checkPlayer=目的值;
break;
case TriggerData.触发器新循环使用开始事件:
触发器.loopStartEvent=目的值;
break;
case TriggerData.调用停留事件:
触发器.AllowStayEvent=目的值;
break;
case TriggerData.额外检查子弹:
触发器.CheckExtraBullet=目的值;
break;
case TriggerData.可复用:
触发器.reusable=目的值;
break;
case TriggerData.只使用一次:
触发器.OnceTime=目的值;
break;
case TriggerData.用自身组件更新:
触发器.UpdateWithSelfComponent=目的值;
break;
} ConnectDo("退出节点");}}}