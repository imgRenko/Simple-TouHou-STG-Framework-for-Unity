using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.力场 
{public class 设置力场浮点数信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public float 目的值;[Input] public Force 力场; 
[Output] public FunctionProgress 退出节点;
public enum ForceData { 
力场强度=0,
圆形半径=1,
力场速度=2,
力场加速度=3,
力场角速度=4
}
public ForceData 力场属性;
 public override void FunctionDo(string PortName,List<object> param = null) {
 力场 = GetInputValue<Force>("力场", null);if (力场 == null) return;目的值 = GetInputValue<float>("目的值", 目的值); switch(力场属性) 
 {case ForceData.力场强度:
力场.ForceStrength=目的值;
break;
case ForceData.圆形半径:
力场.Radius=目的值;
break;
case ForceData.力场速度:
力场.ForceSpeed=目的值;
break;
case ForceData.力场加速度:
力场.ForceAcceleratedSpeed=目的值;
break;
case ForceData.力场角速度:
力场.ForceAccelerRotation=目的值;
break;
} ConnectDo("退出节点");}}}