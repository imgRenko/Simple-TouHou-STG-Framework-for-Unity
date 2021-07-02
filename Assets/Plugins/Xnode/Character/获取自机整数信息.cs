using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.自机 
{public class 获取自机整数信息:Node 
 {[Input] public Character 自机; 
[Output] public int 结果;
public enum CharacterEnumData { 
invincibleTime=0
}
public CharacterEnumData 自机属性; 
public override object GetValue(NodePort port) 
{自机 = GetInputValue<Character>("自机", null);if (自机 == null){ return 0;} 
switch(自机属性) 
 {case CharacterEnumData.invincibleTime:
结果=自机.invincibleTime;
break;
}return 结果;}}}