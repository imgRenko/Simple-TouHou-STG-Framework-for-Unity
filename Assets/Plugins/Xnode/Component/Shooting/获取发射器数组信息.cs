using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.发射器 
{public class 获取发射器数组信息:Node 
 {[Input] public Shooting 发射器; 
[Output] public Anything 结果;
public enum ShootingEnumData { 
子弹序列帧集合=0,
子弹销毁序列帧集合=1,
允许影响该子弹的力场对象=2,
允许影响该子弹的触发器对象=3,
ShotBullet=7
}
public ShootingEnumData 发射器属性; 
public override object GetValue(NodePort port) 
{发射器 = GetInputValue<Shooting>("发射器", null);if (发射器 == null){  return 0;}
            object 结果 = 0;
            switch (发射器属性) 
 {case ShootingEnumData.子弹序列帧集合:
结果=发射器.BulletSpritePerFrame;
break;
case ShootingEnumData.子弹销毁序列帧集合:
结果=发射器.DestroyAnimSprites;
break;
case ShootingEnumData.允许影响该子弹的力场对象:
结果=发射器.ForceObject;
break;
case ShootingEnumData.允许影响该子弹的触发器对象:
结果=发射器.TriggerList;
break;
case ShootingEnumData.ShotBullet:
结果=发射器.ShotBullet;
break;
}return 结果;}}}