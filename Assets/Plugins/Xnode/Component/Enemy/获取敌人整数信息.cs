using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.敌人 
{public class 获取敌人整数信息:Node 
 {[Input] public Enemy 敌人; 
[Output] public int 结果;
public enum EnemyData { 
ID=0,
奖励点个数=1,
奖励P点个数=2,
奖励大P点个数=3,
奖励残机碎片个数=4
}
public EnemyData 敌人属性;
public override object GetValue(NodePort port) 
{敌人 = GetInputValue<Enemy>("敌人", null);if (敌人 == null){ return 0;} 
switch(敌人属性) 
 {case EnemyData.ID:
结果=敌人.ID;
break;
case EnemyData.奖励点个数:
结果=敌人.BounsScoreNumber;
break;
case EnemyData.奖励P点个数:
结果=敌人.BounsPowerNumber;
break;
case EnemyData.奖励大P点个数:
结果=敌人.BounsFullPowerNumber;
break;
case EnemyData.奖励残机碎片个数:
结果=敌人.BounsLivePieceNumber;
break;
}return 结果;}}}