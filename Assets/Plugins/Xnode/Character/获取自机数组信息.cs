using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.自机 
{public class 获取自机数组信息:Node 
 {[Input] public Character 自机; 
[Output] public object 结果;
public enum CharacterEnumData { 
SoundSource=0,
EffectSound=1,
inTrigger=2
}
public CharacterEnumData 自机属性; 
public override object GetValue(NodePort port) 
{自机 = GetInputValue<Character>("自机", null);if (自机 == null){ return 0;} 
switch(自机属性) 
 {case CharacterEnumData.SoundSource:
结果=自机.SoundSource;
break;
case CharacterEnumData.EffectSound:
结果=自机.EffectSound;
break;
case CharacterEnumData.inTrigger:
结果=自机.inTrigger;
break;
}return 结果;}}}