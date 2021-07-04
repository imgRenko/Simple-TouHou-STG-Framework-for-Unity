using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Sirenix.OdinInspector;
using System.Text.RegularExpressions;

[System.Serializable]
public class ObjectTimeLine
{
    [PreviewField(80, ObjectFieldAlignment.Left)]
    [LabelText("对象")]
    public Object Objs;
    [LabelText("时间")]
    public float Time;
}
[System.Serializable]
public class EventList{
    public enum EventTasks
    {
        RANDOM = 0,
        EQUAL = 1,
        MITUS = 2,
        ADD = 3
    }

    public EventTasks infusion = EventTasks.EQUAL;
    public float FrameTarget = 0;
    public int ParmIndex = 0;
    [ShowIf("infusion", EventTasks.RANDOM)]
    public float RandomRange = 0;
    [ShowIf("infusion", EventTasks.MITUS)]
    public float MitusValue = 0;
    [ShowIf("infusion", EventTasks.ADD)]
    public float AddValue = 0;
    [ShowIf("infusion", EventTasks.EQUAL)]
    public float Value = 0;
    public bool Enabled = true;
}
[AddComponentMenu("东方STG框架/弹幕设计/常见事件/发射器事件/发射器参数控制器(曲线+条件)")]
public class BasedEvent_ShootingLocomotion : ShootingEvent
{
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("以帧数作为曲线X轴")]
    [HideIf("eachWay")]
    public bool caluateToFrame = false;
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("以每个Way数作为X轴")]
    [HideIf("caluateToFrame")]
    public bool eachWay = false;

    [InfoBox("该脚本的时间取值范围为[0,1)，由于取不到1这个值，请不要指定此脚本在一个发射器周期内一直替换某参数的值，无法维持周期性，为保证程序正常运行，曲线关键帧条件会忽略掉最后一个曲线关键帧的所有条件，请在曲线编辑器里新增一个不使值变化的帧来防止这个条件被忽略")]
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
    [HideInInspector]
    public string groupTag;

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
    private int singleWayIndex;
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
            isObject = !(fInfoSelected.FieldType == typeof(Vector2) || fInfoSelected.FieldType == typeof(Vector4) || fInfoSelected.FieldType == typeof(Color) || fInfoSelected.FieldType == typeof(float) || fInfoSelected.FieldType == typeof(int) || fInfoSelected.FieldType == typeof(bool));
            if (isObject)
                SingleVector = false;// !SingleVector
        }
    }
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("可操作的成员变量")]
    public List<string> Name = new List<string>();
    FieldInfo[] fInfo = typeof(Shooting).GetFields();
    FieldInfo fInfoSelected;

    [FoldoutGroup("曲线动态控制", expanded: false)]
    [LabelText("自由随机曲线控制器")]
    [SerializeField]
    public List<RandomKeySetting> RandomKeySettings = new List<RandomKeySetting>();
    [FoldoutGroup("曲线动态控制", expanded: false)]
    [LabelText("针对每一弹道都设置一次")]
    [SerializeField]
    public bool EachTimeWayGo = false;
    [FoldoutGroup("曲线动态控制", expanded: false)]
    [LabelText("动态曲线控制器")]
    [SerializeField]
    public List<DynamicParmCurve> CurveChange = new List<DynamicParmCurve>();
    [FoldoutGroup("曲线动态控制", expanded: false)]
    [LabelText("组标识集合")]
    [SerializeField]
    public List<GroupString> groupStrings = new List<GroupString>();
    [FoldoutGroup("曲线动态控制", expanded: false)]

    [LabelText("关键帧开关")]
    [SerializeField]
    public List<FrameSetting> frameSetting = new List<FrameSetting>();
    [FoldoutGroup("曲线动态控制", expanded: false)]
    [LabelText("事件启用设置")]
    [HideInInspector]
    public List<EventList> EventSetting = new List<EventList>();
    [HideInInspector]
    [SerializeField]
    public List<GroupString> oriGroupString = new List<GroupString>();
    [FoldoutGroup("曲线动态控制", expanded: false)]
    [LabelText("目前曲线控制参数方式")]
    public FrameSetting.Infusion CaluateMethod = FrameSetting.Infusion.REPLACE;
    [FoldoutGroup("曲线动态控制(CrazyStorm)", expanded: false)]
    [LabelText("曲线循环间隔")]
    public float CalculateCircle = 0;
    [FoldoutGroup("曲线动态控制(CrazyStorm)", expanded: false)]
    [LabelText("曲线循环间隔自增数值")]
    public float selfIncreasing = 0;
    [FoldoutGroup("曲线动态控制(CrazyStorm)", expanded: false)]
    [LabelText("自动截断曲线")]
    public bool breakCurve = true;
    private FrameSetting NowframeSetting = new FrameSetting(0);
    private float minRandom, maxRandom;
    private int looper = 0; private float timeCount = 0;
    private Keyframe[] OriKeys;// = Curve.keys;
    float t = 0;
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
    public void StartEvent(EventList.EventTasks type, EventList target, Shooting TargetShooting)
    {
 
        FieldInfo fInfoEventSelected = fInfo[target.ParmIndex]; ;
        switch (type)
        {
            case EventList.EventTasks.ADD:
                if (fInfoEventSelected.FieldType == typeof(int))
                    fInfoEventSelected.SetValue(TargetShooting, (int)fInfoEventSelected.GetValue(TargetShooting) + System.Convert.ToInt32(target.AddValue));
                if (fInfoEventSelected.FieldType == typeof(float))
                    fInfoEventSelected.SetValue(TargetShooting, (float)fInfoEventSelected.GetValue(TargetShooting) + target.AddValue);
                break;
            case EventList.EventTasks.MITUS:
                if (fInfoEventSelected.FieldType == typeof(int))
                    fInfoEventSelected.SetValue(TargetShooting, (int)fInfoEventSelected.GetValue(TargetShooting) - System.Convert.ToInt32(target.MitusValue));
                if (fInfoEventSelected.FieldType == typeof(float))
                    fInfoEventSelected.SetValue(TargetShooting, (float)fInfoEventSelected.GetValue(TargetShooting) - target.MitusValue);
                break;
            case EventList.EventTasks.RANDOM:
                if (fInfoEventSelected.FieldType == typeof(int))
                    fInfoEventSelected.SetValue(TargetShooting, System.Convert.ToInt32(Random.Range(-target.RandomRange, target.RandomRange)));
                if (fInfoEventSelected.FieldType == typeof(float))
                    fInfoEventSelected.SetValue(TargetShooting, Random.Range(-target.RandomRange, target.RandomRange));
                break;
            case EventList.EventTasks.EQUAL:
                if (fInfoEventSelected.FieldType == typeof(int))
                    fInfoEventSelected.SetValue(TargetShooting, (int)target.Value);
                if (fInfoEventSelected.FieldType == typeof(float))
                    fInfoEventSelected.SetValue(TargetShooting, target.Value);
                break;
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
    public void GetInfusionMethod(float time, Shooting Target)
    {

        if (frameSetting.Count == 0)
            return;
        int MinIndex = 0;
        int Key = 0;
        for (int i = 0; i != frameSetting.Count; ++i)
        {
            frameSetting[i].Use(false);
           
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

        //  Debug.Log("最终被修改成了" + CaluateMethod);
    }
    public object GetValue(FrameSetting setting, FrameSetting.Original original, Shooting Target, float t, float Const)
    {
        switch (original)
        {
            case FrameSetting.Original.ORINGIAL:
                return fInfoSelected.GetValue(Target);
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
                return Math2D.GetAimToObjectRotation(Target.gameObject, Global.PlayerObject);
        }
        return 0;
    }
    public bool CanEnterFrame(float time, FrameSetting r, Shooting Target)
    {
        if (r.CompareOtherShooting)
            Target = r.compareShooting;
        float TargetValue = r.Compare;
        if (r.resetCount > r.Recircle && r.Recircle != -1)
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
    public void EventCheck(Shooting Target, int Index = -1)
    {
        fInfoSelected = fInfo[index];
        
        if (Index == -1)
            t = (!caluateToFrame ? ((Target.TotalFrame - (Target.CaluateDelayToPercent ? Target.Delay : 0)) / (Target.MaxLiveFrame - (Target.CaluateDelayToPercent ? Target.Delay : 0))) : Target.TotalFrame);
        else
            t = Index;
        foreach (EventList e in EventSetting)
        {
            if (Target.TotalFrame > e.FrameTarget && e.Enabled)
            {
                e.Enabled = false;
                StartEvent(e.infusion, e, Target);
            }
        }
        //bool notUpdate = false;
        if (Target.TotalFrame - Target.Delay < 0)
            return;
        if (t < Curve.keys[0].time)
            return;
        foreach (DynamicParmCurve a in CurveChange)
        {
            if (!a.Use)
                continue;
            AnimationCurve CurveType = new AnimationCurve();
            switch (a.ControledCurve)
            {
                case DynamicParmCurve.CurvesType.Float:
                    CurveType = Curve;
                    break;
                case DynamicParmCurve.CurvesType.VectorX:
                    CurveType = CurveX;
                    break;
                case DynamicParmCurve.CurvesType.VectorY:
                    CurveType = CurveY;
                    break;
                case DynamicParmCurve.CurvesType.Red:
                    CurveType = Red;
                    break;
                case DynamicParmCurve.CurvesType.Green:
                    CurveType = Green;
                    break;
                case DynamicParmCurve.CurvesType.Blue:
                    CurveType = Blue;
                    break;
                case DynamicParmCurve.CurvesType.Alpha:
                    CurveType = Alpha;
                    break;
            }
            if (a.UseCurve)
            {
                foreach (var Tag in groupStrings)
                {
                    // Debug.Log(Target.TotalFrame);
                    if (Tag.resetCircle < Tag.resetCount && Tag.resetCircle != -1)
                        continue;
                    if (Tag.Time + looper * selfIncreasing < t && Tag.Use)
                    {
                        groupTag = Tag.Tag;
                        Tag.resetCount++;
                        Tag.Use = false;
                        break;
                    }
                }


                if (a.GroupTag != groupTag)
                {
                   
                    continue;
                }
                switch (a.YasixType)
                {
                    case DynamicParmCurve.CurvesYasix.RunTime:

                        float ra = CurveType.keys[a.TargetKeyIndex].time;
                        Keyframe aredcr = CurveType.keys[a.TargetKeyIndex];
                        aredcr.value = CalculateValue(a.DymicParmKeysCurve.Evaluate(Time.time) + a.AddValue + a.RandomResult, a.methodInpand, a.method);
                        CurveType.RemoveKey(a.TargetKeyIndex);
                        CurveType.AddKey(aredcr);
                        break;
                    case DynamicParmCurve.CurvesYasix.BulletIndex:
                        float rc = CurveType.keys[a.TargetKeyIndex].time;
                        Keyframe aredc = CurveType.keys[a.TargetKeyIndex];
                        aredc.value = CalculateValue(a.DymicParmKeysCurve.Evaluate(Index) + a.AddValue + a.RandomResult, a.methodInpand, a.method);
                        CurveType.RemoveKey(a.TargetKeyIndex);
                        CurveType.AddKey(aredc);
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
                            (Target.TotalFrame / Target.MaxLiveFrame) + a.AddValue + a.RandomResult, a.methodInpand, a.method);
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
                        CurveType.AddKey(rcb, CalculateValue(Global.PlayerObject.transform.position.y + a.AddValue + a.RandomResult, a.methodInpand, a.method));
                        break;
                    case DynamicParmCurve.ConstValue.ANGLE_TO_PLAYER:
                        float raed = CurveType.keys[a.TargetKeyIndex].time;
                        CurveType.RemoveKey(a.TargetKeyIndex);
                        CurveType.AddKey(raed, CalculateValue(Math2D.GetAimToObjectRotation(Target.gameObject, Global.PlayerObject) + a.AddValue + a.RandomResult, a.methodInpand, a.method));
                        Debug.Log(Math2D.GetAimToObjectRotation(Target.gameObject, Global.PlayerObject) + a.AddValue + a.RandomResult);
                        break;


                }
            }
            switch (a.ControledCurve)
            {
                case DynamicParmCurve.CurvesType.Float:
                    Curve = CurveType;
                    break;
                case DynamicParmCurve.CurvesType.VectorX:
                    CurveX = CurveType;
                    break;
                case DynamicParmCurve.CurvesType.VectorY:
                    CurveY = CurveType;
                    break;
                case DynamicParmCurve.CurvesType.Red:
                    Red = CurveType;
                    break;
                case DynamicParmCurve.CurvesType.Green:
                    Green = CurveType;
                    break;
                case DynamicParmCurve.CurvesType.Blue:
                    Blue = CurveType;
                    break;
                case DynamicParmCurve.CurvesType.Alpha:
                    Alpha = CurveType;
                    break;
            }
            a.timeCount++;
            if (a.timeCount > a.OnceTime && a.OnceTime != -1)
                a.Use = false;
        }
        
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
    public override void OnShootingDestroy(Shooting Target)
    {
        timeCount = 0;
        foreach (var a in frameSetting)
        {
            a.Enable = true;
            a.circleEnable = false;
        }
        foreach (var a in CurveChange)
        {
            a.Use = true;
        }
        foreach (var a in groupStrings)
        {
            a.Use = true;
        }
        for (int i = 0; i != oriGroupString.Count; ++i) {
            groupStrings[i].Time = oriGroupString[i].Time;
        }
        groupTag = "";
       // groupStrings = oriGroupString;
        // LoopCurveOffset();
    }
    public override void OnShootingUsing(Shooting Target)
    {


        if (eachWay == false)
            EventCheck(Target, -1);

        if (CalculateCircle == 0 || selfIncreasing == 0)
            return;
        timeCount = Target.TotalFrame - looper * CalculateCircle;
       // Debug.Log(timeCount);
        if (timeCount >= CalculateCircle)
        {
            timeCount = 0;
            looper++;
            LoopCurveOffset();

        }
    }
    public void LoopCurveOffset()
    {
        
        foreach (var a in CurveChange)
        {
            a.RandomResult = Random.Range(-a.AddRandomValue, a.AddRandomValue);
            a.Use = true;
        }
        
        Keyframe[] Keys = Curve.keys;
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
            Curve.RemoveKey(0);

          
        }
        foreach (var a in groupStrings)
        {
            a.Use = true;
        }
        foreach (var b in addKeys)
        {

            Curve.AddKey(b);
        }
        foreach (DynamicParmCurve a in CurveChange)
        {

            a.BreakSign = Curve.keys[a.TargetKeyIndex].time < t;
        }

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
    public override void BeforeShooting(Shooting Target)
    {


        if (eachWay == false)
            return;
        EventCheck(Target, singleWayIndex);
        Debug.Log(singleWayIndex);
        singleWayIndex++;
    }
     public override void AfterShootingFinishedShooting(Shooting Target,Bullet bullet)
    {
        if (EachTimeWayGo)
            RandomKey();
        singleWayIndex = 0;
     //   Debug.LogError(singleWayIndex);
    }

    public override void StartNewLoop(Shooting Target)
    {
        timeCount = 0;
        foreach (var a in groupStrings)
        {
            a.Use = true;
        }
        for (int i = 0; i != oriGroupString.Count; ++i)
        {
            groupStrings[i].Time = oriGroupString[i].Time;
            Debug.Log(oriGroupString[i].Time);
        }
        looper = 0;
        Curve.keys = OriKeys;
       
        // LoopCurve();
        foreach (EventList e in EventSetting)
        {
            e.Enabled = true;
        }
    }
    public override void EventStart(Shooting[] Target)
    {
        timeCount = 0;
        oriGroupString = groupStrings;
        Debug.Log("Reset");
        RandomKey();
        OriKeys = Curve.keys;
        //LoopCurveOffset();
        objectTimeLine.Sort(delegate (ObjectTimeLine a, ObjectTimeLine b)
        {
            return a.Time.CompareTo(b.Time);
        }
          );
        fInfoSelected = fInfo[index];
        Reorder();
    }
    public int ReturnCircle(float t)
    {
        float T = Mathf.Clamp(Curve.keys[Curve.keys.Length - 1].time - Curve.keys[0].time, 0, 0xffffffff);
        float Cross = t / T;
        return (int)Cross;
    }
    protected override void OnBeforeSerialize()
    {
        if (Name.Count == fInfo.Length || Application.isPlaying)
        {
            return;
        }
        Debug.Log("Updated");
        Name.Clear();

        for (int i = 0; i != fInfo.Length; ++i)
        {
            if (fInfo[i].FieldType != typeof(float) || fInfo[i].FieldType != typeof(int) || fInfo[i].FieldType != typeof(bool) || fInfo[i].FieldType != typeof(Vector2))
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
        fInfoSelected = fInfo[index];

    }

}
