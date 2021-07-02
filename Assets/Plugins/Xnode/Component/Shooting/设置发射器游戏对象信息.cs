using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.发射器 
{public class 设置发射器游戏对象信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public GameObject 目的值;[Input] public Shooting 发射器; 
[Output] public FunctionProgress 退出节点;
public enum ShootingEnumData { 
自定义碰撞盒对象=0,
被发射子弹需要携带的对象=1,
BulletBase=2,
MasterBulletObject=3
}
public ShootingEnumData 发射器属性; 
 public override void FunctionDo(string PortName,List<object> param = null) {
 发射器 = GetInputValue<Shooting>("发射器", null);if (发射器 == null) return;目的值 = GetInputValue<GameObject>("目的值", 目的值); switch(发射器属性) 
 {case ShootingEnumData.自定义碰撞盒对象:
发射器.CustomCollisionGroup=目的值;
break;
case ShootingEnumData.被发射子弹需要携带的对象:
发射器.BulletShootingObject=目的值;
break;
case ShootingEnumData.BulletBase:
发射器.BulletBase=目的值;
break;
case ShootingEnumData.MasterBulletObject:
发射器.MasterBulletObject=目的值;
break;
}ConnectDo("退出节点");}}}