using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.发射器 
{public class 获取发射器字符串信息:Node 
 {[Input] public Shooting 发射器; 
[Output] public string 结果;
public enum ShootingEnumData { 
使用XML数据文件储存发射器数据=0,
Note=1,
BulletTag=2,
子弹销毁时播放动画名称=3,
子弹创建时播放动画名称=4,
XMLPath=5
}
public ShootingEnumData 发射器属性; 
public override object GetValue(NodePort port) 
{发射器 = GetInputValue<Shooting>("发射器", null);if (发射器 == null){ return 0;} 
switch(发射器属性) 
 {case ShootingEnumData.使用XML数据文件储存发射器数据:
结果=发射器.XmlShooting;
break;
case ShootingEnumData.Note:
结果=发射器.Note;
break;
case ShootingEnumData.BulletTag:
结果=发射器.BulletTag;
break;
case ShootingEnumData.子弹销毁时播放动画名称:
结果=发射器.BulletBreakingAnimationName;
break;
case ShootingEnumData.子弹创建时播放动画名称:
结果=发射器.BulletCreatingAnimationName;
break;
case ShootingEnumData.XMLPath:
结果=发射器.XMLPath;
break;
}return 结果;}}}