using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.触发器 
{public class 设置触发器整数信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public int 目的值;[Input] public Trigger 触发器; 
[Output] public FunctionProgress 退出节点;
public enum TriggerData { 
最大使用寿命=0,
最大使用次数 =1
}
public TriggerData 触发器属性;
 public override void FunctionDo(string PortName,List<object> param = null) {
 触发器 = GetInputValue<Trigger>("触发器", null);if (触发器 == null) return;目的值 = GetInputValue<int>("目的值", 目的值); switch(触发器属性) 
 {case TriggerData.最大使用寿命:
触发器.MaxLiveFrame=目的值;
break;
                case TriggerData.最大使用次数:
                    触发器.MaxUseTime = 目的值;
                    break;
            } ConnectDo("退出节点");}}}