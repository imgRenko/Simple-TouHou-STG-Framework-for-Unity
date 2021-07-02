using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.子弹 
{public class 设置子弹三维向量信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public Vector3 目的值;[Input] public Bullet 子弹; 
[Output] public FunctionProgress 退出节点;
public enum BulletData { 
originalPatternPos=0
}
public BulletData 子弹属性;
 public override void FunctionDo(string PortName,List<object> param = null) {
 子弹 = GetInputValue<Bullet>("子弹");if (子弹 == null) return;目的值 = GetInputValue<Vector3>("目的值", 目的值); switch(子弹属性) 
 {case BulletData.originalPatternPos:
子弹.originalPatternPos=目的值;
break;
} ConnectDo("退出节点");}}}