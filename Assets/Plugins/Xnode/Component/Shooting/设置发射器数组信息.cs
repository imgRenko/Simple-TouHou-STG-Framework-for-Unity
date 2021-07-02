using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.发射器 
{public class 设置发射器数组信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public Anything 目的值;[Input] public Shooting 发射器; 
[Output] public FunctionProgress 退出节点;
public enum ShootingEnumData { 
子弹序列帧集合=0,
子弹销毁序列帧集合=1,
允许影响该子弹的力场对象=2,
允许影响该子弹的触发器对象=3,
ShotBullet=7
}
public ShootingEnumData 发射器属性; 
 public override void FunctionDo(string PortName,List<object> param = null) {
 发射器 = GetInputValue<Shooting>("发射器", null);if (发射器 == null) return;object 目的值 = GetInputValue<object>("目的值"); switch(发射器属性) 
 {case ShootingEnumData.子弹序列帧集合:
发射器.BulletSpritePerFrame=(List<Sprite>)目的值;
break;
case ShootingEnumData.子弹销毁序列帧集合:
发射器.DestroyAnimSprites=(List<Sprite>)目的值;
break;
case ShootingEnumData.允许影响该子弹的力场对象:
发射器.ForceObject= (List<Force>)目的值;
break;
case ShootingEnumData.允许影响该子弹的触发器对象:
发射器.TriggerList= (List<Trigger>)目的值;
break;
case ShootingEnumData.ShotBullet:
发射器.ShotBullet= (List<Bullet>)目的值;
break;
}ConnectDo("退出节点");}}}