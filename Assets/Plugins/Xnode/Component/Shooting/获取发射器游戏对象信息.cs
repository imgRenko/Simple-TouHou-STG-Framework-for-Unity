using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.发射器 
{public class 获取发射器游戏对象信息:Node 
 {[Input] public Shooting 发射器; 
[Output] public GameObject 结果;
public enum ShootingEnumData { 
自定义碰撞盒对象=0,
被发射子弹需要携带的对象=1,
BulletBase=2,
MasterBulletObject=3
}
public ShootingEnumData 发射器属性; 
public override object GetValue(NodePort port) 
{发射器 = GetInputValue<Shooting>("发射器", null);if (发射器 == null){ 结果 = null; return 结果;} 
switch(发射器属性) 
 {case ShootingEnumData.自定义碰撞盒对象:
结果=发射器.CustomCollisionGroup;
break;
case ShootingEnumData.被发射子弹需要携带的对象:
结果=发射器.BulletShootingObject;
break;
case ShootingEnumData.BulletBase:
结果=发射器.BulletBase;
break;
case ShootingEnumData.MasterBulletObject:
结果=发射器.MasterBulletObject;
break;
}return 结果;}}}