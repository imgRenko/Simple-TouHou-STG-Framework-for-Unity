using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.子弹 
{public class 设置子弹二维向量信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public Vector2 目的值;[Input] public Bullet 子弹; 
[Output] public FunctionProgress 退出节点;
public enum BulletData { 
AcceleratedSpeedDirectionNow=0,
全局加速度方向=1,
BulletPos=2,
SquareLength=3,
Scale=4,
Zero=5
}
public BulletData 子弹属性;
 public override void FunctionDo(string PortName,List<object> param = null) {
 子弹 = GetInputValue<Bullet>("子弹");if (子弹 == null) return;目的值 = GetInputValue<Vector2>("目的值", 目的值); switch(子弹属性) 
 {case BulletData.AcceleratedSpeedDirectionNow:
子弹.AcceleratedSpeedDirectionNow=目的值;
break;
case BulletData.全局加速度方向:
子弹.AcceleratedSpeedDirectionPer=目的值;
break;
case BulletData.BulletPos:
子弹.BulletPos=目的值;
break;
case BulletData.SquareLength:
子弹.SquareLength=目的值;
break;
case BulletData.Scale:
子弹.Scale=目的值;
break;
} ConnectDo("退出节点");}}}