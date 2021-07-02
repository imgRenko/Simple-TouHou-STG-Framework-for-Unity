using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.力场 
{public class 获取力场二维向量信息:Node 
 {[Input] public Force 力场; 
[Output] public Vector2 结果;
public enum ForceData { 
对角线向量=0,
力场全局偏移=1
}
public ForceData 力场属性;
public override object GetValue(NodePort port) 
{力场 = GetInputValue<Force>("力场", null);if (力场 == null){ return 0;} 
switch(力场属性) 
 {case ForceData.对角线向量:
结果=力场.SquareLength;
break;
case ForceData.力场全局偏移:
结果=力场.GlobalPositionOffset;
break;
}return 结果;}}}