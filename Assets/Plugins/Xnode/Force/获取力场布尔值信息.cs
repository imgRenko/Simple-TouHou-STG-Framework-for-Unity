using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.力场 
{public class 获取力场布尔值信息:Node 
 {[Input] public Force 力场; 
[Output] public bool 结果;
public enum ForceData { 
允许回滚=0,
对玩家生效=1,
对子弹生效=2,
力场方向为吸引=3,
可复用力场=4,
用自身组件更新=5
}
public ForceData 力场属性;
public override object GetValue(NodePort port) 
{力场 = GetInputValue<Force>("力场", null);if (力场 == null){ return 0;} 
switch(力场属性) 
 {case ForceData.允许回滚:
结果=力场.EnableRollBack;
break;
case ForceData.对玩家生效:
结果=力场.ForPlayer;
break;
case ForceData.对子弹生效:
结果=力场.ForBullet;
break;
case ForceData.力场方向为吸引:
结果=力场.Attractive;
break;
case ForceData.可复用力场:
结果=力场.Reusable;
break;
case ForceData.用自身组件更新:
结果=力场.UpdateWithSelfComponent;
break;
}return 结果;}}}