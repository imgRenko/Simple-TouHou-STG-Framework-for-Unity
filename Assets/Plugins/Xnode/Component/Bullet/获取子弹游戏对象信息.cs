using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.子弹 
{public class 获取子弹游戏对象信息:Node 
 {[Input] public Bullet 子弹; 
[Output] public GameObject 结果;
public enum BulletData { 
BulletSpriteController=0,
CustomCollisionGroupMainController=1,
Trail=2,
OtherObject=3
}
public BulletData 子弹属性;
public override object GetValue(NodePort port) 
{子弹 = GetInputValue<Bullet>("子弹");if (子弹 == null){ 结果 = null; return 结果;} 
switch(子弹属性) 
 {case BulletData.BulletSpriteController:
结果=子弹.BulletSpriteController;
break;
case BulletData.CustomCollisionGroupMainController:
结果=子弹.CustomCollisionGroupMainController;
break;
case BulletData.Trail:
结果=子弹.Trail;
break;
case BulletData.OtherObject:
结果=子弹.OtherObject;
break;
}return 结果;}}}