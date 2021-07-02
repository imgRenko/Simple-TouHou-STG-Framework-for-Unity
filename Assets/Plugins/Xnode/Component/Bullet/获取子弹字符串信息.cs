using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.子弹 
{public class 获取子弹字符串信息:Node 
 {[Input] public Bullet 子弹; 
[Output] public string 结果;
public enum BulletData { 
子弹Tag=0,
BulletBreakingAnimationName=1
}
public BulletData 子弹属性;
public override object GetValue(NodePort port) 
{子弹 = GetInputValue<Bullet>("子弹");if (子弹 == null){ return 0;} 
switch(子弹属性) 
 {case BulletData.子弹Tag:
结果=子弹.Tag;
break;
case BulletData.BulletBreakingAnimationName:
结果=子弹.BulletBreakingAnimationName;
break;
}return 结果;}}}