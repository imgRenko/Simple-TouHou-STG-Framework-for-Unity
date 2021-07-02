using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.自机 
{public class 获取自机精灵纹理信息:Node 
 {[Input] public Character 自机; 
[Output] public Sprite 结果;
public enum CharacterEnumData { 
Normal=0,
Deadly=1,
Break=2
}
public CharacterEnumData 自机属性; 
public override object GetValue(NodePort port) 
{自机 = GetInputValue<Character>("自机", null);if (自机 == null){ return 0;} 
switch(自机属性) 
 {case CharacterEnumData.Normal:
结果=自机.Normal;
break;
case CharacterEnumData.Deadly:
结果=自机.Deadly;
break;
case CharacterEnumData.Break:
结果=自机.Break;
break;
}return 结果;}}}