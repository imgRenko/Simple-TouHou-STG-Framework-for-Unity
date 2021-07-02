using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.发射器 
{public class 获取发射器二维向量信息:Node 
 {[Input] public Shooting 发射器; 
[Output] public Vector2 结果;
public enum ShootingEnumData { 
发射器发射中心原点=0,
位置原点自增值=1,
椭圆长短轴向量=2,
碰撞矩形对角线向量=3,
子弹缩放=4,
子弹的全局偏移=5,
子弹全局加速偏移=6
}
public ShootingEnumData 发射器属性; 
public override object GetValue(NodePort port) 
{发射器 = GetInputValue<Shooting>("发射器", null);if (发射器 == null){ 结果 =Vector2.zero; return 结果;} 
switch(发射器属性) 
 {case ShootingEnumData.发射器发射中心原点:
结果=发射器.Orginal;
break;
case ShootingEnumData.位置原点自增值:
结果=发射器.PositionIncrement;
break;
case ShootingEnumData.椭圆长短轴向量:
结果=发射器.ellipseSize;
break;
case ShootingEnumData.碰撞矩形对角线向量:
结果=发射器.SquareLength;
break;
case ShootingEnumData.子弹缩放:
结果=发射器.Scale;
break;
case ShootingEnumData.子弹的全局偏移:
结果=发射器.GlobalOffset;
break;
case ShootingEnumData.子弹全局加速偏移:
结果=发射器.AcceleratedGlobalOffset;
break;
}return 结果;}}}