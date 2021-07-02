using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.符卡系统 
{public class 设置符卡系统数组信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public object 目的值;[Input] public SpellCard 符卡系统; 
[Output] public FunctionProgress 退出节点;
public enum SpellCardData { 
重置用发射器列表=0,
frameSetting=1
}
public SpellCardData 符卡系统属性;
 public override void FunctionDo(string PortName,List<object> param = null) {
 符卡系统 = GetInputValue<SpellCard>("符卡系统", null);if (符卡系统 == null) return;目的值 = GetInputValue<object>("目的值", 目的值); switch(符卡系统属性) 
 {case SpellCardData.重置用发射器列表:
符卡系统.ResetShooting=(List<Shooting>)目的值;
break;
case SpellCardData.frameSetting:
符卡系统.frameSetting=(List<CharacterMovementFrame>)目的值;
break;
} ConnectDo("退出节点");}}}