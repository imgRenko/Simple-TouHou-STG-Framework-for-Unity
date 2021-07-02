using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.子弹
{
    public class 设置子弹浮点数信息 : Node
    {
        [Input] public FunctionProgress 进入节点;[Input] public float 目的值;[Input] public Bullet 子弹;
        [Output] public FunctionProgress 退出节点;
        NodePort 子弹Port, 目的值Port;
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
            子弹Port = GetPort("子弹");
            目的值Port = GetPort("目的值");
            base.Init();
        }
        public BulletData 子弹属性;
        public override void FunctionDo(string PortName, List<object> param = null)
        {
            子弹 = 子弹Port.GetInputValue<Bullet>(); if (子弹 == null) return; 目的值 = 目的值Port.GetInputValue<float>(); switch (子弹属性)
            {
                case BulletData.子弹运动速度:
                    子弹.Speed = 目的值;
                    break;
                case BulletData.子弹运动加速度:
                    子弹.AcceleratedSpeed = 目的值;
                    break;
                case BulletData.轨迹刷新速度:
                    子弹.TrailUpdate = 目的值;
                    break;
                case BulletData.子弹速度插值速度:
                    子弹.ChangeSpeedPercentage = 目的值;
                    break;
                case BulletData.子弹速度方向:
                    子弹.Rotation = 目的值;
                    break;
                case BulletData.子弹角速度:
                    子弹.AcceleratedRotation = 目的值;
                    break;
                case BulletData.角速度插值速度:
                    子弹.ChangeRotationPercentage = 目的值;
                    break;
                case BulletData.碰撞半径:
                    子弹.Radius = 目的值;
                    break;
                case BulletData.子弹使用寿命:
                    子弹.MaxLiveFrame = (int)目的值;
                    break;
                case BulletData.子弹年龄:
                    子弹.TotalLiveFrame = 目的值;
                    break;
                case BulletData.子弹朝向:
                    子弹.InverseRotateDirection = 目的值;
                    break;
                case BulletData.子弹曲线事件延迟:
                    子弹.delayBulletEventRunTime = 目的值;
                    break;
                case BulletData.最低深度:
                    子弹.minDepth = 目的值;
                    break;
                case BulletData.最高深度:
                    子弹.maxDepth = 目的值;
                    break;
                case BulletData.旋转坐标系度量值:
                    子弹.RotatorInAsix = 目的值;
                    break;



            }
            ConnectDo("退出节点");
        }
    }
}