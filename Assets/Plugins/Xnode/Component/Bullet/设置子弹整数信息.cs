using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.子弹
{
    public class 设置子弹整数信息 : Node
    {
        [Input] public FunctionProgress 进入节点;[Input] public int 目的值;[Input] public Bullet 子弹;
        [Output] public FunctionProgress 退出节点;
        public enum BulletData
        {
            轨迹长度 = 0,
            SpriteIndex = 1,
            子弹序号 = 2,
            序列帧动画切换速度 = 3,
            子弹实例ID = 4,
            子弹批次 = 5,
            读取贝塞尔曲线速度 = 6,
            发射器子弹序号 = 7,
            子弹材质序号 = 8
        }
        public BulletData 子弹属性;
        public override void FunctionDo(string PortName, List<object> param = null)
        {
            子弹 = GetInputValue<Bullet>("子弹"); if (子弹 == null) return; 目的值 = GetInputValue<int>("目的值", 目的值); switch (子弹属性)
            {
                case BulletData.轨迹长度:
                    子弹.TrailLength = 目的值;
                    break;
                case BulletData.SpriteIndex:
                    子弹.SpriteIndex = 目的值;
                    break;
                case BulletData.子弹序号:
                    子弹.BulletIndex = 目的值;
                    break;
                case BulletData.序列帧动画切换速度:
                    子弹.nextFramewaitTime = 目的值;
                    break;
                case BulletData.子弹实例ID:
                    子弹.ID = 目的值;
                    break;
                case BulletData.子弹批次:
                    子弹.BarrageBatch = 目的值;
                    break;
                case BulletData.读取贝塞尔曲线速度:
                    子弹.ReadPointTrackSpeed = 目的值;
                    break;
                case BulletData.发射器子弹序号:
                    子弹.shootingIndex = 目的值;
                    break;
                case BulletData.子弹材质序号:
                    子弹.SetMaterial(目的值);
                    break;

            }
            ConnectDo("退出节点");
        }
    }
}