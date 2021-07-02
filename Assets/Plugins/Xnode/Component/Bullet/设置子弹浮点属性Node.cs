using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.子弹
{
    public class 设置子弹浮点属性Node : Node
    {

        [Input] public FunctionProgress 进入节点;
        [Input] public float 目的值;
        [Input] public Bullet 子弹;

        public 获取子弹浮点属性Node.BulletFloat 子弹属性;
        [Output] public FunctionProgress 退出节点;

        // Use this for initialization
        protected override void Init()
        {
            base.Init();

        }
        public override void FunctionDo(string PortName,List<object> param = null) 
        {
            子弹 = GetInputValue<Bullet>("子弹");
            if (子弹 == null)
                return;
            目的值 = GetInputValue<float>("目的值", 0);
            switch (子弹属性)
            {
                case 获取子弹浮点属性Node.BulletFloat.单批次子弹序号:
                    子弹.BulletIndex = (int)目的值;
                    break;
                case 获取子弹浮点属性Node.BulletFloat.子弹年龄:
                    子弹.TotalLiveFrame = 目的值;
                    break;
                case 获取子弹浮点属性Node.BulletFloat.子弹加速度:
                    子弹.AcceleratedSpeed = 目的值;
                    break;
                case 获取子弹浮点属性Node.BulletFloat.子弹加速度方向:
                    子弹.AcceleratedRotation = 目的值;
                    break;
                case 获取子弹浮点属性Node.BulletFloat.子弹寿命:
                    子弹.MaxLiveFrame = (int)目的值;
                    break;
                case 获取子弹浮点属性Node.BulletFloat.子弹序号:
                    子弹.shootingIndex = (int)目的值;
                    break;
                case 获取子弹浮点属性Node.BulletFloat.子弹批次:
                    子弹.BarrageBatch = (int)目的值;
                    break;
                case 获取子弹浮点属性Node.BulletFloat.子弹旋转方向:
                    子弹.InverseRotateDirection = 目的值;
                    break;
                case 获取子弹浮点属性Node.BulletFloat.子弹角速度:
                    子弹.AcceleratedRotation = 目的值;
                    break;
                case 获取子弹浮点属性Node.BulletFloat.子弹速度:
                    子弹.Speed = 目的值;
                    break;
                case 获取子弹浮点属性Node.BulletFloat.子弹速度方向:
                    子弹.Rotation = 目的值;
                    break;
            }
            ConnectDo("退出节点");
        }
        // Return the correct value of an output port when requested
    }
}