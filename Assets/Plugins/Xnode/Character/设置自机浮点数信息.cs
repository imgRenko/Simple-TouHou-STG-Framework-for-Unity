using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.自机 
{public class 设置自机浮点数信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public float 目的值;[Input] public Character 自机; 
[Output] public FunctionProgress 退出节点;
public enum CharacterEnumData { 
Speed=0,
Radius=1,
BulletDamage=2,
ExtraDamage=3,
CollectionAllBounsHeight=4,
defaultCollisionRadius=5
}
public CharacterEnumData 自机属性; 
 public override void FunctionDo(string PortName,List<object> param = null) {
 自机 = GetInputValue<Character>("自机", null);if (自机 == null) return;目的值 = GetInputValue<float>("目的值", 目的值); switch(自机属性) 
 {case CharacterEnumData.Speed:
自机.Speed=目的值;
break;
case CharacterEnumData.Radius:
自机.Radius=目的值;
break;
case CharacterEnumData.BulletDamage:
自机.BulletDamage=目的值;
break;
case CharacterEnumData.ExtraDamage:
自机.ExtraDamage=目的值;
break;
case CharacterEnumData.CollectionAllBounsHeight:
自机.CollectionAllBounsHeight=目的值;
break;
case CharacterEnumData.defaultCollisionRadius:
自机.defaultCollisionRadius=目的值;
break;
}ConnectDo("退出节点");}}}