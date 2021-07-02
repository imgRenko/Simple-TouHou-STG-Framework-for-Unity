using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.发射器 
{public class 设置发射器字符串信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public string 目的值;[Input] public Shooting 发射器; 
[Output] public FunctionProgress 退出节点;
public enum ShootingEnumData { 
使用XML数据文件储存发射器数据=0,
Note=1,
BulletTag=2,
子弹销毁时播放动画名称=3,
子弹创建时播放动画名称=4,
XMLPath=5
}
public ShootingEnumData 发射器属性; 
 public override void FunctionDo(string PortName,List<object> param = null) {
 发射器 = GetInputValue<Shooting>("发射器", null);if (发射器 == null) return;目的值 = GetInputValue<string>("目的值", 目的值); switch(发射器属性) 
 {case ShootingEnumData.使用XML数据文件储存发射器数据:
发射器.XmlShooting=目的值;
break;
case ShootingEnumData.Note:
发射器.Note=目的值;
break;
case ShootingEnumData.BulletTag:
发射器.BulletTag=目的值;
break;
case ShootingEnumData.子弹销毁时播放动画名称:
发射器.BulletBreakingAnimationName=目的值;
break;
case ShootingEnumData.子弹创建时播放动画名称:
发射器.BulletCreatingAnimationName=目的值;
break;
case ShootingEnumData.XMLPath:
发射器.XMLPath=目的值;
break;
}ConnectDo("退出节点");}}}