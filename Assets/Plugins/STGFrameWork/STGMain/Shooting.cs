///  TouHou Project Unity STG FrameWork
///  Renko's Code 
///  2016 Created.

using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System;
using System.Xml;
using UnityEngine;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;
using System.IO;
using System.Security.Policy;

#pragma warning disable 0067
[System.Serializable]
[AddComponentMenu("东方STG框架/弹幕设计/重要组件/子弹发射器")]
public class Shooting : STGComponent
{
    #region Value Expressing
    [TitleGroup("发射器", "", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
    [Tooltip("不使用这种发射器进行发射（除了强制发射函数可以强制让此发射器发射）")]
    [LabelText("无强制发射要求，不发射子弹")] public bool Canceled = false;
    [FoldoutGroup("总体控制", expanded: false)]
    [FoldoutGroup("总体控制/事件细节设定", expanded: false)]
    [LabelText("发射器播放速度控制百分比")]
    public float GlobalSpeedPercent = 1f;
    [FoldoutGroup("总体控制/允许使用的事件设定", expanded: false)]
    [LabelText("发射器播放时是否使用被播放事件")]
    public bool UseShootingEvent = true;
    [FoldoutGroup("总体控制/允许使用的事件设定", expanded: false)]
    [LabelText("发射器被销毁时是否使用事件")]
    public bool UseShootingDestroy = true;
    [FoldoutGroup("总体控制/允许使用的事件设定", expanded: false)]
    [LabelText("发射器使用时是否使用被使用事件")]
    public bool UseShootingUsing = true;
    [FoldoutGroup("总体控制/允许使用的事件设定", expanded: false)]
    [LabelText("发射器完成全部发射任务时使用事件")]
    public bool UseShootingFinishAllShotTask = true;
    [FoldoutGroup("总体控制/允许使用的事件设定", expanded: false)]
    [LabelText("应用子弹创建事件")]
    public bool UseBulletEventWhenBulletCreate = true;
    [FoldoutGroup("总体控制/允许使用的事件设定", expanded: false)]
    [LabelText("应用子弹被销毁事件")]
    public bool UseBulletEventWhenBulletDestroy = true;
    [FoldoutGroup("总体控制/允许使用的事件设定", expanded: false)]
    [LabelText("应用子弹恢复主层级事件")]
    public bool UseBulletEventWhenBulletRestoreMainLevel = true;
    [FoldoutGroup("总体控制/允许使用的事件设定", expanded: false)]
    [LabelText("应用子弹击中玩家事件")]
    public bool UseBulletEventOnDestroyingPlayer = true;
    [FoldoutGroup("总体控制/允许使用的事件设定", expanded: false)]
    [LabelText("应用子弹运动时事件")]
    public bool UseBulletEvent = true;
    [FoldoutGroup("总体控制/事件细节设定", expanded: false)]
    [Tooltip("默认地，计数发射器总生存时间时，若总生存时间大于发射器原定的最大生存时间(定义为t)将会销毁该发射器，如果勾选了这个，那么发射器最多可以生存 t + Delay 帧，假设 t = 200，Delay = 300 那么，不勾选这个的话，发射器将不会发射，如果勾选此项，发射器会发射，并且最大生存时间变为500")]
    [LabelText("计算延迟时间到单位百分比")]
    public bool CaluateDelayToPercent = true;
    [FoldoutGroup("总体控制/事件细节设定", expanded: false)]
    [LabelText("使用XML数据文件储存发射器数据")]
    public string XmlShooting = "";
    [FoldoutGroup("总体控制/流程控制设定", expanded: false)]
    [Tooltip("即使是在符卡声明时间，也会执行Update事件，从而继续发射子弹。")]
    [LabelText("忽略声明符卡时间继续发射子弹")]
    public bool ignoreSCExpression = false;
    [FoldoutGroup("总体控制/流程控制设定", expanded: false)]
    [Tooltip("即使是播放剧情时，也会执行Update事件，从而继续发射子弹。")]
    [LabelText("忽略剧情继续发射子弹")]
    public bool IgnorePlot;
    [FoldoutGroup("总体控制/流程控制设定", expanded: false)]
    [Tooltip("即使是强制销毁也不会销毁子弹。")]
    [LabelText("除符卡被击破时消弹其余情况不消弹")]
    public bool InvaildDestroy;
    [FoldoutGroup("总体控制/事件细节设定", expanded: false)]
    [Tooltip("如果使用了这个选项，将允许你使用延迟事件，这将消耗一些性能。")]
    [LabelText("使用协同事件")]
    public bool UseThread = false;
    [FoldoutGroup("注释与标记", expanded: false)]
    [Multiline]
    [FoldoutGroup("注释与标记", expanded: false)]
    [Tooltip("注释发射器。")]
    public string Note = "要描述这个发射器，请在这里留下注释。";
    [FoldoutGroup("注释与标记", expanded: false)]
    [Tooltip("可以用来标记弹幕，用在Trigger里多一些，假设博丽灵梦(Hakurei)和魔理沙(Marisa)联手，你可以判定哪个是博丽灵梦的弹幕，哪个是魔理沙的弹幕")]
    public string BulletTag = "";
    [FoldoutGroup("发射器属性", expanded: false)]
    [Tooltip("默认发射器的发射中心")]
    [LabelText("发射器发射中心原点")]
    public Vector2 Orginal = Vector2.zero;
    [FoldoutGroup("发射器属性", expanded: false)]
    [HideInInspector]
    [LabelText("位置原点自增值(已被弃用参数)")]
    public Vector2 PositionIncrement = Vector2.zero;
    [Tooltip("勾选此项，发射器将会有一个随机变化的位置中心，随机变化的幅度由下面的XOffset和YOffset来决定")]
    [FoldoutGroup("发射器属性", expanded: false)]
    [LabelText("允许随机原点")]
    public bool UseRandomOffset = false;
    [FoldoutGroup("发射器属性", expanded: false)]
    [Tooltip("<勾选UseRandomOffset才生效> X方向的随机偏移值")]
    [LabelText("原点X轴随机偏移值")]
    public float XOffset = 1;
    [FoldoutGroup("发射器属性", expanded: false)]
    [Tooltip("<勾选UseRandomOffset才生效> Y方向的随机偏移值")]
    [LabelText("原点Y轴随机偏移值")]
    public float YOffset = 1;
    [FoldoutGroup("发射器属性/发射形状属性 -- 椭圆", expanded: false)]
    [Tooltip("使用椭圆")]
    [LabelText("允许使用椭圆")]
    public bool useEllipse = false;
    [FoldoutGroup("发射器属性/发射形状属性 -- 椭圆", expanded: false)]
    [Tooltip("保持一个规整的椭圆")]
    [LabelText("保持椭圆规整(不再使用)")]
    [HideInInspector]
    public bool keepRegularEllipse = true;
    [FoldoutGroup("发射器属性/发射形状属性 -- 椭圆", expanded: false)]
    [Tooltip("椭圆的长轴与短轴。")]
    [LabelText("椭圆长短轴向量")]
    public Vector2 ellipseSize = Vector2.one;
    [FoldoutGroup("发射器属性/发射形状属性 -- 椭圆", expanded: false)]
    [Tooltip("用于创建子弹的椭圆的旋转度")]
    [LabelText("椭圆放大倍数")]
    public float ellipseScale = 0;
    [FoldoutGroup("发射器属性/发射形状属性 -- 椭圆", expanded: false)]
    [LabelText("椭圆旋转度数")]
    public float ellipseRotation = 0;
    [FoldoutGroup("发射器属性/发射形状属性 -- 圆", expanded: false)]
    [Tooltip("将子弹创建在以该Radius为半径的圆上。")]
    [LabelText("圆形弹幕半径")]
    public float Radius;
    // 将子弹创建在以该Radius为半径的圆上。
    [FoldoutGroup("发射器属性/发射形状属性 -- 圆", expanded: false)]
    [LabelText("圆形弹幕半径自增值")]
    public float RadiusIncrement = 0;
    [FoldoutGroup("发射器属性/发射形状属性 -- 圆", expanded: false)]
    [Tooltip("令Radius参数产生随机数，偏移大小下面的RadiusOffset来决定")]
    [LabelText("允许圆形弹幕使用随机半径参数")]
    public bool UseRandomRadius = false;
    [FoldoutGroup("发射器属性/发射形状属性 -- 圆", expanded: false)]
    [LabelText("圆形弹幕半径随机范围")]
    public float RadiusOffset = 5;
    [FoldoutGroup("发射器运动及发射属性", expanded: false)]
    [FoldoutGroup("发射器运动及发射属性/发射器运动属性", expanded: false)]
    [Tooltip("<勾选MoveShooting才生效> 它是发射器的默认运动速度。")]
    [LabelText("发射器移动速度")]
    public float ShootingSpeed = 1f;
    [FoldoutGroup("发射器运动及发射属性/发射器运动属性", expanded: false)]
    [Tooltip("<勾选MoveShooting才生效> 它是发射器的加速度。")]
    [LabelText("发射器移动加速度")]
    public float ShootingAcceleratedSpeed = 0f;
    [FoldoutGroup("发射器运动及发射属性/发射器运动属性", expanded: false)]
    [LabelText("发射器移动方向")]
    public float MoveDirection;
    [FoldoutGroup("发射器运动及发射属性/发射器运动属性", expanded: false)]
    [LabelText("发射器角速度")]
    public float RotationSpeed = 0;
    [FoldoutGroup("发射器运动及发射属性/发射器运动属性", expanded: false)]
    [Tooltip("勾选此项，发射器将有相对于敌人的位置偏移，利用它制作一些位移效果")]
    [LabelText("允许发射器进行移动")]
    public bool MoveShooting = false;
    [HideInInspector]
    public bool CanShoot = true;
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性", expanded: false)]
    [Tooltip("表示发射弹幕的条数。")]
    [LabelText("弹幕发射条数")]
    public int Way = 1;
    // Way数
    [Tooltip("这个变量可以让发射器多发射几次（同时发射），多用来制作速度差或其他类型的弹幕，它们是响应【当发射器发射时（AfterShootingFinishShooting）】的事件的")]
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性", expanded: false)]
    [LabelText("额外发射次数")] 
    public int SpecialBounsShoot = 0;
    //额外发射，与发射器同时。

    [Tooltip("延迟到某一帧才发射")] 
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性", expanded: false)] 
    [LabelText("延迟发射时间")] 
    public int Delay = 0;
    [Tooltip("勾选此项发射弹幕的条数会产生随机数，偏移大小下面的WayOffset来决定")]
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性", expanded: false)]
    [LabelText("允许弹幕以随机条数发射")]
    public bool UseRandomWay;
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性", expanded: false)]
    [LabelText("弹幕随机条数范围")]
    public int WayOffset = 3;
    [Tooltip("以 60 FPS/Second 为标准，例如：Timer填入35意味着每隔35帧发射一次")]
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性", expanded: false)]
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性")]
    [LabelText("发射间隔")] 
    public float Timer = 35;
    // 多少帧发射一次。
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性")]
    [Tooltip("发射范围，若为360 则不会限制发射的角度")]
    [LabelText("发射范围")] public float Range = 360;
    // 发射范围
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性")]
    [Tooltip("它是发射器的发射角")]
    [LabelText("发射度数")] public float Angle = 0f;
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性")]
    [Tooltip("<勾选UseRandomAngle才生效> 在进行随机角度变化的时候，可以限制角度的大小。下面的变量的具体功能见变量名")]
    [LabelText("最大发射度数")] public float MaxAngle = 720f;
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性")] [LabelText("最小发射度数")] public float MinAngle = -720f;
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性")]
    [Tooltip("它是发射器的发射角自增值")]
    [LabelText("发射角自增值")]
    public float AngleIncreament = 0f;
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性")]
    [Tooltip("勾选此项，发射器的发射方向将会进行随机变化，这变化幅度是由下面的RotationOffset来决定的")]
    [LabelText("允许随机发射角度")] 
    public bool UseRandomAngle = false;
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性")]
    [Tooltip("<勾选UseRandomRotation才生效> 它是发射器的发射方向的偏移值")]
    [LabelText("随机发射角度范围")]
    public float AngleOffset = 360;

 
    // 可以使用该发射器的最大时间

    [FoldoutGroup("发射器运动及发射属性/发射器发射属性")]
    [LabelText("允许发射器复用")] 
    public bool Reusable = true;

    [Tooltip("若勾选它，发射器瞄准玩家而进行发射，这个时候Angle以及相关属性无效")]
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性")]
    [LabelText("自机狙子弹")]
    public bool FollowPlayer = false;
    [Tooltip("<与middleIndex不兼容>若该值为False，将不再使用自机狙以最开始发射的那一条子弹为基准来瞄准玩家，而依靠下面的lineIndex来瞄准。")]
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性")]
    [ShowIf("FollowPlayer")]
    [LabelText("把第一条弹幕瞄准自机")] 
    public bool firstLine = false;

    [Tooltip("<与firstLine不兼容>若该值为True，将LineIndex一直使用为Way（发射器发射子弹条数）的中间值，因而该值为True的时候，对lineIndex的修改都将无效。因为会对LineIndex进行修改，因而直接将此值设置为False并不会有什么变化，需要修改LineIndex才会有一些效果")]
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性")]
    [LabelText("把最中央的一条弹幕瞄准自机")]
    [ShowIf("FollowPlayer")] 
    public bool middleIndex = true;

    [FoldoutGroup("发射器运动及发射属性/发射器发射属性")]
    [LabelText("把指定的条序号弹幕瞄准自机")] 
    [ShowIf("FollowPlayer")]
    public int lineIndex = 0;
    // 做自机狙用。
    [Tooltip("若将你的子弹创建到一个以Radius为半径的圆以内，并且玩家在该圆以内运动，该选项控制是否还让自机狙瞄准玩家")]
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性")] [ShowIf("FollowPlayer")] public bool DisallowInverse = false;
    [FoldoutGroup("子弹运动属性", expanded: false)]
    [Tooltip("<只有勾选InverseRotation生效 若勾选 TwistedRotation、FollowPlayer 的其中一项无效> 是否平滑地修改子弹的朝向，如果需要，就在脚本中控制子弹的TargetRotation变量的值即可，这样会自动平滑旋转")]
    [LabelText("允许平滑改变子弹速度方向")] 
    public bool ChangeBulletRotationSmoothly = false;
    [Tooltip("<同时选择了InverseRotation（子弹方向不受速度方向影响选项）、允许平滑改变子弹速度方向两个选项以后，才能使用这个选项>，它将平滑控制子弹方向到目的旋转值")]
    [FoldoutGroup("子弹运动属性")]
    [LabelText("平滑控制子弹朝向")] 
    public bool ChangeInverseRotation = false;
    [FoldoutGroup("子弹运动属性")] 
    [LabelText("子弹速度方向平滑改变百分比")] 
    public float BulletRotationSmoothlyPrecent = 0.05f;
    [FoldoutGroup("子弹运动属性")]
    [LabelText("子弹速度方向平滑改变方式")]
    public Bullet.MoveCurves RotationSmoothType = Bullet.MoveCurves.Lerp;
    [Tooltip("这是子弹的朝向")]
    [FoldoutGroup("子弹运动属性")]
    [LabelText("子弹朝向")] 
    public float BulletRotation;
    [Tooltip("这是子弹的目标速度方向，只有在使用平滑旋转函数的时候才可以使用。")]
    [FoldoutGroup("子弹运动属性")]
    [LabelText("子弹速度方向平滑目标值")]
    public float BulletTargetRotation;
    [Tooltip("勾选此项，在保持不冲突或者是无旋转方向动态变化的情况下，子弹将瞄向自机")]
    [FoldoutGroup("子弹运动属性")]
    [LabelText("子弹速度方向始终朝向玩家")] 
    public bool AimToPlayer = false;

    [Tooltip("<只有勾选InverseRotation生效 若勾选 TwistedRotation、FollowPlayer 的其中一项无效> 勾选此项，可以生成子弹的随机发射角度。")]
    [FoldoutGroup("子弹运动属性")]
    [LabelText("允许随机子弹朝向")] 
    public bool UseRandomBulletRotation = false;

    [Tooltip("<只有勾选InverseRotation生效 若勾选 TwistedRotation、FollowPlayer 的其中一项无效> 生成子弹的随机发射角度的幅度大小")]
    [FoldoutGroup("子弹运动属性")]
    [LabelText("随机子弹朝向范围值")]
    public float BulletRotationOffset = 1;

    [Tooltip("子弹的发射角度的旋转，利用它制作回旋弹幕。")]
    [FoldoutGroup("子弹运动属性")]
    [LabelText("子弹速度方向自增值")] 
    public float BulletAcceleratedRotation = 0f;
    [Tooltip("子弹的发射速度")]
    [FoldoutGroup("子弹运动属性")]
    [LabelText("子弹运动速度")] 
    public float Speed = 5f;
    [Tooltip("这是子弹的目标速度，只有在使用平滑改变速度函数的时候才可以使用。")]
    [FoldoutGroup("子弹运动属性")]
    [LabelText("子弹平滑速度目的值")] 
    public float BulletTargetSpeed;
    [Tooltip("单次发射内一个链条内子弹的发射速度。与AcceleratedSpeed不同，这个变量仅仅控制一次发射内单个链条的速度（不是加速度），而不是控制一次发射生成所有的子弹的加速度。")]
    [FoldoutGroup("子弹运动属性")]
    [LabelText("子弹速度自增值")] 
    public float SpeedIncreament = 0f;

    [Tooltip("<只有在【当子弹移动时（OnBulletMoving）】事件里对TargetSpeed变量使用大幅度的改变才可以生效！>不立刻改变速度，而是慢慢改变，注意，若要使用这项功能，在脚本中修改Bullet.TargetSpeed变量即可")]
    [FoldoutGroup("子弹运动属性")]
    [LabelText("允许平滑子弹速度到某一个值")] 
    public bool ChangeSpeedSmoothly = false;
    [FoldoutGroup("子弹运动属性")] [LabelText("平滑子弹速度百分比")] 
    public float ChangeSpeedPrecent = 0.05f;
    [FoldoutGroup("子弹运动属性")] [LabelText("平滑子弹速度方式")] 
    public Bullet.MoveCurves SpeedSmoothType = Bullet.MoveCurves.Lerp;
    [Tooltip("自行翻译")]
    [FoldoutGroup("子弹运动属性")]
    [LabelText("子弹随机速度")]
    public bool RandomSpeed = false;
    [FoldoutGroup("子弹运动属性")]
    [LabelText("单路子弹随机速度")]
    public bool SingleWayRandomSpeed = false;
    [Tooltip("在AcceleratedSpeed不为零的时候可用，可以限制速度的大小，下面同")]
    [FoldoutGroup("子弹运动属性")]
    [LabelText("最大随机子弹速度")]
    public float MaxSpeed = 8f;
    [FoldoutGroup("子弹运动属性")] 
    [LabelText("最小随机子弹速度")] 
    public float MinSpeed = -8f;
    [FoldoutGroup("子弹运动属性")]
    [LabelText("子弹随机速度范围")]
    public float SpeedOffset = 1;
    [Tooltip("子弹的发射加速度。与SpeedIncreament不同，这个变量是控制一次发射生成所有的子弹的加速度，而另一个仅仅控制一次发射内单个链条的速度（不是加速度）。")]
    [FoldoutGroup("子弹运动属性")]
    [LabelText("子弹运动加速度")] 
    public float AcceleratedSpeed;

    [Tooltip("勾选此项，可以使用随机加速度")]
    [FoldoutGroup("子弹运动属性")]
    [LabelText("允许使用子弹随机加速度")]
    public bool RandomAcceleratedSpeed = false;
    [Tooltip("加速度的偏移值量")]
    [FoldoutGroup("子弹运动属性")]
    [LabelText("子弹运动加速度随机范围")]
    public float AcceleratedSpeedOffset = 1;
    [FoldoutGroup("子弹碰撞属性")]
    [Tooltip("如果勾选，那么你的碰撞盒会随着缩放大小而变化。")]
    [LabelText("允许动态碰撞盒")]
    public bool UseDynamicsCollision = true;
    [Tooltip("在播放创建动画的时候无碰撞。")]
    [FoldoutGroup("子弹碰撞属性")]
    [LabelText("播放子弹创建动画时无碰撞")]
    public bool NoCollisionWhenCreateAnimationPlaying = true;
    [Tooltip("控制创建子弹动画的播放速度")]
    [FoldoutGroup("子弹碰撞属性")]
    [LabelText("子弹创建动画播放速度")]
    public float CreateAnimationSpeed = 1.0f;
    [Tooltip(
        "判定的种类，它是矩形、圆心还是无判定的子弹?你需要设定它,若不知道如何度量判定大小，前往属性面板加入一个碰撞器,将其中的数值记录下来填入表格即可(我不推荐Unity自带的游戏物理引擎来进行碰撞运算（它有许多我不需要的运算，拉低游戏性能)")]
    [FoldoutGroup("子弹碰撞属性")]
    [LabelText("子弹碰撞方式")]
    public Bullet.CollisionType BulletCollision = Bullet.CollisionType.CIRCLE;

    [Tooltip("当透明度过低时不用碰撞")]
    [FoldoutGroup("子弹碰撞属性")]
    [LabelText("透明度过低不使用子弹碰撞")]
    public bool NoCollisionWhenAlphaLow = false;
    [Tooltip("自定义碰撞，你可以用自定义碰撞制作其他具有特殊形状弹幕的碰撞盒。（注意！碰撞盒启用时，仍要使用触发器时，将子弹的碰撞方式设置为CIRCLE，否则触发器无法检测自定义碰撞盒）")]
    [FoldoutGroup("子弹碰撞属性")]
    [LabelText("允许自定义碰撞盒")]
    public bool UseCustomCollisionGroup = false;

    [Tooltip("<勾选 UseCustomCollisionGroup 生效> 你可以使用它来创建一个完全不一样的碰撞，例如激光碰撞，但它们都是由多个圆组成的")]
    [FoldoutGroup("子弹碰撞属性")]
    [LabelText("自定义碰撞盒对象")]
    [ShowIf("UseCustomCollisionGroup")]
    public GameObject CustomCollisionGroup;




    [Tooltip("<判定的种类为矩形才生效> 在以矩形中心为原点的直角平面坐标系上,此变量代表着第一象限的某一个点，这个点是矩形的顶点，这个变量用作测量矩形的对角线长度。")]
    [FoldoutGroup("子弹碰撞属性")]
    [LabelText("碰撞矩形对角线向量")]
    [ShowIf("BulletCollision", Bullet.CollisionType.BOX)]
    public Vector2 SquareLength;

    [Tooltip("<判定的种类为圆心才生效> 圆的半径大小。")]
    [FoldoutGroup("子弹碰撞属性")]
    [LabelText("碰撞圆形半径")]
    [ShowIf("BulletCollision", Bullet.CollisionType.CIRCLE)]
    public float CollisionRadius = 0.03f;
    [FoldoutGroup("子弹特效")]
    [LabelText("子弹透明度随深度变化(实验)")]
    public bool UseAlphaWithDepth = false;
    [FoldoutGroup("子弹特效")] 
    [ShowIf("UseAlphaWithDepth")] 
    [LabelText("子弹最低深度")] 
    public float minDepth = 0;
    [FoldoutGroup("子弹特效")] 
    [ShowIf("UseAlphaWithDepth")] 
    [LabelText("子弹最高深度")] 
    public float maxDepth = 0.5f;
    [Tooltip("将使用你自定义的子弹销毁动画。通过这个变量查询相应名称的动画然后并使用它。")]
    [FoldoutGroup("子弹特效")]
    [HideIf("NoDestroyAnimation")]
    [LabelText("子弹销毁时播放动画名称")]
    public string BulletBreakingAnimationName = "BulletBreak";
    [FoldoutGroup("子弹特效")]
    [HideIf("NoCreateAnimation")]
    [LabelText("子弹创建时播放动画名称")]
    public string BulletCreatingAnimationName = "BulletCreate";
    [Tooltip("勾选此项，将不播放创建子弹时的动画（子弹直接出现并使用碰撞。）")]
    [FoldoutGroup("子弹特效")]
    [LabelText("不播放子弹创建动画")] public bool NoCreateAnimation;
    [Tooltip("子弹使用创建动画的序号")]
    [FoldoutGroup("子弹特效")]
    [HideInInspector]
    [LabelText("子弹创建动画序号")] public int CreateAnimationIndex = 0;
    [Tooltip("勾选此项，将不播放销毁子弹时的动画（子弹直接消失掉）")]
    [FoldoutGroup("子弹特效")]
    [LabelText("不播放子弹销毁动画")] public bool NoDestroyAnimation;
    [Tooltip("子弹使用的销毁动画的序号")]
    [FoldoutGroup("子弹特效")]
    [HideInInspector]
    [LabelText("子弹销毁动画序号")] public int DestroyAnimationIndex = 0;
    [Tooltip("<和InverseRotation不兼容> 勾选此项,可以制作子弹扭曲效果")]
    [FoldoutGroup("子弹特效")]
    [LabelText("允许子弹扭曲旋转")] public bool TwistedRotation = false;
    // 特殊效果，与下面的变量不兼容。

    [Tooltip("<和TwistedRotation不兼容> 勾选此项,可以使用自定义子弹朝向。子弹的方向不会被自动设定，那将由你的Rotation设置来设定。")]
    [FoldoutGroup("子弹特效")]
    [LabelText("允许子弹朝向不受速度方向影响")] public bool InverseRotation = false;
    [Tooltip("使用拖尾效果，要自定义拖尾效果，请在编辑器内找到子弹的原型，修改其子级下内的控制拖尾效果的游戏对象")]
    [FoldoutGroup("子弹特效")]
    [LabelText("允许拖尾效果(耗费性能)")] 
    public bool useTrails = false;
    [FoldoutGroup("子弹特效")]
    [LabelText("拖尾长度")] 
    public int TrailLength = 10;
    [FoldoutGroup("子弹特效")] 
    [LabelText("拖尾更新时间(越短越清楚)")]
    public float TrailUpdate = 3;
    //若勾选为true,那么只有使用Rotation才可以改变子弹方向
    [Tooltip("若该项为0，则子弹图像使用默认的材质，这些材质可以在Global类里找到合集。默认情况下该参数为0，使用默认的Sprite材质（子弹图像正常显示），为1，使用高光材质，子弹图像将进行高光处理")]
    [FoldoutGroup("子弹特效")]
    [LabelText("子弹材质")]
    public int MaterialIndex = 0;
    [Tooltip("子弹深度，用于一些需要进行弹幕分层的情况下。(可以利用它制作中玉遮挡小玉的效果。)")]
    [FoldoutGroup("子弹特效")]
    [LabelText("子弹深度")] 
    public float BulletDepth = 0;
    [Tooltip("这是子弹携带的发射器，通常会把目标对象所有的发射器复制到这个发射器将要发射的子弹身上，因此，你可以对这个目标对象做一些改动，例如添加上敌人类或子弹类，使其拥有敌人或子弹的性质，这样可以制作出非常好玩的效果，例如：可使用它制作子弹发射敌人或子弹的效果。但请注意，需要将你的目标对象的Active设置为False")]
    [FoldoutGroup("子弹特效")]
    [LabelText("被发射子弹需要携带的对象")]
    public GameObject BulletShootingObject;
    [Tooltip("要让该发射器所发射的子弹所携带的发射器发射出来的子弹发射出来时的速度方向与该发射器发射的子弹的速度方向相同。")]
    [FoldoutGroup("子弹特效")]
    [LabelText("携带子弹与发射子弹速度方向一致")]
    [HideInInspector]
    public bool FollowAngle = false;
    [Tooltip("限定发射次数，-1为不限制")]
    [FoldoutGroup("子弹特效")]
    [LabelText("发射器最大发射次数")]
    public int ShootingShotMaxTime = -1;
    [Tooltip("若子弹具有发射器，这个选项让它发射的子弹成为这个发射器所发射的子弹的子层级。请注意，请在使用这个子发射器之前。")]
    [FoldoutGroup("子弹特效")]
    [LabelText("携带子弹作为子级子弹跟随父级子弹")]
    public bool MakeSubBullet = false;
    [Tooltip("如果主级子弹销毁了，不销毁副级子弹。")]
    [FoldoutGroup("子弹特效")]
    [LabelText("父级子弹销毁不销毁子级子弹")] 
    public bool DonDestroyIfMasterDestroy = false;
    [FoldoutGroup("子弹外观")]
    [LabelText("子弹使用时长")]
    public float BulletLiveFrame = 200;
    [Tooltip("<不勾选UseCustomSprite有效> 在全局的图像列表变量里选择相应序号的图像。Index表示序号")]
    [FoldoutGroup("子弹外观")]
    [LabelText("子弹图像组序号")] 
    public int SpriteIndex = 0;
    [Tooltip("除去一些特殊情况，无论是死亡还是放Bomb，都不会销毁该子弹。")]
    [FoldoutGroup("子弹外观")]
    [LabelText("减少子弹销毁可能性")] 
    public bool DonDestroy = false;
    [Tooltip("勾选此项，可以使用自定义图像来装饰子弹。")]
    [FoldoutGroup("子弹外观")]
    [LabelText("允许自定义子弹外观")]
    public bool UseCustomSprite = true;
    [Tooltip("使用自定義的子彈動畫，由此你可以創造子彈移動時的動畫。")]
    [FoldoutGroup("子弹外观")]
    [LabelText("允许使用子弹序列帧动画")]
    public bool UseBulletAnimation;
    [Tooltip("子弹图像集，等待指定帧数后切换到一个图像。用于自定义子弹动画（例如：制作火弹）")]
    [FoldoutGroup("子弹外观")]

    [PreviewField(50, ObjectFieldAlignment.Left)]
    [LabelText("子弹序列帧集合")]
    [ShowIf("UseBulletAnimation")]
    public List<Sprite> BulletSpritePerFrame = new List<Sprite>();
    [FoldoutGroup("子弹外观")]

    [PreviewField(50, ObjectFieldAlignment.Left)]
    [LabelText("子弹销毁序列帧集合")]
    [HideIf("useDefaultSprite")]
    public List<Sprite> DestroyAnimSprites = new List<Sprite>();
    [FoldoutGroup("子弹外观")]
    [LabelText("使用默认集合")]
    public bool useDefaultSprite = true;

    [Tooltip("子弹自定义动画中切换到下一个图像的等待帧数量")]
    [FoldoutGroup("子弹外观")]
    [LabelText("子弹序列帧动画播放间隔")] public int WaitingTime = 3;
    [Tooltip("<只有勾选UseCustomSprite生效> 在这里填入你的自定义图像。")]
    [FoldoutGroup("子弹外观")]
    [PreviewField(50, ObjectFieldAlignment.Left)]
    public Sprite CustomSprite;
    [FoldoutGroup("子弹外观")]
    [PreviewField(50, ObjectFieldAlignment.Left)]
    public Sprite CreatingCustomSprite;
    [FoldoutGroup("子弹外观")] [Tooltip("子弹所使用的颜色。")] [ColorPalette("BulletColor")] public Color BulletColor = Color.white;
    [FoldoutGroup("子弹外观")] [Tooltip("子弹销毁时所使用的颜色。")] [ColorPalette("BulletDestroyColor")] public Color BrokenBulletColor = Color.white;
    [FoldoutGroup("子弹外观")] [Tooltip("子弹生成时所使用的颜色。")] [ColorPalette("BulletBirthColor")] public Color CreateBulletColor = Color.white;
    [FoldoutGroup("子弹外观")] [Tooltip("子弹缩放比")] [LabelText("子弹缩放")] public Vector2 Scale = Vector2.one;
    [FoldoutGroup("子弹外观")] [Tooltip("启用子弹的全局位置偏移")] [LabelText("启用子弹的全局偏移")] public bool EnabledGlobalOffset;



    [FoldoutGroup("子弹外观")] [Tooltip("子弹的全局位置偏移")] [LabelText("子弹的全局偏移")] public Vector2 GlobalOffset;
    [FoldoutGroup("子弹外观")] [Tooltip("启用子弹的全局位置加速偏移。")] [LabelText("启用子弹全局加速偏移")] 
    [ShowIf("EnabledGlobalOffset")] public bool EnabledAcceleratedGlobalOffset;
    [FoldoutGroup("子弹外观")] [Tooltip("子弹的全局位置加速偏移。")] [LabelText("子弹全局加速偏移")]
    [ShowIf("EnabledGlobalOffset")] public Vector2 AcceleratedGlobalOffset;
    [FoldoutGroup("事件总控制")]
    [Tooltip("出屏销毁子弹， 回归弹一般不用此选项。")]
    [FoldoutGroup("事件总控制")]
    [LabelText("出屏销毁子弹")]
    public bool DestroyWhenDonRender = true;
    //出屏就会销毁。
    [Tooltip("允许子弹使用力场（力场需要单独设定对象，在下面的列表进行）")]
    [FoldoutGroup("事件总控制")]
    [LabelText("允许给子弹使用力场")]
    public bool EnableForForce = false;
    [LabelText("允许影响该子弹的力场对象")]
    [ShowIf("EnableForForce")]
    [FoldoutGroup("事件总控制")] public List<Force> ForceObject = new List<Force>();
    [Tooltip("若是共享该发射器，那么控制符卡相关的脚本就不会销毁搭载发射器脚本的对象")]
    [FoldoutGroup("事件总控制")]
    [LabelText("共享发射器")]

    public bool Share = false;
    [Tooltip("要求子弹作为特效子弹时使用，标记了这个状态，子弹将保持静态，删去大量平日可能需要，但不得去除的计算，借以提升性能，此状态下，子弹不能移动，不能修改缩放，发射器创建子弹以后的一切数据都尽可能不能更改。但你仍可以通过最低限度的子弹刷新事件，去除静态子弹标签，以恢复数据的动态修改。在本状态下，子弹事件仅有创建子弹时事件可用，于事件内的数据更改不会受到子弹本身对象的响应。<如果你作为弹幕设计师，想要保持原弹幕效果，又想降低游戏配置时，可以考虑与对象绑定功能结合起来批次化处理子弹>(实验性功能)")]
    [FoldoutGroup("事件总控制")]
    [LabelText("静态子弹")]

    public bool StaticBullet = false;
    [Tooltip("保留静态子弹的碰撞功能")]
    [FoldoutGroup("事件总控制")]
    [LabelText("保留静态子弹的碰撞功能")]
    [ShowIf("StaticBullet")]
    public bool StayCollision = false;

    [Tooltip("保留最低限度的子弹事件(将稍微降低性能)")]
    [FoldoutGroup("事件总控制")]
    [LabelText("最低限度刷新事件")]
    [ShowIf("StaticBullet")]
    public bool StayBulletEvent = false;
    [Tooltip("保留缩放变更行为")]
    [FoldoutGroup("事件总控制")]
    [LabelText("保留缩放变更行为")]
    [ShowIf("StaticBullet")]
    public bool ScaleUpdate = false;

    [Tooltip("保留旋转变更行为(将稍微降低性能)")]
    [FoldoutGroup("事件总控制")]
    [LabelText("保留旋转变更行为")]
    [ShowIf("StaticBullet")]
    public bool RotationUpdate = false;

    [Tooltip("保留动画播放行为(将稍微降低性能)")]
    [FoldoutGroup("事件总控制")]
    [LabelText("保留动画播放行为")]
    [ShowIf("StaticBullet")]
    public bool StayAnimPlayer = false;
    [Tooltip("保留触发器行为(将稍微降低性能)")]
    [FoldoutGroup("事件总控制")]
    [LabelText("保留触发器行为")]
    [ShowIf("StaticBullet")]
    public bool StayTrigger = false;
    [Tooltip("若是此项为勾，那么你的发射器如果在符卡系统里面，一旦符卡被击破它不会被置为关闭(如果符卡系统设定为使用后销毁且发射器绑定在符卡系统对象的子级下时，这个选项始终无效)")]
    [FoldoutGroup("事件总控制")]
    [LabelText("符卡被击破时不关闭发射器")]
    public bool Continue = false;
    [Tooltip("使发射的子弹应用触发器")]
    [FoldoutGroup("事件总控制")]
    [LabelText("使发射的子弹应用触发器")]
    public bool ApplyTrigger = false;
    [LabelText("允许影响该子弹的触发器对象")]
    [ShowIf("ApplyTrigger")]
    [FoldoutGroup("事件总控制")] public List<Trigger> TriggerList = new List<Trigger>();
    [Tooltip("是否在发射时避开玩家，下面的Range是范围")]
    [FoldoutGroup("事件总控制")]
    [LabelText("允许创建子弹时避开玩家")]
    public bool AvoidPlayer = true;
    [FoldoutGroup("事件总控制")] [LabelText("创建子弹时避开玩家的范围")] public float AvoidRange = 0.3f;
    [Tooltip("是否已经进行过擦弹处理操作，若此项为True,那么即使你已经接近该子弹也不会进行擦弹处理操作")]
    [FoldoutGroup("事件总控制")]
    [LabelText("已经实现擦弹")]
    public bool Graze = false;
    [Tooltip("在开始调用销毁事件组之前（或是在之后，先后顺序在下面的枚举变量中决定），对此发射器使用回滚原型函数")]
    [FoldoutGroup("事件总控制")]
    [LabelText("允许发射器回滚")]
    public bool rollBack = false;
    [Tooltip("是否为敌人的发射器，如果是，那么发射器在触发Awake函数的时候可以进行发射器原型回滚。")]
    [FoldoutGroup("事件总控制")]
    [LabelText("作为敌人发射器(已经启用)")]
    public bool isEnemyShooting = false;
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性", expanded: false)]
    [Tooltip("随对象旋转度调整发射角度")]
    [LabelText("随对象旋转度调整发射角度")]
    public bool followRotation = true;
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性", expanded: false)]
    [Tooltip("半径方向")]
    [LabelText("半径方向")]
    public float RadiusDirection = 0;
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性", expanded: false)]
    [Tooltip("随机半径方向范围")]
    [LabelText("随机半径方向范围")]
    public float RandomRadiusDirection = 0;

    protected bool UnprocessingOrginalData = false;
    [HideInInspector]
    public bool DefaultEnabled = true;
    public enum EventOrder
    {
        AFTER = 0,
        BEFORE = 1
    }
    [Title("EventOrder Enum")]
    [EnumToggleButtons]
    [LabelText("回滚事件在脚本中顺序")]
    [FoldoutGroup("事件总控制")] public EventOrder order = EventOrder.BEFORE;
    //在指定范围内避免玩家。



    [FoldoutGroup("事件总控制/声音")] 
    [LabelText("播放声音")] 
    public bool PlayerSound = false;
    [FoldoutGroup("事件总控制/声音")]
    [Tooltip("这是你设定的发射器可以使用的声源")]
    [LabelText("声源")]
    public AudioSource[] Sound;
    [Tooltip("声音播放间隔时间")]
    [FoldoutGroup("事件总控制/声音")]
    [LabelText("播放声音间隔")]
    public int IntervalSoundTime = 4;
    [FoldoutGroup("子弹特效")]
    [Tooltip("射击混乱度")]
    [LabelText("射击混乱度")]
    public float ShotMess = 0;
    [FoldoutGroup("子弹特效")]
    [LabelText("贝塞尔曲线轨迹读取速度")]
    public int ReadPointTrackSpeed;
    [FoldoutGroup("子弹特效")]
    [LabelText("贝塞尔曲线轨迹循环")]
    public bool LoopTrack;
    [FoldoutGroup("子弹特效")]
    [LabelText("计算贝塞尔曲线切线")]
    public bool CalculateAngle = true;
    [FoldoutGroup("子弹外观")]
    [LabelText("子弹图像X轴反转")] 
    public bool FilpX;
    [FoldoutGroup("子弹外观")]
    [LabelText("子弹图像Y轴反转")]
    public bool FilpY;
    [FoldoutGroup("发射器运动及发射属性/发射器运动属性")]
    public bool ShotAtPlayer = false;
    [FoldoutGroup("子弹特效")]
    [LabelText("子弹携带对象需要旋转坐标系")]
    public bool FollowObjectWithSameAngle = false;
    [FoldoutGroup("子弹特效")]
    [LabelText("旋转坐标系度量值")]
    [ShowIf("FollowObjectWithSameAngle")]
    public float RotatorInAsix = 0;
    [FoldoutGroup("总体控制/子弹曲线事件", expanded: false)]
    [LabelText("子弹曲线事件延迟执行时间")]
    public float delayRunTime = 0;
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性")]
    [ShowIf("EnableRandomTimer")]
    [LabelText("随机发射最大周期范围")] 
    public float RandomMaxTimer = 3;
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性")]
    [ShowIf("EnableRandomTimer")]
    [LabelText("随机发射最小周期范围")]
    public float RandomMinTimer = 3;
    [FoldoutGroup("发射器运动及发射属性/发射器发射属性")]
    [Tooltip("它是发射器的发射角")]
    [LabelText("启用随机发射周期")] 
    public bool EnableRandomTimer = false;
    [TitleGroup("敌人发射器", "勾选下方Enable选项启用之，启用后，将不再发射子弹而发射敌人", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
    [LabelText("切换到发射敌人模式")]
    public bool EnableEnemyShot = false;
    [ShowIf("EnableEnemyShot")]
    [LabelText("限制敌人停止移动")]
    [Tooltip("如果想当敌人运动了x帧以后就停止运动，就勾选此项。x的值是子弹最大使用帧数时长")]
    public bool StopWhenDisable = false;
    [ShowIf("EnableEnemyShot")]
    public int EnemyHP = 100;
    [ShowIf("EnableEnemyShot")]
    [LabelText("敌人搭载发射器对象")]
    [Tooltip("如文")]
    public GameObject ShootingObject;
    [ShowIf("EnableEnemyShot")]
    [LabelText("敌人动画序列号")]
    [Tooltip("定义在Global类，可在场景编辑器前往GameAction对象处查看对应序列号，然后对应其序列号填入此处的值即可")]
    public int AnimationIndex = 0;
    [LabelText("奖励点数量")]
    [ShowIf("EnableEnemyShot")]
    public int BounsScoreNumber = 3;
    [LabelText("奖励Power点数量")]
    [ShowIf("EnableEnemyShot")]
    public int BounsPowerNumber = 4;
    [ShowIf("EnableEnemyShot")]
    [LabelText("奖励FullPower数量")]
    public int BounsFullPowerNumber = 0;
    [ShowIf("EnableEnemyShot")]
    [LabelText("奖励残机碎片数量")]
    public int BounsLivePieceNumber = 0;



    [TitleGroup("轨迹描述功能", "", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
    [InfoBox("一些情况可能导致轨迹位置偏移不准确，原因是某些参数必须要启动游戏以后才可以预测或计算。例如：发射角自增、子弹动态曲线等情况都是轨迹无法清楚计算的情况，这种情况下轨迹仅供参考。（仅限侦测子弹事件，发射器事件不受侦测，如果需要使用子弹简易轨迹，将其写入子弹事件组中。）")]
    [LabelText("轨迹时间点")]
    [SerializeField, SetProperty("Display")]
    private int displayer;
    [HideInInspector]
    private bool transCheck = false;
    [HideInInspector]
    public Transform thisTransform;
    public int Display
    {
        get
        {
            return displayer;
        }
        set
        {
            displayer = value;
            foreach (var a in GetComponentsInChildren<ShootingTrackDisplayer>())
            {
                a.Display = displayer;
            }
        }
    }
    [HideInInspector]
    public GameObject BulletBase;
    [HideInInspector]
    public TrackData trackDataInfoRemaining;
    [HideInInspector]
    [LabelText("发射的子弹数目")]
    public int bulletIndexChecking;
    [HideInInspector]
    [LabelText("此发射器为复制品")]
    public bool CopySign = false;
    [ButtonGroup]
    private void BulletTrack()
    {
        ShootingTrackProductor t = this.gameObject.AddComponent<ShootingTrackProductor>();
        t.Tracker = this;
        t.DoPlay();
    }
    [ButtonGroup]
    private void ShootingTrack()
    {
        ShootingEvent[] _l = GetComponents<ShootingEvent>();
        
        foreach (ShootingEvent a in _l)
        {
            a.EventStart((!a.ForOnlyOne ? GetComponents<Shooting>() : new List<Shooting>() { GetComponent<Shooting>() }.ToArray() ));
            ShootingUsingEvent += a.OnShootingUsing;
            

        }
        GameObject trackFollower = (GameObject)GameObject.Instantiate(Resources.Load("Prefeb/EmptyObject"));
        trackFollower.transform.parent = this.gameObject.transform;
        ShootingTrackDisplayer t = trackFollower.AddComponent<ShootingTrackDisplayer>();
        t.InfluenceByRotation = false;
        Vector3 p = this.transform.position;
        Vector3 r = this.transform.localEulerAngles;
        ResetOrginalData(true, true);        
        for (int s = 0; s != MaxLiveFrame; ++s)
        {
            TotalFrame = s;
            MoveShootingProgress();
            t.trackPoint.Add((Vector3)this.transform.position);
            // ShootingEvent(this);
            if (ShootingUsingEvent != null)
                ShootingUsingEvent(this);
            if (ShootingSpeed<= 2)
                t.colorSet.Add(new Color32(255, 255, 255, (byte)(Mathf.Abs(ShootingSpeed) / 2 * 255)));
            else if (ShootingSpeed <= 4)
                t.colorSet.Add(Color32.Lerp(Color.white, Color.yellow, (Mathf.Abs(ShootingSpeed) - 2) / 2));
            else if (ShootingSpeed <= 6)
                t.colorSet.Add(Color32.Lerp(Color.yellow, Color.red, (Mathf.Abs(ShootingSpeed) - 4) / 2));
        }
        ReturnOriginalData(true, true);
        TotalFrame = 0;
        ShootingUsingEvent = null;
        this.transform.position = p;
        this.transform.localEulerAngles = r;
    }

    [ButtonGroup]
    private void Remove()
    {
        ShootingTrackDisplayer[] _t = GetComponentsInChildren<ShootingTrackDisplayer>();
        foreach (ShootingTrackDisplayer a in _t)
        {
            DestroyImmediate(a.gameObject);
        }
    }
    private static Vector2 defaultVector;
    private int TotalShotBatch = 0;
    [HideInInspector]
    public int ShotIndex = 0;
    [HideInInspector]
    public List<Bullet> ShotBullet = new List<Bullet>();
    // -------------------- 主发射器 和 子发射器通信用 -----------------------
    [HideInInspector] public bool ShootSubBullet;
    [HideInInspector] public GameObject MasterBulletObject;
    public delegate void BulletDelegate(Bullet bulletRef);
    public delegate void BulletParentDelegate(Bullet bulletRef, Bullet ParentBullet);
    public event BulletDelegate BulletEvent;
    public event BulletDelegateDelay BulletEventDelay;
    public delegate void ShootingDelegate(Shooting shootingRef);
    public delegate void ShootingBulletDelegate(Shooting shootingRef,Bullet bullet);
    public delegate IEnumerator ShootingDelegateDelay(Shooting shootingRef);
    public delegate IEnumerator BulletDelegateDelay(Bullet bulletRef);
    public event ShootingDelegateDelay WhenRollBack;
    public event ShootingDelegate StartNewLoop;
    public event BulletDelegate BulletEventWhenBulletCreate;
    public event BulletDelegate BulletEventWhenBulletDestroy;
    public event BulletParentDelegate BulletEventWhenBulletRestoreMainLevel;
    public event BulletDelegate BulletEventOnDestroyingPlayer;
    public event BulletDelegateDelay BulletEventWhenBulletCreateDelay;
    public event BulletDelegateDelay BulletEventWhenBulletDestroyDelay;
    public event BulletDelegateDelay BulletEventWhenBulletRestoreMainLevelDelay;
    public event BulletDelegateDelay BulletEventOnDestroyingPlayerDelay;
    //如果你需要控制敌人角色，在Shooting的父对象里头获取即可。
    public event ShootingBulletDelegate ShootingEvent;
    public event ShootingDelegate ShootingDestroy;
    public event ShootingDelegate ShootingUsingEvent;
    public event ShootingDelegate ShootingFinishAllShotTask;
    public event ShootingDelegate BeforeShooting;
    public event ShootingDelegateDelay ShootingEventDelay;
    public event ShootingDelegateDelay ShootingDestroyDelay;
    public event ShootingDelegateDelay ShootingUsingDelay;
    public event ShootingDelegateDelay ShootingFinishAllShotTaskDelay;
    public event ShootingDelegateDelay BeforeShootingDelay;
    [HideInInspector]
    public string XMLPath;
    public XmlDocument XmlDocumentary = new XmlDocument();
    public XmlNode Key;
    [HideInInspector]
    public Vector3[] BezierPointTrack;
    [HideInInspector]
    public BezierDrawer BezierCurveController;
    // -------------------- 全类临时变量 -------------------------
    [HideInInspector]
    public float TotalFrame;
    [HideInInspector]
    public Bullet _Masterbullet;
    protected float _shootingTimer;
    protected float _xOffsetTemp;
    protected float _yOffsetTemp;
    protected float _angle = 0;
    protected float _soundTimeCount;
    protected float _pass;
    protected bool _forceshotRequire;
    //发射波数。
    protected int CountTime = 0;
    protected Vector2 _scaleT = Vector2.zero;
    protected float Radius_T = 0;

    private float Ori_Coll;/// { get; private set; }

    protected Vector2 _squareLengthT = Vector2.zero;
    protected List<string> Name = new List<string>();
    protected FieldInfo[] fInfo = typeof(Shooting).GetFields();
    protected FieldInfo fInfoSelected;
    [HideInInspector]
    public Shooting ShootingOrginalData;
    #endregion
    public enum EventList_Shooting { ONSHOTED = 1, ONDESTROYED = 2, ONUSING = 3, ONFINISHALL = 4 }
    public void ClearEventsList(EventList_Shooting t)
    {
        switch ((int)t)
        {
            case 1:
                ShootingEvent = null;
                break;
            case 2:
                ShootingDestroy = null;
                break;
            case 3:
                ShootingUsingEvent = null;
                break;
            case 4:
                ShootingFinishAllShotTask = null;
                break;
        }
    }
    void OnDrawGizmosSelected()
    {
        if (transform == null)
            return;
        Gizmos.DrawIcon(transform.position, "ShootingTitle");
        Gizmos.color = Color.red;
        if (transform.parent != null)
            Gizmos.DrawLine(transform.position, transform.parent.transform.position);

        Gizmos.color = Color.yellow;
        Vector2 _tar = Vector2.left;
        _tar = Quaternion.Euler(new Vector3(0, 0, Angle + transform.rotation.eulerAngles.z)) * (Vector2)Vector2.up;
        Gizmos.DrawRay(transform.position, _tar * Speed / 5);
        Gizmos.color = Color.blue;
        _tar = Quaternion.Euler(new Vector3(0, 0, MoveDirection)) * (Vector2)Vector2.up;
        Gizmos.DrawRay(transform.position, _tar * ShootingSpeed * 0.4f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Radius);
        if (BulletShootingObject != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, BulletShootingObject.transform.position);
        }
    }
    void Awake()
    {
        if (UnprocessingOrginalData == false)
        {
            ResetOrginalData();
            UnprocessingOrginalData = true;
        }
        else
        {
            if (isEnemyShooting)
                ReturnOriginalData();
        }
        thisTransform = this.transform;
        DefaultEnabled = enabled;
        SpellCard a = GetComponentInParent<SpellCard>();
        if (a != null)
        {
            SingleShootingData b = new SingleShootingData(thisTransform.localPosition, enabled, this);
            a.Credentials.DataList.Add(b);
        }
    }
    void Start()
    {
 
        TotalFrame = 0;
        thisTransform.Rotate(new Vector3(0, 0, MoveDirection));
        Radius_T = Radius;
        Ori_Coll = CollisionRadius;
        _squareLengthT = SquareLength;
        thisTransform = this.transform;
    }
    /// <summary>
    /// 强行进行发射并恢复发射器性能，会将发射器啟用.并返回最后一次发射所生成的子弹（List类型）.你也可以指定位置并进行发射。
    /// </summary>
    /// <param name="t">The english letter T means of the target of function .</param>
    /// <param name="useDefault">If set to <c>true</c> use default.</param>
    static public List<Bullet> RecoverShooting(Shooting t, bool useDefault = false, bool forceshot = true,bool setPos = false,float x =0,float y=0,bool resetFrame = true)
    {
        if (Global.GamePause || Global.WrttienSystem)
            return null;
        if (t.SpecialBounsShoot + 1 < 0)
            return null;

        t.enabled = true;
        if (resetFrame)
        t.TotalFrame = 0;
        List<Bullet> _collection = new List<Bullet>();
        if (useDefault)
            t.ReturnOriginalData();
        Vector3 po = Vector3.zero;
        if (setPos)
        {
            po = t.thisTransform.position;
            t.thisTransform.position = new Vector3(x, y, 0);
        }
        if (forceshot)
        {
            for (int i = 0; i != t.SpecialBounsShoot + 1; ++i)
            {
                _collection = t.Shot(t.Way, true, false, 0, 0, i);
            }
            t.TotalShotBatch = 0;
        }
        if (setPos)
            t.thisTransform.position = po;
        return _collection;
    }
    static public void RecoverUsing(Shooting t)
    {
        t.enabled = true;
    }
    /// <summary>
    /// 将该发射器所有数据还原到原始数据，若使setTotalFrame变为True，则控制发射器总生存时间的TotalFrame变量将会被置为0.如果你的事件中有涉及到这个变量，需要想清楚要不要使用这个功能。如果clearUsingSign为true,则ShootWhenStart会被重置，一般情况下ShootWhenStart只会在发射器初始化的时候被重置，其他情况下不能也不允许重置。
    /// </summary>
    /// <param name="setTotalFrame"></param>
	public void ReturnOriginalData(bool setTotalFrame = false, bool clearUsingSign = false)
    {
        if (WhenRollBack != null)
            StartCoroutine(WhenRollBack(this));
        
        Apply(ShootingOrginalData);
        if (setTotalFrame)
            TotalFrame = 0;
    }
    public object Clone(object o)
    {
        return this.MemberwiseClone();
    }

    public void Apply(Shooting o)
    {

        FieldInfo[] properties = typeof(Shooting).GetFields();
        object p = this;
        foreach (var pi in properties)
        {
            if (pi.Name == "ShootingOrginalData" || pi.Name =="thisTransform" || pi.Name == "rollBack") continue;
            object value = pi.GetValue(o);
            pi.SetValue(p, value);

        }

    }
    /// <summary>
    /// 将发射器当前的所有数据设置为原始数据，以后对发射器使用RollBacktoOrginal函数时，就会将该发射器的数据还原到最近一次使用该SetOrginal函数对该发射器设置其原始数据的时候。
    /// </summary>
    /// <param name="setEvent"></param>
    /// <param name="resetCount"></param>
	public void ResetOrginalData(bool setEvent = false, bool resetCount = false)
    {
        ShootingOrginalData = (Shooting)Clone(this);

        if (resetCount)
            CountTime = 0;
    }
    /// <summary>
    /// 发射函数，发射器使用子弹并应用相关参数和事件的行为。它将返回一个装有一堆Bullet对象的List变量，这个变量里的Bullet都指向新创建的子弹对象。（注意：如果你想要在你的事件获得刚发射的子弹集合，通过这个集合对发射器刚发射的子弹进行操作的时候，请使用该发射器的ShotBullet变量，这个变量每次发射器发射更新一次。）
    /// </summary>
    public List<Bullet> Shot(int way = 1, bool forceShot = false, bool useCustomedPos = false, float Pos_X = 0, float Pos_Y = 0, int ShotBatch = 1)
    {
        if (thisTransform == null && !transCheck)
        {
            thisTransform = this.transform;
            transCheck = true;
        }
        if (Global.GlobalSpeed == 0 || Global.GamePause == true)
            return null;

        bool shotSuccess = false;
        if (forceShot == false || _forceshotRequire == true)
        {
            if (CountTime > ShootingShotMaxTime && ShootingShotMaxTime != -1)
            {
                CanShoot = false;
            }
            if (CanShoot == false)
                return null;
            if (TotalFrame < Delay && Global.GamePause)
                return null;
        }
        if (way < 0)
        {
            Debug.LogError("发射的Way数小于0,这是个非常危险的操作，现已经被拦截。");
            return null;
        }
        if (CustomSprite == null && UseCustomSprite)
        {
            Debug.LogWarning("在允许自定义子弹图像的情况下，" + gameObject.name + "处的发射器发射的子弹不带有图像，它的发射将被驳回。请加入它的图像来停止持续显示该提示。");
            return null;
        }
        List<Bullet> bulletCollection = new List<Bullet>();
        if (UseRandomOffset)
        {
            _xOffsetTemp = Random.Range(-XOffset, XOffset);
            _yOffsetTemp = Random.Range(-YOffset, YOffset);
        }
        else
        {
            _xOffsetTemp = 0;
            _yOffsetTemp = 0;
        }
      
        if (RandomAcceleratedSpeed)
        {
            float acceleratedSpeedoffset = Random.Range(-AcceleratedSpeedOffset, AcceleratedSpeedOffset);
            AcceleratedSpeed = acceleratedSpeedoffset;
        }
        if (RandomSpeed)
        {
            float speedoffset = Random.Range(-SpeedOffset + Speed, SpeedOffset + Speed);
            Speed = speedoffset;
            Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);
        }
        _pass++;
        if (UseRandomWay)
        {
            int i = Random.Range(1, WayOffset);
            Way = i;
            Way = Mathf.Clamp(Way, 1, Way);
        }

        float addRotation = Range / (Way);
        if (UseRandomRadius)
        {
            float radius = Random.Range(-RadiusOffset, RadiusOffset);
            Radius = radius;
        }
        if (UseRandomAngle)
        {
            float rotation = Random.Range(-AngleOffset + Angle, AngleOffset + Angle);
            Angle = rotation;
            Angle = Mathf.Clamp(Angle, MinAngle, MaxAngle);
        }
        Angle = Angle % 360.0f;
        float thisRadius = Radius;
        if (AngleIncreament != 0 && UseRandomAngle == false)
            Angle += AngleIncreament;
        if (BeforeShooting != null)
        {
            BeforeShooting(this);
            if (UseThread)
                StartCoroutine(BeforeShootingDelay(this));
        }
        ellipseSize = Quaternion.Euler(new Vector3(0, 0, ellipseScale)) * (Vector3)ellipseSize;
    //    ShotIndex = 0;
        for (int i = 0; i != way; ++i)
        {

            if (!StaticBullet)
            {
                if (UseRandomBulletRotation)
                {
                    float rotation = Random.Range(-BulletRotationOffset, BulletRotationOffset);
                    BulletRotation = rotation;
                }
                if (useEllipse == false)
                {
                    thisRadius += RadiusIncrement;
                }
                if (Global.GameObjectPool_A == null)
                    break;
                Vector3 postion = new Vector3(thisTransform.position.x + Orginal.x + _xOffsetTemp, thisTransform.position.y + Orginal.y + _yOffsetTemp,
                    z: BulletDepth);
                if (useCustomedPos)
                    postion = new Vector3(Pos_X, Pos_Y, BulletDepth);
                Bullet newBullet = null;
                EnemyState newEnemy = null;
                if (!EnableEnemyShot)
                {
                    newBullet = Global.GameObjectPool_A.ApplyBullet();
                    if (Global.GameObjectPool_A.BulletList.Count == 0 || newBullet == null)
                        continue;
                    StressTester.c++;
                    if (Global.GameObjectPool_A.BulletList.Count > 0)
                        Global.GameObjectPool_A.BulletList.Dequeue();

                    
                }
                else
                {
                    newEnemy = Global.GameObjectPool_A.ApplyEnemy();
                    if (newEnemy == null) continue;
                    newBullet = newEnemy.gameObject.GetComponent<Bullet>();
                    newBullet.BulletTransform = newBullet.transform;
                    newBullet.SpriteTransform = newBullet.BulletSpriteRenderer.transform;
                }
               
                if (newBullet == null)
                    continue;
                newBullet.EnableFollow = false;
                if (BulletShootingObject != null)
                {
                    GameObject t = Instantiate(BulletShootingObject, newBullet.BulletTransform);

                    Shooting[] tarObjShootings = t.GetComponentsInChildren<Shooting>(true);
                    newBullet.BulletShooting = tarObjShootings;
                    for (int c = 0; c != tarObjShootings.Length; ++c)
                    {
                        if (tarObjShootings != null)
                        {
                            tarObjShootings[c].enabled = true;
                            if (MakeSubBullet)
                            {

                                tarObjShootings[c].ShootSubBullet = true;

                                tarObjShootings[c].MasterBulletObject = newBullet.gameObject;
                            }

                            // newBullet.BulletShooting[c].transform.position = newBullet.BulletTransform.position;
                            // tarObjShootings[c].gameObject.SetActive(true);
                            tarObjShootings[c].CopySign = true;
                            Debug.Log("YEAR");
                        }
                    }
                    newBullet.EnableFollow = true;
                    newBullet.OtherObject = t;
                    t.gameObject.SetActive(true);

                }
                if (ShootSubBullet && MasterBulletObject != null)
                {
                    newBullet.BulletTransform.parent = MasterBulletObject.transform;

                    newBullet.AsSubBullet = true;
                }
               
                if (FollowPlayer)
                {

                    Angle = Math2D.GetAimToObjectRotation(gameObject, Global.PlayerObject);
                    if (Way % 2 == 0)
                        Angle = Angle + addRotation / 2;
                    if (middleIndex)
                    {
                        lineIndex = (Way / 2 + 1);
                        firstLine = false;
                    }

                    if (firstLine == false)
                        Angle += addRotation * -lineIndex;
                }
                float FinalAngle = Angle + (followRotation
                    && !FollowPlayer ? thisTransform.rotation.eulerAngles.z : 0);
                if (UseCustomSprite)
                    newBullet.BulletSprite = CustomSprite;
                newBullet.BulletTransform.position = postion;
                newBullet.BarrageBatch = ShotBatch;
              
                newBullet.ReadPointTrackSpeed = (int)ReadPointTrackSpeed;
                //newBullet.InverseRotation = InverseRotation;
                Vector3 _orginal = postion;
                newBullet.Rotation = FinalAngle + (i + 1) * addRotation + Random.Range(-ShotMess, ShotMess);
                if (ReadPointTrackSpeed != 0)
                    newBullet.BezierPointTrack = BezierCurveController;
                Vector3 _positon = postion;
                if (useEllipse)
                {

                    //Debug.Log("椭圆的发射角度是<b>" + (newBullet.Rotation).ToString() +"</b>");
                    _positon = Quaternion.AngleAxis(ellipseRotation, thisTransform.position) * _positon;
                    _positon.x += ellipseSize.x * (1 + Radius) * Mathf.Cos(newBullet.Rotation * Mathf.Deg2Rad);
                    _positon.y += ellipseSize.y * (1 + Radius) * Mathf.Sin(newBullet.Rotation * Mathf.Deg2Rad);
                    _positon = Quaternion.Euler(0, 0, thisTransform.rotation.eulerAngles.z + ellipseRotation) * _positon;
                    _positon.Scale(new Vector3(ellipseScale + 1, ellipseScale + 1, 1));
                    newBullet.Rotation = Math2D.GetAimToTargetRotation((Vector2)postion, (Vector2)_positon) + RadiusDirection + Random.Range(-RandomRadiusDirection, RandomRadiusDirection) - Angle;
                    newBullet.TargetRotation = newBullet.Rotation;
                    newBullet.BulletTransform.position = _positon;
                    newBullet.Speed = (Speed + i * SpeedIncreament) * Vector2.Distance(_orginal, _positon);
                }


                if (_Masterbullet != null)
                {
                    newBullet.InverseRotation = true;
                    newBullet.Rotation = _Masterbullet.Rotation;
                }
                newBullet.BulletTransform.eulerAngles = new Vector3(0, 0, newBullet.Rotation);
                if (useEllipse == false)
                {
                    if (Radius != 0)
                        newBullet.BulletTransform.Translate(Vector2.up * thisRadius, Space.Self);
                    newBullet.Speed = SingleWayRandomSpeed ? Random.Range(MinSpeed, MaxSpeed) : Speed + i * SpeedIncreament * GlobalSpeedPercent;
                    newBullet.TargetSpeed = BulletTargetSpeed;
                }
                if (RadiusDirection != 0 || RadiusDirection != 0)
                {
                    float are = newBullet.Rotation + RadiusDirection + Random.Range(-RandomRadiusDirection, RandomRadiusDirection);
                    newBullet.BulletTransform.eulerAngles = new Vector3(0, 0, are - Angle);
                    newBullet.Rotation = are - Angle;
                }

                //newBullet.AnimationControl.enabled = true;


                if (Vector2.Distance(Global.PlayerObject.gameObject.transform.position, thisTransform.position) < Radius && FollowPlayer == true && DisallowInverse == false && Global.PlayerObject != null)
                    newBullet.Speed = -newBullet.Speed;

                if (FollowAngle)
                {

                    if (thisTransform.parent != null)
                        Angle = (360 + thisTransform.parent.transform.localEulerAngles.z);

                }

                bulletIndexChecking = i;

                SetBulletState(newBullet);

                if (MaterialIndex >= 0)
                    newBullet.BulletSpriteRenderer.material = Global.MaterialCollections_A[MaterialIndex];

                bulletCollection.Add(newBullet);

                newBullet.FollowObjectWithSameAngle = FollowObjectWithSameAngle;
                newBullet.DestroyMode = false;

                newBullet.EnableBulletEvent = false;
                newBullet.SpriteTransform.localScale = Vector3.one;
                newBullet.StaticBullet = StaticBullet;
                newBullet.ScaleUpdate = ScaleUpdate;
                newBullet.StayTrigger = StayTrigger;
                newBullet.RotationUpdate = RotationUpdate;
                newBullet.StayAnimPlayer = StayAnimPlayer;
               
                    newBullet.BulletTransform.eulerAngles = new Vector3(0, 0, newBullet.Rotation);
                
                newBullet.StayBulletEvent = StayBulletEvent;
                newBullet.StayCollision = StayCollision;
                if (BulletEvent != null && UseBulletEvent)
                {
                    newBullet.EnableBulletEvent = true;
                    newBullet.BulletEvent += BulletEvent;
                    newBullet.BulletEventDelay += BulletEventDelay;
                }

                if (UseBulletEventWhenBulletDestroy && BulletEventWhenBulletDestroy != null)
                {
                    newBullet.BulletEventDestroy += BulletEventWhenBulletDestroy;
                    newBullet.BulletEventDestroyDelay += BulletEventWhenBulletDestroyDelay;
                }

                if (UseBulletEventWhenBulletRestoreMainLevel && BulletEventWhenBulletRestoreMainLevel != null)
                {
                    newBullet.BulletEventWhenBulletParentChanged += BulletEventWhenBulletRestoreMainLevel;
                    newBullet.BulletEventWhenBulletRestoreMainLevelDelay += BulletEventWhenBulletRestoreMainLevelDelay;
                }

                if (UseBulletEventOnDestroyingPlayer && BulletEventOnDestroyingPlayer != null)
                {
                    newBullet.BulletEventOnDestroyingPlayer += BulletEventOnDestroyingPlayer;
                    newBullet.BulletEventOnDestroyingPlayerDelay += BulletEventOnDestroyingPlayerDelay;
                }

                newBullet.BrokenBulletColor = BrokenBulletColor;

                if (UseCustomCollisionGroup)
                {
                    newBullet.UseCustomCollisionGroup = UseCustomCollisionGroup;
                    GameObject T = Instantiate(CustomCollisionGroup);
                    newBullet.CustomCollisionGroup = T.GetComponentsInChildren<CustomCollision>();
                    T.transform.parent = newBullet.BulletTransform;
                    T.gameObject.transform.localPosition = Vector3.zero;
                    T.SetActive(true);
                    newBullet.CustomCollisionGroupMainController = T;
                }

                if (newBullet.gameObject.activeSelf && NoCreateAnimation == false)
                {


                    newBullet.BulletSpriteRenderer.sprite = CreatingCustomSprite;
                    Color _Color = CreateBulletColor;
                    _Color.a = 0;
                       newBullet.BulletSpriteRenderer.color = _Color;
             //       newBullet.AnimationControl.PlayInFixedTime(BulletCreatingAnimationName, 0, 0);
                }

               // newBullet.AnimationControl.speed = 1.0f / Global.GlobalSpeed;
                newBullet.ChangeInverseRotation = ChangeInverseRotation;
                newBullet.BulletBreakingAnimationName = BulletBreakingAnimationName;
                newBullet.BulletCreatingAnimationName = BulletCreatingAnimationName;
                newBullet.Scale = Scale;
                newBullet.OriSpd = Speed; 
                newBullet.trackData = trackDataInfoRemaining;
                newBullet.CalculateAngle = CalculateAngle;
                newBullet.DestroyAnimSprites = useDefaultSprite ? Global.DestroySprites : DestroyAnimSprites;
                newBullet.CreatingCustomSprite = CreatingCustomSprite;
                newBullet.LoopTrack = LoopTrack;
                newBullet.BulletSpriteRenderer.gameObject.transform.localScale = Scale;
                newBullet.RotatorInAsix = RotatorInAsix;
                newBullet.TotalLiveFrame = 0;
                newBullet.BulletIndex = i;
                newBullet.shootingIndex = ShotIndex;
                newBullet.SpriteTransform.localScale = Vector3.one;
                UseCreateEvent(newBullet);

                ShotBullet = bulletCollection;
                //  newBullet.ParentShooting = this;
                TotalShotBatch = ShotBatch;
                shotSuccess = true;

                if (ShootingEvent != null && UseShootingEvent)
                    ShootingEvent(this, newBullet);
                if (ShootingEventDelay != null)
                    StartCoroutine(ShootingEventDelay(this));

                ++ShotIndex;
                if (AvoidPlayer)
                {
                    if (Global.PlayerObject != null && Vector2.Distance(newBullet.BulletTransform.position, Global.PlayerObject.gameObject.transform.position) < AvoidRange)
                    {
                        newBullet.NoDestroyAnimation = true;
                        newBullet.DestroyBulletETC();
                        continue;
                    }
                }
                if (NoCreateAnimation)
                {
                    newBullet.animationController.PlayAnimation();
                }

                if (EnableEnemyShot)
                {
                    Vector3 a = newBullet.transform.position;
                    a.x += _xOffsetTemp;
                    a.y += _yOffsetTemp;
                    Enemy t = newEnemy.SetEnemy(a, ShootingObject, AnimationIndex, EnemyHP);
                    t.BounsScoreNumber = BounsScoreNumber;
                    t.BounsPowerNumber = BounsPowerNumber;
                    t.BounsFullPowerNumber = BounsFullPowerNumber;
                    t.BounsLivePieceNumber = BounsLivePieceNumber;
                    newBullet.InverseRotation = true;
                    newBullet.AsEnemyMovement = true;
                    newBullet.Reusable = !StopWhenDisable;
                    t.gameObject.SetActive(true);
                }
            }
            else {
                if (Global.GameObjectPool_A == null)
                    break;
                Vector3 postion = new Vector3(thisTransform.position.x + Orginal.x + _xOffsetTemp, thisTransform.position.y + Orginal.y + _yOffsetTemp,
                    z: BulletDepth);
                if (useCustomedPos)
                    postion = new Vector3(Pos_X, Pos_Y, BulletDepth);
                Bullet newBullet = Global.GameObjectPool_A.ApplyBullet();
                if (Global.GameObjectPool_A.BulletList.Count == 0 ||newBullet == null  )
                    continue;
                StressTester.c++;
                if (Global.GameObjectPool_A.BulletList.Count != 0)
                    Global.GameObjectPool_A.BulletList.Dequeue();
                float Angle_t = Angle + (followRotation
                   && !FollowPlayer ? thisTransform.rotation.eulerAngles.z : 0);
                newBullet.Rotation =  Angle_t + (i + 1) * addRotation + Random.Range(-ShotMess, ShotMess);
              
               
                SetBulletState(newBullet);
                newBullet.Use = true;
                newBullet.BulletTransform.position = postion;
                if (useEllipse == false)
                {
                    if (Radius != 0)
                        newBullet.BulletTransform.Translate(Vector2.up * thisRadius, Space.Self);
                }
                if (UseCustomSprite)
                    newBullet.BulletSprite = CustomSprite;
                newBullet.animationController.PlayAnimation();
                newBullet.BulletSpriteRenderer.gameObject.transform.localScale = Scale;
                if (MaterialIndex >= 0)
                    newBullet.BulletSpriteRenderer.material = Global.MaterialCollections_A[MaterialIndex];
                newBullet.TotalLiveFrame = 0;
                if (useEllipse)
                {
                    Vector3 _positon = postion;
                    //Debug.Log("椭圆的发射角度是<b>" + (newBullet.Rotation).ToString() +"</b>");
                    _positon = Quaternion.AngleAxis(ellipseRotation, thisTransform.position) * _positon;
                    _positon.x += ellipseSize.x * (1 + Radius) * Mathf.Cos(newBullet.Rotation * Mathf.Deg2Rad);
                    _positon.y += ellipseSize.y * (1 + Radius) * Mathf.Sin(newBullet.Rotation * Mathf.Deg2Rad);
                    _positon = Quaternion.Euler(0, 0, thisTransform.rotation.eulerAngles.z + ellipseRotation) * _positon;
                    _positon.Scale(new Vector3(ellipseScale + 1, ellipseScale + 1, 1));
                    newBullet.Rotation = Math2D.GetAimToTargetRotation((Vector2)postion, (Vector2)_positon) + RadiusDirection + Random.Range(-RandomRadiusDirection, RandomRadiusDirection) - Angle;
                    newBullet.TargetRotation = newBullet.Rotation;
                    newBullet.BulletTransform.position = _positon;
                }
                if (BulletEvent != null && UseBulletEvent)
                {
                    newBullet.EnableBulletEvent = true;
                    newBullet.BulletEvent += BulletEvent;
                    newBullet.BulletEventDelay += BulletEventDelay;
                }

                if (UseBulletEventWhenBulletDestroy && BulletEventWhenBulletDestroy != null)
                {
                    newBullet.BulletEventDestroy += BulletEventWhenBulletDestroy;
                    newBullet.BulletEventDestroyDelay += BulletEventWhenBulletDestroyDelay;
                }

                if (UseBulletEventWhenBulletRestoreMainLevel && BulletEventWhenBulletRestoreMainLevel != null)
                {
                    newBullet.BulletEventWhenBulletParentChanged += BulletEventWhenBulletRestoreMainLevel;
                    newBullet.BulletEventWhenBulletRestoreMainLevelDelay += BulletEventWhenBulletRestoreMainLevelDelay;
                }

                if (UseBulletEventOnDestroyingPlayer && BulletEventOnDestroyingPlayer != null)
                {
                    newBullet.BulletEventOnDestroyingPlayer += BulletEventOnDestroyingPlayer;
                    newBullet.BulletEventOnDestroyingPlayerDelay += BulletEventOnDestroyingPlayerDelay;
                }
               
            }
        }
       


        if (PlayerSound && _soundTimeCount > IntervalSoundTime + 3 && shotSuccess)
        {
            _soundTimeCount = 0;
            for (int i = 0; i != Sound.Length; ++i)
            {

                AudioQueue.Play(Sound[i]);

                //.PlayOneShot(Sound[i].clip,0.6f);
            }
        }
        if (EnableRandomTimer)
        {
            Timer = Random.Range(RandomMinTimer, RandomMaxTimer);
            Timer = Mathf.Clamp(Timer, 0, 0xfffffff);
        }
        return bulletCollection;
    }
    public int ReturnTotalShotBatch()
    {
        return TotalShotBatch;
    }
    public void UseCreateEvent(Bullet newBullet)
    {
        if (BulletEventWhenBulletCreate != null)
            BulletEventWhenBulletCreate(newBullet);
        if (BulletEventWhenBulletCreateDelay != null)
            StartCoroutine(BulletEventWhenBulletCreateDelay(newBullet));
    }
    public void AddBulletEvent(Bullet newBullet)
    {
        if (BulletEvent != null && UseBulletEvent)
        {
            newBullet.BulletEvent += BulletEvent;
            newBullet.BulletEventDelay += BulletEventDelay;
        }
    }

    public void SetBulletState(Bullet newBullet)
    {
     //   newBullet.AnimationControl.speed = CreateAnimationSpeed;
        newBullet.AcceleratedSpeed = AcceleratedSpeed;
        newBullet.AcceleratedRotation = BulletAcceleratedRotation;
        newBullet.MaxLiveFrame = (int)BulletLiveFrame;
        newBullet.Use = true;
        //Global.GameObjectPool_A.BulletList_State [newBullet.ID] = true;
        newBullet.AimToPlayer = AimToPlayer;
        newBullet.CreateAnimationPlayed = false;
        newBullet.Radius = CollisionRadius;
        newBullet.SquareLength = SquareLength;
        newBullet.DonDestroy = DonDestroy;
        newBullet.UseThread = UseThread;
        newBullet.Tag = BulletTag;
        newBullet.NoCollisionWhenAlphaLow = NoCollisionWhenAlphaLow;
        newBullet.ChangeSpeedSmoothly = ChangeSpeedSmoothly;
        newBullet.ChangeSpeedPercentage = ChangeSpeedPrecent;
        newBullet.EnableTrail = useTrails;
        newBullet.showTrails = useTrails;
        newBullet.EnabledAcceleratedGlobalOffset = EnabledAcceleratedGlobalOffset;
        newBullet.EnabledGlobalOffset = EnabledGlobalOffset;
        newBullet.ChangeRotationSmoothly = ChangeBulletRotationSmoothly;
        newBullet.UseCustomAnimation = UseBulletAnimation;
        newBullet.useDefaultSprite = useDefaultSprite;
        newBullet.BulletFramesSprites = BulletSpritePerFrame;
        newBullet.nextFramewaitTime = WaitingTime;
        newBullet.DonDestroyIfMasterDestroy = DonDestroyIfMasterDestroy;
        newBullet.ForceObject = ForceObject;
        newBullet.UseForce = EnableForForce;
        newBullet.UseForceComponent = EnableForForce;
        newBullet.UseTriggerComponent = ApplyTrigger;
        newBullet.TargetSpeed = BulletTargetSpeed;
        newBullet.TargetRotation = BulletTargetRotation;
        newBullet.TrailUpdate = TrailUpdate;
        newBullet.SubTrail = useTrails;
        newBullet.TrailLength = TrailLength;
        newBullet.FilpX = FilpX;
        newBullet.FilpY = FilpY;
        newBullet.delayBulletEventRunTime = delayRunTime;
        if (ChangeBulletRotationSmoothly)
        {
            newBullet.TargetRotation = BulletRotation;
            newBullet.RotationSmoothType = RotationSmoothType;
            newBullet.ChangeRotationPercentage = BulletRotationSmoothlyPrecent;
        }
        if (ChangeSpeedSmoothly)
        {
            newBullet.TargetSpeed = Speed;
            newBullet.SpeedSmoothType = SpeedSmoothType;
        }
        newBullet.DestroyWhenDontRender = DestroyWhenDonRender;
        newBullet.AcceleratedSpeedDirectionNow = GlobalOffset;
        newBullet.AcceleratedSpeedDirectionPer = AcceleratedGlobalOffset;
        newBullet.NoCollisionWhenCreateAnimationPlaying = NoCollisionWhenCreateAnimationPlaying;
        newBullet.NoDestroyAnimation = NoDestroyAnimation;
        newBullet.noDestroy = InvaildDestroy;
        newBullet.TriggerList = TriggerList;
        newBullet.InverseRotateDirection = BulletRotation;
        newBullet.BulletColor = BulletColor;
        newBullet.BulletCollision = BulletCollision;
        newBullet.InverseRotation = InverseRotation;
        newBullet.Grazed = Graze;
        newBullet.UseAlphaWithDepth = UseAlphaWithDepth;
        newBullet.minDepth = minDepth;
        newBullet.maxDepth = maxDepth;
        newBullet.TwistedRotation = TwistedRotation;
        newBullet.inTrigger.Clear();
        if (newBullet.showTrails && newBullet.TrailShooting != null)
            newBullet.TrailShooting.CustomSprite = CustomSprite;
        for (int i = 0; i != newBullet.TriggerList.Count; ++i) {  
            newBullet.inTrigger.Add(false);
            newBullet.inStayTrigger.Add(0);
            newBullet.enterTimeTrigger.Add(0);

        }
    }
    /// <summary>
    /// 重置发射器的计数状态。
    /// </summary>
    public void ResetCountTime() { CountTime = 0; TotalFrame = 0; }
    public void UseShootingUsingEvent()
    {

        if (ShootingUsingEvent != null)
            ShootingUsingEvent(this);
    }
    void Update()
    {
        if (CopySign)
            UpdateWithSelfComponent = true;
        if (UpdateWithSelfComponent)
            UpdateInfo();
    }
    public override void UpdateInfo()
    {
        
        _soundTimeCount += Global.GlobalSpeed;
        if (Global.GamePause || Canceled)
            return;
        if (Global.WrttienSystem && IgnorePlot == false)
            return;
        if (lineIndex > Way)
            lineIndex = Way - 1;
        if (lineIndex < 0)
            lineIndex = 0;
        if (Global.SpellCardExpressing && ignoreSCExpression == false)
            return;

        UseShootingUsingEvent();
        TotalFrame += Global.GlobalSpeed * Global.GlobalBulletSpeed;
        _shootingTimer += 1 * Global.GlobalSpeed;





        if (UseDynamicsCollision)
        {
            if (_scaleT != Scale)
            {
                _scaleT = Scale;
                CollisionRadius = Ori_Coll * _scaleT.sqrMagnitude;
                SquareLength = new Vector2(SquareLength.x * _squareLengthT.x, SquareLength.y * _squareLengthT.y);
            }
        }
        else
        {
            CollisionRadius = Radius_T;
            SquareLength = _squareLengthT;
        }

        if (MoveShooting)
        {
            MoveShootingProgress();
        }
        if (_shootingTimer >= Timer && TotalFrame > Delay)
        {
            ShotEvent();
            _pass = 0;

        }/*
        if (ShootWhenStart)
        {
            ShotEvent ();
            ShootWhenStart = false;
        }*/
        if (TotalFrame >= MaxLiveFrame + (CaluateDelayToPercent ? Delay : 0))
        {
            if (rollBack)
            {
                if (order == EventOrder.BEFORE)
                    ReturnOriginalData();
            }
            if (ShootingDestroy != null)
                ShootingDestroy(this);

            if (rollBack)
            {
                if (order == EventOrder.AFTER)
                    ReturnOriginalData();
            }
            if (Reusable == false)
            {
                enabled = false;
                UnprocessingOrginalData = false;
            }
            else
            {
                if (StartNewLoop != null)
                    StartNewLoop(this);
                CountTime = 0;
                UnprocessingOrginalData = false;

                CanShoot = true;
                TotalFrame = 0;
                ShotIndex = 0;
                _shootingTimer -= Timer;
            }
        }
    }
    public void MoveShootingProgress()
    {
        if (ShootingAcceleratedSpeed != 0)
            ShootingSpeed += ShootingAcceleratedSpeed;
        if (ShootingSpeed != 0)
            gameObject.transform.Translate(Vector3.up * ShootingSpeed / 100 * Global.GlobalSpeed * (Application.isEditor ? 1 : Global.GlobalBulletSpeed), Space.Self);
        if (RotationSpeed != 0)
            MoveDirection += RotationSpeed;
        if (ShotAtPlayer)
            MoveDirection = Math2D.GetAimToObjectRotation(gameObject, Global.PlayerObject);
        gameObject.transform.eulerAngles = new Vector3(0, 0, MoveDirection);
    }
    private void ShotEvent()
    {
        if (!Application.isPlaying) return;
        if (FollowPlayer)
            Angle = -_angle;
        _shootingTimer = 0;
        if (SpecialBounsShoot + 1 < 0 || (CountTime > ShootingShotMaxTime && ShootingShotMaxTime != -1))
            return;
        ++CountTime;
        for (int i = 0; i != SpecialBounsShoot + 1; ++i)
        {
            Shot(Way, _forceshotRequire, false, 0, 0, i);
            if (_forceshotRequire)
                _forceshotRequire = false;
        }
        TotalShotBatch = 0;
        if (ShootingFinishAllShotTask != null)
            ShootingFinishAllShotTask(this);
    }
    public int ShootingBatch()
    {
        return TotalShotBatch;
    }
    /// <summary>
    /// 重設發射器到可以重新發射的狀態（直接調用RollBacktoOrginal不行！）
    /// </summary>
    /// <param name="a">The alpha component.</param>
    static public void ResetShooting(Shooting a)
    {
        a.enabled = true;
        a.TotalFrame = 0;
        a.ResetCountTime();
        a.ReturnOriginalData(true, true);
    }

}