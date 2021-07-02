using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.符卡系统 
{public class 获取符卡系统字符串信息:Node 
 {[Input] public SpellCard 符卡系统; 
[Output] public string 结果;
public enum SpellCardData { 
符卡记录信息文件名称=0,
符卡名字=1
}
public SpellCardData 符卡系统属性;
public override object GetValue(NodePort port) 
{符卡系统 = GetInputValue<SpellCard>("符卡系统", null);if (符卡系统 == null){ return 0;} 
switch(符卡系统属性) 
 {case SpellCardData.符卡记录信息文件名称:
结果=符卡系统.XmlfileName;
break;
case SpellCardData.符卡名字:
结果=符卡系统.Name;
break;
}return 结果;}}}