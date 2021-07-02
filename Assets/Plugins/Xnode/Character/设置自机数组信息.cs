using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.自机 
{public class 设置自机数组信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public Anything 目的值;[Input] public Character 自机; 
[Output] public FunctionProgress 退出节点;
public enum CharacterEnumData { 
SoundSource=0,
EffectSound=1,
inTrigger=2
}
public CharacterEnumData 自机属性; 
 public override void FunctionDo(string PortName,List<object> param = null) {
 自机 = GetInputValue<Character>("自机", null);if (自机 == null) return;object 目的 = GetInputValue<object>("目的值", null); switch(自机属性) 
 {case CharacterEnumData.SoundSource:
自机.SoundSource=(List<AudioSource>)目的;
break;
case CharacterEnumData.EffectSound:
自机.EffectSound= (List<AudioClip>)目的;
break;
case CharacterEnumData.inTrigger:
自机.inTrigger= (List<bool>)目的;
break;
}ConnectDo("退出节点");}}}