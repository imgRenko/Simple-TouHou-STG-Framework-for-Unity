using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.子弹
{
    public class 设置子弹游戏对象信息 : Node
    {
        [Input] public FunctionProgress 进入节点;[Input] public GameObject 目的值;[Input] public Bullet 子弹;
        [Output] public FunctionProgress 退出节点;
        public enum BulletData
        {
            BulletSpriteController = 0,
            CustomCollisionGroupMainController = 1,
            Trail = 2,
            OtherObject = 3
        }
        public BulletData 子弹属性;
        public override void FunctionDo(string PortName, List<object> param = null)
        {
            子弹 = GetInputValue<Bullet>("子弹"); if (子弹 == null) return; 目的值 = GetInputValue<GameObject>("目的值", 目的值); switch (子弹属性)
            {
                case BulletData.BulletSpriteController:
                    子弹.BulletSpriteController = 目的值;
                    break;
                case BulletData.CustomCollisionGroupMainController:
                    子弹.CustomCollisionGroupMainController = 目的值;
                    break;
                case BulletData.Trail:
                    子弹.Trail = 目的值;
                    break;
                case BulletData.OtherObject:
                    子弹.ChangeOtherObject(目的值);
                    break;
            }
            ConnectDo("退出节点");
        }
    }
}