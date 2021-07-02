using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.敌人 
{public class 获取敌人数组信息:Node 
 {[Input] public Enemy 敌人; 
[Output] public Anything 结果;
public enum EnemyData { 
临时浮点变量=0,
临时浮点变量自增值=1,
临时布尔变量自增值=2,
弹幕合集=3,
SpellCardListIndex=4
}
public EnemyData 敌人属性;
public override object GetValue(NodePort port) 
{敌人 = GetInputValue<Enemy>("敌人", 敌人);if (敌人 == null){ return 0;}
            object 结果 = 0;
switch (敌人属性) 
 {case EnemyData.临时浮点变量:
结果=敌人.TempValue;
break;
case EnemyData.临时浮点变量自增值:
结果=敌人.TempValueIncrease;
break;
case EnemyData.临时布尔变量自增值:
结果=敌人.TempValueBool;
break;
case EnemyData.弹幕合集:
结果=敌人.SpellCardList;
break;
case EnemyData.SpellCardListIndex:
结果=敌人.SpellCardListIndex;
break;
}return 结果;}}}