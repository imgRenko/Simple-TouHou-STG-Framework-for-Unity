using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.子弹
{
    public class 设置子弹精灵纹理信息 : Node
    {
        [Input] public FunctionProgress 进入节点;
        [Input] public Sprite 目的值;
        [Input] public Bullet 子弹;
        [Output] public FunctionProgress 退出节点;
        NodePort 想要操作的变量Port,目的;
        protected override void Init()
        {
            想要操作的变量Port = GetPort("子弹");
            目的 = GetPort("目的值");
        }
        public override void FunctionDo(string PortName, List<object> param = null)
        {
            子弹 = 想要操作的变量Port.GetInputValue<Bullet>();
            if (子弹 == null)
                return;
            目的值 = 目的.GetInputValue<Sprite>();

            子弹.ChangeSprite(目的值);

            ConnectDo("退出节点");
        }
    }
}