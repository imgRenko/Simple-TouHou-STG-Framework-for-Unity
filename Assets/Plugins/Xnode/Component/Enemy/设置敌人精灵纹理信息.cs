using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.敌人 
{public class 设置敌人精灵纹理信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public Sprite 目的值;[Input] public Enemy 敌人; 
[Output] public FunctionProgress 退出节点;
public enum EnemyData { 
敌人正常立绘=0,
敌人击败立绘=1,
敌人受挫立绘=2
}
public EnemyData 敌人属性;
 public override void FunctionDo(string PortName,List<object> param = null) {
 敌人 = GetInputValue<Enemy>("敌人", null);if (敌人 == null) return;目的值 = GetInputValue<Sprite>("目的值", 目的值); switch(敌人属性) 
 {case EnemyData.敌人正常立绘:
敌人.Normal=目的值;
break;
case EnemyData.敌人击败立绘:
敌人.Deadly=目的值;
break;
case EnemyData.敌人受挫立绘:
敌人.Break=目的值;
break;
} ConnectDo("退出节点");}}}