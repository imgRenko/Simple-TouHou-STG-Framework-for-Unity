using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.子弹 
{public class 获取子弹数组信息:Node 
 {[Input] public Bullet 子弹; 
[Output] public Anything 结果;
public enum BulletData { 
TriggerList=0,
BulletFramesSprites=1,
ForceObject=2,
SingleParmCurve=3,
ShootingRef=4,
DestroyAnimSprites=5,
trackInfoSign=6,
CommandList=7,
inTrigger=8,
SubBullet = 9
}
public BulletData 子弹属性;
public override object GetValue(NodePort port) 
{子弹 = GetInputValue<Bullet>("子弹");if (子弹 == null){ return 0;}
            object 结果 = 0;
            switch (子弹属性) 
 {case BulletData.TriggerList:
结果=子弹.TriggerList;
break;
case BulletData.BulletFramesSprites:
结果=子弹.BulletFramesSprites;
break;
case BulletData.ForceObject:
结果=子弹.ForceObject;
break;
case BulletData.SingleParmCurve:
结果=子弹.SingleParmCurve;
break;
case BulletData.ShootingRef:
结果=子弹.ShootingRef;
break;
case BulletData.DestroyAnimSprites:
结果=子弹.DestroyAnimSprites;
break;
case BulletData.trackInfoSign:
结果=子弹.trackInfoSign;
break;
case BulletData.CommandList:
结果=子弹.CommandList;
break;
case BulletData.inTrigger:
结果=子弹.inTrigger;
break;
                case BulletData.SubBullet:
                    List<Bullet> bullets = new List<Bullet>(子弹.GetComponentsInChildren<Bullet>());
                    结果 = bullets;
                    break;
            }
            return 结果;}}}