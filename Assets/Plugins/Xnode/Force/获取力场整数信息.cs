using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.力场 
{public class 获取力场整数信息:Node 
 {[Input] public Force 力场; 
[Output] public int 结果;
public enum ForceData { 
组件最大使用时长=0
}
public ForceData 力场属性;
public override object GetValue(NodePort port) 
{力场 = GetInputValue<Force>("力场", null);if (力场 == null){ return 0;} 
switch(力场属性) 
 {case ForceData.组件最大使用时长:
结果=力场.MaxLiveFrame;
break;
}return 结果;}}}