using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.力场 
{public class 设置力场布尔值信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public bool 目的值;[Input] public Force 力场; 
[Output] public FunctionProgress 退出节点;
public enum ForceData { 
允许回滚=0,
对玩家生效=1,
对子弹生效=2,
力场方向为吸引=3,
可复用力场=4,
用自身组件更新=5
}
public ForceData 力场属性;
 public override void FunctionDo(string PortName,List<object> param = null) {
 力场 = GetInputValue<Force>("力场", null);if (力场 == null) return;目的值 = GetInputValue<bool>("目的值", 目的值); switch(力场属性) 
 {case ForceData.允许回滚:
力场.EnableRollBack=目的值;
break;
case ForceData.对玩家生效:
力场.ForPlayer=目的值;
break;
case ForceData.对子弹生效:
力场.ForBullet=目的值;
break;
case ForceData.力场方向为吸引:
力场.Attractive=目的值;
break;
case ForceData.可复用力场:
力场.Reusable=目的值;
break;
case ForceData.用自身组件更新:
力场.UpdateWithSelfComponent=目的值;
break;
} ConnectDo("退出节点");}}}