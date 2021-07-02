using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode;
namespace 基础事件.子弹
{
    public class 获取子弹布尔值信息 : Node
    {
        [Input] public Bullet 子弹;
        [Output] public bool 结果;
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
            静态子弹下保留碰撞 = 40, 保留缩放行为 = 41, 保留旋转行为 = 42, 保留动画更新行为 = 43, 保留触发器行为 = 44

        }
        public BulletData 子弹属性;
        public override object GetValue(NodePort port)
        {
            子弹 = GetInputValue<Bullet>("子弹"); if (子弹 == null) { return 0; }
            switch (子弹属性)
            {
                case BulletData.作为敌人移动器使用:
                    结果 = 子弹.AsEnemyMovement;
                    break;
                case BulletData.使用协程事件:
                    结果 = 子弹.UseThread;
                    break;
                case BulletData.可复用子弹:
                    结果 = 子弹.Reusable;
                    break;
                case BulletData.减低子弹销毁可能性:
                    结果 = 子弹.noDestroy;
                    break;
                case BulletData.涉及全局速度:
                    结果 = 子弹.UseGlobalSpeed;
                    break;
                case BulletData.于特别情况暂停运动:
                    结果 = 子弹.StopOnSpecialSituation;
                    break;
                case BulletData.平滑改变子弹速度:
                    结果 = 子弹.ChangeSpeedSmoothly;
                    break;
                case BulletData.平滑改变角速度:
                    结果 = 子弹.ChangeRotationSmoothly;
                    break;
                case BulletData.已经擦弹:
                    结果 = 子弹.Grazed;
                    break;
                case BulletData.时刻瞄准玩家:
                    结果 = 子弹.AimToPlayer;
                    break;
                case BulletData.不进行销毁:
                    结果 = 子弹.DonDestroy;
                    break;
                case BulletData.使用轨迹:
                    结果 = 子弹.showTrails;
                    break;
                case BulletData.使用子弹:
                    结果 = 子弹.Use;
                    break;
                case BulletData.UseCollision:
                    结果 = 子弹.UseCollision;
                    break;
                case BulletData.UseCustomCollisionGroup:
                    结果 = 子弹.UseCustomCollisionGroup;
                    break;
                case BulletData.TwistedRotation:
                    结果 = 子弹.TwistedRotation;
                    break;
                case BulletData.InverseRotation:
                    结果 = 子弹.InverseRotation;
                    break;
                case BulletData.DestroyWhenDontRender:
                    结果 = 子弹.DestroyWhenDontRender;
                    break;
                case BulletData.NoDestroyAnimation:
                    结果 = 子弹.NoDestroyAnimation;
                    break;
                case BulletData.NoCollisionWhenCreateAnimationPlaying:
                    结果 = 子弹.NoCollisionWhenCreateAnimationPlaying;
                    break;
                case BulletData.NoCollisionWhenAlphaLow:
                    结果 = 子弹.NoCollisionWhenAlphaLow;
                    break;
                case BulletData.UseCustomAnimation:
                    结果 = 子弹.UseCustomAnimation;
                    break;
                case BulletData.EnabledAcceleratedGlobalOffset:
                    结果 = 子弹.EnabledAcceleratedGlobalOffset;
                    break;
                case BulletData.EnabledGlobalOffset:
                    结果 = 子弹.EnabledGlobalOffset;
                    break;
                case BulletData.ChangeInverseRotation:
                    结果 = 子弹.ChangeInverseRotation;
                    break;
                case BulletData.DonDestroyIfMasterDestroy:
                    结果 = 子弹.DonDestroyIfMasterDestroy;
                    break;
                case BulletData.UseForce:
                    结果 = 子弹.UseForce;
                    break;
                case BulletData.UseAlphaWithDepth:
                    结果 = 子弹.UseAlphaWithDepth;
                    break;
                case BulletData.CreateAnimationPlayed:
                    结果 = 子弹.CreateAnimationPlayed;
                    break;
                case BulletData.FilpX:
                    结果 = 子弹.FilpX;
                    break;
                case BulletData.FilpY:
                    结果 = 子弹.FilpY;
                    break;
                case BulletData.AsSubBullet:
                    结果 = 子弹.AsSubBullet;
                    break;
                case BulletData.DestroyMode:
                    结果 = 子弹.DestroyMode;
                    break;
                case BulletData.FollowObjectWithSameAngle:
                    结果 = 子弹.FollowObjectWithSameAngle;
                    break;
                case BulletData.trackLerpBegin:
                    结果 = 子弹.trackLerpBegin;
                    break;
                case BulletData.EnableBulletEvent:
                    结果 = 子弹.EnableBulletEvent;
                    break;
                case BulletData.EnableFollow:
                    结果 = 子弹.EnableFollow;
                    break;
                case BulletData.EnableTrail:
                    结果 = 子弹.EnableTrail;
                    break;
                case BulletData.静态子弹:
                    结果 = 子弹.StaticBullet;
                    break;
                case BulletData.静态子弹下保留碰撞:
                    结果 = 子弹.StayBulletEvent;
                    break;
                case BulletData.允许最低限度子弹事件:
                    结果 = 子弹.StayCollision;
                    break;
                case BulletData.保留动画更新行为:
                    结果 = 子弹.StayAnimPlayer;
                    break;
                case BulletData.保留旋转行为:
                    结果 = 子弹.RotationUpdate;
                    break;
                case BulletData.保留缩放行为:
                    结果 = 子弹.ScaleUpdate;
                    break;
                case BulletData.保留触发器行为:
                    结果 = 子弹.StayTrigger;
                    break;
            }
            return 结果;
        }
    }
}