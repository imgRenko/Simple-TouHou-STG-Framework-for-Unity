using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.子弹 
{public class 设置子弹数组信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public Anything 目的值;[Input] public Bullet 子弹; 
[Output] public FunctionProgress 退出节点;
public enum BulletData { 
TriggerList=0,
BulletFramesSprites=1,
ForceObject=2,
SingleParmCurve=3,
ShootingRef=4,
DestroyAnimSprites=5,
trackInfoSign=6,
CommandList=7,
inTrigger=8
}
public BulletData 子弹属性;
 public override void FunctionDo(string PortName,List<object> param = null) {
 子弹 = GetInputValue<Bullet>("子弹");if (子弹 == null) return;object 目的值 = GetInputValue<object>("目的值"); switch(子弹属性) 

 {case BulletData.TriggerList:
子弹.TriggerList=(List<Trigger>)目的值;
break;
case BulletData.BulletFramesSprites:
子弹.BulletFramesSprites= (List<Sprite>)目的值;
break;
case BulletData.ForceObject:
子弹.ForceObject= (List<Force>)目的值;
break;
case BulletData.SingleParmCurve:
子弹.SingleParmCurve= (List<AnimationCurve>)目的值;
break;
case BulletData.ShootingRef:
子弹.ShootingRef = (List<Shooting>)目的值;
break;
case BulletData.DestroyAnimSprites:
子弹.DestroyAnimSprites = (List<Sprite>)目的值;
break;
case BulletData.trackInfoSign:
子弹.trackInfoSign = (List<bool>)目的值;
break;
case BulletData.inTrigger:
子弹.inTrigger = (List<bool>)目的值;
break;

} ConnectDo("退出节点");}}}