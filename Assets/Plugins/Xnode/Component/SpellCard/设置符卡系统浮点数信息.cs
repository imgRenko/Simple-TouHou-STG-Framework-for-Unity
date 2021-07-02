using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.符卡系统 
{public class 设置符卡系统浮点数信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public float 目的值;[Input] public SpellCard 符卡系统; 
[Output] public FunctionProgress 退出节点;
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
 public override void FunctionDo(string PortName,List<object> param = null) {
 符卡系统 = GetInputValue<SpellCard>("符卡系统", null);if (符卡系统 == null) return;目的值 = GetInputValue<float>("目的值", 目的值); switch(符卡系统属性) 
 {case SpellCardData.符卡使用时间秒数:
符卡系统.Time=目的值;
break;
case SpellCardData.符卡使用时间毫秒:
符卡系统.MixSecond=目的值;
break;
case SpellCardData.符卡血量:
符卡系统.MaxHP=目的值;
break;
case SpellCardData.符卡发射器重置时间:
符卡系统.ResetTime=目的值;
break;
case SpellCardData.下一张符卡等待时间:
符卡系统.nextSpellCardWaitingTime=目的值;
break;
case SpellCardData.符卡已使用时间:
符卡系统.SpellCardUsingTime=目的值;
break;
case SpellCardData.符卡重置发射器计时:
符卡系统.ResetTotalFrame=目的值;
break;
case SpellCardData.符卡使用时间总帧数:
符卡系统.maxFrame=目的值;
break;
case SpellCardData.最大使用时间:
符卡系统.maxMovementFrame=目的值;
break;

} ConnectDo("退出节点");}}}