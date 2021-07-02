using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.子弹
{
    public class 获取子弹浮点属性Node : Node
    {
        [Input] public Bullet 子弹;

        public enum BulletFloat
        {
            子弹速度 = 0,
            子弹加速度 = 1,
            子弹速度方向 = 2,
            子弹角速度 = 3,
            子弹年龄 = 4,
            子弹寿命 = 5,
            子弹加速度方向 = 6,
            子弹批次 = 7,
            子弹序号 = 8,
            单批次子弹序号 = 9,
            子弹旋转方向 = 10

        }

        public BulletFloat 子弹属性;
        [Output] public float 结果;

        // Use this for initialization
        protected override void Init()
        {
            base.Init();

        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            子弹 = GetInputValue<Bullet>("子弹");
            if (子弹 == null)
            {
                结果 = -108;
                return 结果;

            }

            switch (子弹属性)
            {
                case BulletFloat.单批次子弹序号:
                    结果 = 子弹.BulletIndex;
                    break;
                case BulletFloat.子弹年龄:
                    结果 = 子弹.TotalLiveFrame;
                    break;
                case BulletFloat.子弹加速度:
                    结果 = 子弹.AcceleratedSpeed;
                    break;
                case BulletFloat.子弹加速度方向:
                    结果 = 子弹.AcceleratedRotation;
                    break;
                case BulletFloat.子弹寿命:
                    结果 = 子弹.MaxLiveFrame;
                    break;
                case BulletFloat.子弹序号:
                    结果 = 子弹.shootingIndex;
                    break;
                case BulletFloat.子弹批次:
                    结果 = 子弹.BarrageBatch;
                    break;
                case BulletFloat.子弹旋转方向:
                    结果 = 子弹.InverseRotateDirection;
                    break;
                case BulletFloat.子弹角速度:
                    结果 = 子弹.AcceleratedRotation;
                    break;
                case BulletFloat.子弹速度:
                    结果 = 子弹.Speed;
                    break;
                case BulletFloat.子弹速度方向:
                    结果 = 子弹.Rotation;
                    break;
            }

            return 结果;

        }
    }
}