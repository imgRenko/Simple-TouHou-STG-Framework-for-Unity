using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.发射器 
{public class 设置发射器二维向量信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public Vector2 目的值;[Input] public Shooting 发射器; 
[Output] public FunctionProgress 退出节点;
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
 public override void FunctionDo(string PortName,List<object> param = null) {
 发射器 = GetInputValue<Shooting>("发射器", null);if (发射器 == null) return;目的值 = GetInputValue<Vector2>("目的值", 目的值); switch(发射器属性) 
 {case ShootingEnumData.发射器发射中心原点:
发射器.Orginal=目的值;
break;
case ShootingEnumData.位置原点自增值:
发射器.PositionIncrement=目的值;
break;
case ShootingEnumData.椭圆长短轴向量:
发射器.ellipseSize=目的值;
break;
case ShootingEnumData.碰撞矩形对角线向量:
发射器.SquareLength=目的值;
break;
case ShootingEnumData.子弹缩放:
发射器.Scale=目的值;
break;
case ShootingEnumData.子弹的全局偏移:
发射器.GlobalOffset=目的值;
break;
case ShootingEnumData.子弹全局加速偏移:
发射器.AcceleratedGlobalOffset=目的值;
break;
}ConnectDo("退出节点");}}}