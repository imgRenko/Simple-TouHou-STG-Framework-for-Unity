using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.子弹
{
    public class 获取子弹浮点数信息 : Node
    {
        [Input] public Bullet 子弹;
        [Output] public float 结果;
        NodePort 子弹Port;
        public enum BulletData
        {
            子弹运动速度 = 0,
            子弹运动加速度 = 1,
            轨迹刷新速度 = 2,
            子弹速度插值速度 = 3,
            子弹速度方向 = 4,
            子弹角速度 = 5,
            角速度插值速度 = 6,
            碰撞半径 = 7,
            子弹使用寿命 = 8,
            子弹年龄 = 9, 旋转坐标系度量值 = 19,

            子弹朝向 = 10,
            子弹曲线事件延迟 = 11,
            最低深度 = 12,
            最高深度 = 13
        }
        protected override void Init()
        {
            base.Init();
            子弹Port = GetPort("子弹");
        }
        public BulletData 子弹属性;
        public override object GetValue(NodePort port)
        {
            子弹 = 子弹Port.GetInputValue<Bullet>(); if (子弹 == null) { 结果 = -108; return 结果; }
            switch (子弹属性)
            {
                case BulletData.子弹运动速度:
                    结果 = 子弹.Speed;
                    break;
                case BulletData.子弹运动加速度:
                    结果 = 子弹.AcceleratedSpeed;
                    break;
                case BulletData.轨迹刷新速度:
                    结果 = 子弹.TrailUpdate;
                    break;
                case BulletData.子弹速度插值速度:
                    结果 = 子弹.ChangeSpeedPercentage;
                    break;
                case BulletData.子弹速度方向:
                    结果 = 子弹.Rotation;
                    break;
                case BulletData.子弹角速度:
                    结果 = 子弹.AcceleratedRotation;
                    break;
                case BulletData.角速度插值速度:
                    结果 = 子弹.ChangeRotationPercentage;
                    break;
                case BulletData.碰撞半径:
                    结果 = 子弹.Radius;
                    break;
                case BulletData.子弹使用寿命:
                    结果 = 子弹.MaxLiveFrame;
                    break;
                case BulletData.子弹年龄:
                    结果 = 子弹.TotalLiveFrame;
                    break;
                case BulletData.子弹朝向:
                    结果 = 子弹.InverseRotateDirection;
                    break;
                case BulletData.子弹曲线事件延迟:
                    结果 = 子弹.delayBulletEventRunTime;
                    break;
                case BulletData.最低深度:
                    结果 = 子弹.minDepth;
                    break;
                case BulletData.最高深度:
                    结果 = 子弹.maxDepth;
                    break;
                case BulletData.旋转坐标系度量值:
                    结果 = 子弹.RotatorInAsix;
                    break;
            }
            return 结果;
        }
    }
}