using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.力场 
{public class 获取力场浮点数信息:Node 
 {[Input] public Force 力场; 
[Output] public float 结果;
public enum ForceData { 
力场强度=0,
圆形半径=1,
力场速度=2,
力场加速度=3,
力场角速度=4
}
public ForceData 力场属性;
public override object GetValue(NodePort port) 
{力场 = GetInputValue<Force>("力场", null);if (力场 == null){ return 0;} 
switch(力场属性) 
 {case ForceData.力场强度:
结果=力场.ForceStrength;
break;
case ForceData.圆形半径:
结果=力场.Radius;
break;
case ForceData.力场速度:
结果=力场.ForceSpeed;
break;
case ForceData.力场加速度:
结果=力场.ForceAcceleratedSpeed;
break;
case ForceData.力场角速度:
结果=力场.ForceAccelerRotation;
break;
}return 结果;}}}