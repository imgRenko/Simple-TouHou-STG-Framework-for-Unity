using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.符卡系统 
{public class 设置符卡系统精灵纹理信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public Sprite 目的值;[Input] public SpellCard 符卡系统; 
[Output] public FunctionProgress 退出节点;
public enum SpellCardData { 
符卡动画立绘=0
}
public SpellCardData 符卡系统属性;
 public override void FunctionDo(string PortName,List<object> param = null) {
 符卡系统 = GetInputValue<SpellCard>("符卡系统", null);if (符卡系统 == null) return;目的值 = GetInputValue<Sprite>("目的值", 目的值); switch(符卡系统属性) 
 {case SpellCardData.符卡动画立绘:
符卡系统.CharacterSprite=目的值;
break;
} ConnectDo("退出节点");}}}