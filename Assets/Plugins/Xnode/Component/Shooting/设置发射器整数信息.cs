using System.Collections; 
 using System.Collections.Generic;
 using UnityEngine; 
 using XNode; 
namespace 基础事件.发射器 
{public class 设置发射器整数信息:Node 
 {[Input] public FunctionProgress 进入节点;[Input] public int 目的值;[Input] public Shooting 发射器; 
[Output] public FunctionProgress 退出节点;
public enum ShootingEnumData { 
弹幕发射条数=0,
额外发射次数=1,
延迟发射时间=2,
弹幕随机条数范围=3,
把指定的条序号弹幕瞄准自机=4,
子弹创建动画序号=5,
子弹销毁动画序号=6,
拖尾长度=7,
子弹材质=8,
发射器最大发射次数=9,
子弹最大使用时间=10,
子弹图像组序号=11,
子弹序列帧动画播放间隔=12,
播放声音间隔=13,
贝塞尔曲线轨迹读取速度=14,
子弹单批次序号=15,
发射器子弹序号=16
}
public ShootingEnumData 发射器属性; 
 public override void FunctionDo(string PortName,List<object> param = null) {
 发射器 = GetInputValue<Shooting>("发射器", null);if (发射器 == null) return;目的值 = GetInputValue<int>("目的值", 目的值); switch(发射器属性) 
 {case ShootingEnumData.弹幕发射条数:
发射器.Way=目的值;
break;
case ShootingEnumData.额外发射次数:
发射器.SpecialBounsShoot=目的值;
break;
case ShootingEnumData.延迟发射时间:
发射器.Delay=目的值;
break;
case ShootingEnumData.弹幕随机条数范围:
发射器.WayOffset=目的值;
break;
case ShootingEnumData.把指定的条序号弹幕瞄准自机:
发射器.lineIndex=目的值;
break;
case ShootingEnumData.子弹创建动画序号:
发射器.CreateAnimationIndex=目的值;
break;
case ShootingEnumData.子弹销毁动画序号:
发射器.DestroyAnimationIndex=目的值;
break;
case ShootingEnumData.拖尾长度:
发射器.TrailLength=目的值;
break;
case ShootingEnumData.子弹材质:
发射器.MaterialIndex=目的值;
break;
case ShootingEnumData.发射器最大发射次数:
发射器.ShootingShotMaxTime=目的值;
break;
case ShootingEnumData.子弹最大使用时间:
发射器.MaxLiveFrame=目的值;
break;
case ShootingEnumData.子弹图像组序号:
发射器.SpriteIndex=目的值;
break;
case ShootingEnumData.子弹序列帧动画播放间隔:
发射器.WaitingTime=目的值;
break;
case ShootingEnumData.播放声音间隔:
发射器.IntervalSoundTime=目的值;
break;
case ShootingEnumData.贝塞尔曲线轨迹读取速度:
发射器.ReadPointTrackSpeed=目的值;
break;
case ShootingEnumData.子弹单批次序号:
发射器.bulletIndexChecking=目的值;
break;
case ShootingEnumData.发射器子弹序号:
发射器.ShotIndex=目的值;
break;
}ConnectDo("退出节点");}}}