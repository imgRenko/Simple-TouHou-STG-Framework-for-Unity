using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.符卡系统 
{public class 获取符卡系统精灵纹理信息:Node 
 {[Input] public SpellCard 符卡系统; 
[Output] public Sprite 结果;
public enum SpellCardData { 
符卡动画立绘=0
}
public SpellCardData 符卡系统属性;
public override object GetValue(NodePort port) 
{符卡系统 = GetInputValue<SpellCard>("符卡系统", null);if (符卡系统 == null){ return 0;} 
switch(符卡系统属性) 
 {case SpellCardData.符卡动画立绘:
结果=符卡系统.CharacterSprite;
break;
}return 结果;}}}