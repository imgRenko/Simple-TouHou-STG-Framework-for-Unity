using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.发射器 
{public class 设置发射器浮点数信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public float 目的值;[Input] public Shooting 发射器; 
[Output] public FunctionProgress 退出节点;
public enum ShootingEnumData { 
发射器播放速度控制百分比=0,
原点X轴随机偏移值=1,
原点Y轴随机偏移值=2,
椭圆放大倍数=3,
椭圆旋转度数=4,
圆形弹幕半径=5,
圆形弹幕半径自增值=6,
圆形弹幕半径随机范围=7,
发射器移动速度=8,
发射器移动加速度=9,
发射器移动方向=10,
发射器移动方向随机偏移=11,
发射间隔=12,
发射范围=13,
发射度数=14,
最大发射度数=15,
最小发射度数=16,
发射角自增值=17,
随机发射角度范围=18,
发射器最大使用帧数=19,
子弹速度方向平滑改变百分比=20,
子弹朝向=21,
子弹速度方向平滑目标值=22,
随机速度方向范围值=23,
子弹速度方向自增值=24,
子弹运动速度=25,
子弹平滑速度目的值=26,
子弹速度自增值=27,
平滑子弹速度百分比=28,
最大子弹速度=29,
最小子弹速度=30,
子弹随机速度范围=31,
子弹运动加速度=32,
子弹运动加速度随机范围=33,
子弹创建动画播放速度=34,
碰撞圆形半径=35,
子弹最低深度=36,
子弹最高深度=37,
拖尾更新时间=38,
子弹深度=39,
创建子弹时避开玩家的范围=40,
半径方向=41,
随机半径方向范围=42,
射击混乱度=43,
子弹曲线事件延迟执行时间=44,
随机发射最大周期范围=45,
            旋转坐标系度量值 = 47,
            TotalFrame =46,
            随机发射最小周期范围= 48,
            子弹寿命 = 49
}
public ShootingEnumData 发射器属性; 
 public override void FunctionDo(string PortName,List<object> param = null) {
 发射器 = GetInputValue<Shooting>("发射器", null);
        if (发射器 == null) return;
        目的值 = GetInputValue<float>("目的值", 目的值); 
        switch(发射器属性) 
 {case ShootingEnumData.发射器播放速度控制百分比:
发射器.GlobalSpeedPercent=目的值;
break;
case ShootingEnumData.原点X轴随机偏移值:
发射器.XOffset=目的值;
break;
case ShootingEnumData.原点Y轴随机偏移值:
发射器.YOffset=目的值;
break;
case ShootingEnumData.椭圆放大倍数:
发射器.ellipseScale=目的值;
break;
case ShootingEnumData.椭圆旋转度数:
发射器.ellipseRotation=目的值;
break;
case ShootingEnumData.圆形弹幕半径:
发射器.Radius=目的值;
break;
case ShootingEnumData.圆形弹幕半径自增值:
发射器.RadiusIncrement=目的值;
break;
case ShootingEnumData.圆形弹幕半径随机范围:
发射器.RadiusOffset=目的值;
break;
case ShootingEnumData.发射器移动速度:
发射器.ShootingSpeed=目的值;
break;
case ShootingEnumData.发射器移动加速度:
发射器.ShootingAcceleratedSpeed=目的值;
break;
case ShootingEnumData.发射器移动方向:
发射器.MoveDirection=目的值;
break;
case ShootingEnumData.发射器移动方向随机偏移:
发射器.RotationSpeed=目的值;
break;
case ShootingEnumData.发射间隔:
发射器.Timer=目的值;
break;
case ShootingEnumData.发射范围:
发射器.Range=目的值;
break;
case ShootingEnumData.发射度数:
发射器.Angle=目的值;
break;
case ShootingEnumData.最大发射度数:
发射器.MaxAngle=目的值;
break;
case ShootingEnumData.最小发射度数:
发射器.MinAngle=目的值;
break;
case ShootingEnumData.发射角自增值:
发射器.AngleIncreament=目的值;
break;
case ShootingEnumData.随机发射角度范围:
发射器.AngleOffset=目的值;
break;
case ShootingEnumData.发射器最大使用帧数:
发射器.MaxLiveFrame=(int)目的值;
break;
case ShootingEnumData.子弹速度方向平滑改变百分比:
发射器.BulletRotationSmoothlyPrecent=目的值;
break;
case ShootingEnumData.子弹朝向:
发射器.BulletRotation=目的值;
break;
case ShootingEnumData.子弹速度方向平滑目标值:
发射器.BulletTargetRotation=目的值;
break;
case ShootingEnumData.随机速度方向范围值:
发射器.BulletRotationOffset=目的值;
break;
case ShootingEnumData.子弹速度方向自增值:
发射器.BulletAcceleratedRotation=目的值;
break;
case ShootingEnumData.子弹运动速度:
发射器.Speed=目的值;
break;
case ShootingEnumData.子弹平滑速度目的值:
发射器.BulletTargetSpeed=目的值;
break;
case ShootingEnumData.子弹速度自增值:
发射器.SpeedIncreament=目的值;
break;
case ShootingEnumData.平滑子弹速度百分比:
发射器.ChangeSpeedPrecent=目的值;
break;
case ShootingEnumData.最大子弹速度:
发射器.MaxSpeed=目的值;
break;
case ShootingEnumData.最小子弹速度:
发射器.MinSpeed=目的值;
break;
case ShootingEnumData.子弹随机速度范围:
发射器.SpeedOffset=目的值;
break;
case ShootingEnumData.子弹运动加速度:
发射器.AcceleratedSpeed=目的值;
break;
case ShootingEnumData.子弹运动加速度随机范围:
发射器.AcceleratedSpeedOffset=目的值;
break;
case ShootingEnumData.子弹创建动画播放速度:
发射器.CreateAnimationSpeed=目的值;
break;
case ShootingEnumData.碰撞圆形半径:
发射器.CollisionRadius=目的值;
break;
case ShootingEnumData.子弹最低深度:
发射器.minDepth=目的值;
break;
case ShootingEnumData.子弹最高深度:
发射器.maxDepth=目的值;
break;
case ShootingEnumData.拖尾更新时间:
发射器.TrailUpdate=目的值;
break;
case ShootingEnumData.子弹深度:
发射器.BulletDepth=目的值;
break;
case ShootingEnumData.创建子弹时避开玩家的范围:
发射器.AvoidRange=目的值;
break;
case ShootingEnumData.半径方向:
发射器.RadiusDirection=目的值;
break;
case ShootingEnumData.随机半径方向范围:
发射器.RandomRadiusDirection=目的值;
break;
case ShootingEnumData.射击混乱度:
发射器.ShotMess=目的值;
break;
case ShootingEnumData.子弹曲线事件延迟执行时间:
发射器.delayRunTime=目的值;
break;
case ShootingEnumData.随机发射最大周期范围:
发射器.RandomMaxTimer=目的值;
break;
                case ShootingEnumData.随机发射最小周期范围:
                    发射器.RandomMinTimer = 目的值;
                    break;

                case ShootingEnumData.TotalFrame:
发射器.TotalFrame=目的值;
break;
                case ShootingEnumData.旋转坐标系度量值:
                    发射器.RotatorInAsix = 目的值;
                    break;
                case ShootingEnumData.子弹寿命:
                    发射器.BulletLiveFrame = 目的值;
                    break;

            }
            ConnectDo("退出节点");}}}