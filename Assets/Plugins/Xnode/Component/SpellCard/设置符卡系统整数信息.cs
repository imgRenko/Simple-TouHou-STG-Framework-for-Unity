using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.符卡系统 
{public class 设置符卡系统整数信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public int 目的值;[Input] public SpellCard 符卡系统; 
[Output] public FunctionProgress 退出节点;
public enum SpellCardData { 
播放符卡动画的序号=0,
符卡序号=1,
ScoreBounsNumber=2,
PowerBounsNumber=3,
LiveBounsNumber=4
}
public SpellCardData 符卡系统属性;
 public override void FunctionDo(string PortName,List<object> param = null) {
 符卡系统 = GetInputValue<SpellCard>("符卡系统", null);if (符卡系统 == null) return;目的值 = GetInputValue<int>("目的值", 目的值); switch(符卡系统属性) 
 {case SpellCardData.播放符卡动画的序号:
符卡系统.animationIndex=目的值;
break;
case SpellCardData.符卡序号:
符卡系统.SpellIndex=目的值;
break;
case SpellCardData.ScoreBounsNumber:
符卡系统.ScoreBounsNumber=目的值;
break;
case SpellCardData.PowerBounsNumber:
符卡系统.PowerBounsNumber=目的值;
break;
case SpellCardData.LiveBounsNumber:
符卡系统.LiveBounsNumber=目的值;
break;
} ConnectDo("退出节点");}}}