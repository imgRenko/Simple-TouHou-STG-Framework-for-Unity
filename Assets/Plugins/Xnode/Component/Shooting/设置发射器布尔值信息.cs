using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.发射器
{
    public class 设置发射器布尔值信息 : Node
    {
        [Input] public FunctionProgress 进入节点;[Input] public bool 目的值;[Input] public Shooting 发射器;
        [Output] public FunctionProgress 退出节点;
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
            允许静态子弹动态缩放 =78, 允许静态子弹动态旋转 = 79,允许静态子弹使用动态动画=80, 保留触发器功能 = 81
        }
        public ShootingEnumData 发射器属性;
        public override void FunctionDo(string PortName, List<object> param = null)
        {
            发射器 = GetInputValue<Shooting>("发射器", null); if (发射器 == null) return; 目的值 = GetInputValue<bool>("目的值", 目的值); switch (发射器属性)
            {
                case ShootingEnumData.无强制发射要求不发射子弹:
                    发射器.Canceled = 目的值;
                    break;
                case ShootingEnumData.发射器播放时是否使用被播放事件:
                    发射器.UseShootingEvent = 目的值;
                    break;
                case ShootingEnumData.发射器被销毁时是否使用事件:
                    发射器.UseShootingDestroy = 目的值;
                    break;
                case ShootingEnumData.发射器使用时是否使用被使用事件:
                    发射器.UseShootingUsing = 目的值;
                    break;
                case ShootingEnumData.发射器完成全部发射任务时使用事件:
                    发射器.UseShootingFinishAllShotTask = 目的值;
                    break;
                case ShootingEnumData.应用子弹创建事件:
                    发射器.UseBulletEventWhenBulletCreate = 目的值;
                    break;
                case ShootingEnumData.应用子弹被销毁事件:
                    发射器.UseBulletEventWhenBulletDestroy = 目的值;
                    break;
                case ShootingEnumData.应用子弹恢复主层级事件:
                    发射器.UseBulletEventWhenBulletRestoreMainLevel = 目的值;
                    break;
                case ShootingEnumData.应用子弹击中玩家事件:
                    发射器.UseBulletEventOnDestroyingPlayer = 目的值;
                    break;
                case ShootingEnumData.应用子弹运动时事件:
                    发射器.UseBulletEvent = 目的值;
                    break;
                case ShootingEnumData.计算延迟时间到单位百分比:
                    发射器.CaluateDelayToPercent = 目的值;
                    break;
                case ShootingEnumData.忽略声明符卡时间继续发射子弹:
                    发射器.ignoreSCExpression = 目的值;
                    break;
                case ShootingEnumData.忽略剧情继续发射子弹:
                    发射器.IgnorePlot = 目的值;
                    break;
                case ShootingEnumData.除符卡被击破时消弹其余情况不消弹:
                    发射器.InvaildDestroy = 目的值;
                    break;
                case ShootingEnumData.使用协同事件:
                    发射器.UseThread = 目的值;
                    break;
                case ShootingEnumData.允许随机原点:
                    发射器.UseRandomOffset = 目的值;
                    break;
                case ShootingEnumData.允许使用椭圆:
                    发射器.useEllipse = 目的值;
                    break;
                case ShootingEnumData.保持椭圆规整:
                    发射器.keepRegularEllipse = 目的值;
                    break;
                case ShootingEnumData.允许圆形弹幕使用随机半径参数:
                    发射器.UseRandomRadius = 目的值;
                    break;
                case ShootingEnumData.允许发射器进行移动:
                    发射器.MoveShooting = 目的值;
                    break;
                case ShootingEnumData.CanShoot:
                    发射器.CanShoot = 目的值;
                    break;
                case ShootingEnumData.允许弹幕以随机条数发射:
                    发射器.UseRandomWay = 目的值;
                    break;
                case ShootingEnumData.允许随机发射角度:
                    发射器.UseRandomAngle = 目的值;
                    break;
                case ShootingEnumData.允许发射器复用:
                    发射器.Reusable = 目的值;
                    break;
                case ShootingEnumData.自机狙子弹:
                    发射器.FollowPlayer = 目的值;
                    break;
                case ShootingEnumData.把第一条弹幕瞄准自机:
                    发射器.firstLine = 目的值;
                    break;
                case ShootingEnumData.把最中央的一条弹幕瞄准自机:
                    发射器.middleIndex = 目的值;
                    break;
                case ShootingEnumData.DisallowInverse:
                    发射器.DisallowInverse = 目的值;
                    break;
                case ShootingEnumData.允许平滑改变子弹速度方向:
                    发射器.ChangeBulletRotationSmoothly = 目的值;
                    break;
                case ShootingEnumData.子弹朝向不受速度方向影响:
                    发射器.ChangeInverseRotation = 目的值;
                    break;
                case ShootingEnumData.子弹速度方向始终朝向玩家:
                    发射器.AimToPlayer = 目的值;
                    break;
                case ShootingEnumData.允许随机子弹朝向:
                    发射器.UseRandomBulletRotation = 目的值;
                    break;
                case ShootingEnumData.允许平滑子弹速度到某一个值:
                    发射器.ChangeSpeedSmoothly = 目的值;
                    break;
                case ShootingEnumData.子弹随机速度:
                    发射器.RandomSpeed = 目的值;
                    break;
                case ShootingEnumData.单路子弹随机速度:
                    发射器.SingleWayRandomSpeed = 目的值;
                    break;
                case ShootingEnumData.允许使用子弹随机加速度:
                    发射器.RandomAcceleratedSpeed = 目的值;
                    break;
                case ShootingEnumData.允许动态碰撞盒:
                    发射器.UseDynamicsCollision = 目的值;
                    break;
                case ShootingEnumData.播放子弹创建动画时无碰撞:
                    发射器.NoCollisionWhenCreateAnimationPlaying = 目的值;
                    break;
                case ShootingEnumData.透明度过低不使用子弹碰撞:
                    发射器.NoCollisionWhenAlphaLow = 目的值;
                    break;
                case ShootingEnumData.允许自定义碰撞盒:
                    发射器.UseCustomCollisionGroup = 目的值;
                    break;
                case ShootingEnumData.子弹透明度随深度变化:
                    发射器.UseAlphaWithDepth = 目的值;
                    break;
                case ShootingEnumData.不播放子弹创建动画:
                    发射器.NoCreateAnimation = 目的值;
                    break;
                case ShootingEnumData.不播放子弹销毁动画:
                    发射器.NoDestroyAnimation = 目的值;
                    break;
                case ShootingEnumData.允许子弹扭曲旋转:
                    发射器.TwistedRotation = 目的值;
                    break;
                case ShootingEnumData.允许子弹朝向不受速度方向影响:
                    发射器.InverseRotation = 目的值;
                    break;
                case ShootingEnumData.允许拖尾效果:
                    发射器.useTrails = 目的值;
                    break;

                case ShootingEnumData.携带子弹作为子级子弹跟随父级子弹:
                    发射器.MakeSubBullet = 目的值;
                    break;
                case ShootingEnumData.父级子弹销毁不销毁子级子弹:
                    发射器.DonDestroyIfMasterDestroy = 目的值;
                    break;
                case ShootingEnumData.减少子弹销毁可能性:
                    发射器.DonDestroy = 目的值;
                    break;
                case ShootingEnumData.允许自定义子弹外观:
                    发射器.UseCustomSprite = 目的值;
                    break;
                case ShootingEnumData.允许使用子弹序列帧动画:
                    发射器.UseBulletAnimation = 目的值;
                    break;
                case ShootingEnumData.使用默认集合:
                    发射器.useDefaultSprite = 目的值;
                    break;
                case ShootingEnumData.启用子弹的全局偏移:
                    发射器.EnabledGlobalOffset = 目的值;
                    break;
                case ShootingEnumData.启用子弹全局加速偏移:
                    发射器.EnabledAcceleratedGlobalOffset = 目的值;
                    break;
                case ShootingEnumData.出屏销毁子弹:
                    发射器.DestroyWhenDonRender = 目的值;
                    break;
                case ShootingEnumData.允许给子弹使用力场:
                    发射器.EnableForForce = 目的值;
                    break;
                case ShootingEnumData.共享发射器:
                    发射器.Share = 目的值;
                    break;
                case ShootingEnumData.符卡被击破时不关闭发射器:
                    发射器.Continue = 目的值;
                    break;
                case ShootingEnumData.使发射的子弹应用触发器:
                    发射器.ApplyTrigger = 目的值;
                    break;
                case ShootingEnumData.允许创建子弹时避开玩家:
                    发射器.AvoidPlayer = 目的值;
                    break;
                case ShootingEnumData.已经实现擦弹:
                    发射器.Graze = 目的值;
                    break;
                case ShootingEnumData.允许发射器回滚:
                    发射器.rollBack = 目的值;
                    break;
                case ShootingEnumData.作为敌人发射器:
                    发射器.isEnemyShooting = 目的值;
                    break;
                case ShootingEnumData.随对象旋转度调整发射角度:
                    发射器.followRotation = 目的值;
                    break;
                case ShootingEnumData.DefaultEnabled:
                    发射器.DefaultEnabled = 目的值;
                    break;
                case ShootingEnumData.播放声音:
                    发射器.PlayerSound = 目的值;
                    break;
                case ShootingEnumData.贝塞尔曲线轨迹循环:
                    发射器.LoopTrack = 目的值;
                    break;
                case ShootingEnumData.计算贝塞尔曲线切线:
                    发射器.CalculateAngle = 目的值;
                    break;
                case ShootingEnumData.子弹图像X轴反转:
                    发射器.FilpX = 目的值;
                    break;
                case ShootingEnumData.子弹图像Y轴反转:
                    发射器.FilpY = 目的值;
                    break;
                case ShootingEnumData.ShotAtPlayer:
                    发射器.ShotAtPlayer = 目的值;
                    break;
                case ShootingEnumData.子弹携带对象需要旋转坐标系:
                    发射器.FollowObjectWithSameAngle = 目的值;
                    break;
                case ShootingEnumData.启用随机发射周期:
                    发射器.EnableRandomTimer = 目的值;
                    break;
                case ShootingEnumData.ShootSubBullet:
                    发射器.ShootSubBullet = 目的值;
                    break;
                case ShootingEnumData.静态子弹:
                    发射器.StaticBullet = 目的值;
                    break;
                case ShootingEnumData.静态子弹下保留碰撞:
                    发射器.StayCollision = 目的值;
                    break;
                case ShootingEnumData.允许最低限度子弹事件:
                    发射器.StayBulletEvent = 目的值;
                    break;
                case ShootingEnumData.允许静态子弹使用动态动画:
                    发射器.StayAnimPlayer = 目的值;
                    break;
                case ShootingEnumData.允许静态子弹动态缩放:
                    发射器.ScaleUpdate = 目的值;
                    break;
                case ShootingEnumData.允许静态子弹动态旋转:
                    发射器.RotationUpdate = 目的值;
                    break;
                case ShootingEnumData.保留触发器功能:
                    发射器.StayTrigger = 目的值;
                    break;

            }
            ConnectDo("退出节点");
        }
    }
}