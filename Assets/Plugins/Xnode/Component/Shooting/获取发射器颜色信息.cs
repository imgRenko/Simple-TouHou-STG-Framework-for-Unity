using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.发射器 
{public class 获取发射器颜色信息:Node 
 {[Input] public Shooting 发射器; 
[Output] public Color 结果;
public enum ShootingEnumData { 
BulletColor=0,
BrokenBulletColor=1,
CreateBulletColor=2
}
public ShootingEnumData 发射器属性; 
public override object GetValue(NodePort port) 
{发射器 = GetInputValue<Shooting>("发射器", null);if (发射器 == null){ 结果 = Color.white; return 结果;} 
switch(发射器属性) 
 {case ShootingEnumData.BulletColor:
结果=发射器.BulletColor;
break;
case ShootingEnumData.BrokenBulletColor:
结果=发射器.BrokenBulletColor;
break;
case ShootingEnumData.CreateBulletColor:
结果=发射器.CreateBulletColor;
break;
}return 结果;}}}