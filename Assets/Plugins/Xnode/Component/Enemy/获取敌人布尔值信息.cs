using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.敌人 
{public class 获取敌人布尔值信息:Node 
 {[Input] public Enemy 敌人; 
[Output] public bool 结果;
public enum EnemyData { 
Moving=0,
ToRight=1,
播放动画时反转X轴=2,
允许体术玩家=3,
作为BOSS出现=4,
出屏销毁=5,
BulletControled=6,
播放造成伤害时音效=7,
玩家无敌时不造成任何伤害=8,
敌人动画播放速度随全局速度变化=9,
声明符卡时无敌=10,
isGone=11,
HP为0时将销毁=12,
敌人出现后立即播放弹幕=13
}
public EnemyData 敌人属性;
public override object GetValue(NodePort port) 
{敌人 = GetInputValue<Enemy>("敌人", null);if (敌人 == null){ return 0;} 
switch(敌人属性) 
 {case EnemyData.Moving:
结果=敌人.Moving;
break;
case EnemyData.ToRight:
结果=敌人.ToRight;
break;
case EnemyData.播放动画时反转X轴:
结果=敌人.FilpX;
break;
case EnemyData.允许体术玩家:
结果=敌人.CanDestroyPlayer;
break;
case EnemyData.作为BOSS出现:
结果=敌人.isBoss;
break;
case EnemyData.出屏销毁:
结果=敌人.destroyWhenOutScreen;
break;
case EnemyData.BulletControled:
结果=敌人.BulletControled;
break;
case EnemyData.播放造成伤害时音效:
结果=敌人.UseDamageSound;
break;
case EnemyData.玩家无敌时不造成任何伤害:
结果=敌人.NobodyNoDamage;
break;
case EnemyData.敌人动画播放速度随全局速度变化:
结果=敌人.ControlAnimSpeed;
break;
case EnemyData.声明符卡时无敌:
结果=敌人.expressingSpellNoBody;
break;
case EnemyData.isGone:
结果=敌人.isGone;
break;
case EnemyData.HP为0时将销毁:
结果=敌人.CanDestroy;
break;
case EnemyData.敌人出现后立即播放弹幕:
结果=敌人.UseBarragesWhenGameStart;
break;
}return 结果;}}}