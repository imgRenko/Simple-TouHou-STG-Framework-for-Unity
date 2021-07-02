using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.子弹 
{public class 获取子弹精灵纹理信息:Node 
 {[Input] public Bullet 子弹; 
[Output] public Sprite 结果;
public enum BulletData { 
BulletSprite=0
}
public BulletData 子弹属性;
public override object GetValue(NodePort port) 
{子弹 = GetInputValue<Bullet>("子弹");if (子弹 == null){ 结果 = null; return 结果;} 
switch(子弹属性) 
 {case BulletData.BulletSprite:
结果=子弹.BulletSprite;
break;
}return 结果;}}}