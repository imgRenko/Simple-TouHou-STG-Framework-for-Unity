using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.敌人
{
    public class 敌人发动弹幕 : Node
    {
        [Input] public FunctionProgress 进入节点;
        [Input] public Enemy 敌人;
        [Output] public FunctionProgress 退出节点;
        
        
        public override void FunctionDo(string PortName, List<object> param = null)
        {
            敌人 = GetInputValue("敌人", 敌人);
            敌人.UseBarrages();
            ConnectDo("退出节点");
        }
    }
}