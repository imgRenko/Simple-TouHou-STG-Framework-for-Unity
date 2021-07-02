using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.子弹
{
    public class 获取子弹整数信息 : Node
    {
        [Input] public Bullet 子弹;
        [Output] public int 结果;
        public enum BulletData
        {
            轨迹长度 = 0,
            SpriteIndex = 1,
            子弹序号 = 2,
            序列帧动画切换速度 = 3,
            子弹实例ID = 4,
            子弹批次 = 5,
            读取贝塞尔曲线速度 = 6,
            发射器子弹序号 = 7
        }
        public BulletData 子弹属性;
        public override object GetValue(NodePort port)
        {
            子弹 = GetInputValue<Bullet>("子弹"); if (子弹 == null) { 结果 = -108; return 结果; }
            switch (子弹属性)
            {
                case BulletData.轨迹长度:
                    结果 = 子弹.TrailLength;
                    break;
                case BulletData.SpriteIndex:
                    结果 = 子弹.SpriteIndex;
                    break;
                case BulletData.子弹序号:
                    结果 = 子弹.BulletIndex;
                    break;
                case BulletData.序列帧动画切换速度:
                    结果 = 子弹.nextFramewaitTime;
                    break;
                case BulletData.子弹实例ID:
                    结果 = 子弹.ID;
                    break;
                case BulletData.子弹批次:
                    结果 = 子弹.BarrageBatch;
                    break;
                case BulletData.读取贝塞尔曲线速度:
                    结果 = 子弹.ReadPointTrackSpeed;
                    break;
                case BulletData.发射器子弹序号:
                    结果 = 子弹.shootingIndex;
                    break;

            }
            return 结果;
        }
    }
}