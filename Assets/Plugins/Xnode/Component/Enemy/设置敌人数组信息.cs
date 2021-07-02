using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.敌人 
{public class 设置敌人数组信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public Anything 目的值;[Input] public Enemy 敌人; 
[Output] public FunctionProgress 退出节点;
public enum EnemyData { 
临时浮点变量=0,
临时浮点变量自增值=1,
临时布尔变量自增值=2,
弹幕合集=3,
SpellCardListIndex=4
}
public EnemyData 敌人属性;
 public override void FunctionDo(string PortName,List<object> param = null) {
 敌人 = GetInputValue<Enemy>("敌人", null);if (敌人 == null) return;object 目的值 = GetInputValue<object>("目的值"); switch(敌人属性) 
 {case EnemyData.临时浮点变量:
敌人.TempValue=(List<float>)目的值;
break;
case EnemyData.临时浮点变量自增值:
敌人.TempValueIncrease= (List<float>)目的值;
break;
case EnemyData.临时布尔变量自增值:
敌人.TempValueBool= (List<bool>)目的值;
break;
case EnemyData.弹幕合集:
敌人.SpellCardList= (List<SpellCard>)目的值;
break;
case EnemyData.SpellCardListIndex:
敌人.SpellCardListIndex= (List<int>)目的值;
break;
} ConnectDo("退出节点");}}}