using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.力场 
{public class 设置力场二维向量信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public Vector2 目的值;[Input] public Force 力场; 
[Output] public FunctionProgress 退出节点;
public enum ForceData { 
对角线向量=0,
力场全局偏移=1
}
public ForceData 力场属性;
 public override void FunctionDo(string PortName,List<object> param = null) {
 力场 = GetInputValue<Force>("力场", null);if (力场 == null) return;目的值 = GetInputValue<Vector2>("目的值", 目的值); switch(力场属性) 
 {case ForceData.对角线向量:
力场.SquareLength=目的值;
break;
case ForceData.力场全局偏移:
力场.GlobalPositionOffset=目的值;
break;
} ConnectDo("退出节点");}}}