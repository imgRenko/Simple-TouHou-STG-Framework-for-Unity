using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.敌人 
{public class 设置敌人布尔值信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public bool 目的值;[Input] public Enemy 敌人; 
[Output] public FunctionProgress 退出节点;
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
 public override void FunctionDo(string PortName,List<object> param = null) {
 敌人 = GetInputValue<Enemy>("敌人", null);if (敌人 == null) return;目的值 = GetInputValue<bool>("目的值", 目的值); switch(敌人属性) 
 {case EnemyData.Moving:
敌人.Moving=目的值;
break;
case EnemyData.ToRight:
敌人.ToRight=目的值;
break;
case EnemyData.播放动画时反转X轴:
敌人.FilpX=目的值;
break;
case EnemyData.允许体术玩家:
敌人.CanDestroyPlayer=目的值;
break;
case EnemyData.作为BOSS出现:
敌人.isBoss=目的值;
break;
case EnemyData.出屏销毁:
敌人.destroyWhenOutScreen=目的值;
break;
case EnemyData.BulletControled:
敌人.BulletControled=目的值;
break;
case EnemyData.播放造成伤害时音效:
敌人.UseDamageSound=目的值;
break;
case EnemyData.玩家无敌时不造成任何伤害:
敌人.NobodyNoDamage=目的值;
break;
case EnemyData.敌人动画播放速度随全局速度变化:
敌人.ControlAnimSpeed=目的值;
break;
case EnemyData.声明符卡时无敌:
敌人.expressingSpellNoBody=目的值;
break;
case EnemyData.isGone:
敌人.isGone=目的值;
break;
case EnemyData.HP为0时将销毁:
敌人.CanDestroy=目的值;
break;
case EnemyData.敌人出现后立即播放弹幕:
敌人.UseBarragesWhenGameStart=目的值;
break;
} ConnectDo("退出节点");}}}