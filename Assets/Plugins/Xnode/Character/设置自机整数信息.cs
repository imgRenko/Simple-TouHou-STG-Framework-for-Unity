using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.自机 
{public class 设置自机整数信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public int 目的值;[Input] public Character 自机; 
[Output] public FunctionProgress 退出节点;
public enum CharacterEnumData { 
invincibleTime=0
}
public CharacterEnumData 自机属性; 
 public override void FunctionDo(string PortName,List<object> param = null) {
 自机 = GetInputValue<Character>("自机", null);if (自机 == null) return;目的值 = GetInputValue<int>("目的值", 目的值); switch(自机属性) 
 {case CharacterEnumData.invincibleTime:
自机.invincibleTime=目的值;
break;
}ConnectDo("退出节点");}}}