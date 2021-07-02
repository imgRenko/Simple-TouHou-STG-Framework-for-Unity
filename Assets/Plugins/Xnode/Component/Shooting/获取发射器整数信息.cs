using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.发射器
{
    public class 获取发射器整数信息 : Node
    {
        [Input] public Shooting 发射器;
        [Output] public int 结果;
        public enum ShootingEnumData
        {
            弹幕发射条数 = 0,
            额外发射次数 = 1,
            延迟发射时间 = 2,
            弹幕随机条数范围 = 3,
            把指定的条序号弹幕瞄准自机 = 4,
            子弹创建动画序号 = 5,
            子弹销毁动画序号 = 6,
            拖尾长度 = 7,
            子弹材质 = 8,
            发射器最大发射次数 = 9,
            子弹最大使用时间 = 10,
            子弹图像组序号 = 11,
            子弹序列帧动画播放间隔 = 12,
            播放声音间隔 = 13,
            贝塞尔曲线轨迹读取速度 = 14,
            子弹单批序号 = 15,
            发射器子弹序号 = 16,
            子弹批次 = 17
        }
        public ShootingEnumData 发射器属性;
        public override object GetValue(NodePort port)
        {
            发射器 = GetInputValue<Shooting>("发射器", null); if (发射器 == null) { 结果 = -108; return 结果; }
            switch (发射器属性)
            {
                case ShootingEnumData.弹幕发射条数:
                    结果 = 发射器.Way;
                    break;
                case ShootingEnumData.额外发射次数:
                    结果 = 发射器.SpecialBounsShoot;
                    break;
                case ShootingEnumData.延迟发射时间:
                    结果 = 发射器.Delay;
                    break;
                case ShootingEnumData.弹幕随机条数范围:
                    结果 = 发射器.WayOffset;
                    break;
                case ShootingEnumData.把指定的条序号弹幕瞄准自机:
                    结果 = 发射器.lineIndex;
                    break;
                case ShootingEnumData.子弹创建动画序号:
                    结果 = 发射器.CreateAnimationIndex;
                    break;
                case ShootingEnumData.子弹销毁动画序号:
                    结果 = 发射器.DestroyAnimationIndex;
                    break;
                case ShootingEnumData.拖尾长度:
                    结果 = 发射器.TrailLength;
                    break;
                case ShootingEnumData.子弹材质:
                    结果 = 发射器.MaterialIndex;
                    break;
                case ShootingEnumData.发射器最大发射次数:
                    结果 = 发射器.ShootingShotMaxTime;
                    break;
                case ShootingEnumData.子弹最大使用时间:
                    结果 = 发射器.MaxLiveFrame;
                    break;
                case ShootingEnumData.子弹图像组序号:
                    结果 = 发射器.SpriteIndex;
                    break;
                case ShootingEnumData.子弹序列帧动画播放间隔:
                    结果 = 发射器.WaitingTime;
                    break;
                case ShootingEnumData.播放声音间隔:
                    结果 = 发射器.IntervalSoundTime;
                    break;
                case ShootingEnumData.贝塞尔曲线轨迹读取速度:
                    结果 = 发射器.ReadPointTrackSpeed;
                    break;
                case ShootingEnumData.子弹单批序号:
                    结果 = 发射器.bulletIndexChecking;
                    break;
                case ShootingEnumData.发射器子弹序号:
                    结果 = 发射器.ShotIndex;
                    break;
                case ShootingEnumData.子弹批次:
                    结果 = 发射器.ReturnTotalShotBatch();
                    break;
            }
            return 结果;
        }
    }
}