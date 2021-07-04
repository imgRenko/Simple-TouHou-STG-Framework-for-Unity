using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Runtime.CompilerServices;
/// <summary>
/// 可视化编程使用。在可视化编程中，本类可缩减节点数量，提高子弹刷新事件的执行效率。在子弹创建时事件使用它。为了更方便将CrazyStorm融合到本框架中，这一个类的操作方式与CS类似。
/// </summary>
[System.Serializable]
public class BulletTrackProduct
{
    public enum Condition
    {
        Frame,X, Y , None
    }
    public enum Operation
    {
        Less, EqualLess, Equal, More, MoreEqual
    }

    public enum Operator
    {
        Life, ScaleX, ScaleY, R, G, B, A, Direction, Rotation, Speed, AccSpeed, AccSpeedRotaion, ElliX, ElliY,Radius,BulletSprite,Trail,Invaild,
    }
    public enum CurveMethod
    {
        Replace,Add, Minus
    }
    [LabelText("第一判据")]
    public Condition condition1;
    [LabelText("比较方式")]
    public Operation Method1;
    [LabelText("判断常数1")]
    public float Const1;
    [LabelText("仅唯一判据")]
    public bool onlyCondition1 = true;
    [LabelText("第二判据")]
    [HideIf("onlyCondition1")]
    public Condition condition2;
    [LabelText("比较方式")]
    [HideIf("onlyCondition1")]
    public Operation Method2;
    [HideIf("onlyCondition1")]
    [LabelText("判断常数2")]
    public float Const2;

    [LabelText("变量变化方法")]
    public CurveMethod curveMethod;
    [HideIf("onlyCondition1")]

    public bool And;
    [LabelText("欲操作变量")]
    public Operator valueOperator;
  
    [LabelText("改变时间")]
    public float changeTime = 1;
    [LabelText("最大操作次数")]
    public int operationMaxTime;

    [LabelText("条件自增")]
    public float conditionIncreament;
    [LabelText("条件自增间隔")]
    public float conditionIncrementInterval;

    //仅在计算方式为Replace时，使用曲线;
    [ShowIf("curveMethod", CurveMethod.Replace)]
    [LabelText("运算用曲线")]
    public AnimationCurve calcateCurve;
   
    [LabelText("布尔值(如果参数为布尔值)")]
    public bool boolValue;
    [ShowIf("curveMethod", CurveMethod.Replace)]
 
    [LabelText("随机关键帧序号")]
    public int keyRandomIndex = 0;

    [ShowIf("curveMethod", CurveMethod.Replace)]
    [LabelText("随机范围")]
    public float RandomRange = 0;
    [HideIf("curveMethod", CurveMethod.Replace)]
    [LabelText("增加/减少")]
    public float Arrvage;
    [ShowIf("valueOperator", Operator.Rotation)]
    [LabelText("瞄准玩家")]
    public bool toPlayer = false;
    [LabelText("取子弹变量")]
    public bool changeToValue = false;
    [ShowIf("changeToValue")]
    [LabelText("变量名")]
    public string valueName;
    [LabelText("取表格变量")]
    public bool graphValue = false;

    [ShowIf("graphValue")]
    [LabelText("变量名")]
    public string globalValueName;

    private float unitedArrvage = 0;

    private Bullet bullet;

    private float tempArrvage;

    

    public void Init(Bullet tarBullet)
    {
        bullet = tarBullet;
        if (RandomRange != 0)
        {
            if (toPlayer == false)
            {
                float value = calcateCurve.keys[keyRandomIndex].value + Random.Range(-RandomRange, RandomRange);
                float time = calcateCurve.keys[keyRandomIndex].time;
                calcateCurve.RemoveKey(keyRandomIndex);
                calcateCurve.AddKey(time, value);
            }

        }
        tempArrvage = Arrvage + Random.Range(-RandomRange, RandomRange);
        unitedArrvage = tempArrvage / changeTime;
        Executed = false;
       
    }

    private bool isValueEnter(float calcateValue, float constValue, Operation operation)
    {
        switch (operation)
        {
            case Operation.Equal:
                return calcateValue == constValue;
            case Operation.EqualLess:
                return calcateValue <= constValue;
            case Operation.More:
                return calcateValue > constValue;
            case Operation.MoreEqual:
                return calcateValue >= constValue;
            case Operation.Less:
                return calcateValue < constValue;

        }
        return false;
    }

    private bool ConditionCalcate(Condition condition, Operation method)
    {
        bool boolCondition = false;
        switch (condition)
        {
            case Condition.None:
                boolCondition = true;
                break;
            case Condition.Frame:
                boolCondition = isValueEnter(bullet.TotalLiveFrame, Const1, method);
                break;
            case Condition.X:
                boolCondition = isValueEnter(bullet.BulletTransform.position.x, Const1, method);
                break;
            case Condition.Y:
                boolCondition = isValueEnter(bullet.BulletTransform.position.y, Const1, method);
                break;
        }
        return boolCondition;
    }
    private float frameCount;
    public void UpdateValue()
    {
        if (conditionIncrementInterval != 0)
        {
            frameCount += Global.GlobalSpeed;
            if (frameCount > conditionIncrementInterval)
            {
                frameCount = 0;
                Const1 += conditionIncreament;
                Const2 += conditionIncreament;
            }
        }
        if (Executed)
        {
            Do();
            return;
        }
        bool boolCondition1, boolCondition2;
        if (!onlyCondition1)
        {
            boolCondition1 = ConditionCalcate(condition1, Method1);
            boolCondition2 = ConditionCalcate(condition2, Method2);

            if (boolCondition1 && boolCondition2 && And) Do();
            if ((boolCondition1 || boolCondition2) && !And) Do();
        }
        else
        {
            boolCondition1 = ConditionCalcate(condition1, Method1);
            if (boolCondition1) Do();
        }
    }

    private float executedTime = 0;

    private bool Executed = false;

    private int executedCount = 0;

    private float defRotate = 0;

    

    private XNode.NodeGraph Graph;

    public void SetGraph(XNode.NodeGraph graph) {
        Graph = graph;
    }
        
    private float PlayerAngle = 0;

    private float RandomInfo = 0;
    private void Do()
    {

        if (executedCount > operationMaxTime && operationMaxTime != 0)
            return;
        if (!Executed)
        {
            tempArrvage = Arrvage + Random.Range(-RandomRange, RandomRange);
            unitedArrvage = tempArrvage / changeTime;
            executedTime = bullet.TotalLiveFrame;
            Executed = true;
            executedCount++;
            if (toPlayer)
            {
                changeToValue = false;
                graphValue = false;
                PlayerAngle = bullet.GetAimToPlayerObjectRotation();
                //Debug.Log(PlayerAngle);
                defRotate = bullet.Rotation;
                if (RandomRange != 0)
                {
                    RandomInfo = Random.Range(-RandomRange, RandomRange);
                    
                   
                }
                tempArrvage = PlayerAngle + RandomInfo;
                unitedArrvage = tempArrvage / changeTime;
            }
            if (changeToValue) {
                toPlayer = false;
                graphValue = false;
                float r = 0;
                bullet.tempFloatPairs.TryGetValue(valueName, out r);
                int length = calcateCurve.keys.Length;
                float time = calcateCurve.keys[length - 1].time;
                calcateCurve.RemoveKey(length - 1);
                calcateCurve.AddKey(time, r);
                if (RandomRange != 0)
                {
                    RandomInfo = Random.Range(-RandomRange, RandomRange);
                   

                }
                tempArrvage = r + RandomInfo;
                unitedArrvage = tempArrvage / changeTime;
            }
            if (graphValue)
            {
                toPlayer = false;
                changeToValue = false;
                float r = 0;
                int index = 0;
                Graph.valueNode.TryGetValue(globalValueName, out index);
                r = (float)Graph.nodes[index].GetOutputPort("变量值").GetOutputValue();
                int length = calcateCurve.keys.Length;
                float time = calcateCurve.keys[length - 1].time;
                calcateCurve.RemoveKey(length - 1);
                calcateCurve.AddKey(time, r);
                if (RandomRange != 0)
                {
                    RandomInfo = Random.Range(-RandomRange, RandomRange);


                }
                tempArrvage = r + RandomInfo;
                unitedArrvage = tempArrvage / changeTime;
            }
        }

        float a = bullet.TotalLiveFrame, b = executedTime, c = changeTime;
        float percentResult = (a - b) / (c);
        if (percentResult > 1)
        {
            Executed = false;
            return;
        }
        switch (valueOperator)
        {
            case Operator.Direction:
                ChangeValue(ref bullet.InverseRotateDirection, percentResult);
                break;
            case Operator.Rotation:
                ChangeValue(ref bullet.Rotation, percentResult);
                break;
            case Operator.Life:
                float r = ((float)bullet.MaxLiveFrame);
                ChangeValue(ref r, percentResult);
                break;
            case Operator.Speed:
                ChangeValue(ref bullet.Speed, percentResult);
                break;
            case Operator.AccSpeed:
                ChangeValue(ref bullet.AcceleratedSpeed, percentResult);
                break;
            case Operator.Radius:
                ChangeValue(ref bullet.Radius, percentResult);
                break;
            case Operator.AccSpeedRotaion:
                ChangeValue(ref bullet.AcceleratedRotation, percentResult);
                break;
            case Operator.A:
                ChangeValue(ref bullet.BulletColor, percentResult,3);
                break;
            case Operator.B:
                ChangeValue(ref bullet.BulletColor, percentResult, 2);
                break;
            case Operator.G:
                ChangeValue(ref bullet.BulletColor, percentResult, 1);
                break;
            case Operator.R:
                ChangeValue(ref bullet.BulletColor, percentResult,0);
                break;
            case Operator.ScaleX:
                ChangeValue(ref bullet.Scale, percentResult, 0);
                break;
            case Operator.ScaleY:
                ChangeValue(ref bullet.Scale, percentResult, 1);
                break;
            case Operator.Trail:
                ChangeValue(ref bullet.EnableTrail, boolValue);
                break;
            case Operator.Invaild:
                ChangeValue(ref bullet.DonDestroy, boolValue);
                break;

        }
    }


    public void ChangeValue(ref float value, float percent)
    {
        switch (curveMethod)
        {
            case CurveMethod.Replace:
                if (toPlayer == false)
                    value = calcateCurve.Evaluate(percent);
                else
                {
                    value = Mathf.Lerp(defRotate, ((PlayerAngle + RandomInfo)), calcateCurve.Evaluate(percent));
                    //Debug.Log(percent);
                }
                break;
            case CurveMethod.Add:
                value += unitedArrvage;
                break;
            case CurveMethod.Minus:
                value -= unitedArrvage;
                break;

        }
    }
    public void ChangeValue(ref Color32 value, float percent,int axis)
    {
        float r = 0;
        switch (axis)
        {
            case 0:
                r = (float)value.r;
                break;
            case 1:
                r = (float)value.g;//
                break;
            case 2:
                r = (float)value.b;//
                break;
            case 3:
                r = (float)value.a;//
                break;
        }
       
        switch (curveMethod)
        {
            case CurveMethod.Replace:
                r = calcateCurve.Evaluate(percent);
                break;
            case CurveMethod.Add:
                r += unitedArrvage;
                break;
            case CurveMethod.Minus:
                r -= unitedArrvage;
                break;

        }
        switch (axis) {
            case 0:
                value.r = (byte)r;
                break;
            case 1:
                value.g = (byte)r;
                break;
            case 2:
                value.b = (byte)r;
                break;
            case 3:
                value.a = (byte)r;
                break;
        }
    }
    public void ChangeValue(ref Vector2 value, float percent, int axis)
    {
        float r = 0;
        switch (axis)
        {
            case 0:
                r = value.x;
                break;
            case 1:
                r = value.y;
                break;

        }
        switch (curveMethod)
        {
            case CurveMethod.Replace:
                r = calcateCurve.Evaluate(percent);
                break;
            case CurveMethod.Add:
                r += unitedArrvage;
                break;
            case CurveMethod.Minus:
                r -= unitedArrvage;
                break;

        }
        switch (axis)
        {
            case 0:
                value.x = r;
                break;
            case 1:
                value.y = r;
                break;
       
        }
    }
    public void ChangeValue(ref bool value, bool percent)
    {
        value = percent;
    }
    public BulletTrackProduct Clone() {
        return (BulletTrackProduct)this.MemberwiseClone();
    }
}


/// <summary>
/// 曲线事件使用的命令控制器
/// </summary>
[System.Serializable]
public class CommandController {
    public FrameSetting.Infusion CaluateMethod = FrameSetting.Infusion.REPLACE;
    public int MinIndex = 0;
    public float RandomValue = 0;
    public int IndexChecking = 0;
    public float RandomCaValue;
    public bool Once = true;
    public object c;
    public FrameSetting.Original DefaultValue = FrameSetting.Original.ORINGIAL;
    public FrameInfo[] ResultDefaultValue = new FrameInfo[4] { new FrameInfo(), new FrameInfo() , new FrameInfo() ,new FrameInfo() };
    public Shooting compareShooting;
    public float Const = 0;
    public List<DynamicParmCurve> CurveChange = new List<DynamicParmCurve>();
    [SerializeField]
    public List<GroupString> groupStrings = new List<GroupString>();
    public int UseTime = 0;
    public string groupTag;
    [HideInInspector]
    public int CalculateCircle = 0, _C;
    public int looper = 0; public float timeCount = 0;

    public bool Enable { get; internal set; }
}

[AddComponentMenu("东方STG框架/框架核心/敌人子弹")]
[System.Serializable]
public class Bullet : STGComponent
{
    // -------------------- public value for Unity -------------------------
    [FoldoutGroup("总体控制", expanded: false)]
    [LabelText("作为敌人移动器使用")]
    public bool AsEnemyMovement = false;
    [FoldoutGroup("总体控制", expanded: false)]
    [LabelText("目的敌人")]
    [ShowIf("AsEnemyMovement")]
    public Enemy Character;
    [FoldoutGroup("总体控制", expanded: false)]
    [LabelText("使用协程事件")]
    public bool UseThread = true;
    [FoldoutGroup("总体控制", expanded: false)]
    [LabelText("可复用子弹")]
    public bool Reusable = false;
    [FoldoutGroup("总体控制", expanded: false)]
    [LabelText("减低子弹销毁可能性")]
    public bool noDestroy = false;
    [FoldoutGroup("总体控制", expanded: false)]
    [LabelText("涉及全局速度")]
    public bool UseGlobalSpeed = true;
    [FoldoutGroup("总体控制", expanded: false)]
    [LabelText("于特别情况暂停运动")]
    public bool StopOnSpecialSituation;
    [FoldoutGroup("标记", expanded: false)]
    [LabelText("子弹Tag")]
    [Multiline]
    public string Tag = "";
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("子弹运动速度")]
    public float Speed = 1f;
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("子弹运动加速度")]
    public float AcceleratedSpeed = 0f;
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("轨迹长度")]
    public int TrailLength = 20;
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("轨迹刷新速度")]
    public float TrailUpdate = 1;
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("平滑改变子弹速度")]
    public bool ChangeSpeedSmoothly = false;
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("子弹速度插值速度")]
    public float ChangeSpeedPercentage = 0.05f;
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("速度插值方式")]
    public MoveCurves SpeedSmoothType = MoveCurves.Lerp;
    [HideInInspector]
    public Vector2 AcceleratedSpeedDirectionNow; // 目前的加速度方向
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("全局加速度方向")]
    public Vector2 AcceleratedSpeedDirectionPer; //每秒更换的加速度方向。
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("子弹速度方向")]
    public float Rotation = 0f; // 子彈旋轉方向，也會影響相關的旋轉方向。會影響EngleAngle的值
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("子弹角速度")]
    public float AcceleratedRotation = 0f;
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("平滑改变角速度")]
    public bool ChangeRotationSmoothly = false;
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("角速度插值速度")]
    public float ChangeRotationPercentage = 0.05f;
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("角速度插值方式")]
    public MoveCurves RotationSmoothType = MoveCurves.Lerp;
    [FoldoutGroup("运行状况", expanded: false)]
    [LabelText("碰撞半径")]
    public float Radius = 0.09f;
    [HideInInspector]
    public int SpriteIndex = 0;
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("子弹年龄")]
    public float TotalLiveFrame = 0;
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("已经擦弹")]
    public bool Grazed = false;
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("时刻瞄准玩家")]
    public bool AimToPlayer = false;
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("不进行销毁")]
    public bool DonDestroy = false;
    [FoldoutGroup("运动状况", expanded: false)]
    [LabelText("使用轨迹")]
    public bool showTrails = false;
    public Vector2 BulletPos;
    private Vector2 bulletPos;
    [FoldoutGroup("全局开关", expanded: false)]
    [LabelText("使用子弹")]
    public bool Use = false;

    public List<BulletTrackProduct> bulletTrackProducts = new List<BulletTrackProduct>();
    
    // public Shooting ParentShooting;
    [Title("引用对象")]
    public Shooting[] BulletShooting; //子弹发射器。
    public int BulletIndex = -1;
    public Sprite BulletSprite;
    public SpriteRenderer BulletSpriteRenderer;
    public List<Trigger> TriggerList = new List<Trigger>();
    public Color32 BrokenBulletColor = Color.white;
    public Color32 BulletColor = Color.white;
    public GameObject BulletSpriteController;
    public bool UseCollision = false;
    public bool UseCustomCollisionGroup = false;
    public GameObject CustomCollisionGroupMainController;
    public GameObject Trail;
    public Shooting TrailShooting;
    public CustomCollision[] CustomCollisionGroup;
    public CollisionType BulletCollision = CollisionType.CIRCLE;
    public enum MoveCurves { Line = 0, Lerp = 1 }
    public Vector2 SquareLength;
    //public Vector2 SquarePoint;
    public Vector2 Scale;
    public TrackData trackData;
    public string BulletBreakingAnimationName = "BulletBreak";
    public bool StaticBullet;
    public bool StayCollision; public bool StayBulletEvent;
    public bool ScaleUpdate,RotationUpdate,StayAnimPlayer,StayTrigger;
    public enum CollisionType { BOX = 0, CIRCLE = 1, NONE = 2 }
    public bool UseForceComponent, UseTriggerComponent;
    public bool TwistedRotation = false;
    public bool InverseRotation = false;
    public float InverseRotateDirection = 0;
    public bool DestroyWhenDontRender = false;
    public bool NoDestroyAnimation;
    public bool NoCollisionWhenCreateAnimationPlaying = false;
    public bool NoCollisionWhenAlphaLow;
    public bool UseCustomAnimation;
    public bool EnabledAcceleratedGlobalOffset;
    public bool EnabledGlobalOffset;
    public AnimationController animationController;
    public bool ChangeInverseRotation = false;
    public bool DonDestroyIfMasterDestroy = false;
    public List<Sprite> BulletFramesSprites = new List<Sprite>();
    public bool UseForce;
    public bool UseAlphaWithDepth;
    public float delayBulletEventRunTime = 0;
    public bool UseSimpleEvent = false;
    public float minDepth;
    public float maxDepth;
    public List<Force> ForceObject = new List<Force>();
    public int nextFramewaitTime = 3;
    public int ID = 0;
    public int BarrageBatch = 0;
    // --------------------------- temp value --------------------------------
    public bool CreateAnimationPlayed = false;
    public int ReadPointTrackSpeed;
    [HideInInspector]
    public float Pos_X, Pos_Y;
    [HideInInspector]
    public BezierDrawer BezierPointTrack;
    public bool FilpX, FilpY;
    private bool isEditor;
    public List<AnimationCurve> SingleParmCurve = new List<AnimationCurve>();
    float characterBulletDistance = 0;

    public delegate void BulletAnimation(float current, float end,Bullet bullet);
    public event BulletAnimation BulletCreateAnimation, BulletDestroyAnimation;
    public event Shooting.BulletDelegate BulletEvent;
    public event Shooting.BulletDelegate BulletEventDestroy;
    public event Shooting.BulletParentDelegate BulletEventWhenBulletParentChanged;

    public event Shooting.BulletDelegate BulletEventOnDestroyingPlayer;
    public event Shooting.BulletDelegateDelay BulletEventDelay;
    public event Shooting.BulletDelegateDelay BulletEventDestroyDelay;
    public event Shooting.BulletDelegateDelay BulletEventWhenBulletRestoreMainLevelDelay;
    public event Shooting.BulletDelegateDelay BulletEventOnDestroyingPlayerDelay;

  //  public Animator AnimationControl;
    Vector3 RotationVector = Vector3.zero;
    public float TargetSpeed = 0;
    public float TargetRotation = 0;
    public Transform Parent;
    [HideInInspector]
    public bool AsSubBullet;
    [HideInInspector]
    public Bullet[] SubBullet;
    public List<Shooting> ShootingRef = new List<Shooting>();
    [Header("必填项")]
    public Character PlayerCharacter;
    public PlayerController Player;
    public Vector2 Zero = Vector2.zero;
    public GameObject OtherObject;
    public List<Sprite> DestroyAnimSprites = new List<Sprite>();
    public List<bool> trackInfoSign = new List<bool>();
    public bool DestroyMode = false;
    public float countingTime = 0;
    public bool FollowObjectWithSameAngle;
    [SerializeField]
    public List<CommandController> CommandList = new List<CommandController>();
    /*
    public Vector2 velocity;
    public Vector2 Velocity { 
        get { return Velocity; }
        set { velocity = value;
            Speed = value.magnitude;
            Rotation = (value.x >= (Quaternion.Euler(0,0,Rotation) * Vector2.up).x)
            ? Vector2.Angle(Vector2.up,value) + 180:  Vector2.Angle(Vector2.up, value);
            Debug.Log(Rotation.ToString());
        }
    }*/
    private int _Index;
    private float _timeCount;
    private float orginalSpeed, orginalRotation, BulletanimSpeed;
    private float _readTrack = 0;
    public bool trackLerpBegin = false;
    private float trackLength = 0;
    public float trackCount = 0;
    private float trackPercent = 0;
    private float RotationTemp = 0;
    private bool filpX, filpY;

    [HideInInspector]
    public float OriSpd = 0;
    public Vector3 originalPatternPos;
    private Vector3 lerpPos;
    public List<bool> inTrigger = new List<bool>();
    public List<float> inStayTrigger = new List<float>();
    public List<int> enterTimeTrigger = new List<int>();

    public Transform BulletTransform;
    public Transform SpriteTransform;
    public bool EnableBulletEvent;
    public Vector2 defSpeed;
    [HideInInspector]
    public int shootingIndex = 0;
    [HideInInspector]
    public float globalSpd = 1;
    // -------------------------- Const Value ---------------------------------
    public const int UnitScale = 100;
    public enum EventList_Bullets { ONUSING = 1, ONDESTROYED = 2, ONRESTORE_MAINLEVEL = 3, ONMOVING = 4 }
    // --------------------------   Function --------------------------------- 
    public float signedFrame = 0;
    public void ClearEventsList(EventList_Bullets t)
    {
        switch ((int)t)
        {
            case 1:
                BulletEvent = null;
                break;
            case 2:
                BulletEventDestroy = null;
                break;
            case 3:
                BulletEventWhenBulletParentChanged = null;
                break;


        }
    }
    public void UseSimpleTrack(List<BulletTrackProduct> products) {
      
      
        foreach (var a in products)
        {
            a.Init(this);
            bulletTrackProducts.Add(a.Clone());
        }
        UseSimpleEvent = true;

    }
    public void UseSimpleTrack(BulletTrackProduct product)
    {
        BulletTrackProduct copy_product = product.Clone();
        copy_product.Init(this);
        bulletTrackProducts.Add(copy_product);

        UseSimpleEvent = true;

    }
    public void ClearAllEvent()
    {
        BulletEventWhenBulletParentChanged = null;
        BulletEventDestroy = null;
        BulletEvent = null;
        BulletEventOnDestroyingPlayer = null;
        BulletEventOnDestroyingPlayerDelay = null;
        BulletEventWhenBulletRestoreMainLevelDelay = null;
        BulletEventDestroyDelay = null;
        BulletEventDelay = null;
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
    void Start()
    {
       
        BulletTransform = gameObject.transform;
        SpriteTransform = BulletSpriteRenderer.transform;
        orginalSpeed = Speed;
        orginalRotation = Rotation;
   //     BulletanimSpeed = AnimationControl.speed;
        Parent = BulletTransform.parent;
        isEditor = Application.isEditor;
        defSpeed = Vector2.up / UnitScale * Global.GlobalSpeed * (isEditor ? 1 : Global.GlobalBulletSpeed);
        //EnableBulletEvent = BulletEvent != null;
        // EnableTrail = Trail != null;
        // EnableFollow = OtherObject != null;
        if (PlayerCharacter == null)
        {

            if (Global.PlayerObject == null)
            {
                if (Global.dialoging)
                    return;
                Global.WindowDialog_A.Show("不满足运行需求", " 出现问题，子弹池无法与玩家组件建立联系。请确认一件事情，前往Unity编辑界面，点击Hierarchy列表里的GameAction选项，在弹出的Inspector界面中，Global类的Player项是否不为None，而为场景中有效的对象？因为这个问题，游戏将无法再继续运行。将返回标题界面。", "Window_Show");
                Global.WindowDialog_A.eventDriver[0].ButtonBlindEvent += WindowsDismiss;
            }
            else
            {
                PlayerCharacter = Global.PlayerObject.GetComponent<Character>();
            }
        }

    }
    void Awake()
    {

      //  if (BulletSpriteRenderer == null)
        //    AnimationControl = BulletSpriteRenderer.gameObject.GetComponent<Animator>();

    }
    IEnumerator WindowsDismiss()
    {
        Global.GamePause = false;
        Global.WindowDialog_A.Hide();
        yield return null;
    }
    /// <summary>
    /// 添加需要修正子弹旋转方向的发射器。
    /// </summary>
    public void AddShootingRef(Shooting target)
    {
        if (ShootingRef.Find((x) =>
        {
            return x = target;
        }) == false)
            ShootingRef.Add(target);
    }
    /// <summary>
    /// 实际功能解释见函数名。
    /// </summary>
    public void DestroyBullet(bool showDestroyAnim = true)
    {
        if (!showDestroyAnim)
            NoDestroyAnimation = true;
        TotalLiveFrame = (int)(MaxLiveFrame  + 50);
    }
    /// <summary>
    /// 在子弹被用作另外一个子弹的副级的时候，可以使用，这会让这颗子弹恢复成主层级。不受原副级的控制。
    /// </summary>
	public void RestoreNormalLevel()
    {
        if (BulletTransform.parent == Parent.transform)
            return;
        BulletTransform.parent = Parent.transform;
        if (BulletEventWhenBulletParentChanged != null)
            BulletEventWhenBulletParentChanged(this,null);
        if (this.gameObject.activeSelf == true)
        {
            if (BulletEventWhenBulletRestoreMainLevelDelay != null && UseThread)
                StartCoroutine(BulletEventWhenBulletRestoreMainLevelDelay(this));
        }
    }
    /// <summary>
    /// 设置一颗子弹的父级。
    /// </summary>
    /// <param name="Orginal">原子弹的引用</param>
    /// <param name="Target">要原子弹附加到的子弹</param>
    public void SetBulletParent(Bullet Orginal, Bullet Target)
    {
        if (Orginal.BulletTransform.parent == Target.BulletTransform)
            return;
        if (Orginal.BulletEventWhenBulletParentChanged != null)
            BulletEventWhenBulletParentChanged(Orginal,Parent.gameObject.GetComponentInParent<Bullet>());
        Orginal.BulletTransform.parent = Parent.transform;
    }
    public bool IsOutofScreen(Vector3 pos)
    {
        Vector3 a = Global.ScreenX_A;
        Vector3 b = Global.ScreenX_A;
        if (a.x > pos.x || a.y < pos.x )
            return true;
        if (b.x > pos.y || b.y < pos.y )
            return true;
        return false;
    }
    static public bool IsOutofScreen(Vector2 target)
    {
        if (Global.ScreenX_A.x > target.x || Global.ScreenX_A.y < target.x)
            return true;
        if (Global.ScreenY_A.x > target.y || Global.ScreenY_A.y < target.y)
            return true;
        return false;
    }
    /// <summary>
    /// 玩家被子弹判定中弹的时候使用的函数。
    /// </summary>
    public void PlayerDie()
    {
        if (PlayerCharacter.Invincible)
        {
            if (!DonDestroy)
                this.DestroyBullet(!NoDestroyAnimation);
            return;
        }
        if (PlayerCharacter != null)
        {
            PlayerCharacter.Die();
        }
        if (!DonDestroy)
            this.DestroyBullet(!NoDestroyAnimation);
        if (BulletEventOnDestroyingPlayer != null)
            BulletEventOnDestroyingPlayer(this);
        if (BulletEventOnDestroyingPlayerDelay != null && UseThread)
            StartCoroutine(BulletEventOnDestroyingPlayerDelay(this));
    }
    public void MoveTargetLerp()
    {
        if (trackData != null)
        {
            if (trackData.animationCurve.keys.Length < 2)
                return;
            lerpPos = BulletTransform.localPosition;
            trackLength = trackData.animationCurve.keys[trackData.animationCurve.keys.Length - 1].time;
            trackLerpBegin = true;
        }
        else {
            Debug.Log("TRUE");
           }
    }
    public Vector3 CalcateLerpPos(float t)
    {
        if (trackData != null)
        {
            float p = trackData.animationCurve.Evaluate(t);
            return Vector3.LerpUnclamped(lerpPos, trackData.targetPos, p);
        }
        return Vector3.zero;
    }
    /// <summary>
    /// 擦弹，在游戏中，擦弹默认可得2000分，你可以在该函数原型中设定该值。
    /// </summary>
    public void Graze()
    {

        if (PlayerCharacter != null)
        {
            if ((PlayerCharacter.Invincible == false || PlayerCharacter.countGrazeWhenNobody == true) && Grazed == false)
            {
                Grazed = true;
                if (Global.SpellCardNow != null)
                    Global.SpellCardNow.MaxBouns += (long)(200.0f / Vector2.Distance(transform.position, PlayerCharacter.transform.position));
                PlayerCharacter.Controller.UseGrazedEvent();
            }
        }

    }
    /// <summary>
    /// 反弹，P1 P2为直线向量
    /// </summary>
    /// <param name="Target"></param>
    public void Rebound(Bullet P1, Trigger which)
    {

        Vector2 Normal =  (Vector2)(which.AuxiliaryLinesEnd.transform.position - which.AuxiliaryLinesStart.transform.position).normalized;
        Vector2 SpdDir = (P1.transform.rotation * Vector2.up).normalized;

        Vector2 re = Vector2.Reflect(-SpdDir, Normal);

        


        P1.Rotation = Vector2.SignedAngle(Vector2.up, re);
        //Debug.Log (P2);
    }
    public void Rebound(Bullet P1, Vector2 normal)
    {

        Vector2 Normal = normal;
        Vector2 SpdDir = (P1.transform.rotation * Vector2.up).normalized;

        Vector2 re = Vector2.Reflect(-SpdDir, Normal);



        P1.Rotation = Vector2.SignedAngle(Vector2.up, re);
        //Debug.Log (P2);
    }
    void InserverRotation()
    {
        TwistedRotation = false;
        BulletSpriteController.transform.localRotation = Quaternion.Inverse(BulletTransform.localRotation);
        if (ChangeRotationSmoothly)
        {
            if (SpeedSmoothType == MoveCurves.Lerp)
            {
                if (ChangeInverseRotation)
                    InverseRotateDirection = Mathf.Lerp(InverseRotateDirection, TargetRotation, ChangeRotationPercentage);
                Rotation = Mathf.Lerp(Rotation, TargetRotation, ChangeRotationPercentage);
            }
            if (SpeedSmoothType == MoveCurves.Line)
            {
                if (ChangeInverseRotation)
                {
                    InverseRotateDirection = Mathf.Lerp(InverseRotateDirection, TargetRotation, ChangeRotationPercentage);
                    InverseRotateDirection = Mathf.Clamp(InverseRotateDirection, -360, TargetRotation);
                }
                Rotation = Mathf.Lerp(Rotation, TargetRotation, ChangeRotationPercentage);
                Rotation = Mathf.Clamp(Rotation, -360, TargetRotation);
            }
        }

        Vector3 Ra = new Vector3(0, 0, InverseRotateDirection);

        BulletSpriteController.transform.Rotate(Ra);
    }
    void CheckForce()
    {
       
            for (int i = 0; i != ForceObject.Count; ++i)
            {
                if (ForceObject[i] == null) continue;
                if (ForceObject[i].ForBullet == false) continue;

                if (ForceObject[i].Type == Force.ForceType.Box)
                {
                    if (ForceObject[i].EnableForTag != Tag) continue;
                    bool Result = Intersection(BulletTransform.position, ForceObject[i].SquareLength, ForceObject[i].gameObject.transform.position, Radius);

                    if (Result)
                    {
                        //  if (ForceObject[i].BulletList.Contains(ID) == false)
                        ForceObject[i].BulletUpdate(this);
                    }/*
                    else
                    {
                        if (ForceObject[i].BulletList.Contains(ID) == false)
                            ForceObject[i].BulletList.Remove(ID);
                    }*/
                }
                if (ForceObject[i].Type == Force.ForceType.Circle)
                {
                    if (ForceObject[i].EnableForTag != Tag) continue;
                    bool Result = Radius + ForceObject[i].Radius > Vector2.Distance(BulletTransform.position, ForceObject[i].gameObject.transform.position);

                    if (Result)
                    {
                        //if (ForceObject[i].BulletList.Contains(ID) == false)
                        ForceObject[i].BulletUpdate(this);//ForceObject[i].BulletList.Add(ID);
                    }/*
                    else
                    {
                        if (ForceObject[i].BulletList.Contains(ID) == false)
                            ForceObject[i].BulletList.Remove(ID);
                    }*/
                }
            }
        
    }
    void TriggerEvent(Trigger trigger,bool Result,int i) {
        if (Result == true && inTrigger[i] == false && trigger.Use == true)
        {

            inTrigger[i] = true;
            enterTimeTrigger[i]++;
            if (trigger.MaxUseTime == -1 || enterTimeTrigger[i] < trigger.MaxUseTime)
                trigger.OnBulletEnterIntoTrigger(this, null, inStayTrigger[i], enterTimeTrigger[i]);
            if (trigger.OnceTime)
            {
                trigger.Use = false;

            }
            inStayTrigger[i] = 0;

        }
        if (Result && trigger.Use == true)
        {
            if (trigger.MaxUseTime == -1 || enterTimeTrigger[i] < trigger.MaxUseTime)
                trigger.UseStayEvent(this, inStayTrigger[i], enterTimeTrigger[i]);
            inStayTrigger[i]++;
        }
        if (Result == false && inTrigger[i] == true && trigger.Use == true)
        {
            inTrigger[i] = false;
            if (trigger.MaxUseTime == -1 || enterTimeTrigger[i] < trigger.MaxUseTime)
                trigger.OnBulletExitFromTrigger(this, null, inStayTrigger[i], enterTimeTrigger[i]);
            inStayTrigger[i] = 0;
            if (trigger.OnceTime)
            {
                trigger.Use = false;
            }
        }
    }
    void CheckTrigger()
    {

        for (int i = 0; i != TriggerList.Count; ++i)
        {
            Trigger trigger = TriggerList[i];
            if (trigger == null) continue;
            if (trigger.Type == Trigger.TriggerType.Box)
            {
                bool Result = Intersection(BulletTransform.position, trigger.SquareLength, trigger.gameObject.transform.position, Radius);
                TriggerEvent(trigger, Result, i);

                //inTrigger[i] = Result;
            }
            if (trigger.Type == Trigger.TriggerType.Circle)
            {

                bool Result = Radius + trigger.Radius > Vector2.Distance(BulletTransform.position, trigger.gameObject.transform.position);
                TriggerEvent(trigger, Result, i);
            }
            if (trigger.Type == Trigger.TriggerType.Line)
            {
                Vector2 pos = BulletTransform.position;
                Vector2 start = trigger.AuxiliaryLinesStart.transform.position;
                Vector2 end = trigger.AuxiliaryLinesEnd.transform.position;
                bool Result = Math2D.IsCircleIntersectLineSeg(pos.x,pos.y,Radius,start.x,start.y ,end.x,end.y);
                TriggerEvent(trigger, Result, i);

            }

        }


    }
    //  private float rotation = 0, rotate =0;

    public bool EnableFollow = true;
    public bool EnableTrail;
    internal bool useDefaultSprite;

    public string BulletCreatingAnimationName { get; internal set; }
    public Sprite CreatingCustomSprite { get; internal set; }
    public bool LoopTrack { get; internal set; }
    public bool CalculateAngle { get; internal set; }
    public float RotatorInAsix { get; internal set; }
    public bool SubTrail;

    public void DestroySubBullets()
    {
        if (DonDestroyIfMasterDestroy == false)
        {
            SubBullet = GetComponentsInChildren<Bullet>();
            for (var i = 0; i != SubBullet.Length; ++i)
            {
                if (SubBullet[i] == null) continue;
                //   SubBullet[i].RestoreNormalLevel();
                //Vector3 postion = SubBullet[i].BulletTransform.position;

                SubBullet[i].DestroyBullet();
            }
        }
    }
    public void CheckCollision(Vector3 pos) {

        if (UseCustomCollisionGroup == false && CreateAnimationPlayed && UseCollision ||
             (NoCollisionWhenCreateAnimationPlaying == false && UseCustomCollisionGroup == false && UseCollision))
        {

            if (BulletCollision == CollisionType.CIRCLE)
            {

                characterBulletDistance = Vector2.Distance(pos, PlayerCharacter.gameObject.transform.position);
                if (Radius + PlayerCharacter.Radius > characterBulletDistance)
                    PlayerDie();

            }

            float characterBulletMinDistance = 0.4f;

            // Graze 擦弹。

            if (Grazed == false && Radius + PlayerCharacter.Radius + characterBulletMinDistance > characterBulletDistance)
                Graze();

            if (BulletCollision == CollisionType.BOX)
            {
                if (Intersection(pos, SquareLength, PlayerCharacter.transform.position, PlayerCharacter.Radius))
                    PlayerDie();
            }
        }

        else if (UseCustomCollisionGroup == true && CustomCollisionGroup.Length != 0 && CreateAnimationPlayed && UseCollision || (NoCollisionWhenCreateAnimationPlaying == false && UseCustomCollisionGroup == true && UseCollision && CustomCollisionGroup.Length != 0))
        {
            for (int i = 0; i != CustomCollisionGroup.Length; ++i)
            {
                if (CustomCollisionGroup[i].Check())
                {
                    PlayerDie();
                    break;
                }
            }
        }

    }
    
    public void UpdateBullet()
    {
      

        if (AsEnemyMovement)
            Use = true;
        float GlobalSpeed = Global.GlobalSpeed;
      
        if (!Use || GlobalSpeed == 0) return;
        if (Global.GamePause)
        {
           // AnimationControl.speed = 0;
            globalSpd = 0;
            return;
        }
        if (CreateAnimationPlayed == false && DestroyMode == false && AsEnemyMovement == false)
                BulletCreateAnimation(TotalLiveFrame, signedFrame + 12, this);
        if (StopOnSpecialSituation)
        {
            if (Global.WrttienSystem || Global.GamePause || Character.enemyState.Target.HP <= 0)
                return;
        }
        if (DestroyMode)
        {
            if (CreateAnimationPlayed == false && AsEnemyMovement == false)
                animationController.PlayAnimation();
            DestroySubBullets();
           
            float length = 24;
            countingTime += 1 * GlobalSpeed;
            if (countingTime < length)
                    BulletDestroyAnimation(countingTime, length, this);
            return;
        }
        TotalLiveFrame = TotalLiveFrame + 1 * GlobalSpeed * (isEditor ? 1 : Global.GlobalBulletSpeed);
        #region 子弹销毁
        if (TotalLiveFrame >= MaxLiveFrame && AsEnemyMovement)
        {
            if (Reusable == false)
                enabled = false;
            TotalLiveFrame = 0;
        }
        if (TotalLiveFrame >= MaxLiveFrame ) // 销毁子弹。
        {
            for (int i = 0; i != ShootingRef.Count; ++i)
            {
                ShootingRef[i]._Masterbullet = null;
            }
            ShootingRef.Clear();
            if (!DestroyMode)
            {
                if (OtherObject != null)
                    Destroy(OtherObject);
            }


            if (DestroyAnimSprites.Count == 0)
            {
                DestroySubBullets();
                DestroyBulletETC();
            }
            else
            {
                DestroyMode = true;
                BulletSpriteRenderer.color = BrokenBulletColor;
            }

        }
   
        #endregion
        if (StaticBullet)
        {
            if (StayCollision) {
                CheckCollision(BulletTransform.position);
            }
            if (StayBulletEvent) {
                if (BulletEvent != null)
                {
                    BulletEvent(this);
                    if (UseThread)
                        StartCoroutine(BulletEventDelay(this));
                }
                if (UseSimpleEvent)
                {
                    foreach (var a in bulletTrackProducts)
                    {
                        a.UpdateValue();
                    }
                }
            }
            if (ScaleUpdate && CreateAnimationPlayed)
                BulletTransform.localScale = Scale;
            if (RotationUpdate) {
               if (InverseRotation)
                    InserverRotation();
            }
            if (StayAnimPlayer) {
                if (UseCustomAnimation && CreateAnimationPlayed)
                    UpdateBulletSpriteVoid();
            }
            if (StayTrigger)
            {
                if (UseTriggerComponent)
                    CheckTrigger();
            }
            return;
        }
        if (globalSpd != GlobalSpeed )
        {
            //AnimationControl.speed = GlobalSpeed;
            globalSpd = GlobalSpeed;
        }
        Vector3 pos = BulletTransform.position;
        if (UseAlphaWithDepth)
        {
            Color bulletColor = BulletSpriteRenderer.color;
            BulletSpriteRenderer.color = new Color(bulletColor.r, bulletColor.g, bulletColor.b,
               (Mathf.Lerp(minDepth, maxDepth, (float)Mathf.Clamp(pos.z, minDepth, maxDepth)) - minDepth) / (maxDepth - minDepth) * 255);
        }

        if (EnableTrail)
        {
            Trail.gameObject.SetActive(showTrails);
     
        }
        if (DestroyWhenDontRender)
        {
            if (IsOutofScreen(pos))
            {
                NoDestroyAnimation = true;
                TotalLiveFrame = (int)(MaxLiveFrame + 50);
            }
        }
        if (AsEnemyMovement == false && CreateAnimationPlayed)
            BulletTransform.localScale = Scale;

        // 子弹必须播放了创建动画后才能进行有效碰撞。
        if (BulletCollision != CollisionType.NONE)
        {
            CheckCollision(pos);
        }
        if (NoCollisionWhenAlphaLow)
            UseCollision = !(NoCollisionWhenAlphaLow && (int)BulletColor.a <= 255 * 0.75);

        if (UseTriggerComponent)
            CheckTrigger();
        if (UseForceComponent)
            CheckForce();
        // 使用一个临时变量,从而保证FilpX、FilpY变量被修改后，子弹图像仅仅被一次反转，不被Update多次调用。
        if (FilpX != filpX)
        {
            BulletSpriteRenderer.flipX = FilpX;
            filpX = FilpX;
        }
        if (FilpY != filpY)
        {
            BulletSpriteRenderer.flipY = FilpY;
            filpY = FilpY;
        }
        
       
        if (MaxLiveFrame <= 2)
            return;
        UpdateState();
        
        if (AimToPlayer)
            AimToPlayerObject();
 

        if (BulletEvent != null)
        {
            BulletEvent(this);
            if (UseThread)
                StartCoroutine(BulletEventDelay(this));
        }
        if (UseSimpleEvent)
        {
            foreach (var a in bulletTrackProducts)
            {
                a.UpdateValue();
            }
        }
        if (AsEnemyMovement)
        {
            if (Character == null)
            {
                AsEnemyMovement = false;
                return;
            }
            Character.BulletControled = true;
            DonDestroy = true;
            Character.Moving = Mathf.Abs(Speed) >= 0.05f;
            float rot = (Rotation + 3600) % 360;


            Character.ToRight = (rot > 0 && rot < 180);

          
            if (Character.AnimationController != null)
            {
                Character.AnimationController.SetBool("Move", Character.Moving);
                Character.AnimationController.SetBool("Right", !Character.ToRight);
            }
            if (Character.FilpX)
                Character.SpriteRender.flipX = !Character.ToRight;
        }/*
        if (showTrails)
        {
            TrailShooting.CustomSprite = BulletSpriteRenderer.sprite;
            TrailShooting.Angle = Rotation;
            TrailShooting.Timer = Trailn;
            TrailShooting.MaxLiveFrame = TrailLength;
        }*/
        // UpdateBulletMovement();
    }
    /// <summary>
    /// 子弹类的主要部分，包括子弹的移动，判定等都在这里执行。
    /// </summary>
    void Update()
    {
        UpdateBullet();
    }
    public void ChangeSprite(Sprite sprite) {
        BulletSprite = sprite;
        BulletSpriteRenderer.sprite = BulletSprite;
    }
    public void ChangeColor(Color color) {
        BulletColor = color;
        BulletSpriteRenderer.color = color;

    }
    public Vector2 GetSpeedVector() {
        Vector2 spdVet = BulletTransform.rotation * (Vector2.up * Speed);
        return spdVet;
    }
    public void SetSpeedVector(Vector2 spdVector)
    {
        if (Speed == 0)
            return;
        Speed = spdVector.magnitude;
        Rotation = Vector2.SignedAngle(Vector2.up, spdVector);
       // Debug.LogWarning("[Seted]" + Speed.ToString() + "," + Rotation.ToString());
    }
    public void UpdateState()
    {
        //  rotate = Rotation;
        if (EnabledGlobalOffset)
            AcceleratedSpeedDirectionNow += AcceleratedSpeedDirectionPer /UnitScale* Global.GlobalSpeed * (isEditor ? 1 : Global.GlobalBulletSpeed);

        if (EnabledAcceleratedGlobalOffset || EnabledGlobalOffset)
        {
            Vector2 r = Vector2.up * Speed;
            r = Quaternion.Euler(0, 0, Rotation) * r;
            r += AcceleratedSpeedDirectionNow;
            Rotation = Vector2.SignedAngle(Vector2.up, r);
            Speed = r.magnitude;
        }
     
        // Rotation = Rotation % 360;

        if (trackLerpBegin)
        {
            AnimationCurve curve = trackData.animationCurve;

            if (trackData.calculateFrame)
            {
                trackPercent += Global.GlobalSpeed;
                trackCount = trackPercent;
            }
            else
            {
                trackPercent += 0.016667f * Global.GlobalSpeed;
                trackCount = trackPercent / curve.keys[curve.keys.Length - 1].time;
            }
            if (trackCount >= trackLength)
            {
                trackCount = 0;
                trackPercent = 0;
                Vector2 pos = originalPatternPos;
                //    transform.position = trackData.targetPos;
                if (trackData.regularPattern)
                    Speed = OriSpd * Vector2.Distance(pos, (Vector2)BulletTransform.position);
                else
                    Speed = OriSpd;
                Rotation = Math2D.GetAimToTargetRotation(pos, (Vector2)BulletTransform.position);
               
                trackLerpBegin = false;
            }
        }
        // InverseRotateDirection = InverseRotateDirection % 360;
        //  TargetRotation = TargetRotation % 360;
        if (TotalLiveFrame < MaxLiveFrame)
        {
            if (AcceleratedRotation != 0)
            {
                Rotation += AcceleratedRotation * Global.GlobalSpeed * (isEditor ? 1 : Global.GlobalBulletSpeed);
                RotationVector.z = AcceleratedRotation * Global.GlobalSpeed * (isEditor ? 1 : Global.GlobalBulletSpeed);
            }
            if (RotationTemp != Rotation)
            {
                BulletTransform.eulerAngles = new Vector3(0, 0, Rotation);
                //  rotation = Rotation;
                RotationTemp = Rotation;
            }
        }
        if (TwistedRotation)
            BulletSpriteController.transform.localRotation = Quaternion.AngleAxis(Quaternion.Angle(Quaternion.Euler(0, 0, 0), BulletTransform.localRotation), Vector3.up);
        if (InverseRotation)
            InserverRotation();
        if (!FollowObjectWithSameAngle && EnableFollow)
        {
            Transform OtherObjectTransform = OtherObject.transform;
            OtherObjectTransform.localRotation = BulletSpriteRenderer.transform.localRotation;
         //   OtherObjectTransform.localRotation = Quaternion.Inverse(OtherObjectTransform.localRotation);
        }
        else if (EnableFollow && FollowObjectWithSameAngle)
            OtherObject.transform.eulerAngles = new Vector3(0,0,RotatorInAsix);
        if (UseCustomAnimation && TotalLiveFrame < MaxLiveFrame && CreateAnimationPlayed)
            UpdateBulletSpriteVoid();
        if (ReadPointTrackSpeed != 0)
        {
            _readTrack += ReadPointTrackSpeed; //'
            float time = _readTrack / BezierPointTrack.maxFrame;
            if (_readTrack >= BezierPointTrack.maxFrame)
                if (LoopTrack)
                    _readTrack -= BezierPointTrack.maxFrame;
            _readTrack = Mathf.Clamp(_readTrack, 0, BezierPointTrack.maxFrame);
            Vector3 t = BezierPointTrack.bezierSpline.GetPoint(time);
            BulletTransform.position = t;
            Speed = BezierPointTrack.bezierSpline.GetFloatVelocity(time);
            if (CalculateAngle)
            {
                float berAngle = BezierPointTrack.bezierSpline.GetFloatDirection(time);
                if (berAngle != -65535)
                    Rotation = berAngle;
            }

        }

        if (ChangeSpeedSmoothly == false)
        {
            if (AcceleratedSpeed != 0)
                Speed += AcceleratedSpeed / UnitScale * Global.GlobalSpeed * (isEditor ? 1 : Global.GlobalBulletSpeed);
        }
        else
        {
            TargetSpeed += AcceleratedSpeed / UnitScale * Global.GlobalSpeed * (isEditor ? 1 : Global.GlobalBulletSpeed);
            if (SpeedSmoothType == MoveCurves.Line)
                Speed = Mathf.Lerp(Speed, TargetSpeed, ChangeSpeedPercentage);
            if (SpeedSmoothType == MoveCurves.Lerp)
            {
                Speed = Mathf.Lerp(Speed, TargetSpeed, ChangeSpeedPercentage);
            }
        }
      

        if (UseGlobalSpeed)
        {
            if (!trackLerpBegin)
            {
                if (Speed != 0)
                {
                    // BulletTransform.position += new Vector3(Mathf.Sin(this.Rotation), Mathf.Cos(this.Rotation), 0f) * (this.Speed / 100.0f);  * (TotalLiveFrame >= MaxLiveFrame && AsEnemyMovement == false ? Random.Range(0, 2f) : Speed)
                    BulletTransform.Translate(defSpeed * Speed * Global.GlobalSpeed, Space.Self);
                }
            }
            else
                BulletTransform.localPosition = CalcateLerpPos(trackCount);
        }
        else
        {
            if (!trackLerpBegin)
            {
                if (Speed != 0)
                {
                    //BulletTransform.position += new Vector3(Mathf.Sin(this.Rotation), Mathf.Cos(this.Rotation), 0f) * (this.Speed / 100.0f);

                     BulletTransform.Translate(defSpeed * Speed * Global.GlobalSpeed, Space.Self);
                }
            }
            else
                BulletTransform.localPosition = CalcateLerpPos(trackCount);
        }
       
    

    }
    public void ChangeOtherObject (GameObject tarGameObject){
        EnableFollow = true;
        GameObject t = Instantiate(tarGameObject, this.transform);
        OtherObject = t;
    }
    void OnDisable()
    {
        if (AsEnemyMovement)
        {
            if (Character != null)
                Character.BulletControled = false;
        }
    }
    void OnEnable()
    {
        if (AsEnemyMovement)
        {
            if (Character != null)
                Character.BulletControled = true;
        }
    }

    void SetBullet(Bullet a) { a = Global.BasicBullet; }
    public void RestoreNormalLevelForSubBullet()
    {
        SubBullet = GetComponentsInChildren<Bullet>();
        for (var i = 0; i != SubBullet.Length; ++i)
        {
            if (SubBullet[i] == null) continue;
            SubBullet[i].RestoreNormalLevel();
            Vector3 postion = SubBullet[i].BulletTransform.position;
        }
    }
    public void SetSignedFrame(float time) {
        signedFrame = time;
    }
    /// <summary>
    /// 得到包括自身引用的Bullet对象合集，该合集取自该Bullet对象的子级。
    /// </summary>
    /// <returns></returns>
    public Bullet[] GetSubBullet()
    {
        return GetComponentsInChildren<Bullet>();
    }
    /// <summary>
    /// 销毁子弹的最终函数，将彻底销毁子弹，重置所有数据。间接由DestroyBullet调用，也可在某些特殊情形直接调用。
    /// </summary>
    /// <returns></returns>
    public void DestroyBulletETC()
    {
        BulletIndex = -1;
        DestroyMode = false;
        countingTime = 0;
        showTrails = false;
        if (EnableTrail)
         TrailShooting.gameObject.SetActive(false);
   
        if (BulletEventDestroy != null)
            BulletEventDestroy(this);
        if (BulletEventDestroyDelay != null && UseThread)
        {
            StartCoroutine(BulletEventDestroyDelay(this));
        }
        //sDestroyAnimSprites.Clear();
        ClearAll();
        Bullet[] A = new Bullet[0];
        SubBullet = A;
        ScaleUpdate = false;
        RotationUpdate = false;
        SubTrail = false;
        StaticBullet = false;
        StayTrigger = false;
        StayCollision = false;
        StayBulletEvent = false;
        StayAnimPlayer = false;
        SingleParmCurve.Clear();
      //  AnimationControl.speed = 1.0f;
        TotalLiveFrame = -17;
        Speed = 0;
        signedFrame = 0;
        shootingIndex = 0;
        AcceleratedSpeed = 0f;
        AcceleratedRotation = 0f;
        Radius = 0f;
        SpriteIndex = 0;
        UseSimpleEvent = false;
        MaxLiveFrame = 200;
        delayBulletEventRunTime = 0;
        CreateAnimationPlayed = false;
        BulletEvent = null;
        BarrageBatch = 0;
        trackData = null;
        trackCount = 0;
        trackPercent = 0;
        BezierPointTrack = null;
        trackLerpBegin = false;
        originalPatternPos = Vector3.zero;
        lerpPos = Vector3.zero;
        inTrigger.Clear();
        inStayTrigger.Clear();
        enterTimeTrigger.Clear();
        ClearAllEvent();
        bulletTrackProducts.Clear();
        AcceleratedSpeedDirectionNow = Vector2.zero;
        AsSubBullet = false;
        _readTrack = 0;
        RotationVector = Vector3.zero;
        BulletCollision = CollisionType.NONE;
        trackInfoSign.Clear();
        if (CustomCollisionGroup.Length != 0)
        {
            UseCustomCollisionGroup = false;
            Destroy(CustomCollisionGroupMainController);
        }

        Grazed = false;
        BulletShooting = null;
        showTrails = false;
        Color p = BulletSpriteRenderer.color;
        p.a = 0;
        BulletSpriteRenderer.color = p;
        Use = false;
        CommandList.Clear();


        if (showTrails)
            Trail.gameObject.SetActive(false);
      //  trackData = null;
     
        Scale = Vector3.one;
        BulletSprite = null;
        CreateAnimationPlayed = false;
        noDestroy = false;
        UseCustomAnimation = false;
        nextFramewaitTime = 0;
        Rotation = 0;
        BulletTransform.localScale = Vector3.one;
      
        if (!AsEnemyMovement)
            animationController.Break();
       
    }
    /// <summary>
    /// 判断一个矩形是否与圆相切。
    /// </summary>
    static public bool Intersection(Vector2 c, Vector2 h, Vector2 p, float r)
    {
        Vector2 v = p - c;
        v.x = Mathf.Abs(v.x);
        v.y = Mathf.Abs(v.y);
        Vector2 u = Vector2.Max(v - h, Vector2.zero);
        return Vector2.Dot(u, u) < r * r;
    }
    public void UpdateBulletSpriteVoid()
    {
        _timeCount += 1 * Global.GlobalSpeed;
        if (BulletFramesSprites.Count == 0 && BulletFramesSprites.Count == 1)
        {
            UseCustomAnimation = false;
            return;
        }
        if (_timeCount < nextFramewaitTime) return;
        ++_Index;
        if (_Index >= BulletFramesSprites.Count - 1)
            _Index = 0;
        if (BulletFramesSprites[_Index] != null)
            BulletSpriteRenderer.sprite = BulletFramesSprites[_Index];
        _timeCount = 0;
    }


    public void SetMaterial(int index)
    {
        BulletSpriteRenderer.material = Global.MaterialCollections_A[index];
    }
    static public Bullet CreateBullet(Vector2 postion)
    {
        Bullet _b = Global.GameObjectPool_A.ApplyBullet();
        Global.GameObjectPool_A.BulletList.Dequeue();
        _b.Use = true;
        _b.BulletTransform.position = postion;
        return _b;
    }
    public void SetMaterial(Material T)
    {
        BulletSpriteRenderer.material = T;
    }
    /// <summary>
    /// 将子弹瞄准玩家，若在发射器面板标注有AmiToPlayer 此函数不会被执行，且无意义。
    /// </summary>
    public void AimToPlayerObject()
    {
        Rotation= Math2D.GetAimToTargetRotation(BulletTransform.position, PlayerCharacter.Controller.gameObject.transform.position);

    }
    /// <summary>
    /// 得到子弹瞄准玩家后的旋转值
    /// </summary>
    /// <returns>返回的是浮点数的角度 返回值 属于 [0,360)</returns>
    public float GetAimToPlayerObjectRotation()
    {
       
        return Math2D.GetAimToTargetRotation(BulletTransform.position, PlayerCharacter.Controller.gameObject.transform.position);
    }
   
}
public static class Math2D {

    public static float GetAimToObjectRotation(GameObject Target, GameObject Orginal)
    {
        float angle = 0;
        if (Target.transform.position.x < Orginal.transform.position.x && Target.transform.position.y < Orginal.transform.position.y)
            angle = 270 + Mathf.Rad2Deg * Mathf.Atan((Orginal.transform.position.y - Target.transform.position.y) / (Orginal.transform.position.x - Target.transform.position.x));
        if (Target.transform.position.x < Orginal.transform.position.x && Target.transform.position.y > Orginal.transform.position.y)
            angle = 180 + 90 - Mathf.Rad2Deg * Mathf.Atan((Target.transform.position.y - Orginal.transform.position.y) / (Orginal.transform.position.x - Target.transform.position.x));
        if (Target.transform.position.x > Orginal.transform.position.x && Target.transform.position.y > Orginal.transform.position.y)
            angle = 90 + Mathf.Rad2Deg * Mathf.Atan((Target.transform.position.y - Orginal.transform.position.y) / (Target.transform.position.x - Orginal.transform.position.x));
        if (Target.transform.position.x > Orginal.transform.position.x && Target.transform.position.y < Orginal.transform.position.y)
            angle = 90 - Mathf.Rad2Deg * Mathf.Atan((Orginal.transform.position.y - Target.transform.position.y) / (Target.transform.position.x - Orginal.transform.position.x));
        return angle;
    }
    public static float GetAimToTargetRotation(Vector2 Target, Vector2 Orginal)
    {
        float angle = 0;
        if (Target.x <= Orginal.x && Target.y <= Orginal.y)
            angle = 270 + Mathf.Rad2Deg * Mathf.Atan((Orginal.y - Target.y) / (Orginal.x - Target.x));
        if (Target.x <= Orginal.x && Target.y >= Orginal.y)
            angle = 180 + 90 - Mathf.Rad2Deg * Mathf.Atan((Target.y - Orginal.y) / (Orginal.x - Target.x));
        if (Target.x >= Orginal.x && Target.y >= Orginal.y)
            angle = 90 + Mathf.Rad2Deg * Mathf.Atan((Target.y - Orginal.y) / (Target.x - Orginal.x));
        if (Target.x >= Orginal.x && Target.y <= Orginal.y)
            angle = 90 - Mathf.Rad2Deg * Mathf.Atan((Orginal.y - Target.y) / (Target.x - Orginal.x));
        return angle;
    }
    public static bool IsCircleIntersectLineSeg(float x, float y, float r, float x1, float y1, float x2, float y2)
    {
        float vx1 = x - x1;
        float vy1 = y - y1;
        float vx2 = x2 - x1;
        float vy2 = y2 - y1;

        bool dc = Mathf.Abs(vx2) > 0.00001f || Mathf.Abs(vy2) > 0.00001f;
        if (!dc)
            return false;
        float len = Mathf.Sqrt(vx2 * vx2 + vy2 * vy2);
        vx2 /= len;
        vy2 /= len;


        float u = vx1 * vx2 + vy1 * vy2;

        float x0 = 0f;
        float y0 = 0f;
        if (u <= 0)
        {
            x0 = x1;
            y0 = y1;
        }
        else if (u >= len)
        {
            x0 = x2;
            y0 = y2;
        }
        else
        {
            x0 = x1 + vx2 * u;
            y0 = y1 + vy2 * u;
        }
        return (x - x0) * (x - x0) + (y - y0) * (y - y0) <= r * r;
    }

}