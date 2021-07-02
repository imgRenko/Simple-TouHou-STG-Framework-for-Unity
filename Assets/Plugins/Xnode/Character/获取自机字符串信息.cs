using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.自机 
{public class 获取自机字符串信息:Node 
 {[Input] public Character 自机; 
[Output] public string 结果;
public enum CharacterEnumData { 
CharacterName=0,
PlayerTag=1
}
public CharacterEnumData 自机属性; 
public override object GetValue(NodePort port) 
{自机 = GetInputValue<Character>("自机", null);if (自机 == null){ return 0;} 
switch(自机属性) 
 {case CharacterEnumData.CharacterName:
结果=自机.CharacterName;
break;
case CharacterEnumData.PlayerTag:
结果 = Character.PlayerTag;
break;
}return 结果;}}}