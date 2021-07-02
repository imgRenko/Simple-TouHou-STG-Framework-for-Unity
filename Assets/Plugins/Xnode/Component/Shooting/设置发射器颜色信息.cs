using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.发射器 
{public class 设置发射器颜色信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public Color 目的值;[Input] public Shooting 发射器; 
[Output] public FunctionProgress 退出节点;
public enum ShootingEnumData { 
BulletColor=0,
BrokenBulletColor=1,
CreateBulletColor=2
}
public ShootingEnumData 发射器属性; 
 public override void FunctionDo(string PortName,List<object> param = null) {
 发射器 = GetInputValue<Shooting>("发射器", null);if (发射器 == null) return;目的值 = GetInputValue<Color>("目的值", Color.white); switch(发射器属性) 
 {case ShootingEnumData.BulletColor:
发射器.BulletColor=目的值;
break;
case ShootingEnumData.BrokenBulletColor:
发射器.BrokenBulletColor=目的值;
break;
case ShootingEnumData.CreateBulletColor:
发射器.CreateBulletColor=目的值;
break;
}ConnectDo("退出节点");}}}