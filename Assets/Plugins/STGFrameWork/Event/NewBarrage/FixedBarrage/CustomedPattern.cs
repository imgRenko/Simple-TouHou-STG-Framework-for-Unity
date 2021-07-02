using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Sirenix.OdinInspector;
[AddComponentMenu("东方STG框架/弹幕设计/弹幕特殊化/发射器(发射器事件，允许自绘图案)")]
public class CustomedPattern : SerializedMonoBehaviour
{
    [InfoBox("该脚本的时间取值范围为[0,1)，为保证程序正常运行，曲线关键帧条件会忽略掉最后一个曲线关键帧的所有条件，请在曲线编辑器里新增一个不使值变化的帧来防止这个条件被忽略")]
    [FoldoutGroup("处理进程")]
    [ProgressBar(0, 100)]
    [LabelText("播放进度")]
    public float Progress = 0;
    [FoldoutGroup("处理进程")]
    [LabelText("发射间隔")]
    public float Timer = 30;
    [FoldoutGroup("处理进程")]
    [LabelText("使用寿命")]
    public float MaxFrame = 200;
    [FoldoutGroup("处理进程")]
    [LabelText("可复用")]
    public bool Reuse = true;
    [FoldoutGroup("整体式发射")]
    [LabelText("整体式发射")]
    public bool ShotWay = true;
    [FoldoutGroup("整体式发射")]
    [LabelText("Way数")]
    [ShowIf("ShotWay")]
    public int Way=1;
    [FoldoutGroup("整体式发射")]
    [LabelText("要控制的发射器")]
    public Shooting Basement;
    [FoldoutGroup("整体式发射")]
    [LabelText("发射角")]
    [ShowIf("ShotWay")]
    public float Angle;
    [FoldoutGroup("整体式发射")]
    [LabelText("发射角自增")]
    [ShowIf("ShotWay")]
    public float AccAngle;
    [FoldoutGroup("整体式发射")]
    [LabelText("发射延迟")]
    public float Delay = 0;
    [FoldoutGroup("整体式发射")]
    [LabelText("图案尺寸")]
    public Vector2 directlyScale = Vector2.one;
    [FoldoutGroup("整体式发射")]
    [LabelText("图案原点")]
    public Vector2 ShotPoint = Vector2.one * 5;
    [FoldoutGroup("整体式发射")]
    [LabelText("发射范围")]
    [ShowIf("ShotWay")]
    public float Range = 360;
    [FoldoutGroup("整体式发射")]
    [LabelText("发射混乱程度")]
    [ShowIf("ShotWay")]
    public float ShotMess = 0;
    [FoldoutGroup("整体式发射")]
    [LabelText("启用速度差")]
    [ShowIf("ShotWay")]
    public bool DynamicSpd = false;
    [FoldoutGroup("整体式发射")]
    [LabelText("速度差大小")]

    public float SpdDistance = 0;
   
    [HideIf("isVector2")]
    public AnimationCurve FloatCurve;
    [ShowIf("isVector2")]
    public AnimationCurve CurveX;
    [ShowIf("isVector2")]
    public AnimationCurve CurveY;
    [HideInInspector]
    public bool isVector2 = false;
    [FoldoutGroup("微型曲线事件")]
    [LabelText("要控制的成员变量序号")]
    [SerializeField, SetProperty("_index")]
    private int ParmIndex;

    public int _index
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
        }
    }
    [FoldoutGroup("微型曲线事件")]
    [LabelText("成员变量名称")]
    public List<string> Name = new List<string>();
    [HideInInspector]
    public float _timer = 0;
    private FieldInfo[] fInfo = typeof(CustomedPattern).GetFields();
    private FieldInfo fInfoSelected;
    private float _stopWatch = 0, _origin = 0;
    [TableMatrix(HorizontalTitle = "请鼠标点击绘画图案", DrawElementMethod = "DrawMatrix")]
    public bool[,] customPattern = new bool[11, 11];
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
            if (fInfo[i].FieldType != typeof(float) || fInfo[i].FieldType != typeof(bool) || fInfo[i].FieldType != typeof(bool))
                Name.Add(fInfo[i].Name);
        }
    }
    [SerializeField]
    public List<FrameSetting> frameSetting = new List<FrameSetting>();
    public FrameSetting.Infusion CaluateMethod = FrameSetting.Infusion.REPLACE;
    [ButtonGroup]
    public void AutoCreateFrameSetting()
    {
        for (int i = 0; i != FloatCurve.keys.Length; ++i)
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
    [ButtonGroup]
    public void Reset()
    {
        customPattern = new bool[11, 11];
    }
   
    public void RandomCurveValue()
    {



    }
    public void GetInfusionMethod(float time, CustomedPattern Target)
    {
        if (frameSetting.Count == 1)
            CaluateMethod = frameSetting[0].infusion;
        if (frameSetting.Count == 0 || frameSetting.Count == 1)
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

            if (FloatCurve.keys[frameSetting[i].KeyIndex].time > time)
            {
                MinIndex = i - 1;
                frameSetting[MinIndex].Use(true);
                Key = frameSetting[MinIndex].KeyIndex;
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

                CaluateMethod = frameSetting[MinIndex].infusion;
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

            }
            else
                CaluateMethod = frameSetting[MinIndex].Default;

        }

        //  Debug.Log("最终被修改成了" + CaluateMethod);
    }
    public bool CanEnterFrame(float time, FrameSetting r, CustomedPattern Target)
    {
        float TargetValue = r.Compare;
        if (fInfo[r.ParmIndex].FieldType == typeof(int))
        {
            int t = (int)fInfo[r.ParmIndex].GetValue(Target);
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
    public void OnUpdatingParm(CustomedPattern Target)
    {
        fInfoSelected = fInfo[ParmIndex];
        float t = _timer / MaxFrame;
        if (fInfoSelected.FieldType != typeof(Vector2))
            if (FloatCurve.postWrapMode == WrapMode.Default && (FloatCurve.keys.Length > 0 && FloatCurve.keys[0].time > t) || (FloatCurve.keys.Length > 0 && FloatCurve.keys[FloatCurve.keys.Length - 1].time < t)) return;

        if (Target._timer / Target.MaxFrame < Delay)
            return;

        GetInfusionMethod(t, Target);
        if (CaluateMethod == FrameSetting.Infusion.REPLACE)
        {
            if (fInfoSelected.FieldType == typeof(int))
                fInfoSelected.SetValue(Target, System.Convert.ToInt32(FloatCurve.Evaluate(t)));
            if (fInfoSelected.FieldType == typeof(float))
                fInfoSelected.SetValue(Target, FloatCurve.Evaluate(t));
            if (fInfoSelected.FieldType == typeof(bool))
                fInfoSelected.SetValue(Target, System.Convert.ToBoolean(FloatCurve.Evaluate(t)));
            if (fInfoSelected.FieldType == typeof(Vector2))
            {
                Vector2 v = (Vector2)fInfoSelected.GetValue(Target);
                if (!((CurveX.keys.Length > 0 && CurveX.keys[0].time > t) || (CurveX.keys.Length > 0 && CurveX.keys[CurveX.keys.Length - 1].time < t)))
                    v.x = CurveX.Evaluate(t);
                if (!((CurveY.keys.Length > 0 && CurveY.keys[0].time > t) || (CurveY.keys.Length > 0 && CurveY.keys[CurveY.keys.Length - 1].time < t)))
                    v.y = CurveY.Evaluate(t);
                fInfoSelected.SetValue(Target, v);
            }

        }
        if (CaluateMethod == FrameSetting.Infusion.ADD)
        {
            if (fInfoSelected.FieldType == typeof(int))
                fInfoSelected.SetValue(Target, (int)fInfoSelected.GetValue(Target) + System.Convert.ToInt32(FloatCurve.Evaluate(t)));
            if (fInfoSelected.FieldType == typeof(float))
                fInfoSelected.SetValue(Target, (float)fInfoSelected.GetValue(Target) + FloatCurve.Evaluate(t));
            if (fInfoSelected.FieldType == typeof(bool))
                fInfoSelected.SetValue(Target, System.Convert.ToBoolean(FloatCurve.Evaluate(t)));
            if (fInfoSelected.FieldType == typeof(Vector2))
            {
                Vector2 v = (Vector2)fInfoSelected.GetValue(Target);
                if (!((CurveX.keys.Length > 0 && CurveX.keys[0].time > t) || (CurveX.keys.Length > 0 && CurveX.keys[CurveX.keys.Length - 1].time < t)))
                    v.x += CurveX.Evaluate(t);
                if (!((CurveY.keys.Length > 0 && CurveY.keys[0].time > t) || (CurveY.keys.Length > 0 && CurveY.keys[CurveY.keys.Length - 1].time < t)))
                    v.y += CurveY.Evaluate(t);
                fInfoSelected.SetValue(Target, v);
            }
        }
        if (CaluateMethod == FrameSetting.Infusion.MINUS)
        {
            if (fInfoSelected.FieldType == typeof(int))
                fInfoSelected.SetValue(Target, (int)fInfoSelected.GetValue(Target) - System.Convert.ToInt32(FloatCurve.Evaluate(t)));
            if (fInfoSelected.FieldType == typeof(float))
                fInfoSelected.SetValue(Target, (float)fInfoSelected.GetValue(Target) - FloatCurve.Evaluate(t));
            if (fInfoSelected.FieldType == typeof(bool))
                fInfoSelected.SetValue(Target, System.Convert.ToBoolean(FloatCurve.Evaluate(t)));
            if (fInfoSelected.FieldType == typeof(Vector2))
            {
                Vector2 v = (Vector2)fInfoSelected.GetValue(Target);
                if (!((CurveX.keys.Length > 0 && CurveX.keys[0].time > t) || (CurveX.keys.Length > 0 && CurveX.keys[CurveX.keys.Length - 1].time < t)))
                    v.x -= CurveX.Evaluate(t);
                if (!((CurveY.keys.Length > 0 && CurveY.keys[0].time > t) || (CurveY.keys.Length > 0 && CurveY.keys[CurveY.keys.Length - 1].time < t)))
                    v.y -= CurveY.Evaluate(t);
                fInfoSelected.SetValue(Target, v);
            }
        }
    }
    private static bool DrawMatrix(Rect rect, bool value)
    {
#if UNITY_EDITOR
        if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
        {
            value = !value;
            GUI.changed = true;
            Event.current.Use();
        }
        UnityEditor.EditorGUI.DrawRect(rect, value ? new Color(0.1f, 0.4f, 0.2f) : new Color(0, 0, 0, 0.34f));

#endif
        return value;
    }

    // Use this for initialization
    void Start()
    {
        _origin = Basement.Speed;
        RandomCurveValue();
    }

    public List<Vector2> GetTargetVector2()
    {
        List<Vector2> pos = new List<Vector2>();
        float Xunit, Yunit, XL, YL, partRotation;
        XL = customPattern.GetLength(0);
        YL = customPattern.GetLength(1);
        Xunit = directlyScale.x / XL;
        Yunit = directlyScale.x / YL;
        partRotation = Range / Way;
        Transform _transform = transform;
        for (int g = 0; g != Way; ++g)
        {
            float _angle = Angle + g * partRotation + Random.Range(-ShotMess, ShotMess);
            for (int i = 0; i != XL; ++i)
            {
                for (int s = 0; s != YL; ++s)
                {
                    Quaternion r = Quaternion.Euler(new Vector3(0, 0, _angle));

                    if (customPattern[i, s])
                    {
                        if (ShotWay)
                        {

                            Vector2 _t = r * new Vector2(directlyScale.x * (i - ShotPoint.x) * Xunit, directlyScale.y * (s - ShotPoint.y) * Yunit);
                            _t = Quaternion.Euler(0, 0, _transform.eulerAngles.z) * _t;
                            pos.Add(_t);
                        }
                        else
                        {
                            Vector2 _t = r * new Vector2(directlyScale.x * (i - ShotPoint.x) * Xunit, directlyScale.y * (s - ShotPoint.y) * Yunit);
                            _t = Quaternion.Euler(0, 0, _transform.eulerAngles.z) * _t;
                            pos.Add(_t);
                        }
                    }

                }
            }
        }
        return pos;
    }
    // Update is called once per frame
    void Update()
    {
        if ( !Application.isPlaying)
            return;
        if (!Global.WrttienSystem && !Global.GamePause)
        {
            _timer += 1 * Global.GlobalSpeed;
            _stopWatch += 1 * Global.GlobalSpeed;
        }
        if (_timer > MaxFrame)
        {
            if (Reuse) _timer = 0;
            return;
        }
        if (_timer < Delay)
            return;

        Progress = _timer / MaxFrame * 100;
        if (_stopWatch > Timer)
        {
            _stopWatch = 0;
            if (ParmIndex != -1)
                OnUpdatingParm(this);
            float Spd = Basement.Speed;
            float Xunit, Yunit, XL, YL, partRotation;
            XL = customPattern.GetLength(0);
            YL = customPattern.GetLength(1);
            Xunit = directlyScale.x / XL;
            Yunit = directlyScale.x / YL;
            partRotation = Range / Way;
            Angle += AccAngle;
            Basement.Canceled = true;
            Transform _transform = transform;
            Transform _Basement_transform = Basement.transform ;
            int p = 0;
            for (int g = 0; g != Way; ++g)
            {
                float _angle = Angle + g * partRotation + Random.Range(-ShotMess, ShotMess);
                for (int i = 0; i != XL; ++i)
                {
                    for (int s = 0; s != YL; ++s)
                    {
                        Quaternion r = Quaternion.Euler(new Vector3(0, 0, _angle));
                        Basement.Angle = _angle;
                        Basement.followRotation = false;
                        if (customPattern[i, s])
                        {
                            if (ShotWay)
                            {
                                if (!DynamicSpd)
                                {
                                    Vector2 _t = r * new Vector2(directlyScale.x * (i - ShotPoint.x) * Xunit, directlyScale.y * (s - ShotPoint.y) * Yunit);
                                    _t = Quaternion.Euler(0, 0, _transform.eulerAngles.z) * _t;
                                    Basement.Shot(Basement.Way, true, true, _transform.position.x + _t.x, _transform.position.y + _t.y);
                                }
                                else
                                {
                                    Vector2 _t = r * new Vector2(directlyScale.x * (i - ShotPoint.x) * Xunit, 0);
                                    _t = Quaternion.Euler(0, 0, _transform.eulerAngles.z) * _t;
                                    Basement.Speed = SpdDistance * (s + 1);
                                    Basement.Shot(Basement.Way, true, true, _transform.position.x + _t.x, _transform.position.y + _t.y);
                                }
                            }
                            else {
                                Vector2 _t = r * new Vector2(directlyScale.x * (i - ShotPoint.x) * Xunit, directlyScale.y * (s - ShotPoint.y) * Yunit);
                                _t = Quaternion.Euler(0, 0, _transform.eulerAngles.z) * _t; 
                                Basement.Speed = Spd * Vector2.Distance(_t, (Vector2)_Basement_transform.position) * (SpdDistance+1);
                                Basement.Angle = 180+Bullet.GetAimToTargetRotation(_t,(Vector2)_Basement_transform.position);
                                Basement.Shot(1, true, true, _transform.position.x + _t.x, _transform.position.y + _t.y,p);
                                ++p;
                            }
                        }

                    }
                }
            }
            Basement.Speed = _origin;
        }

    }
}
