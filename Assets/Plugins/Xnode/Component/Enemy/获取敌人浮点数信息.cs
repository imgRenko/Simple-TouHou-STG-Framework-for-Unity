using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.敌人 
{public class 获取敌人浮点数信息:Node 
 {[Input] public Enemy 敌人; 
[Output] public float 结果;
public enum EnemyData { 
敌人目前生命值=0,
敌人最大生命值=1,
Speed=2,
初始移动=3
}
public EnemyData 敌人属性;
public override object GetValue(NodePort port) 
{敌人 = GetInputValue<Enemy>("敌人", null);if (敌人 == null){ return 0;} 
switch(敌人属性) 
 {case EnemyData.敌人目前生命值:
结果=敌人.HP;
break;
case EnemyData.敌人最大生命值:
结果=敌人.MaxHP;
break;
case EnemyData.Speed:
结果=敌人.Speed;
break;
case EnemyData.初始移动:
结果=敌人.RunTime;
break;
}return 结果;}}}