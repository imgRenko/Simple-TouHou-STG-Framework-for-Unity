using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.子弹
{
    public class 获取子弹二维向量信息 : Node
    {
        [Input] public Bullet 子弹;
        [Output] public Vector2 结果;
        public enum BulletData
        {
            AcceleratedSpeedDirectionNow = 0,
            全局加速度方向 = 1,
            BulletPos = 2,
            SquareLength = 3,
            Scale = 4,
            Zero = 5
        }
        public BulletData 子弹属性;
        public override object GetValue(NodePort port)
        {
            子弹 = GetInputValue<Bullet>("子弹"); 
            if (子弹 == null) { 结果 = Vector2.zero; return 结果; }
            switch (子弹属性)
            {
                case BulletData.AcceleratedSpeedDirectionNow:
                    结果 = 子弹.AcceleratedSpeedDirectionNow;
                    break;
                case BulletData.全局加速度方向:
                    结果 = 子弹.AcceleratedSpeedDirectionPer;
                    break;
                case BulletData.BulletPos:
                    结果 = 子弹.BulletPos;
                    break;
                case BulletData.SquareLength:
                    结果 = 子弹.SquareLength;
                    break;
                case BulletData.Scale:
                    结果 = 子弹.Scale;
                    break;
            }
            return 结果;
        }
    }
}