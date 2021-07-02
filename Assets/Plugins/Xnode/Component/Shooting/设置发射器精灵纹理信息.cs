using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.发射器 
{public class 设置发射器精灵纹理信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public Sprite 目的值;[Input] public Shooting 发射器; 
[Output] public FunctionProgress 退出节点;
public enum ShootingEnumData { 
CustomSprite=0,
CreatingCustomSprite=1
}
public ShootingEnumData 发射器属性; 
 public override void FunctionDo(string PortName,List<object> param = null) {
 发射器 = GetInputValue<Shooting>("发射器", null);if (发射器 == null) return;目的值 = GetInputValue<Sprite>("目的值", 目的值); switch(发射器属性) 
 {case ShootingEnumData.CustomSprite:
发射器.CustomSprite=目的值;
break;
case ShootingEnumData.CreatingCustomSprite:
发射器.CreatingCustomSprite=目的值;
break;
}ConnectDo("退出节点");}}}