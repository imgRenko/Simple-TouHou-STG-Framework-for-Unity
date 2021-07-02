using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.符卡系统 
{public class 获取符卡系统整数信息:Node 
 {[Input] public SpellCard 符卡系统; 
[Output] public int 结果;
public enum SpellCardData { 
播放符卡动画的序号=0,
符卡序号=1,
ScoreBounsNumber=2,
PowerBounsNumber=3,
LiveBounsNumber=4
}
public SpellCardData 符卡系统属性;
public override object GetValue(NodePort port) 
{符卡系统 = GetInputValue<SpellCard>("符卡系统", null);if (符卡系统 == null){ return 0;} 
switch(符卡系统属性) 
 {case SpellCardData.播放符卡动画的序号:
结果=符卡系统.animationIndex;
break;
case SpellCardData.符卡序号:
结果=符卡系统.SpellIndex;
break;
case SpellCardData.ScoreBounsNumber:
结果=符卡系统.ScoreBounsNumber;
break;
case SpellCardData.PowerBounsNumber:
结果=符卡系统.PowerBounsNumber;
break;
case SpellCardData.LiveBounsNumber:
结果=符卡系统.LiveBounsNumber;
break;
}return 结果;}}}