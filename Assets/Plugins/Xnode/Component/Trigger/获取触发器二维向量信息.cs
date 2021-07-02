using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.触发器 
{public class 获取触发器二维向量信息:Node 
 {[Input] public Trigger 触发器; 
[Output] public Vector2 结果;
public enum TriggerData { 
对角线长度=0,
全局运动=1
}
public TriggerData 触发器属性;
public override object GetValue(NodePort port) 
{触发器 = GetInputValue<Trigger>("触发器", null);if (触发器 == null){ return 0;} 
switch(触发器属性) 
 {case TriggerData.对角线长度:
结果=触发器.SquareLength;
break;
case TriggerData.全局运动:
结果=触发器.GlobalPositionOffset;
break;
}return 结果;}}}