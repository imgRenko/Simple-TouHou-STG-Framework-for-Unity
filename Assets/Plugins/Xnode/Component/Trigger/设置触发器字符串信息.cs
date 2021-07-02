using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.触发器 
{public class 设置触发器字符串信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public string 目的值;[Input] public Trigger 触发器; 
[Output] public FunctionProgress 退出节点;
public enum TriggerData { 
额外检查子弹Tag=0
}
public TriggerData 触发器属性;
 public override void FunctionDo(string PortName,List<object> param = null) {
 触发器 = GetInputValue<Trigger>("触发器", null);if (触发器 == null) return;目的值 = GetInputValue<string>("目的值", 目的值); switch(触发器属性) 
 {case TriggerData.额外检查子弹Tag:
触发器.ExtraBulletTag=目的值;
break;
} ConnectDo("退出节点");}}}