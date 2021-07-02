using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.敌人 
{public class 获取敌人字符串信息:Node 
 {[Input] public Enemy 敌人; 
[Output] public string 结果;
public enum EnemyData { 
敌人名称=0
}
public EnemyData 敌人属性;
public override object GetValue(NodePort port) 
{敌人 = GetInputValue<Enemy>("敌人", null);if (敌人 == null){ return 0;} 
switch(敌人属性) 
 {case EnemyData.敌人名称:
结果=敌人.Name;
break;
}return 结果;}}}