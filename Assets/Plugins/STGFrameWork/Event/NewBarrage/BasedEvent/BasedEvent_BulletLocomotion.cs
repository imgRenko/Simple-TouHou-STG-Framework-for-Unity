using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using System.Reflection;
using System.Reflection.Emit;
using Sirenix.OdinInspector;

/*public class DisplayOnly : PropertyAttribute
{

}
[CustomPropertyDrawer(typeof(DisplayOnly))]
public class ReadOnlyDrawer : PropertyDrawer
{
public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
{
return EditorGUI.GetPropertyHeight(property, label, true);
}
public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
{
GUI.enabled = false;
EditorGUI.PropertyField(position, property, label, true);
GUI.enabled = true;
}
}*/
[System.Serializable]
public class FrameInfo {
    [LabelText("基础值种类")]
    public FrameSetting.Original BaseValue = FrameSetting.Original.CURVE;
    [ShowIf("BaseValue", FrameSetting.Original.CONST)]
    [LabelText("右常数值")]
    public float ResultConst;

}
[System.Serializable]
public class FrameSetting {

    [LabelText ("曲线关键帧序号")]
    public int KeyIndex = 0 ;

    [LabelText("需要比较的参数")]
    public int ParmIndex = 0;
    [LabelText("参数计算")]
    public DynamicParmCurve.CalculateMethod method = DynamicParmCurve.CalculateMethod.PLUS;
    [LabelText("计算因数")]
    [HideIf("method", DynamicParmCurve.CalculateMethod.NONE)]
    public float methodInpand = 0;
    [LabelText("比较参数方式")]
    public FrameCondition condition = FrameCondition.MORETHAN;
    [LabelText("待比较的常数值")]
    public float Compare = 0;
    [LabelText("全条件成立时曲线对应参数变化方法")]
    public Infusion infusion = Infusion.REPLACE;
  
    [ShowIf("infusion", Infusion.RANDOM)]
    [LabelText("最大随机范围")]
    public float MaxRandomRange = 0;
    [ShowIf("infusion", Infusion.RANDOM)]
    [LabelText("最小随机范围")]
    public float MinRandomRange = 0;
    [LabelText("条件不符合时使用的参数变化方法")]
    public Infusion Default = Infusion.NONE;
    [LabelText("比对其他发射器")]
    public bool CompareOtherShooting;
    [LabelText("要比对的发射器")]
    [ShowIf("CompareOtherShooting", Infusion.RANDOM)]
    public Shooting compareShooting;
    
    public enum FrameCondition
    {
        MORETHAN = 0,
        LESSTHAN = 1,
        EQUAL = 2,
        BOOLONLY_FALSE = 3,
        BOOLONLY_TRUE = 4
    }
    public enum Infusion
    {
        NONE = 0,
        REPLACE = 1,
        MINUS = 2,
        RANDOM = 3,
        ADD = 4
    }
    public enum Original
    {
        CURVE = 0,
        CURVE_X=12,
        CURVE_Y=13,
        CURVE_R=14,
        CURVE_G=15,
        CURVE_B=16,
        CURVE_A=17,
        PLAYER_POS_X = 1,
        PLAYER_POS_Y = 2,
        THIS_POS_X = 3,
        THIS_POS_Y = 4,
        OBJECT_POS_X = 5,
        OBJECT_POS_Y = 6,
        ANGLE_TO_PLAYER = 11,
        CONST = 7,
        ORINGIAL = 8,
        CURVE_VECTOR2 = 9,
        CURVE_VECTOR4 = 10
    }


    [HideIf("infusion", Infusion.REPLACE)]
    [LabelText("左基础值种类")]
    public Original DefaultValue = Original.ORINGIAL;
    [ShowIf("DefaultValue", Original.CONST)]
    [LabelText("左常数值")]
    public float Const = 0;

    [LabelText("右基础值种类")]
    public FrameInfo[] ResultDefaultValue = new FrameInfo[4] { new FrameInfo(), new FrameInfo(), new FrameInfo(), new FrameInfo()};

    [HideInInspector]
    public bool Enable = true;
    [LabelText("只需此条件为True就判定完成")]
    public bool OnlyOr = false;

    [LabelText("条件可复用次数")]
    public int Recircle = -1;


    [HideInInspector]
    public string Tip = "";

    public int resetCount = 0;
    //[DisplayOnly]
    public bool Enter = false;
    public FrameSetting(int keyIndex) {
        KeyIndex = keyIndex;
    }
    [HideInInspector]
    public bool circleEnable = false;
    public void Use(bool a,bool bullet = false)
    {
        if (!circleEnable && bullet == false)
        {
            resetCount++;
            circleEnable = true;
        }
        Enter = a;

    }
}
[System.Serializable]
public class RandomKeySetting {
    [LabelText("欲随机曲线关键帧序号")]
    public int KeyIndex;
    [LabelText("基础值")]
    public float BaseValue = 1;
    [LabelText("基础时间")]
    public float BaseTime = 1;
    [LabelText("随机值范围")]
    public float RandomRange = 1;
    [LabelText("随机时间范围")]
    public float RandomTimerRange = 1;
    [LabelText("应用到浮点数曲线")]
    public bool Float = true;
    [LabelText("应用到X轴曲线")]
    public bool X;
    [LabelText("应用到Y轴曲线")]
    public bool Y;
    [LabelText("应用到颜色曲线（红色）")]
    public bool R;
    [LabelText("应用到颜色曲线（绿色）")]
    public bool G;
    [LabelText("应用到颜色曲线（蓝色）")]
    public bool B;
    [LabelText("应用到颜色曲线（透明度）")]
    public bool A;

}

[System.Serializable]
public class GroupString {
    public GroupString Copy()
    {
        return (GroupString)this.MemberwiseClone();
    }
    [LabelText("组标识名称")]
    public string Tag;
    [LabelText("启用时间")]
    public float Time;
    [LabelText("受到循环因子影响")]
    public bool Circle = true;
    [LabelText("最大重复刷新次数")]
    public int resetCircle = -1;
    [LabelText("可被启用")]
    public bool Use = true;

    [HideInInspector]
    public int resetCount = 0;
}

[System.Serializable]
public class DynamicParmCurve {

    public DynamicParmCurve Copy() {
        return (DynamicParmCurve)this.MemberwiseClone();
    }
    [FoldoutGroup("动态曲线设置", expanded: false)]
    [LabelText("使用曲线控制曲线")]
    public bool UseCurve = true;
    [FoldoutGroup("动态曲线设置", expanded: false)]
    [LabelText("控制时间")]
    public bool controlTime = false;
    [FoldoutGroup("动态曲线设置", expanded: false)]
    [LabelText("被控制关键帧曲线")]
    [ShowIf("UseCurve")]
    [HideIf("YasixType", CurvesYasix.SomeSingleParm)]
    [HideIf("YasixType", CurvesYasix.ThisCurveIndex)]
    public AnimationCurve DymicParmKeysCurve;
    [FoldoutGroup("动态曲线设置", expanded: false)]
    [LabelText("待控制曲线类型")]
    public CurvesType ControledCurve;
    [FoldoutGroup("动态曲线设置", expanded: false)]
    [LabelText("被控制关键帧在原曲线中序号")]
    public int TargetKeyIndex = 0;

    [FoldoutGroup("动态曲线设置", expanded: false)]
    [LabelText("待指定参数序号")]
    [ShowIf("YasixType", CurvesYasix.SomeSingleParm)]
    public int SingleParmIndex = 0;

    [FoldoutGroup("动态曲线设置", expanded: false)]
    [LabelText("与指定关键帧等同")]
    [ShowIf("YasixType", CurvesYasix.ThisCurveIndex)]
    public int SameWithKey = 0;
    [FoldoutGroup("动态曲线设置", expanded: false)]
    [LabelText("附加值")]
    public float AddValue = 0;
    [FoldoutGroup("动态曲线设置", expanded: false)]
    [LabelText("参数计算")]
    public CalculateMethod method = CalculateMethod.PLUS;
    [FoldoutGroup("动态曲线设置", expanded: false)]
    [LabelText("计算因数")]
    [HideIf("method", CalculateMethod.NONE)]
    public float methodInpand = 0;
    [FoldoutGroup("动态曲线设置", expanded: false)]
    [LabelText("附加随机因子")]
    public float AddRandomValue = 0;

    [FoldoutGroup("动态曲线设置", expanded: false)]
    [LabelText("可刷新次数")]
    public int OnceTime =-1;

    [FoldoutGroup("动态曲线设置", expanded: false)]
    [LabelText("刷新组标识")]
    public string GroupTag;

    [FoldoutGroup("动态曲线设置", expanded: false)]
    [LabelText("截断标识(自动修改)")]
    public bool BreakSign;

    
    public bool Use = true;

    [HideInInspector]
    public float RandomResult = 0;
    public enum CurvesType
    {
        Float = 0,
        VectorX = 1,
        VectorY = 2,
        Red = 3,
        Green = 4,
        Blue = 5,
        Alpha = 6
    }
    public enum CurvesYasix
    {
        RunTime = 0,
        BulletIndex = 1,
        SomeSingleParm = 2,
        ShootingTime = 3,
        ThisCurveIndex = 4,
        BarrageBatch = 5,
        ShootingIndex = 6
    }
    public enum ConstValue
    {
        PLAYER_POS_X = 0,
        PLAYER_POS_Y = 1,
        ANGLE_TO_PLAYER=2
    }
    public enum CalculateMethod
    {
        NONE = -1,
        PLUS = 0,
        MINUS = 1,
        DEVIDE = 2,
        MOD = 3

    }
    [LabelText("因变量结果类型")]
    [ShowIf("UseCurve")]
    public CurvesYasix YasixType;
    [HideIf("UseCurve")]
    public ConstValue Const;
    [HideInInspector]
    public int timeCount = 0;
}
[AddComponentMenu("东方STG框架/弹幕设计/常见事件/子弹事件/子弹参数控制器(曲线+条件)")]
public class BasedEvent_BulletLocomotion : BulletEvent
{
    [InfoBox("该脚本的时间取值范围为[0,1)，为保证程序正常运行，曲线关键帧条件会忽略掉最后一个曲线关键帧的所有条件，请在曲线编辑器里新增一个不使值变化的帧来防止这个条件被忽略")]
    [FoldoutGroup("曲线控制", expanded: false)]
    [LabelText("浮点数(时间)曲线")]

    public AnimationCurve Curve;
    [FoldoutGroup("曲线控制", expanded: false)]
    [LabelText("二维向量X")]
    [ShowIf("isVector2")]
    public AnimationCurve CurveX;
    [FoldoutGroup("曲线控制", expanded: false)]
    [LabelText("二维向量Y")]
    [ShowIf("isVector2")]
    public AnimationCurve CurveY;
    [FoldoutGroup("曲线控制", expanded: false)]
    [LabelText("颜色(红色)")]
    [ShowIf("isVector4")]
    public AnimationCurve Red;
    [FoldoutGroup("曲线控制", expanded: false)]
    [LabelText("颜色(绿色)")]
    [ShowIf("isVector4")]
    public AnimationCurve Green;
    [FoldoutGroup("曲线控制", expanded: false)]
    [LabelText("颜色(蓝色)")]
    [ShowIf("isVector4")]
    public AnimationCurve Blue;
    [FoldoutGroup("曲线控制", expanded: false)]
    [LabelText("颜色(透明度)")]
    [ShowIf("isVector4")]
    public AnimationCurve Alpha;
    [HideInInspector]
    public bool isVector2 = false;
    [HideInInspector]
    public bool isVector4 = false;
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("设置参数时使用反射机制")]
    public bool EnableRelection = true;
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("简化型参数设置(实验)")]
    [HideIf("EnableRelection")]
    public BulletChange simpleSetting ;
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("要控制的成员变量序号")]
    [ShowIf("EnableRelection")]
    [SerializeField, SetProperty("index")]
    private int ParmIndex;
    float t;
    public enum BulletChange { 
        子弹速度 = 0,
        子弹加速度 = 1,
        子弹速度方向 =2,
        子弹速度方向自增 = 3,
        子弹剩余寿命 = 4,
        子弹寿命=5,
        子弹加速度方向 = 6,
        子弹尺寸 = 7,
        子弹颜色 = 8,
        播放创建动画 = 9,
        播放销毁动画 = 10,
        拖影 = 11,
        检测屏幕位置 = 12,
        降低子弹销毁限制 = 13,
    }

    public BulletChange SimpleSetting
    {
        get
        {
            return simpleSetting;
        }
        set
        {
            simpleSetting = value;

            isVector2 = simpleSetting == BulletChange.子弹尺寸 || simpleSetting == BulletChange.子弹加速度方向;
            isVector4 = simpleSetting == BulletChange.子弹颜色;

        }
    }
    public int index
    {
        get
        {
            return ParmIndex;
        }
        set
        {
            ParmIndex = value;

            fInfoSelected = fInfo[ParmIndex];
            if (fInfoSelected != null)
                isVector2 = fInfoSelected.FieldType == typeof(Vector2);
            if (fInfoSelected != null)
                isVector4 = fInfoSelected.FieldType == typeof(Color32);

        }
    }
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("以帧数作为自变量控制曲线")]
    public bool caluateToFrame = false;
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("发射器时间来计算自变量")]
    [Tooltip("以发射器时间来计算时间，需要你提供一个发射器参数。")]
    public bool ShootingTime = false;
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("用来计算自变量的发射器")]
    public Shooting Intend;
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("动态曲线控制器")]
    [SerializeField]
    public List<DynamicParmCurve> CurveChange = new List<DynamicParmCurve>();
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("自由随机曲线控制器")]
    [SerializeField]
    public List<RandomKeySetting> RandomKeySettings = new List<RandomKeySetting>();
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("组标识集合")]
    [SerializeField]
    public List<GroupString> groupStrings = new List<GroupString>();

    private FieldInfo[] fInfo = typeof(Bullet).GetFields();
    private FieldInfo[] fShootingInfo = typeof(Shooting).GetFields();
    private object fInfoValue;          
    private FieldInfo fInfoSelected;
    [FoldoutGroup("曲线开关控制", expanded: false)]
    [LabelText("关键帧开关")]
    [SerializeField]
    public List<FrameSetting> frameSetting = new List<FrameSetting>();
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("可控制的成员变量")]
    public List<string> Name = new List<string>();
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("可控制的发射器成员变量")]
    public List<string> ShootingName = new List<string>();
    [FoldoutGroup("曲线动态控制(CrazyStorm)", expanded: false)]
    [LabelText("曲线循环间隔")]
    public float CalculateCircle = 0;
    [FoldoutGroup("曲线动态控制(CrazyStorm)", expanded: false)]
    [LabelText("曲线循环间隔自增数值")]
    public float selfIncreasing = 0;
  
    [HideInInspector]
    public int OrderIndex = -1;
    [HideInInspector]
    public int OrderCurveIndex = -1;
    [HideInInspector]
    public int OrderCurveIndexX = -1;
    [HideInInspector]
    public int OrderCurveIndexY = -1;
    [HideInInspector]
    public int OrderCurveIndexR = -1;
    [HideInInspector]
    public int OrderCurveIndexG = -1;
    [HideInInspector]
    public int OrderCurveIndexB = -1;
    [HideInInspector]
    public int OrderCurveIndexA = -1;

    [ButtonGroup]
    public void AutoCreateFrameSetting()
    {
        for (int i = 0; i != Curve.keys.Length; ++i)
        {
            frameSetting.Add(new FrameSetting(i));
        }
    }
    [ButtonGroup]
    public void Reorder()
    {
        frameSetting.Sort(delegate (FrameSetting p1, FrameSetting p2)
        {
            return p1.KeyIndex.CompareTo(p2.KeyIndex);
        });
    }

    public void GetInfusionMethod(float time, Bullet Target)
    {
        if (index != -1)
            ParmIndex = index;

        if (frameSetting.Count == 0)
            return;

        int Key = 0;
        for (int i = 0; i != frameSetting.Count; ++i)
        {
            frameSetting[i].Use(false,true);
          
        }
        // Debug.Log("当前时间点" + time.ToString());
        for (int i = 0; i != frameSetting.Count; ++i)
        {

            if (Curve.keys[frameSetting[i].KeyIndex].time > time)
            {
                Target.CommandList[OrderIndex].MinIndex = i;
                if (i - 1 == 0)
                {
                    foreach (var r in Target.CommandList)
                    {
                        r.Enable = true;
                    }
                }
                frameSetting[Target.CommandList[OrderIndex].MinIndex].Use(true,true);
                Target.CommandList[OrderIndex].UseTime++;
                Key = frameSetting[Target.CommandList[OrderIndex].MinIndex].KeyIndex;
                break;
            }

        }
        //  Debug.Log("MinIndex 为" + MinIndex.ToString());
        List<FrameSetting> muitiConditon = new List<FrameSetting>();
        for (int i = 0; i != frameSetting.Count; ++i)
        {
            if (frameSetting[i].KeyIndex == Key)
                muitiConditon.Add(frameSetting[i]);

        }
        // Debug.Log("检测到有" + muitiConditon.Count.ToString() + "个条件");
        List<bool> muitiConditonChecking = new List<bool>();
        for (int i = 0; i != muitiConditon.Count; ++i)
        {
            bool t = CanEnterFrame(time, muitiConditon[i], Target);
            muitiConditonChecking.Add(t);
            if (t && muitiConditon[i].OnlyOr)
            {
                Target.CommandList[OrderIndex].CaluateMethod = frameSetting[Target.CommandList[OrderIndex].MinIndex].infusion;
                Target.CommandList[OrderIndex].DefaultValue = frameSetting[Target.CommandList[OrderIndex].MinIndex].DefaultValue;
                Target.CommandList[OrderIndex].ResultDefaultValue = frameSetting[Target.CommandList[OrderIndex].MinIndex].ResultDefaultValue;
                Target.CommandList[OrderIndex].Const = frameSetting[Target.CommandList[OrderIndex].MinIndex].Const;
                Target.CommandList[OrderIndex].compareShooting = frameSetting[Target.CommandList[OrderIndex].MinIndex].compareShooting;
                return;
            }
        }
        bool allTrue = true;

        for (int i = 0; i != muitiConditonChecking.Count; ++i)
        {
            if (muitiConditonChecking[i] == false)
                allTrue = false;
            if (allTrue)
            {
                Target.CommandList[OrderIndex].CaluateMethod = frameSetting[Target.CommandList[OrderIndex].MinIndex].infusion;
                Target.CommandList[OrderIndex].DefaultValue = frameSetting[Target.CommandList[OrderIndex].MinIndex].DefaultValue;
                Target.CommandList[OrderIndex].ResultDefaultValue = frameSetting[Target.CommandList[OrderIndex].MinIndex].ResultDefaultValue;
                Target.CommandList[OrderIndex].Const = frameSetting[Target.CommandList[OrderIndex].MinIndex].Const;
                Target.CommandList[OrderIndex].compareShooting = frameSetting[Target.CommandList[OrderIndex].MinIndex].compareShooting;

            }
            else
                Target.CommandList[OrderIndex].CaluateMethod = FrameSetting.Infusion.NONE;

        }

        //  Debug.Log("最终被修改成了" + CaluateMethod);
    }

    public bool CanEnterFrame(float time, FrameSetting r, Bullet Target)
    {
        Shooting TargetShooting = null;
        if (r.CompareOtherShooting)
            TargetShooting = r.compareShooting;

        float TargetValue = r.Compare;
        if (Target.CommandList[OrderIndex].UseTime > r.Recircle || r.Recircle != -1)
            return false;
        if (r.ParmIndex == -1)
        {
            float t = Global.PlayerObject.gameObject.transform.position.x;
            t = CalculateValue(t, r.methodInpand, r.method);
            switch (r.condition)
            {
                case FrameSetting.FrameCondition.EQUAL:
                    return t == TargetValue;
                case FrameSetting.FrameCondition.LESSTHAN:
                    return t < TargetValue;
                case FrameSetting.FrameCondition.MORETHAN:
                    return t > TargetValue;
            }

        }
        if (r.ParmIndex == -2)
        {
            float t = Global.PlayerObject.gameObject.transform.position.y;
            t = CalculateValue(t, r.methodInpand, r.method);
            switch (r.condition)
            {
                case FrameSetting.FrameCondition.EQUAL:
                    return t == TargetValue;
                case FrameSetting.FrameCondition.LESSTHAN:
                    return t < TargetValue;
                case FrameSetting.FrameCondition.MORETHAN:
                    return t > TargetValue;
            }

        }

        if (r.ParmIndex == -3)
        {
            float t = gameObject.transform.position.x;
            t = CalculateValue(t, r.methodInpand, r.method);
            switch (r.condition)
            {
                case FrameSetting.FrameCondition.EQUAL:
                    return t == TargetValue;
                case FrameSetting.FrameCondition.LESSTHAN:
                    return t < TargetValue;
                case FrameSetting.FrameCondition.MORETHAN:
                    return t > TargetValue;
            }

        }

        if (r.ParmIndex == -4)
        {
            float t = gameObject.transform.position.y;
            t = CalculateValue(t, r.methodInpand, r.method);
            switch (r.condition)
            {
                case FrameSetting.FrameCondition.EQUAL:
                    return t == TargetValue;
                case FrameSetting.FrameCondition.LESSTHAN:
                    return t < TargetValue;
                case FrameSetting.FrameCondition.MORETHAN:
                    return t > TargetValue;
            }

        }
        if (r.ParmIndex == -5)
        {
            if (!r.CompareOtherShooting) return false;
            float t = r.compareShooting.gameObject.transform.position.x;
            t = CalculateValue(t, r.methodInpand, r.method);
            switch (r.condition)
            {
                case FrameSetting.FrameCondition.EQUAL:
                    return t == TargetValue;
                case FrameSetting.FrameCondition.LESSTHAN:
                    return t < TargetValue;
                case FrameSetting.FrameCondition.MORETHAN:
                    return t > TargetValue;
            }

        }
        if (r.ParmIndex == -6)
        {
            if (!r.CompareOtherShooting) return false;
            float t = r.compareShooting.gameObject.transform.position.y;
            t = CalculateValue(t, r.methodInpand, r.method);
            switch (r.condition)
            {
                case FrameSetting.FrameCondition.EQUAL:
                    return t == TargetValue;
                case FrameSetting.FrameCondition.LESSTHAN:
                    return t < TargetValue;
                case FrameSetting.FrameCondition.MORETHAN:
                    return t > TargetValue;
            }

        }
        if (r.ParmIndex == -7)
        {
            if (!r.CompareOtherShooting) return false;
            float t = Vector2.Distance(Target.gameObject.transform.position, Global.PlayerObject.transform.position);
            t = CalculateValue(t, r.methodInpand, r.method);
            switch (r.condition)
            {
                case FrameSetting.FrameCondition.EQUAL:
                    return t == TargetValue;
                case FrameSetting.FrameCondition.LESSTHAN:
                    return t < TargetValue;
                case FrameSetting.FrameCondition.MORETHAN:
                    return t > TargetValue;
            }

        }
        if (r.ParmIndex == -8)
        {
            return true;

        }
        if (fInfo[r.ParmIndex].FieldType == typeof(int))
        {
            int t = r.CompareOtherShooting ? (int)fShootingInfo[r.ParmIndex].GetValue(TargetShooting) : (int)fInfo[r.ParmIndex].GetValue(Target);
            t = (int)CalculateValue(t, r.methodInpand, r.method);
            switch (r.condition)
            {
                case FrameSetting.FrameCondition.EQUAL:
                    return t == (int)TargetValue;
                case FrameSetting.FrameCondition.LESSTHAN:
                    return t < (int)TargetValue;
                case FrameSetting.FrameCondition.MORETHAN:
                    return t > (int)TargetValue;
            }
        }
        if (fInfo[r.ParmIndex].FieldType == typeof(float))
        {
            float t = r.CompareOtherShooting ? (float)fShootingInfo[r.ParmIndex].GetValue(TargetShooting) : (float)fInfo[r.ParmIndex].GetValue(Target);
            t = CalculateValue(t, r.methodInpand,r.method);
            switch (r.condition)
            {
                case FrameSetting.FrameCondition.EQUAL:
                    return t == TargetValue;
                case FrameSetting.FrameCondition.LESSTHAN:
                    return t < TargetValue;
                case FrameSetting.FrameCondition.MORETHAN:
                    return t > TargetValue;
            }
        }
        if (fInfo[r.ParmIndex].FieldType == typeof(bool))
        {
            bool t = System.Convert.ToBoolean(r.CompareOtherShooting ? (float)fShootingInfo[r.ParmIndex].GetValue(TargetShooting) : (float)fInfo[r.ParmIndex].GetValue(Target));
            switch (r.condition)
            {
                case FrameSetting.FrameCondition.BOOLONLY_FALSE:
                    return false == t;
                case FrameSetting.FrameCondition.BOOLONLY_TRUE:
                    return true == t;
            }
        }
        return false;
    }
    public object GetValue(CommandController setting, FrameSetting.Original original, Bullet Target, float t, float Const)
    {

        switch (original)
        {
            case FrameSetting.Original.ORINGIAL:
                return (float)fInfoValue;
            case FrameSetting.Original.CONST:
                return Const;
            case FrameSetting.Original.PLAYER_POS_X:
                return Global.PlayerObject.gameObject.transform.position.x;
            case FrameSetting.Original.PLAYER_POS_Y:
                return Global.PlayerObject.gameObject.transform.position.y;
            case FrameSetting.Original.THIS_POS_X:
                return gameObject.transform.position.x;
            case FrameSetting.Original.THIS_POS_Y:
                return gameObject.transform.position.y;
            case FrameSetting.Original.OBJECT_POS_X:
                return setting.compareShooting.gameObject.transform.position.x;
            case FrameSetting.Original.OBJECT_POS_Y:
                return setting.compareShooting.gameObject.transform.position.y;
            case FrameSetting.Original.CURVE:
                return Target.SingleParmCurve[OrderCurveIndex].Evaluate(t);
            case FrameSetting.Original.CURVE_X:
                return Target.SingleParmCurve[OrderCurveIndexX].Evaluate(t);
            case FrameSetting.Original.CURVE_Y:
                return Target.SingleParmCurve[OrderCurveIndexY].Evaluate(t);
            case FrameSetting.Original.CURVE_R:
                return Target.SingleParmCurve[OrderCurveIndexR].Evaluate(t);
            case FrameSetting.Original.CURVE_G:
                return Target.SingleParmCurve[OrderCurveIndexG].Evaluate(t);
            case FrameSetting.Original.CURVE_B:
                return Target.SingleParmCurve[OrderCurveIndexB].Evaluate(t);
            case FrameSetting.Original.CURVE_A:
                return Target.SingleParmCurve[OrderCurveIndexA].Evaluate(t);
            case FrameSetting.Original.ANGLE_TO_PLAYER:
                return Math2D.GetAimToObjectRotation(Target.gameObject, Global.PlayerObject);

        }
        return 0;
    }
    private void Update()
    {

    }
    public float CalculateValue(float a,float b,DynamicParmCurve.CalculateMethod method) {
        switch (method) {
            case DynamicParmCurve.CalculateMethod.NONE:
                return a;
            case DynamicParmCurve.CalculateMethod.PLUS:
                return a + b;
            case DynamicParmCurve.CalculateMethod.MOD:
                return a % b;
            case DynamicParmCurve.CalculateMethod.MINUS:
                return a - b;
            case DynamicParmCurve.CalculateMethod.DEVIDE:
                return a / b;


        }
        return 0;
    }
    public int ReturnCircle(float t)
    {
        float T = Mathf.Clamp(Curve.keys[Curve.keys.Length - 1].time - Curve.keys[0].time, 0, 0xffffffff);
        float Cross = t / T;
        return (int)Cross;
    }
    public override void OnBulletMoving(Bullet Target)
    {
        Target.CommandList[OrderIndex].timeCount = Target.TotalLiveFrame - Target.CommandList[OrderIndex].looper * CalculateCircle;
        if (CalculateCircle != 0 && selfIncreasing != 0  &&Target.CommandList[OrderIndex].timeCount > CalculateCircle)
        {
            Target.CommandList[OrderIndex].timeCount = 0;
            Target.CommandList[OrderIndex].looper++;
            LoopCurveOffset(Target);

        }
        if (Target.TotalLiveFrame - Target.delayBulletEventRunTime < 0 || Target == null)
            return;

        fInfoSelected = fInfo[ParmIndex];

        
        if (ShootingTime && TargetShooting != null)
            t = Intend.TotalFrame / Intend.MaxLiveFrame * (caluateToFrame ? Target.MaxLiveFrame : 1);
        else
        {
            t = (Target.TotalLiveFrame - Target.delayBulletEventRunTime) / (Target.MaxLiveFrame - Target.delayBulletEventRunTime) * (caluateToFrame ? Target.MaxLiveFrame : 1);
            //Debug.Log(t);
        }
        
        if (t < Target.SingleParmCurve[OrderCurveIndex].keys[0].time || t > Target.SingleParmCurve[OrderCurveIndex].keys[Target.SingleParmCurve[OrderCurveIndex].keys.Length - 1].time)
            return;
        foreach (DynamicParmCurve a in Target.CommandList[OrderIndex].CurveChange)
        {
            if (!a.Use)
                continue;
            AnimationCurve CurveType = new AnimationCurve(Target.SingleParmCurve[OrderCurveIndex + (int)a.ControledCurve].keys);
          
            if (a.UseCurve)
            {
         

                foreach (var Tag in Target.CommandList[OrderIndex].groupStrings)
                {
                    if (Tag.resetCircle < Tag.resetCount && Tag.resetCircle != -1)
                        continue;
                    if (Tag.Time <t && Tag.Use)
                    {
                        Target.CommandList[OrderIndex].groupTag = Tag.Tag;
                        Tag.Use = false;
                        Tag.resetCount++;
                       // Debug.Log("Circle Passed");
                        break;
                    }
                }

                if (a.GroupTag != Target.CommandList[OrderIndex].groupTag && a.GroupTag != string.Empty)
                {
                    Target.SingleParmCurve[OrderCurveIndex + (int)a.ControledCurve] = CurveType;
                  //  Debug.Log("Group Check Passed");
                    continue;
                }
                switch (a.YasixType)
                {
                    case DynamicParmCurve.CurvesYasix.RunTime:

                        float ra = CurveType.keys[a.TargetKeyIndex].time;
                        Keyframe aredcr = CurveType.keys[a.TargetKeyIndex];
                        aredcr.value = CalculateValue( a.DymicParmKeysCurve.Evaluate(Time.time) + a.AddValue + a.RandomResult,a.methodInpand,a.method);
                        CurveType.RemoveKey(a.TargetKeyIndex);
                        CurveType.AddKey(aredcr);
                        break;
                    case DynamicParmCurve.CurvesYasix.BulletIndex:
                        float rc = CurveType.keys[a.TargetKeyIndex].time;
                        Keyframe aredc = CurveType.keys[a.TargetKeyIndex];
                        aredc.value = CalculateValue(a.DymicParmKeysCurve.Evaluate(Target.BulletIndex) + a.AddValue + a.RandomResult,a.methodInpand,a.method);
                        CurveType.RemoveKey(a.TargetKeyIndex);
                        CurveType.AddKey(aredc);
                        break;
                    case DynamicParmCurve.CurvesYasix.BarrageBatch:
                        float rcer = CurveType.keys[a.TargetKeyIndex].time;
                        Keyframe aredcer = CurveType.keys[a.TargetKeyIndex];
                        aredcer.value = CalculateValue(a.DymicParmKeysCurve.Evaluate(Target.BarrageBatch) + a.AddValue + a.RandomResult,a.methodInpand,a.method);
                        CurveType.RemoveKey(a.TargetKeyIndex);
                        CurveType.AddKey(aredcer);
                        break;

                    case DynamicParmCurve.CurvesYasix.SomeSingleParm:
                        float rd = CurveType.keys[a.TargetKeyIndex].time;
                        Keyframe arec = CurveType.keys[a.TargetKeyIndex];
                        arec.value = CalculateValue((float)fInfo[a.SingleParmIndex].GetValue(Target) + a.AddValue + a.RandomResult, a.methodInpand, a.method);
                        CurveType.RemoveKey(a.TargetKeyIndex);
                        CurveType.AddKey(arec);
                        break;
                    case DynamicParmCurve.CurvesYasix.ShootingTime:
                        float red = CurveType.keys[a.TargetKeyIndex].time;
                        Keyframe areedcr = CurveType.keys[a.TargetKeyIndex];
                        areedcr.value = CalculateValue(a.DymicParmKeysCurve.Evaluate
                            (Intend.TotalFrame / Intend.MaxLiveFrame) + a.AddValue + a.RandomResult, a.methodInpand, a.method);
                        CurveType.RemoveKey(a.TargetKeyIndex);
                        CurveType.AddKey(areedcr);
                        break;
                    case DynamicParmCurve.CurvesYasix.ThisCurveIndex:
                        float reda = CurveType.keys[a.TargetKeyIndex].time;
                        Keyframe areedcra = CurveType.keys[a.TargetKeyIndex];
                        areedcra.value = CalculateValue(CurveType.keys[a.SameWithKey].value + a.AddValue + a.RandomResult, a.methodInpand, a.method);
                        CurveType.RemoveKey(a.TargetKeyIndex);
                        CurveType.AddKey(areedcra);
                        break;
                    case DynamicParmCurve.CurvesYasix.ShootingIndex:
                        float redad = CurveType.keys[a.TargetKeyIndex].time;
                        Keyframe areedcrad = CurveType.keys[a.TargetKeyIndex];
                        areedcrad.value = CalculateValue(a.DymicParmKeysCurve.Evaluate(Target.shootingIndex) + a.AddValue + a.RandomResult, a.methodInpand, a.method);
                        CurveType.RemoveKey(a.TargetKeyIndex);
                        CurveType.AddKey(areedcrad);
                        break;
                }
            }
            else
            {
                switch (a.Const)
                {
                    case DynamicParmCurve.ConstValue.PLAYER_POS_X:
                        float rab = CurveType.keys[a.TargetKeyIndex].time;
                        CurveType.RemoveKey(a.TargetKeyIndex);
                        CurveType.AddKey(rab, Global.PlayerObject.transform.position.x + a.AddValue + a.RandomResult);
                        break;
                    case DynamicParmCurve.ConstValue.PLAYER_POS_Y:
                        float rcb = CurveType.keys[a.TargetKeyIndex].time;
                        CurveType.RemoveKey(a.TargetKeyIndex);
                        CurveType.AddKey(rcb, CalculateValue(Global.PlayerObject.transform.position.y + a.AddValue + a.RandomResult ,a.methodInpand, a.method));
                        break;
                    case DynamicParmCurve.ConstValue.ANGLE_TO_PLAYER:
                        float raed = CurveType.keys[a.TargetKeyIndex].time;
                        CurveType.RemoveKey(a.TargetKeyIndex);
                        CurveType.AddKey(raed, CalculateValue(Math2D.GetAimToObjectRotation(Target.gameObject, Global.PlayerObject) + a.AddValue + a.RandomResult ,a.methodInpand, a.method));
                        Debug.Log(Math2D.GetAimToObjectRotation(Target.gameObject, Global.PlayerObject) + a.AddValue + a.RandomResult);
                        break;

                }
            }
            a.timeCount++;
            if (a.timeCount > a.OnceTime && a.OnceTime != -1)
                a.Use = false;
            Target.SingleParmCurve[OrderCurveIndex+(int)a.ControledCurve] = CurveType;
        }


        Target.CommandList[OrderIndex].IndexChecking = Target.CommandList[OrderIndex].MinIndex;

        GetInfusionMethod(t, Target);
        // 以下将设置子弹的值。
        if (Target.CommandList[OrderIndex].CaluateMethod == FrameSetting.Infusion.REPLACE)
        {
           
            object c = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[0].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[0].ResultConst);
            if (fInfoSelected.FieldType == typeof(int))
                SetValue(Target, System.Convert.ToInt32(c));
            if (fInfoSelected.FieldType == typeof(float))
                SetValue(Target, c);
            if (fInfoSelected.FieldType == typeof(bool))
               SetValue(Target, System.Convert.ToBoolean(c));
            if (fInfoSelected.FieldType == typeof(Vector2))
            {
                if (frameSetting.Count == 0)
                {
                    Target.CommandList[OrderIndex].ResultDefaultValue[0].BaseValue = FrameSetting.Original.CURVE_X;
                    Target.CommandList[OrderIndex].ResultDefaultValue[1].BaseValue = FrameSetting.Original.CURVE_Y;
                }
                c = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[0].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[0].ResultConst);
                object g = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[1].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[1].ResultConst);
                Vector2 v = new Vector2((float)c,(float)g);

               SetValue(Target, v);
            }
            if (fInfoSelected.FieldType == typeof(Color))
            {
                if (frameSetting.Count == 0)
                {
                    Target.CommandList[OrderIndex].ResultDefaultValue[0].BaseValue = FrameSetting.Original.CURVE_R;
                    Target.CommandList[OrderIndex].ResultDefaultValue[1].BaseValue = FrameSetting.Original.CURVE_G;
                    Target.CommandList[OrderIndex].ResultDefaultValue[2].BaseValue = FrameSetting.Original.CURVE_B;
                    Target.CommandList[OrderIndex].ResultDefaultValue[3].BaseValue = FrameSetting.Original.CURVE_A;
                }
                c = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[0].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[0].ResultConst);
                object g = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[1].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[1].ResultConst);
                object b = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[2].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[2].ResultConst);
                object a = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[3].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[3].ResultConst);
      

                Color v = new Color((float)c, (float)g, (float)b, (float)a);



                SetValue(Target, v);
            }

        }
        if (Target.CommandList[OrderIndex].CaluateMethod == FrameSetting.Infusion.ADD)
        {
            object c = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[0].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[0].ResultConst);
            object r = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].DefaultValue, Target, t, Target.CommandList[OrderIndex].Const);
            if (fInfoSelected.FieldType == typeof(int))
               SetValue(Target, (int)r + System.Convert.ToInt32(c));
            if (fInfoSelected.FieldType == typeof(float))
               SetValue(Target, (float)r + (float)c);
            if (fInfoSelected.FieldType == typeof(bool))
                SetValue(Target, System.Convert.ToBoolean(c));
            if (fInfoSelected.FieldType == typeof(Vector2))
            {
                if (frameSetting.Count == 0)
                {
                    Target.CommandList[OrderIndex].ResultDefaultValue[0].BaseValue = FrameSetting.Original.CURVE_X;
                    Target.CommandList[OrderIndex].ResultDefaultValue[1].BaseValue = FrameSetting.Original.CURVE_Y;
                }
                c = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[0].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[0].ResultConst);
                object g = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[1].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[1].ResultConst);
                Vector2 v = new Vector2((float)c, (float)g);
                Vector2 p = (Vector2)r;
                p.x += v.x;

                p.y += v.y;
                SetValue(Target, p);
            }
            if (fInfoSelected.FieldType == typeof(Color))
            {
                if (frameSetting.Count == 0)
                {
                    Target.CommandList[OrderIndex].ResultDefaultValue[0].BaseValue = FrameSetting.Original.CURVE_R;
                    Target.CommandList[OrderIndex].ResultDefaultValue[1].BaseValue = FrameSetting.Original.CURVE_G;
                    Target.CommandList[OrderIndex].ResultDefaultValue[2].BaseValue = FrameSetting.Original.CURVE_B;
                    Target.CommandList[OrderIndex].ResultDefaultValue[3].BaseValue = FrameSetting.Original.CURVE_A;
                }
                c = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[0].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[0].ResultConst);
                object g = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[1].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[1].ResultConst);
                object b = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[2].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[2].ResultConst);
                object a = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[3].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[3].ResultConst);
                Color v = new Color((float)c, (float)g, (float)b, (float)a);
                Color p = (Color)r;
                p.r += v.r;

                p.b +=v.g;

                //p.g += v.b;

                p.a += v.a;
               SetValue(Target, p);
            }

        }
        if (Target.CommandList[OrderIndex].CaluateMethod == FrameSetting.Infusion.MINUS)
        {
            object c = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[0].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[0].ResultConst);
            object r = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].DefaultValue, Target, t, Target.CommandList[OrderIndex].Const);
            if (fInfoSelected.FieldType == typeof(int))
                SetValue(Target, (int)r - System.Convert.ToInt32(c));
            if (fInfoSelected.FieldType == typeof(float))
                SetValue(Target, (float)r - (float)c);
            if (fInfoSelected.FieldType == typeof(bool))
                SetValue(Target, System.Convert.ToBoolean(c));
            if (fInfoSelected.FieldType == typeof(Vector2))
            {
                c = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[0].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[0].ResultConst);
                object g = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[1].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[1].ResultConst);
                Vector2 v = new Vector2((float)c, (float)g);
                Vector2 p = (Vector2)r;
                p.x -= v.x;

                p.y -= v.y;
                SetValue(Target, p);
            }
            if (fInfoSelected.FieldType == typeof(Color))
            {
                c = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[0].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[0].ResultConst);
                object g = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[1].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[1].ResultConst);
                object b = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[2].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[2].ResultConst);
                object a = GetValue(Target.CommandList[OrderIndex], Target.CommandList[OrderIndex].ResultDefaultValue[3].BaseValue, Target, t, Target.CommandList[OrderIndex].ResultDefaultValue[3].ResultConst);
                Color v = new Color((float)c, (float)g, (float)b, (float)a);

                Color p = (Color)r;
                p.r -= v.r;

                p.b -= v.g;

                p.g -= v.b;

                p.a -= v.a;
                SetValue(Target, p);
            }

        }
        if (Target.CommandList[OrderIndex].CaluateMethod == FrameSetting.Infusion.RANDOM)
        {

            if (Target.CommandList[OrderIndex].IndexChecking != Target.CommandList[OrderIndex].MinIndex)
            {
                Target.CommandList[OrderIndex].RandomCaValue = Random.Range(frameSetting[Target.CommandList[OrderIndex].MinIndex].MinRandomRange,
                    frameSetting[Target.CommandList[OrderIndex].MinIndex].MaxRandomRange);
            }

            if (fInfoSelected.FieldType == typeof(int))
            {
                if ((Target.CommandList[OrderIndex].Enable))
                {
                    int keyIndex = frameSetting[Target.CommandList[OrderIndex].MinIndex].KeyIndex;
                    AnimationCurve curve = Target.SingleParmCurve[OrderCurveIndex];
                    Keyframe keyframe = curve[keyIndex];
                    Target.SingleParmCurve[OrderCurveIndex].RemoveKey(keyIndex);
                    Target.SingleParmCurve[OrderCurveIndex].AddKey(keyframe.time, keyframe.value + (int)Random.Range(frameSetting[Target.CommandList[OrderIndex].MinIndex].MinRandomRange,
                        frameSetting[Target.CommandList[OrderIndex].MinIndex].MaxRandomRange));
                }
                SetValue(Target, Target.SingleParmCurve[OrderCurveIndex].Evaluate(t));
            }
            if (fInfoSelected.FieldType == typeof(float))
            {
                if ((Target.CommandList[OrderIndex].Enable))
                {
                    int keyIndex = frameSetting[Target.CommandList[OrderIndex].MinIndex].KeyIndex;
                    AnimationCurve curve = Target.SingleParmCurve[OrderCurveIndex];
                    Keyframe keyframe = curve[keyIndex];
                    Target.SingleParmCurve[OrderCurveIndex].RemoveKey(keyIndex);
                    Target.SingleParmCurve[OrderCurveIndex].AddKey(keyframe.time, keyframe.value + (int)Random.Range(frameSetting[Target.CommandList[OrderIndex].MinIndex].MinRandomRange,
                        frameSetting[Target.CommandList[OrderIndex].MinIndex].MaxRandomRange));
                }
                SetValue(Target, Target.SingleParmCurve[OrderCurveIndex].Evaluate(t));
            }
            if (fInfoSelected.FieldType == typeof(Color))
            {

                Color v = (Color)fInfoValue;

                v.r = Random.Range(frameSetting[Target.CommandList[OrderIndex].MinIndex].MinRandomRange,
                frameSetting[Target.CommandList[OrderIndex].MinIndex].MaxRandomRange);

                v.b = Random.Range(frameSetting[Target.CommandList[OrderIndex].MinIndex].MinRandomRange,
                frameSetting[Target.CommandList[OrderIndex].MinIndex].MaxRandomRange);

                v.g = Random.Range(frameSetting[Target.CommandList[OrderIndex].MinIndex].MinRandomRange,
                frameSetting[Target.CommandList[OrderIndex].MinIndex].MaxRandomRange);

                v.a = Random.Range(frameSetting[Target.CommandList[OrderIndex].MinIndex].MinRandomRange,
                frameSetting[Target.CommandList[OrderIndex].MinIndex].MaxRandomRange);
                SetValue(Target, v);
            }
            if (fInfoSelected.FieldType == typeof(Vector2))
            {
                Vector2 v = (Vector2)fInfoValue;

                v.x = Random.Range(frameSetting[Target.CommandList[OrderIndex].MinIndex].MinRandomRange,
                    frameSetting[Target.CommandList[OrderIndex].MinIndex].MaxRandomRange);

                v.y = Random.Range(frameSetting[Target.CommandList[OrderIndex].MinIndex].MinRandomRange,
                    frameSetting[Target.CommandList[OrderIndex].MinIndex].MaxRandomRange);
                SetValue(Target, v);
            }
            if (Target.CommandList[OrderIndex].Once)
                Target.CommandList[OrderIndex].Enable = false;
        }
    }
    public override void OnBulletActive()
    {
  
        fInfoSelected = fInfo[ParmIndex];

    }

    public void RandomKey(Bullet Target)
    {
        foreach (var a in RandomKeySettings)
        {
            if (a.Float)
            {

                Target.SingleParmCurve[OrderCurveIndex].RemoveKey(a.KeyIndex);
                Target.SingleParmCurve[OrderCurveIndex].AddKey(a.BaseTime + Random.Range(-a.RandomTimerRange, a.RandomTimerRange), a.BaseValue + Random.Range(-a.RandomRange, a.RandomRange));
            }
            if (a.X)
            {
                Target.SingleParmCurve[OrderCurveIndexX].RemoveKey(a.KeyIndex);
                Target.SingleParmCurve[OrderCurveIndexX].AddKey(a.BaseTime + Random.Range(-a.RandomTimerRange, a.RandomTimerRange), a.BaseValue + Random.Range(-a.RandomRange, a.RandomRange));
            }
            if (a.Y)
            {
                Target.SingleParmCurve[OrderCurveIndexY].RemoveKey(a.KeyIndex);
                Target.SingleParmCurve[OrderCurveIndexY].AddKey(a.BaseTime + Random.Range(-a.RandomTimerRange, a.RandomTimerRange), a.BaseValue + Random.Range(-a.RandomRange, a.RandomRange));
            }
            if (a.R)
            {
                Target.SingleParmCurve[OrderCurveIndexR].RemoveKey(a.KeyIndex);
                Target.SingleParmCurve[OrderCurveIndexR].AddKey(a.BaseTime + Random.Range(-a.RandomTimerRange, a.RandomTimerRange), a.BaseValue + Random.Range(-a.RandomRange, a.RandomRange));
            }
            if (a.G)
            {
                Target.SingleParmCurve[OrderCurveIndexG].RemoveKey(a.KeyIndex);
                Target.SingleParmCurve[OrderCurveIndexG].AddKey(a.BaseTime + Random.Range(-a.RandomTimerRange, a.RandomTimerRange), a.BaseValue + Random.Range(-a.RandomRange, a.RandomRange));
            }
            if (a.B)
            {
                Target.SingleParmCurve[OrderCurveIndexB].RemoveKey(a.KeyIndex);
                Target.SingleParmCurve[OrderCurveIndexB].AddKey(a.BaseTime + Random.Range(-a.RandomTimerRange, a.RandomTimerRange), a.BaseValue + Random.Range(-a.RandomRange, a.RandomRange));
            }
            if (a.A)
            {
                Target.SingleParmCurve[OrderCurveIndexA].RemoveKey(a.KeyIndex);
                Target.SingleParmCurve[OrderCurveIndexA].AddKey(a.BaseTime + Random.Range(-a.RandomTimerRange, a.RandomTimerRange), a.BaseValue + Random.Range(-a.RandomRange, a.RandomRange));
            }
        }
    }
    public override void OnBulletCreated(Bullet Target)
    {
        fInfoSelected = fInfo[ParmIndex];
        fInfoValue = fInfoSelected.GetValue(Target);
        Target.CommandList.Add(new CommandController());
        OrderIndex = Target.CommandList.Count - 1;
        Target.SingleParmCurve.Add(new AnimationCurve( Curve.keys));
        OrderCurveIndex = Target.SingleParmCurve.Count - 1;
        Target.SingleParmCurve.Add(new AnimationCurve(CurveX.keys));
        OrderCurveIndexX = Target.SingleParmCurve.Count - 1;
        Target.SingleParmCurve.Add(new AnimationCurve(CurveY.keys));
        OrderCurveIndexY = Target.SingleParmCurve.Count - 1;
        Target.SingleParmCurve.Add(new AnimationCurve(Red.keys));
        OrderCurveIndexR = Target.SingleParmCurve.Count - 1;
        Target.SingleParmCurve.Add(new AnimationCurve(Green.keys));
        OrderCurveIndexG = Target.SingleParmCurve.Count - 1;
        Target.SingleParmCurve.Add(new AnimationCurve(Blue.keys));
        OrderCurveIndexB = Target.SingleParmCurve.Count - 1;
        Target.SingleParmCurve.Add(new AnimationCurve(Alpha.keys));
        OrderCurveIndexA = Target.SingleParmCurve.Count - 1;
        foreach (var a in CurveChange)
        {
            Target.CommandList[OrderIndex].CurveChange.Add(a.Copy());
        }
        foreach (var a in groupStrings)
        {
            Target.CommandList[OrderIndex].groupStrings.Add(a.Copy());
        }
        RandomKey(Target);
    }
   
    public void LoopCurveOffset(Bullet Target)
    {
        foreach (var a in Target.CommandList[OrderIndex].CurveChange)
        {
            a.RandomResult = Random.Range(-a.AddRandomValue, a.AddRandomValue);
            a.Use = true;
        }

        Keyframe[] Keys = Target.SingleParmCurve[OrderCurveIndex].keys;
        foreach (var a in frameSetting)
        {
            a.Enable = true;
        }
        List<Keyframe> addKeys = new List<Keyframe>();
        foreach (var a in Keys)
        {
            Keyframe keyframe = a;
            keyframe.time += selfIncreasing;
            addKeys.Add(keyframe);
            Target.SingleParmCurve[OrderCurveIndex].RemoveKey(0);


        }
      
        foreach (var a in groupStrings)
        {
            if (a.Circle)
                a.Time += selfIncreasing;
        }
        foreach (DynamicParmCurve a in Target.CommandList[OrderIndex].CurveChange)
        {
         
                a.BreakSign = Target.SingleParmCurve[OrderCurveIndex].keys[a.TargetKeyIndex].time < t;
        }
        foreach (var b in addKeys)
        {

            Target.SingleParmCurve[OrderCurveIndex].AddKey(b);
        }


    }
    protected override void OnBeforeSerialize()
    {
        if (Name.Count == fInfo.Length || Application.isPlaying)
        {
            return;
        }
        Reorder();
        Debug.Log("Updated");
        Name.Clear();
        for (int i = 0; i != fInfo.Length; ++i)
        {
            if (fInfo[i].FieldType != typeof(float) || fInfo[i].FieldType != typeof(bool) || fInfo[i].FieldType != typeof(bool))
            {
                object[] Translate = fInfo[i].GetCustomAttributes(typeof(LabelTextAttribute), true);

                for (int g = 0; g != Translate.Length; ++g)
                {
                    LabelTextAttribute _Lable = (LabelTextAttribute)Translate[g];
                    Name.Add(_Lable.Text);

                }

                if (Translate.Length == 0)
                    Name.Add(fInfo[i].Name);
            }
        }
        for (int i = 0; i != fShootingInfo.Length; ++i)
        {
            if (fShootingInfo[i].FieldType != typeof(float) || fShootingInfo[i].FieldType != typeof(bool) || fShootingInfo[i].FieldType != typeof(bool))
            {
                object[] Translate = fShootingInfo[i].GetCustomAttributes(typeof(LabelTextAttribute), true);

                for (int g = 0; g != Translate.Length; ++g)
                {
                    LabelTextAttribute _Lable = (LabelTextAttribute)Translate[g];
                    ShootingName.Add(_Lable.Text);

                }

                if (Translate.Length == 0)
                    ShootingName.Add(fShootingInfo[i].Name);
            }
        }
    }
    public void SetValue(Bullet Target, object Value) {
        if (!EnableRelection)
        {
            switch (simpleSetting)
            {
                case BulletChange.子弹速度:
                    Target.Speed = (float)Value;
                    break;
                case BulletChange.子弹剩余寿命:
                    Target.TotalLiveFrame = (float)Value;
                    break;
                case BulletChange.子弹加速度:
                    Target.AcceleratedSpeed = (float)Value;
                    break;
                case BulletChange.子弹加速度方向:
                    Target.AcceleratedSpeedDirectionNow = (Vector2)Value;
                    break;
                case BulletChange.子弹寿命:
                    Target.MaxLiveFrame = (int)Value;
                    break;
                case BulletChange.子弹速度方向:
                    Target.Rotation = (float)Value;
                    break;
                case BulletChange.子弹速度方向自增:
                    Target.AcceleratedRotation = (float)Value;
                    break;
                case BulletChange.子弹颜色:
                    Target.BulletColor = (Color)Value;
                    break;
                case BulletChange.拖影:
                    Target.showTrails = (bool)Value;
                    break;
                case BulletChange.播放创建动画:
                    Target.CreateAnimationPlayed = (bool)Value;
                    break;
                case BulletChange.播放销毁动画:
                    Target.NoDestroyAnimation = !(bool)Value;
                    break;
                case BulletChange.检测屏幕位置:
                    Target.DestroyWhenDontRender = (bool)Value;
                    break;
                case BulletChange.降低子弹销毁限制:
                    Target.noDestroy = (bool)Value;
                    break;
            }
        }
        else {
            fInfoSelected.SetValue(Target, Value);
        }
    }
}
