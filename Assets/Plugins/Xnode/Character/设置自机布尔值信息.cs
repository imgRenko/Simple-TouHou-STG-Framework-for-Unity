using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.自机 
{public class 设置自机布尔值信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public bool 目的值;[Input] public Character 自机; 
[Output] public FunctionProgress 退出节点;
public enum CharacterEnumData { 
Invincible=0,
countGrazeWhenNobody=1,
ControlAniSpeed=2,
GameOverRightNow=3,
DestroyingBullet=4,
isGone=5
}
public CharacterEnumData 自机属性; 
 public override void FunctionDo(string PortName,List<object> param = null) {
 自机 = GetInputValue<Character>("自机", null);if (自机 == null) return;目的值 = GetInputValue<bool>("目的值", 目的值); switch(自机属性) 
 {case CharacterEnumData.Invincible:
自机.Invincible=目的值;
break;
case CharacterEnumData.countGrazeWhenNobody:
自机.countGrazeWhenNobody=目的值;
break;
case CharacterEnumData.ControlAniSpeed:
自机.ControlAniSpeed=目的值;
break;
case CharacterEnumData.GameOverRightNow:
自机.GameOverRightNow=目的值;
break;
case CharacterEnumData.DestroyingBullet:
自机.DestroyingBullet=目的值;
break;
case CharacterEnumData.isGone:
自机.isGone=目的值;
break;
}ConnectDo("退出节点");}}}