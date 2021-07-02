using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.子弹
{
    public class 设置子弹布尔值信息 : Node
    {
        [Input] public FunctionProgress 进入节点;[Input] public bool 目的值;[Input] public Bullet 子弹;
        [Output] public FunctionProgress 退出节点;
        public enum BulletData
        {
            作为敌人移动器使用 = 0,
            使用协程事件 = 1,
            可复用子弹 = 2,
            减低子弹销毁可能性 = 3,
            涉及全局速度 = 4,
            于特别情况暂停运动 = 5,
            平滑改变子弹速度 = 6,
            平滑改变角速度 = 7,
            已经擦弹 = 8,
            时刻瞄准玩家 = 9,
            不进行销毁 = 10,
            使用轨迹 = 11,
            使用子弹 = 12,
            UseCollision = 13,
            UseCustomCollisionGroup = 14,
            TwistedRotation = 15,
            InverseRotation = 16,
            DestroyWhenDontRender = 17,
            NoDestroyAnimation = 18,
            NoCollisionWhenCreateAnimationPlaying = 19,
            NoCollisionWhenAlphaLow = 20,
            UseCustomAnimation = 21,
            EnabledAcceleratedGlobalOffset = 22,
            EnabledGlobalOffset = 23,
            ChangeInverseRotation = 24,
            DonDestroyIfMasterDestroy = 25,
            UseForce = 26,
            UseAlphaWithDepth = 27,
            CreateAnimationPlayed = 28,
            FilpX = 29,
            FilpY = 30,
            AsSubBullet = 31,
            DestroyMode = 32,
            FollowObjectWithSameAngle = 33,
            trackLerpBegin = 34,
            EnableBulletEvent = 35,
            EnableFollow = 36,
            EnableTrail = 37,
            静态子弹 = 38,
            允许最低限度子弹事件 = 39,
            静态子弹下保留碰撞 = 40, 保留缩放行为 = 41, 保留旋转行为 = 42, 保留动画更新行为 = 43,保留触发器行为=44

        }
        public BulletData 子弹属性;
        public override void FunctionDo(string PortName, List<object> param = null)
        {
            子弹 = GetInputValue<Bullet>("子弹"); if (子弹 == null) return; 目的值 = GetInputValue<bool>("目的值", 目的值); switch (子弹属性)
            {
                case BulletData.作为敌人移动器使用:
                    子弹.AsEnemyMovement = 目的值;
                    break;
                case BulletData.使用协程事件:
                    子弹.UseThread = 目的值;
                    break;
                case BulletData.可复用子弹:
                    子弹.Reusable = 目的值;
                    break;
                case BulletData.减低子弹销毁可能性:
                    子弹.noDestroy = 目的值;
                    break;
                case BulletData.涉及全局速度:
                    子弹.UseGlobalSpeed = 目的值;
                    break;
                case BulletData.于特别情况暂停运动:
                    子弹.StopOnSpecialSituation = 目的值;
                    break;
                case BulletData.平滑改变子弹速度:
                    子弹.ChangeSpeedSmoothly = 目的值;
                    break;
                case BulletData.平滑改变角速度:
                    子弹.ChangeRotationSmoothly = 目的值;
                    break;
                case BulletData.已经擦弹:
                    子弹.Grazed = 目的值;
                    break;
                case BulletData.时刻瞄准玩家:
                    子弹.AimToPlayer = 目的值;
                    break;
                case BulletData.不进行销毁:
                    子弹.DonDestroy = 目的值;
                    break;
                case BulletData.使用轨迹:
                    子弹.showTrails = 目的值;
                    break;
                case BulletData.使用子弹:
                    子弹.Use = 目的值;
                    break;
                case BulletData.UseCollision:
                    子弹.UseCollision = 目的值;
                    break;
                case BulletData.UseCustomCollisionGroup:
                    子弹.UseCustomCollisionGroup = 目的值;
                    break;
                case BulletData.TwistedRotation:
                    子弹.TwistedRotation = 目的值;
                    break;
                case BulletData.InverseRotation:
                    子弹.InverseRotation = 目的值;
                    break;
                case BulletData.DestroyWhenDontRender:
                    子弹.DestroyWhenDontRender = 目的值;
                    break;
                case BulletData.NoDestroyAnimation:
                    子弹.NoDestroyAnimation = 目的值;
                    break;
                case BulletData.NoCollisionWhenCreateAnimationPlaying:
                    子弹.NoCollisionWhenCreateAnimationPlaying = 目的值;
                    break;
                case BulletData.NoCollisionWhenAlphaLow:
                    子弹.NoCollisionWhenAlphaLow = 目的值;
                    break;
                case BulletData.UseCustomAnimation:
                    子弹.UseCustomAnimation = 目的值;
                    break;
                case BulletData.EnabledAcceleratedGlobalOffset:
                    子弹.EnabledAcceleratedGlobalOffset = 目的值;
                    break;
                case BulletData.EnabledGlobalOffset:
                    子弹.EnabledGlobalOffset = 目的值;
                    break;
                case BulletData.ChangeInverseRotation:
                    子弹.ChangeInverseRotation = 目的值;
                    break;
                case BulletData.DonDestroyIfMasterDestroy:
                    子弹.DonDestroyIfMasterDestroy = 目的值;
                    break;
                case BulletData.UseForce:
                    子弹.UseForce = 目的值;
                    break;
                case BulletData.UseAlphaWithDepth:
                    子弹.UseAlphaWithDepth = 目的值;
                    break;
                case BulletData.CreateAnimationPlayed:
                    子弹.CreateAnimationPlayed = 目的值;
                    break;
                case BulletData.FilpX:
                    子弹.FilpX = 目的值;
                    break;
                case BulletData.FilpY:
                    子弹.FilpY = 目的值;
                    break;
                case BulletData.AsSubBullet:
                    子弹.AsSubBullet = 目的值;
                    break;
                case BulletData.DestroyMode:
                    子弹.DestroyMode = 目的值;
                    break;
                case BulletData.FollowObjectWithSameAngle:
                    子弹.FollowObjectWithSameAngle = 目的值;
                    break;
                case BulletData.trackLerpBegin:
                    子弹.trackLerpBegin = 目的值;
                    break;
                case BulletData.EnableBulletEvent:
                    子弹.EnableBulletEvent = 目的值;
                    break;
                case BulletData.EnableTrail:
                    子弹.EnableTrail = 目的值;
                    break;
                case BulletData.静态子弹:
                    子弹.StaticBullet = 目的值;
                    break;
                case BulletData.静态子弹下保留碰撞:
                    子弹.StayBulletEvent = 目的值;
                    break;
                case BulletData.允许最低限度子弹事件:
                    子弹.StayCollision = 目的值;
                    break;
                case BulletData.保留动画更新行为:
                     子弹.StayAnimPlayer = 目的值;
                    break;
                case BulletData.保留旋转行为:
                     子弹.RotationUpdate = 目的值;
                    break;
                case BulletData.保留缩放行为:
                     子弹.ScaleUpdate = 目的值;
                    break;
                case BulletData.保留触发器行为:
                     子弹.StayTrigger = 目的值;
                    break;

            }
            ConnectDo("退出节点");
        }
    }
}