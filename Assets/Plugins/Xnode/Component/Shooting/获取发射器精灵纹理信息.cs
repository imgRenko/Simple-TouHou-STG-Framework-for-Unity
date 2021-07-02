using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.发射器 
{public class 获取发射器精灵纹理信息:Node 
 {[Input] public Shooting 发射器; 
[Output] public Sprite 结果;
public enum ShootingEnumData { 
CustomSprite=0,
CreatingCustomSprite=1
}
public ShootingEnumData 发射器属性; 
public override object GetValue(NodePort port) 
{发射器 = GetInputValue<Shooting>("发射器", null);if (发射器 == null){ 结果 = null; return 结果;} 
switch(发射器属性) 
 {case ShootingEnumData.CustomSprite:
结果=发射器.CustomSprite;
break;
case ShootingEnumData.CreatingCustomSprite:
结果=发射器.CreatingCustomSprite;
break;
}return 结果;}}}