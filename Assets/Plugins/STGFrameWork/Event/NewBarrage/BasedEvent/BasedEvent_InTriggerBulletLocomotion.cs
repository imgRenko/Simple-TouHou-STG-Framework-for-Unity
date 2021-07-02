using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Sirenix.OdinInspector;
[AddComponentMenu("东方STG框架/弹幕设计/常见事件/触发器事件/触发器参数控制器(曲线+条件)")]
public class BasedEvent_InTriggerBulletLocomotion : TriggerEvent {
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
			if (fInfoSelected != null)
				isVector2 = fInfoSelected.FieldType == typeof(Vector2);
            if (fInfoSelected != null)
                isVector4 = fInfoSelected.FieldType == typeof(Color32);
        }
	}

    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("延迟使用")]
    public float Delay = 0f;
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("最长使用时间")]
    public float MaxTime = 200;
    [HideInInspector]
    public float i = 0;
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("离开触发器时将当前值设为默认值")]
    public bool resetDefaultValue;
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("要被设置的默认值")]
    public float newDefaultValue;
    [FoldoutGroup("流程控制", expanded: false)]
    [LabelText("可操作的成员变量")]
    public List<string> Name = new List<string> ();
    FieldInfo[] fInfo = typeof(Bullet).GetFields ();  
    FieldInfo fInfoSelected;
	float defaultValue;
    [HideInInspector]
    public int OrderIndex = -1;
    [SerializeField]
    public List<FrameSetting> frameSetting = new List<FrameSetting>();
    public FrameSetting.Infusion CaluateMethod = FrameSetting.Infusion.REPLACE;
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

        if (frameSetting.Count == 0)
            return;
        int minIndex = 0;
        int Key = 0;
        for (int i = 0; i != frameSetting.Count; ++i)
        {
            frameSetting[i].Use(false);
        }
        // Debug.Log("当前时间点" + time.ToString());
        for (int i = 0; i != frameSetting.Count; ++i)
        {
    
            if (Curve.keys[frameSetting[i].KeyIndex].time > time)
            {
                minIndex = i;
  
                frameSetting[minIndex].Use(true);
                Key = frameSetting[minIndex].KeyIndex;
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
                CaluateMethod = frameSetting[minIndex].infusion;
             
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
                CaluateMethod = frameSetting[minIndex].infusion;

            }
            else
                CaluateMethod = frameSetting[minIndex].Default;

        }

        //  Debug.Log("最终被修改成了" + CaluateMethod);
    }
    public bool CanEnterFrame(float time, FrameSetting r, Bullet Target)
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
    public override void OnBulletStayInTrigger(Bullet Target, Trigger Which, float stayTime, int enterTime)
    {
        if (OrderIndex == -1) {
            Target.CommandList.Add(new CommandController());
            OrderIndex = Target.CommandList.Count - 1;

        }
        if (OrderIndex > Target.CommandList.Count - 1) {
            OrderIndex = 0;
            Debug.Log("OUT RANGE");
            return; }
        fInfoSelected = fInfo[index];
        if (!Global.WrttienSystem && !Global.GamePause)
            i += 1 * Global.GlobalSpeed;
        float t = i / MaxTime;

        if (Target.TotalLiveFrame < Delay)
        {
             Debug.Log("OUT RANGE");
        }

        Target.CommandList[OrderIndex].IndexChecking = Target.CommandList[OrderIndex].MinIndex;

        GetInfusionMethod(t, Target);
        // 以下将设置子弹的值。
        if (Target.CommandList[OrderIndex].CaluateMethod == FrameSetting.Infusion.REPLACE)
        {
            if (fInfoSelected.FieldType == typeof(int))
                fInfoSelected.SetValue(Target, System.Convert.ToInt32(Curve.Evaluate(t)));
            if (fInfoSelected.FieldType == typeof(float))
                fInfoSelected.SetValue(Target, Curve.Evaluate(t));
            if (fInfoSelected.FieldType == typeof(bool))
                fInfoSelected.SetValue(Target, System.Convert.ToBoolean(Curve.Evaluate(t)));
            if (fInfoSelected.FieldType == typeof(Vector2)) {
                Vector2 v =  (Vector2)fInfoSelected.GetValue (Target);
                if (!((CurveX.keys.Length > 0 && CurveX.keys[0].time > t) || (CurveX.keys.Length > 0 && CurveX.keys[CurveX.keys.Length - 1].time < t)))
                    v.x = CurveX.Evaluate(t);
                if (!((CurveY.keys.Length > 0 && CurveY.keys[0].time > t) || (CurveY.keys.Length > 0 && CurveY.keys[CurveY.keys.Length - 1].time < t)))
                    v.y = CurveY.Evaluate(t);
                fInfoSelected.SetValue (Target, v );
            }

        }
        if (Target.CommandList[OrderIndex].CaluateMethod == FrameSetting.Infusion.ADD)
        {
            if (fInfoSelected.FieldType == typeof(int))
                fInfoSelected.SetValue(Target, (int)fInfoSelected.GetValue(Target) + System.Convert.ToInt32(Curve.Evaluate(t)));
            if (fInfoSelected.FieldType == typeof(float))
                fInfoSelected.SetValue(Target, (float)fInfoSelected.GetValue(Target) + Curve.Evaluate(t));
            if (fInfoSelected.FieldType == typeof(bool))
                fInfoSelected.SetValue(Target, System.Convert.ToBoolean(Curve.Evaluate(t)));
            if (fInfoSelected.FieldType == typeof(Vector2)) {
                Vector2 v = (Vector2)fInfoSelected.GetValue (Target);
                if (!((CurveX.keys.Length > 0 && CurveX.keys[0].time > t) || (CurveX.keys.Length > 0 && CurveX.keys[CurveX.keys.Length - 1].time < t)))
                    v.x += CurveX.Evaluate(t);
                if (!((CurveY.keys.Length > 0 && CurveY.keys[0].time > t) || (CurveY.keys.Length > 0 && CurveY.keys[CurveY.keys.Length - 1].time < t)))
                    v.y += CurveY.Evaluate(t);
                fInfoSelected.SetValue (Target, v );
            }

        }
        if (Target.CommandList[OrderIndex].CaluateMethod == FrameSetting.Infusion.MINUS)
        {
            if (fInfoSelected.FieldType == typeof(int))
                fInfoSelected.SetValue(Target, (int)fInfoSelected.GetValue(Target) - System.Convert.ToInt32(Curve.Evaluate(t)));
            if (fInfoSelected.FieldType == typeof(float))
                fInfoSelected.SetValue(Target, (float)fInfoSelected.GetValue(Target) - Curve.Evaluate(t));
            if (fInfoSelected.FieldType == typeof(bool))
                fInfoSelected.SetValue(Target, System.Convert.ToBoolean(Curve.Evaluate(t)));
            if (fInfoSelected.FieldType == typeof(Vector2)) {
                Vector2 v = (Vector2)fInfoSelected.GetValue (Target);
                if (!((CurveX.keys.Length > 0 && CurveX.keys[0].time > t) || (CurveX.keys.Length > 0 && CurveX.keys[CurveX.keys.Length - 1].time < t)))
                    v.x -= CurveX.Evaluate(t);
                if (!((CurveY.keys.Length > 0 && CurveY.keys[0].time > t) || (CurveY.keys.Length > 0 && CurveY.keys[CurveY.keys.Length - 1].time < t)))
                    v.y -= CurveY.Evaluate(t);
                fInfoSelected.SetValue (Target, v );
            }

        }
        if (Target.CommandList[OrderIndex].CaluateMethod == FrameSetting.Infusion.RANDOM)
        {
            if (Target.CommandList[OrderIndex].IndexChecking != Target.CommandList[OrderIndex].MinIndex)
            {
                Target.CommandList[OrderIndex].RandomCaValue = Random.Range(frameSetting[Target.CommandList[OrderIndex].MinIndex].MinRandomRange, 
                    frameSetting[Target.CommandList[OrderIndex].MinIndex].MaxRandomRange);
            }
            Target.CommandList[OrderIndex].IndexChecking = Target.CommandList[OrderIndex].MinIndex;
            if (Target.CommandList[OrderIndex].IndexChecking >= Curve.keys.Length || Target.CommandList[OrderIndex].IndexChecking-1 <0) return;
            float TimeDistance = Curve.keys[frameSetting[Target.CommandList[OrderIndex].IndexChecking].KeyIndex].time - 
                Curve.keys[frameSetting[Target.CommandList[OrderIndex].IndexChecking-1].KeyIndex].time;
            float PrimaryTime = t - Curve.keys[frameSetting[Target.CommandList[OrderIndex].IndexChecking-1].KeyIndex].time;


            if (fInfoSelected.FieldType == typeof(int))
                fInfoSelected.SetValue(Target, Mathf.Lerp(Curve.keys[frameSetting[Target.CommandList[OrderIndex].IndexChecking].KeyIndex].value,
                    (int)Curve.keys[frameSetting[Target.CommandList[OrderIndex].IndexChecking +1].KeyIndex].value + 
                    (int)Target.CommandList[OrderIndex].RandomCaValue, PrimaryTime / TimeDistance));
            if (fInfoSelected.FieldType == typeof(float))
                fInfoSelected.SetValue(Target, Mathf.Lerp(Curve.keys[frameSetting[Target.CommandList[OrderIndex].IndexChecking].KeyIndex].value,
                    (float)Curve.keys[frameSetting[Target.CommandList[OrderIndex].IndexChecking +1].KeyIndex].value +  
                    Target.CommandList[OrderIndex].RandomCaValue, PrimaryTime / TimeDistance));

            if (fInfoSelected.FieldType == typeof(Vector2))
            {
                Vector2 v = (Vector2)fInfoSelected.GetValue(Target);  
                if (!((CurveX.keys.Length > 0 && CurveX.keys[0].time > t) || (CurveX.keys.Length > 0 && CurveX.keys[CurveX.keys.Length - 1].time < t)))
                    v.x = Mathf.Lerp(CurveX.keys[frameSetting[Target.CommandList[OrderIndex].IndexChecking].KeyIndex].value,
                        (float)CurveX.keys[frameSetting[Target.CommandList[OrderIndex].IndexChecking +1].KeyIndex].value + 
                        Target.CommandList[OrderIndex].RandomCaValue, PrimaryTime / TimeDistance);
                if (!((CurveY.keys.Length > 0 && CurveY.keys[0].time > t) || (CurveY.keys.Length > 0 && CurveY.keys[CurveY.keys.Length - 1].time < t)))
                    v.y = Mathf.Lerp(CurveY.keys[frameSetting[Target.CommandList[OrderIndex].IndexChecking].KeyIndex].value,
                        (float)CurveY.keys[frameSetting[Target.CommandList[OrderIndex].IndexChecking +1].KeyIndex].value +
                        Target.CommandList[OrderIndex].RandomCaValue, PrimaryTime / TimeDistance);
                fInfoSelected.SetValue(Target, v);
            }
        }
    }
    public override void EventStart(Trigger Target)
    {
        fInfoSelected = fInfo [index];
    }
	public override void OnBulletEnterIntoTrigger (Bullet Which, Trigger Target, float a, int enterTime)
	{
        if (fInfoSelected.FieldType == typeof(float))
		    defaultValue = (float)fInfoSelected.GetValue(Which);

    }
	public override void OnBulletExitFromTrigger (Bullet Which, Trigger Target, float a, int enterTime)
	{
        i = 0;
		if (resetDefaultValue) {
			fInfoSelected.SetValue (Which, newDefaultValue);
			return;
		}
		fInfoSelected.SetValue (Which,defaultValue);

	}
    protected override void OnBeforeSerialize ()
    {
        if (Name.Count == fInfo.Length || Application.isPlaying) {
            return;
        }
        Debug.Log("Updated");
        Name.Clear ();
        for (int i = 0; i != fInfo.Length; ++i) {
            if (fInfo[i].FieldType != typeof(float) || fInfo[i].FieldType != typeof(bool) || fInfo[i].FieldType != typeof(bool) )
                    Name.Add (fInfo [i].Name);
        }
    }
}
