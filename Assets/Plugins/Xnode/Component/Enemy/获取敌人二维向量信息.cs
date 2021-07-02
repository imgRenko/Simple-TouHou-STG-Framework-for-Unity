using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.敌人 
{public class 获取敌人二维向量信息:Node 
 {[Input] public Enemy 敌人; 
[Output] public Vector2 结果;
public enum EnemyData { 
MovePoint=0,
PictrueMaskOffset=1
}
public EnemyData 敌人属性;
public override object GetValue(NodePort port) 
{敌人 = GetInputValue<Enemy>("敌人", null);if (敌人 == null){ return 0;} 
switch(敌人属性) 
 {case EnemyData.MovePoint:
结果=敌人.GetMoveTargetPoint();
break;
case EnemyData.PictrueMaskOffset:
结果=敌人.PictrueMaskOffset;
break;
}return 结果;}}}