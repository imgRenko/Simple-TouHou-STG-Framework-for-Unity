using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.自机 
{public class 获取自机浮点数信息:Node 
 {[Input] public Character 自机; 
[Output] public float 结果;
public enum CharacterEnumData { 
Speed=0,
Radius=1,
BulletDamage=2,
ExtraDamage=3,
CollectionAllBounsHeight=4,
defaultCollisionRadius=5
}
public CharacterEnumData 自机属性; 
public override object GetValue(NodePort port) 
{自机 = GetInputValue<Character>("自机", null);if (自机 == null){ return 0;} 
switch(自机属性) 
 {case CharacterEnumData.Speed:
结果=自机.Speed;
break;
case CharacterEnumData.Radius:
结果=自机.Radius;
break;
case CharacterEnumData.BulletDamage:
结果=自机.BulletDamage;
break;
case CharacterEnumData.ExtraDamage:
结果=自机.ExtraDamage;
break;
case CharacterEnumData.CollectionAllBounsHeight:
结果=自机.CollectionAllBounsHeight;
break;
case CharacterEnumData.defaultCollisionRadius:
结果=自机.defaultCollisionRadius;
break;
}return 结果;}}}