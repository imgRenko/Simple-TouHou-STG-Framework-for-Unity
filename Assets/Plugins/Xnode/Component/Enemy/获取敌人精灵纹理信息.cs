using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.敌人 
{public class 获取敌人精灵纹理信息:Node 
 {[Input] public Enemy 敌人; 
[Output] public Sprite 结果;
public enum EnemyData { 
敌人正常立绘=0,
敌人击败立绘=1,
敌人受挫立绘=2
}
public EnemyData 敌人属性;
public override object GetValue(NodePort port) 
{敌人 = GetInputValue<Enemy>("敌人", null);if (敌人 == null){ return 0;} 
switch(敌人属性) 
 {case EnemyData.敌人正常立绘:
结果=敌人.Normal;
break;
case EnemyData.敌人击败立绘:
结果=敌人.Deadly;
break;
case EnemyData.敌人受挫立绘:
结果=敌人.Break;
break;
}return 结果;}}}