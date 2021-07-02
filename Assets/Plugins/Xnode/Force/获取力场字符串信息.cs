using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.力场 
{public class 获取力场字符串信息:Node 
 {[Input] public Force 力场; 
[Output] public string 结果;
public enum ForceData { 
生效的子弹Tag=0
}
public ForceData 力场属性;
public override object GetValue(NodePort port) 
{力场 = GetInputValue<Force>("力场", null);if (力场 == null){ return 0;} 
switch(力场属性) 
 {case ForceData.生效的子弹Tag:
结果=力场.EnableForTag;
break;
}return 结果;}}}