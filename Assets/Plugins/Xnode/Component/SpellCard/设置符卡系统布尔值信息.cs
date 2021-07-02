using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.符卡系统 
{public class 设置符卡系统布尔值信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public bool 目的值;[Input] public SpellCard 符卡系统; 
[Output] public FunctionProgress 退出节点;
public enum SpellCardData { 
Use=0,
显示击破时文字=1,
用XML文件记录符卡收率=2,
不销毁全屏子弹=3,
不销毁符卡对象=4,
无时间限制=5,
自动使用下一张符卡=6,
重置发射器时销毁全屏子弹=7,
这是时符=8,
显示BOSSHPGUI=9,
显示计时器=10,
播放符卡动画=11,
隐藏游戏原背景=12,
允许改变关卡流程状态=13,
在屏幕底下使用位置标记=14,
启动游戏时自关闭=15,
启用符卡移动器=16,
于使用符卡前重置=17,
可复用的移动器=18
}
public SpellCardData 符卡系统属性;
 public override void FunctionDo(string PortName,List<object> param = null) {
 符卡系统 = GetInputValue<SpellCard>("符卡系统", null);if (符卡系统 == null) return;目的值 = GetInputValue<bool>("目的值", 目的值); switch(符卡系统属性) 
 {case SpellCardData.Use:
符卡系统.Use=目的值;
break;
case SpellCardData.显示击破时文字:
符卡系统.ShowTip=目的值;
break;
case SpellCardData.用XML文件记录符卡收率:
符卡系统.spellRecord=目的值;
break;
case SpellCardData.不销毁全屏子弹:
符卡系统.DonDestroyBullets=目的值;
break;
case SpellCardData.不销毁符卡对象:
符卡系统.DonDestroySpell=目的值;
break;
case SpellCardData.无时间限制:
符卡系统.NoTimeLimit=目的值;
break;
case SpellCardData.自动使用下一张符卡:
符卡系统.nextSpell=目的值;
break;
case SpellCardData.重置发射器时销毁全屏子弹:
符卡系统.resetShootingDestroyingBullets=目的值;
break;
case SpellCardData.这是时符:
符卡系统.TimeSpell=目的值;
break;
case SpellCardData.显示BOSSHPGUI:
符卡系统.UseHP=目的值;
break;
case SpellCardData.显示计时器:
符卡系统.UseTimer=目的值;
break;
case SpellCardData.播放符卡动画:
符卡系统.ShowAnimation=目的值;
break;
case SpellCardData.隐藏游戏原背景:
符卡系统.HideDefaultBackGround=目的值;
break;
case SpellCardData.允许改变关卡流程状态:
符卡系统.ChangeRoadSign=目的值;
break;
case SpellCardData.在屏幕底下使用位置标记:
符卡系统.UseEnemyPositonSign=目的值;
break;
case SpellCardData.启动游戏时自关闭:
符卡系统.ObjectDisactive=目的值;
break;
case SpellCardData.启用符卡移动器:
符卡系统.enableThen=目的值;
break;
case SpellCardData.于使用符卡前重置:
符卡系统.resetBeforeSpellCard=目的值;
break;
case SpellCardData.可复用的移动器:
符卡系统.resetReuse=目的值;
break;
} ConnectDo("退出节点");}}}