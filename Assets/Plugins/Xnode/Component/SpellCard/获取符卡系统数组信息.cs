using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.符卡系统 
{public class 获取符卡系统数组信息:Node 
 {[Input] public SpellCard 符卡系统; 
[Output] public object 结果;
public enum SpellCardData { 
重置用发射器列表=0,
frameSetting=1
}
public SpellCardData 符卡系统属性;
public override object GetValue(NodePort port) 
{符卡系统 = GetInputValue<SpellCard>("符卡系统", null);if (符卡系统 == null){ return 0;} 
switch(符卡系统属性) 
 {case SpellCardData.重置用发射器列表:
结果=符卡系统.ResetShooting;
break;
case SpellCardData.frameSetting:
结果=符卡系统.frameSetting;
break;
}return 结果;}}}