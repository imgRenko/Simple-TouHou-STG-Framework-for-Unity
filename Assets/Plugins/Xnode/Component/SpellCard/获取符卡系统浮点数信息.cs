using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.符卡系统 
{public class 获取符卡系统浮点数信息:Node 
 {[Input] public SpellCard 符卡系统; 
[Output] public float 结果;
public enum SpellCardData { 
符卡使用时间秒数=0,
符卡使用时间毫秒=1,
符卡血量=2,
符卡发射器重置时间=3,
下一张符卡等待时间=4,
符卡已使用时间=5,
符卡重置发射器计时=6,
符卡使用时间总帧数=7,
最大使用时间=8
}
public SpellCardData 符卡系统属性;
public override object GetValue(NodePort port) 
{符卡系统 = GetInputValue<SpellCard>("符卡系统", null);if (符卡系统 == null){ return 0;} 
switch(符卡系统属性) 
 {case SpellCardData.符卡使用时间秒数:
结果=符卡系统.Time;
break;
case SpellCardData.符卡使用时间毫秒:
结果=符卡系统.MixSecond;
break;
case SpellCardData.符卡血量:
结果=符卡系统.MaxHP;
break;
case SpellCardData.符卡发射器重置时间:
结果=符卡系统.ResetTime;
break;
case SpellCardData.下一张符卡等待时间:
结果=符卡系统.nextSpellCardWaitingTime;
break;
case SpellCardData.符卡已使用时间:
结果=符卡系统.SpellCardUsingTime;
break;
case SpellCardData.符卡重置发射器计时:
结果=符卡系统.ResetTotalFrame;
break;
case SpellCardData.符卡使用时间总帧数:
结果=符卡系统.maxFrame;
break;
case SpellCardData.最大使用时间:
结果=符卡系统.maxMovementFrame;
break;

}return 结果;}}}