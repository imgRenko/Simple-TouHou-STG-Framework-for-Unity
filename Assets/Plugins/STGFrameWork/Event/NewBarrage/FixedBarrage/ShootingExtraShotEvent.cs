using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Sirenix.OdinInspector;
[AddComponentMenu("东方STG框架/弹幕设计/常见事件/发射器事件/发射器参数控制器(曲线+额外发射)")]
public class ShootingExtraShotEvent : ShootingEvent {

    [InfoBox("该脚本的时间取值为某一子弹的额外发射批次，如果某一子弹是额外发射操作中，第一组发射的子弹，那么这个批次为1，以此类推。注意，第一批子弹需要使用曲线变化的数值在曲线对应的时间是0秒，同理，第二批子弹是1秒。为保证程序正常运行，曲线关键帧条件会忽略掉最后一个曲线关键帧的所有条件，请在曲线编辑器里新增一个不使值变化的帧来防止这个条件被忽略")]
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
    [LabelText("颜色 (红色)")]
    [ShowIf("isVector4")]
    public AnimationCurve Red;
    [FoldoutGroup("曲线控制", expanded: false)]
    [LabelText("颜色 (绿色)")]
    [ShowIf("isVector4")]
    public AnimationCurve Green;
    [FoldoutGroup("曲线控制", expanded: false)]
    [LabelText("颜色 (蓝色)")]
    [ShowIf("isVector4")]
    public AnimationCurve Blue;
    [FoldoutGroup("曲线控制", expanded: false)]
    [LabelText("颜色 (透明度)")]
    [ShowIf("isVector4")]
    public AnimationCurve Alpha;
    [FoldoutGroup("曲线控制", expanded: false)]
    [LabelText("对象时间轴")]
    [ShowIf("isObject")]
    public List<ObjectTimeLine> objectTimeLine = new List<ObjectTimeLine>();

    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("是否连接到一个发射器")]
    public bool LinkShooting = false;
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("被连接到的发射器")]
    [ShowIf("LinkShooting")]
    public Shooting LinkedShooting;
    [ShowIf("LinkShooting")]
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("发射器参数增值")]
    public float AddFloat = 0;
    [ShowIf("AddFieldSingleVector")]
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("发射器参数增值（向量）")]
    public Vector4 AddVector = Vector4.zero;
    [HideInInspector]
    public bool isVector2 = false;
    [HideInInspector]
    public bool isVector4 = false;
    [HideInInspector]
    public bool SingleVector = false;
    [HideInInspector]
    public bool AddFieldSingleVector = false;
    [HideInInspector]
    public bool isObject = false;
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("要操作的成员变量序号")]
    [SerializeField, SetProperty("_index")]
    private int index;

    public int _index
    {
        get
        {
            return index;
        }
        set
        {
            index = value;
            fInfoSelected = fInfo[index];
            if (fInfoSelected == null) return;
            isVector2 = fInfoSelected.FieldType == typeof(Vector2);
            isVector4 = fInfoSelected.FieldType == typeof(Color);
            SingleVector = (fInfoSelected.FieldType != typeof(Vector2) && fInfoSelected.FieldType != typeof(Vector4));
            AddFieldSingleVector = LinkShooting && !SingleVector;
            isObject = !(fInfoSelected.FieldType == typeof(Vector2) || fInfoSelected.FieldType == typeof(Vector4) || fInfoSelected.FieldType == typeof(Color) || fInfoSelected.FieldType == typeof(float) || fInfoSelected.FieldType == typeof(int));
        }
    }
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("可操作的成员变量")]
    public List<string> Name = new List<string>();
    FieldInfo[] fInfo = typeof(Shooting).GetFields();
    FieldInfo fInfoSelected;
    [FoldoutGroup("曲线动态控制", expanded: false)]
    [LabelText("关键帧开关")]
    [SerializeField]
    public List<FrameSetting> frameSetting = new List<FrameSetting>();
    [FoldoutGroup("曲线动态控制", expanded: false)]
    [LabelText("自由随机曲线控制器")]
    [SerializeField]
    public List<RandomKeySetting> RandomKeySettings = new List<RandomKeySetting>();
    [FoldoutGroup("曲线动态控制", expanded: false)]
    [LabelText("目前曲线控制参数方式")]
    public FrameSetting.Infusion CaluateMethod = FrameSetting.Infusion.REPLACE;
    private FrameSetting NowframeSetting = new FrameSetting(0);
    private Keyframe[] OriKeys;
    private float minRandom, maxRandom;

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

    public void GetInfusionMethod(float time, Shooting Target)
    {
       
        if (frameSetting.Count == 0 )
            return;
        int MinIndex = 0;
        int Key = 0;
        for (int i = 0; i != frameSetting.Count; ++i)
        {
            frameSetting[i].Use(false);
            frameSetting[i].resetCount++;
        }
        // Debug.Log("当前时间点" + time.ToString());
        for (int i = 0; i != frameSetting.Count; ++i)
        {

            if (!isObject)
            {
                if (Curve.keys[frameSetting[i].KeyIndex].time > time)
                {
                    MinIndex = i;
                    frameSetting[MinIndex].Use(true);
                    Key = frameSetting[MinIndex].KeyIndex;
                    break;
                }
            }
            else
            {
                if (objectTimeLine[frameSetting[i].KeyIndex].Time > time)
                {
                    MinIndex = i;
                    frameSetting[MinIndex].Use(true);
                    Key = frameSetting[MinIndex].KeyIndex;
                    Debug.Log("DEDE");
                    break;
                }
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
                CaluateMethod = frameSetting[MinIndex].infusion;
                NowframeSetting = frameSetting[MinIndex];
                if (CaluateMethod == FrameSetting.Infusion.RANDOM)
                {
                    minRandom = muitiConditon[i].MinRandomRange;
                    maxRandom = muitiConditon[i].MaxRandomRange;
                }

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
                CaluateMethod = frameSetting[MinIndex].infusion;
                NowframeSetting = frameSetting[MinIndex];
                if (CaluateMethod == FrameSetting.Infusion.RANDOM)
                {
                    minRandom = frameSetting[MinIndex].MinRandomRange;
                    maxRandom = frameSetting[MinIndex].MaxRandomRange;
                }
            }
            else
                CaluateMethod = frameSetting[MinIndex].Default;

        }
    }
    public float CalculateValue(float a, float b, DynamicParmCurve.CalculateMethod method)
    {
        switch (method)
        {
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
    public bool CanEnterFrame(float time, FrameSetting r, Shooting Target)
    {
        if (r.CompareOtherShooting)
            Target = r.compareShooting;
        float TargetValue = r.Compare;
        if (r.resetCount > r.Recircle || r.Recircle != -1)
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
            int t = (int)fInfo[r.ParmIndex].GetValue(Target);
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
            float t = (float)fInfo[r.ParmIndex].GetValue(Target);
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
        if (fInfo[r.ParmIndex].FieldType == typeof(bool))
        {
            bool t = System.Convert.ToBoolean(fInfo[r.ParmIndex].GetValue(Target));
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
    public object GetValue(FrameSetting setting, FrameSetting.Original original, Shooting Target, float t,float Const)
    {
        switch (original)
        {
            case FrameSetting.Original.ORINGIAL:
                return (float)fInfoSelected.GetValue(Target);
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
                return Curve.Evaluate(t);
            case FrameSetting.Original.CURVE_X:
                return CurveX.Evaluate(t);
            case FrameSetting.Original.CURVE_Y:
                return CurveY.Evaluate(t);
            case FrameSetting.Original.CURVE_R:
                return Red.Evaluate(t);
            case FrameSetting.Original.CURVE_G:
                return Green.Evaluate(t);
            case FrameSetting.Original.CURVE_B:
                return Blue.Evaluate(t);
            case FrameSetting.Original.CURVE_A:
                return Alpha.Evaluate(t);
            case FrameSetting.Original.CURVE_VECTOR2:
                return new Vector2(CurveX.Evaluate(t), CurveY.Evaluate(t));
            case FrameSetting.Original.CURVE_VECTOR4:
                return new Color(Red.Evaluate(t), Green.Evaluate(t), Blue.Evaluate(t), Alpha.Evaluate(t));
            case FrameSetting.Original.ANGLE_TO_PLAYER:
                return Bullet.GetAimToObjectRotation(Target.gameObject, Global.PlayerObject);
        }
        return 0;
    }
     public override void AfterShootingFinishedShooting(Shooting Target,Bullet bullet)
    {
        //RandomKey();
        fInfoSelected = fInfo[index];
        float t = (float)Target.ReturnTotalShotBatch ();
        //Debug.Log (t);


        GetInfusionMethod(t, Target);
        if (CaluateMethod == FrameSetting.Infusion.REPLACE)
        {
            object c = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[0].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[0].ResultConst);
            if (fInfoSelected.FieldType == typeof(int))
                fInfoSelected.SetValue(Target, System.Convert.ToInt32(c));
            if (fInfoSelected.FieldType == typeof(float))
                fInfoSelected.SetValue(Target, c);
            if (fInfoSelected.FieldType == typeof(bool))
                fInfoSelected.SetValue(Target, System.Convert.ToBoolean(c));
            if (fInfoSelected.FieldType == typeof(Vector2))
            {
                if (frameSetting.Count == 0)
                {
                    NowframeSetting.ResultDefaultValue[0].BaseValue = FrameSetting.Original.CURVE_X;
                    NowframeSetting.ResultDefaultValue[1].BaseValue = FrameSetting.Original.CURVE_Y;
                }
                c = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[0].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[0].ResultConst);
                object g = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[1].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[1].ResultConst);
                Vector2 v = new Vector2((float)c, (float)g);
                fInfoSelected.SetValue(Target, v);
            }
            if (fInfoSelected.FieldType == typeof(Color))
            {
                if (frameSetting.Count == 0)
                {
                    NowframeSetting.ResultDefaultValue[0].BaseValue = FrameSetting.Original.CURVE_R;
                    NowframeSetting.ResultDefaultValue[1].BaseValue = FrameSetting.Original.CURVE_G;
                    NowframeSetting.ResultDefaultValue[2].BaseValue = FrameSetting.Original.CURVE_B;
                    NowframeSetting.ResultDefaultValue[3].BaseValue = FrameSetting.Original.CURVE_A;
                }
                c = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[0].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[0].ResultConst);
                object g = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[1].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[1].ResultConst);
                object b = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[2].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[2].ResultConst);
                object a = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[3].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[3].ResultConst);
                Color v = (Color)fInfoSelected.GetValue(Target);

                fInfoSelected.SetValue(Target, v);
            }
            if (isObject)
            {
                int g = 0;
                for (int i = 0; i != objectTimeLine.Count; ++i)
                {
                    if (objectTimeLine[i].Time >= t)
                    {
                        g = i;
                        break;
                    }
                }
                Debug.Log("Enaslk");
                fInfoSelected.SetValue(Target, objectTimeLine[g].Objs);


            }
        }
        if (CaluateMethod == FrameSetting.Infusion.ADD)
        {
            object c = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[0].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[0].ResultConst);
            object r = GetValue(NowframeSetting, NowframeSetting.DefaultValue, Target, t, NowframeSetting.Const);
            if (fInfoSelected.FieldType == typeof(int))
                fInfoSelected.SetValue(Target, (int)r + System.Convert.ToInt32(c));
            if (fInfoSelected.FieldType == typeof(float))
                fInfoSelected.SetValue(Target, (float)r + (float)c);
            if (fInfoSelected.FieldType == typeof(bool))
                fInfoSelected.SetValue(Target, System.Convert.ToBoolean(c));

            if (fInfoSelected.FieldType == typeof(Vector2))
            {
                if (frameSetting.Count == 0)
                {
                    NowframeSetting.ResultDefaultValue[0].BaseValue = FrameSetting.Original.CURVE_X;
                    NowframeSetting.ResultDefaultValue[1].BaseValue = FrameSetting.Original.CURVE_Y;
                }
                c = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[0].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[0].ResultConst);
                object g = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[1].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[1].ResultConst);
                Vector2 v = (Vector2)c;
                Vector2 p = (Vector2)r;
                p.x += v.x;

                p.y += v.y;
                fInfoSelected.SetValue(Target, p);
            }
            if (fInfoSelected.FieldType == typeof(Color))
            {
                if (frameSetting.Count == 0)
                {
                    NowframeSetting.ResultDefaultValue[0].BaseValue = FrameSetting.Original.CURVE_R;
                    NowframeSetting.ResultDefaultValue[1].BaseValue = FrameSetting.Original.CURVE_G;
                    NowframeSetting.ResultDefaultValue[2].BaseValue = FrameSetting.Original.CURVE_B;
                    NowframeSetting.ResultDefaultValue[3].BaseValue = FrameSetting.Original.CURVE_A;
                }
                c = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[0].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[0].ResultConst);
                object g = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[1].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[1].ResultConst);
                object b = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[2].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[2].ResultConst);
                object a = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[3].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[3].ResultConst);
                Color v = (Color)fInfoSelected.GetValue(Target);
                Color p = (Color)r;
                p.r += v.r;

                p.b += v.g;

                p.g += v.b;

                p.a += v.a;
                fInfoSelected.SetValue(Target, p);
            }
        }
        if (CaluateMethod == FrameSetting.Infusion.MINUS)
        {
            object c = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[0].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[0].ResultConst);
            object r = GetValue(NowframeSetting, NowframeSetting.DefaultValue, Target, t, NowframeSetting.Const);
            if (fInfoSelected.FieldType == typeof(int))
                fInfoSelected.SetValue(Target, (int)r - System.Convert.ToInt32(Curve.Evaluate(t)));
            if (fInfoSelected.FieldType == typeof(float))
                fInfoSelected.SetValue(Target, (float)r - (float)c);
            if (fInfoSelected.FieldType == typeof(bool))
                fInfoSelected.SetValue(Target, System.Convert.ToBoolean(c));
            if (fInfoSelected.FieldType == typeof(Vector2))
            {
                if (frameSetting.Count == 0)
                {
                    NowframeSetting.ResultDefaultValue[0].BaseValue = FrameSetting.Original.CURVE_X;
                    NowframeSetting.ResultDefaultValue[1].BaseValue = FrameSetting.Original.CURVE_Y;
                }
                c = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[0].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[0].ResultConst);
                object g = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[1].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[1].ResultConst);
                Vector2 v = (Vector2)c;
                Vector2 p = (Vector2)r;
                p.x -= v.x;

                p.y -= v.y;
                fInfoSelected.SetValue(Target, p);
            }
            if (fInfoSelected.FieldType == typeof(Color))
            {
                if (frameSetting.Count == 0)
                {
                    NowframeSetting.ResultDefaultValue[0].BaseValue = FrameSetting.Original.CURVE_R;
                    NowframeSetting.ResultDefaultValue[1].BaseValue = FrameSetting.Original.CURVE_G;
                    NowframeSetting.ResultDefaultValue[2].BaseValue = FrameSetting.Original.CURVE_B;
                    NowframeSetting.ResultDefaultValue[3].BaseValue = FrameSetting.Original.CURVE_A;
                }
                c = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[0].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[0].ResultConst);
                object g = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[1].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[1].ResultConst);
                object b = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[2].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[2].ResultConst);
                object a = GetValue(NowframeSetting, NowframeSetting.ResultDefaultValue[3].BaseValue, Target, t, NowframeSetting.ResultDefaultValue[3].ResultConst);
                Color v = (Color)fInfoSelected.GetValue(Target);
                Color p = (Color)r;
                p.r -= v.r;

                p.b -= v.g;

                p.g -= v.b;

                p.a -= v.a;
                fInfoSelected.SetValue(Target, p);
            }
        }
        if (CaluateMethod == FrameSetting.Infusion.RANDOM)
        {


            if (fInfoSelected.FieldType == typeof(int))
            {
                if (NowframeSetting.Enable == true)
                {
                    int keyIndex = NowframeSetting.KeyIndex;
                    AnimationCurve curve = Curve;
                    Keyframe keyframe = curve[keyIndex];
                    Curve.RemoveKey(keyIndex);
                    Curve.AddKey(keyframe.time, OriKeys[keyIndex].value + (int)Random.Range(NowframeSetting.MinRandomRange,
                       NowframeSetting.MaxRandomRange));
                }
                fInfoSelected.SetValue(Target, Curve.Evaluate(t));

            }
            if (fInfoSelected.FieldType == typeof(float))
            {
                if (NowframeSetting.Enable == true)
                {
                    int keyIndex = NowframeSetting.KeyIndex;
                    AnimationCurve curve = Curve;
                    Keyframe keyframe = curve[keyIndex];
                    Curve.RemoveKey(keyIndex);
                    Curve.AddKey(keyframe.time, OriKeys[keyIndex].value + Random.Range(NowframeSetting.MinRandomRange,
                       NowframeSetting.MaxRandomRange));
                    Debug.Log(keyframe.value + Random.Range(NowframeSetting.MinRandomRange,
                       NowframeSetting.MaxRandomRange));
                }
                fInfoSelected.SetValue(Target, Curve.Evaluate(t));
            }
            if (fInfoSelected.FieldType == typeof(bool))
                fInfoSelected.SetValue(Target, System.Convert.ToBoolean(Random.Range(minRandom, maxRandom)));
            if (fInfoSelected.FieldType == typeof(Vector2))
            {
                Vector2 v = (Vector2)fInfoSelected.GetValue(Target);

                v.x = Random.Range(minRandom, maxRandom);

                v.y = Random.Range(minRandom, maxRandom);
                fInfoSelected.SetValue(Target, v);
            }
            if (fInfoSelected.FieldType == typeof(Color))
            {
                Color v = (Color)fInfoSelected.GetValue(Target);

                v.r = Random.Range(minRandom, maxRandom);

                v.b = Random.Range(minRandom, maxRandom);

                v.g = Random.Range(minRandom, maxRandom);

                v.a = Random.Range(minRandom, maxRandom);
                fInfoSelected.SetValue(Target, v);
            }
            if (isObject)
            {
                fInfoSelected.SetValue(Target, objectTimeLine[Mathf.Clamp(Random.Range((int)minRandom, (int)maxRandom), 0, objectTimeLine.Count - 1)].Objs);


            }
        }
        NowframeSetting.Enable = false;
        if (LinkedShooting == null) return;
        if (fInfoSelected.FieldType == typeof(float))
            fInfoSelected.SetValue(LinkedShooting, (float)fInfoSelected.GetValue(Target) + AddFloat);
        if (fInfoSelected.FieldType == typeof(int))
            fInfoSelected.SetValue(LinkedShooting, (int)fInfoSelected.GetValue(Target) + AddFloat);
        if (fInfoSelected.FieldType == typeof(Vector2))
            fInfoSelected.SetValue(LinkedShooting, (Vector2)fInfoSelected.GetValue(Target) + (Vector2)AddVector);
        if (fInfoSelected.FieldType == typeof(Vector4))
            fInfoSelected.SetValue(LinkedShooting, (Vector4)fInfoSelected.GetValue(Target) + AddVector);







    }
    public override void EventStart (Shooting[] Target)
    {

        OriKeys = Curve.keys;
        fInfoSelected = fInfo [index];
        Reorder();
    }
    public void RandomKey()
    {
        foreach (var a in RandomKeySettings)
        {
            if (a.Float)
            {

                Curve.RemoveKey(a.KeyIndex);
                Curve.AddKey(a.BaseTime + Random.Range(-a.RandomTimerRange, a.RandomTimerRange), a.BaseValue + Random.Range(-a.RandomRange, a.RandomRange));
            }
            if (a.X)
            {
                CurveX.RemoveKey(a.KeyIndex);
                CurveX.AddKey(a.BaseTime + Random.Range(-a.RandomTimerRange, a.RandomTimerRange), a.BaseValue + Random.Range(-a.RandomRange, a.RandomRange));
            }
            if (a.Y)
            {
                CurveY.RemoveKey(a.KeyIndex);
                CurveY.AddKey(a.BaseTime + Random.Range(-a.RandomTimerRange, a.RandomTimerRange), a.BaseValue + Random.Range(-a.RandomRange, a.RandomRange));
            }
            if (a.R)
            {
                Red.RemoveKey(a.KeyIndex);
                Red.AddKey(a.BaseTime + Random.Range(-a.RandomTimerRange, a.RandomTimerRange), a.BaseValue + Random.Range(-a.RandomRange, a.RandomRange));
            }
            if (a.G)
            {
                Green.RemoveKey(a.KeyIndex);
                Green.AddKey(a.BaseTime + Random.Range(-a.RandomTimerRange, a.RandomTimerRange), a.BaseValue + Random.Range(-a.RandomRange, a.RandomRange));
            }
            if (a.B)
            {
                Blue.RemoveKey(a.KeyIndex);
                Blue.AddKey(a.BaseTime + Random.Range(-a.RandomTimerRange, a.RandomTimerRange), a.BaseValue + Random.Range(-a.RandomRange, a.RandomRange));
            }
            if (a.A)
            {
                Alpha.RemoveKey(a.KeyIndex);
                Alpha.AddKey(a.BaseTime + Random.Range(-a.RandomTimerRange, a.RandomTimerRange), a.BaseValue + Random.Range(-a.RandomRange, a.RandomRange));
            }
        }
    }
    public override void OnShootingDestroy(Shooting Target)
    {
        foreach (var a in frameSetting)
        {
            a.Enable = true;
        }
    }
    protected override void OnBeforeSerialize ()
    {
        if (Name.Count == fInfo.Length || Application.isPlaying) {
            return;
        }
        Debug.Log("Updated");
        Name.Clear ();
        for (int i = 0; i != fInfo.Length; ++i) {
            if (fInfo [i].FieldType != typeof(float) || fInfo [i].FieldType != typeof(int) || fInfo [i].FieldType != typeof(bool) || fInfo [i].FieldType != typeof(Vector2)) {
                object[] Translate = fInfo [i].GetCustomAttributes (typeof(LabelTextAttribute), true);

                for (int g = 0; g != Translate.Length; ++g) {
                    LabelTextAttribute _Lable = (LabelTextAttribute)Translate [g];
                    Name.Add (_Lable.Text);

                }

                if (Translate.Length == 0)
                    Name.Add (fInfo [i].Name);
            }
        }
        fInfoSelected = fInfo[index];

    }

}
