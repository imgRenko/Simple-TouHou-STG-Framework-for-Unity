using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.敌人 
{public class 设置敌人字符串信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public string 目的值;[Input] public Enemy 敌人; 
[Output] public FunctionProgress 退出节点;
public enum EnemyData { 
敌人名称=0
}
public EnemyData 敌人属性;
 public override void FunctionDo(string PortName,List<object> param = null) {
 敌人 = GetInputValue<Enemy>("敌人", null);if (敌人 == null) return;目的值 = GetInputValue<string>("目的值", 目的值); switch(敌人属性) 
 {case EnemyData.敌人名称:
敌人.Name=目的值;
break;
} ConnectDo("退出节点");}}}