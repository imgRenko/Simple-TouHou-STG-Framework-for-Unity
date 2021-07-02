using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.发射器
{
    public class 获取发射器布尔值信息 : Node
    {
        [Input] public Shooting 发射器;
        [Output] public bool 结果;
        public enum ShootingEnumData
        {
            无强制发射要求不发射子弹 = 0,
            发射器播放时是否使用被播放事件 = 1,
            发射器被销毁时是否使用事件 = 2,
            发射器使用时是否使用被使用事件 = 3,
            发射器完成全部发射任务时使用事件 = 4,
            应用子弹创建事件 = 5,
            应用子弹被销毁事件 = 6,
            应用子弹恢复主层级事件 = 7,
            应用子弹击中玩家事件 = 8,
            应用子弹运动时事件 = 9,
            计算延迟时间到单位百分比 = 10,
            忽略声明符卡时间继续发射子弹 = 11,
            忽略剧情继续发射子弹 = 12,
            除符卡被击破时消弹其余情况不消弹 = 13,
            使用协同事件 = 14,
            允许随机原点 = 15,
            允许使用椭圆 = 16,
            保持椭圆规整 = 17,
            允许圆形弹幕使用随机半径参数 = 18,
            允许发射器进行移动 = 19,
            CanShoot = 20,
            允许弹幕以随机条数发射 = 21,
            允许随机发射角度 = 22,
            允许发射器复用 = 23,
            自机狙子弹 = 24,
            把第一条弹幕瞄准自机 = 25,
            把最中央的一条弹幕瞄准自机 = 26,
            DisallowInverse = 27,
            允许平滑改变子弹速度方向 = 28,
            子弹朝向不受速度方向影响 = 29,
            子弹速度方向始终朝向玩家 = 30,
            允许随机子弹朝向 = 31,
            允许平滑子弹速度到某一个值 = 32,
            子弹随机速度 = 33,
            单路子弹随机速度 = 34,
            允许使用子弹随机加速度 = 35,
            允许动态碰撞盒 = 36,
            播放子弹创建动画时无碰撞 = 37,
            透明度过低不使用子弹碰撞 = 38,
            允许自定义碰撞盒 = 39,
            子弹透明度随深度变化 = 40,
            不播放子弹创建动画 = 41,
            不播放子弹销毁动画 = 42,
            允许子弹扭曲旋转 = 43,
            允许子弹朝向不受速度方向影响 = 44,
            允许拖尾效果 = 45,
            携带子弹作为子级子弹跟随父级子弹 = 47,
            父级子弹销毁不销毁子级子弹 = 48,
            减少子弹销毁可能性 = 49,
            允许自定义子弹外观 = 50,
            允许使用子弹序列帧动画 = 51,
            使用默认集合 = 52,
            启用子弹的全局偏移 = 53,
            启用子弹全局加速偏移 = 54,
            出屏销毁子弹 = 55,
            允许给子弹使用力场 = 56,
            共享发射器 = 57,
            符卡被击破时不关闭发射器 = 58,
            使发射的子弹应用触发器 = 59,
            允许创建子弹时避开玩家 = 60,
            已经实现擦弹 = 61,
            允许发射器回滚 = 62,
            作为敌人发射器 = 63,
            随对象旋转度调整发射角度 = 64,
            DefaultEnabled = 65,
            播放声音 = 66,
            贝塞尔曲线轨迹循环 = 67,
            计算贝塞尔曲线切线 = 68,
            子弹图像X轴反转 = 69,
            子弹图像Y轴反转 = 70,
            ShotAtPlayer = 71,
            子弹携带对象需要旋转坐标系 = 72,
            启用随机发射周期 = 73,
            ShootSubBullet = 74,
            静态子弹 = 75,
            允许最低限度子弹事件 = 76,
            静态子弹下保留碰撞 = 77,
            允许静态子弹动态缩放 = 78, 允许静态子弹动态旋转 = 79, 允许静态子弹使用动态动画 = 80,保留触发器功能=81
        }
        public ShootingEnumData 发射器属性;
        public override object GetValue(NodePort port)
        {
            发射器 = GetInputValue<Shooting>("发射器", null); if (发射器 == null) { return 0; }
            switch (发射器属性)
            {
                case ShootingEnumData.无强制发射要求不发射子弹:
                    结果 = 发射器.Canceled;
                    break;
                case ShootingEnumData.发射器播放时是否使用被播放事件:
                    结果 = 发射器.UseShootingEvent;
                    break;
                case ShootingEnumData.发射器被销毁时是否使用事件:
                    结果 = 发射器.UseShootingDestroy;
                    break;
                case ShootingEnumData.发射器使用时是否使用被使用事件:
                    结果 = 发射器.UseShootingUsing;
                    break;
                case ShootingEnumData.发射器完成全部发射任务时使用事件:
                    结果 = 发射器.UseShootingFinishAllShotTask;
                    break;
                case ShootingEnumData.应用子弹创建事件:
                    结果 = 发射器.UseBulletEventWhenBulletCreate;
                    break;
                case ShootingEnumData.应用子弹被销毁事件:
                    结果 = 发射器.UseBulletEventWhenBulletDestroy;
                    break;
                case ShootingEnumData.应用子弹恢复主层级事件:
                    结果 = 发射器.UseBulletEventWhenBulletRestoreMainLevel;
                    break;
                case ShootingEnumData.应用子弹击中玩家事件:
                    结果 = 发射器.UseBulletEventOnDestroyingPlayer;
                    break;
                case ShootingEnumData.应用子弹运动时事件:
                    结果 = 发射器.UseBulletEvent;
                    break;
                case ShootingEnumData.计算延迟时间到单位百分比:
                    结果 = 发射器.CaluateDelayToPercent;
                    break;
                case ShootingEnumData.忽略声明符卡时间继续发射子弹:
                    结果 = 发射器.ignoreSCExpression;
                    break;
                case ShootingEnumData.忽略剧情继续发射子弹:
                    结果 = 发射器.IgnorePlot;
                    break;
                case ShootingEnumData.除符卡被击破时消弹其余情况不消弹:
                    结果 = 发射器.InvaildDestroy;
                    break;
                case ShootingEnumData.使用协同事件:
                    结果 = 发射器.UseThread;
                    break;
                case ShootingEnumData.允许随机原点:
                    结果 = 发射器.UseRandomOffset;
                    break;
                case ShootingEnumData.允许使用椭圆:
                    结果 = 发射器.useEllipse;
                    break;
                case ShootingEnumData.保持椭圆规整:
                    结果 = 发射器.keepRegularEllipse;
                    break;
                case ShootingEnumData.允许圆形弹幕使用随机半径参数:
                    结果 = 发射器.UseRandomRadius;
                    break;
                case ShootingEnumData.允许发射器进行移动:
                    结果 = 发射器.MoveShooting;
                    break;
                case ShootingEnumData.CanShoot:
                    结果 = 发射器.CanShoot;
                    break;
                case ShootingEnumData.允许弹幕以随机条数发射:
                    结果 = 发射器.UseRandomWay;
                    break;
                case ShootingEnumData.允许随机发射角度:
                    结果 = 发射器.UseRandomAngle;
                    break;
                case ShootingEnumData.允许发射器复用:
                    结果 = 发射器.Reusable;
                    break;
                case ShootingEnumData.自机狙子弹:
                    结果 = 发射器.FollowPlayer;
                    break;
                case ShootingEnumData.把第一条弹幕瞄准自机:
                    结果 = 发射器.firstLine;
                    break;
                case ShootingEnumData.把最中央的一条弹幕瞄准自机:
                    结果 = 发射器.middleIndex;
                    break;
                case ShootingEnumData.DisallowInverse:
                    结果 = 发射器.DisallowInverse;
                    break;
                case ShootingEnumData.允许平滑改变子弹速度方向:
                    结果 = 发射器.ChangeBulletRotationSmoothly;
                    break;
                case ShootingEnumData.子弹朝向不受速度方向影响:
                    结果 = 发射器.ChangeInverseRotation;
                    break;
                case ShootingEnumData.子弹速度方向始终朝向玩家:
                    结果 = 发射器.AimToPlayer;
                    break;
                case ShootingEnumData.允许随机子弹朝向:
                    结果 = 发射器.UseRandomBulletRotation;
                    break;
                case ShootingEnumData.允许平滑子弹速度到某一个值:
                    结果 = 发射器.ChangeSpeedSmoothly;
                    break;
                case ShootingEnumData.子弹随机速度:
                    结果 = 发射器.RandomSpeed;
                    break;
                case ShootingEnumData.单路子弹随机速度:
                    结果 = 发射器.SingleWayRandomSpeed;
                    break;
                case ShootingEnumData.允许使用子弹随机加速度:
                    结果 = 发射器.RandomAcceleratedSpeed;
                    break;
                case ShootingEnumData.允许动态碰撞盒:
                    结果 = 发射器.UseDynamicsCollision;
                    break;
                case ShootingEnumData.播放子弹创建动画时无碰撞:
                    结果 = 发射器.NoCollisionWhenCreateAnimationPlaying;
                    break;
                case ShootingEnumData.透明度过低不使用子弹碰撞:
                    结果 = 发射器.NoCollisionWhenAlphaLow;
                    break;
                case ShootingEnumData.允许自定义碰撞盒:
                    结果 = 发射器.UseCustomCollisionGroup;
                    break;
                case ShootingEnumData.子弹透明度随深度变化:
                    结果 = 发射器.UseAlphaWithDepth;
                    break;
                case ShootingEnumData.不播放子弹创建动画:
                    结果 = 发射器.NoCreateAnimation;
                    break;
                case ShootingEnumData.不播放子弹销毁动画:
                    结果 = 发射器.NoDestroyAnimation;
                    break;
                case ShootingEnumData.允许子弹扭曲旋转:
                    结果 = 发射器.TwistedRotation;
                    break;
                case ShootingEnumData.允许子弹朝向不受速度方向影响:
                    结果 = 发射器.InverseRotation;
                    break;
                case ShootingEnumData.允许拖尾效果:
                    结果 = 发射器.useTrails;
                    break;

                case ShootingEnumData.携带子弹作为子级子弹跟随父级子弹:
                    结果 = 发射器.MakeSubBullet;
                    break;
                case ShootingEnumData.父级子弹销毁不销毁子级子弹:
                    结果 = 发射器.DonDestroyIfMasterDestroy;
                    break;
                case ShootingEnumData.减少子弹销毁可能性:
                    结果 = 发射器.DonDestroy;
                    break;
                case ShootingEnumData.允许自定义子弹外观:
                    结果 = 发射器.UseCustomSprite;
                    break;
                case ShootingEnumData.允许使用子弹序列帧动画:
                    结果 = 发射器.UseBulletAnimation;
                    break;
                case ShootingEnumData.使用默认集合:
                    结果 = 发射器.useDefaultSprite;
                    break;
                case ShootingEnumData.启用子弹的全局偏移:
                    结果 = 发射器.EnabledGlobalOffset;
                    break;
                case ShootingEnumData.启用子弹全局加速偏移:
                    结果 = 发射器.EnabledAcceleratedGlobalOffset;
                    break;
                case ShootingEnumData.出屏销毁子弹:
                    结果 = 发射器.DestroyWhenDonRender;
                    break;
                case ShootingEnumData.允许给子弹使用力场:
                    结果 = 发射器.EnableForForce;
                    break;
                case ShootingEnumData.共享发射器:
                    结果 = 发射器.Share;
                    break;
                case ShootingEnumData.符卡被击破时不关闭发射器:
                    结果 = 发射器.Continue;
                    break;
                case ShootingEnumData.使发射的子弹应用触发器:
                    结果 = 发射器.ApplyTrigger;
                    break;
                case ShootingEnumData.允许创建子弹时避开玩家:
                    结果 = 发射器.AvoidPlayer;
                    break;
                case ShootingEnumData.已经实现擦弹:
                    结果 = 发射器.Graze;
                    break;
                case ShootingEnumData.允许发射器回滚:
                    结果 = 发射器.rollBack;
                    break;
                case ShootingEnumData.作为敌人发射器:
                    结果 = 发射器.isEnemyShooting;
                    break;
                case ShootingEnumData.随对象旋转度调整发射角度:
                    结果 = 发射器.followRotation;
                    break;
                case ShootingEnumData.DefaultEnabled:
                    结果 = 发射器.DefaultEnabled;
                    break;
                case ShootingEnumData.播放声音:
                    结果 = 发射器.PlayerSound;
                    break;
                case ShootingEnumData.贝塞尔曲线轨迹循环:
                    结果 = 发射器.LoopTrack;
                    break;
                case ShootingEnumData.计算贝塞尔曲线切线:
                    结果 = 发射器.CalculateAngle;
                    break;
                case ShootingEnumData.子弹图像X轴反转:
                    结果 = 发射器.FilpX;
                    break;
                case ShootingEnumData.子弹图像Y轴反转:
                    结果 = 发射器.FilpY;
                    break;
                case ShootingEnumData.ShotAtPlayer:
                    结果 = 发射器.ShotAtPlayer;
                    break;
                case ShootingEnumData.子弹携带对象需要旋转坐标系:
                    结果 = 发射器.FollowObjectWithSameAngle;
                    break;
                case ShootingEnumData.启用随机发射周期:
                    结果 = 发射器.EnableRandomTimer;
                    break;
                case ShootingEnumData.ShootSubBullet:
                    结果 = 发射器.ShootSubBullet;
                    break;

                case ShootingEnumData.静态子弹:
                    结果 = 发射器.StaticBullet;
                    break;
                case ShootingEnumData.静态子弹下保留碰撞:
                    结果 = 发射器.StayCollision;
                    break;
                case ShootingEnumData.允许最低限度子弹事件:
                    结果 = 发射器.StayBulletEvent;
                    break;
                case ShootingEnumData.允许静态子弹使用动态动画:
                    结果 = 发射器.StayAnimPlayer ;
                    break;
                case ShootingEnumData.允许静态子弹动态缩放:
                    结果 = 发射器.ScaleUpdate ;
                    break;
                case ShootingEnumData.允许静态子弹动态旋转:
                    结果 = 发射器.RotationUpdate ;
                    break;
                case ShootingEnumData.保留触发器功能:
                    结果 = 发射器.StayTrigger;
                    break;
            }
            return 结果;
        }
    }
}