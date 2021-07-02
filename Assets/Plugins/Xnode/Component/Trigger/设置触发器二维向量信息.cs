using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.触发器 
{public class 设置触发器二维向量信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public Vector2 目的值;[Input] public Trigger 触发器; 
[Output] public FunctionProgress 退出节点;
public enum TriggerData { 
对角线长度=0,
全局运动=1
}
public TriggerData 触发器属性;
 public override void FunctionDo(string PortName,List<object> param = null) {
 触发器 = GetInputValue<Trigger>("触发器", null);if (触发器 == null) return;目的值 = GetInputValue<Vector2>("目的值", 目的值); switch(触发器属性) 
 {case TriggerData.对角线长度:
触发器.SquareLength=目的值;
break;
case TriggerData.全局运动:
触发器.GlobalPositionOffset=目的值;
break;
} ConnectDo("退出节点");}}}