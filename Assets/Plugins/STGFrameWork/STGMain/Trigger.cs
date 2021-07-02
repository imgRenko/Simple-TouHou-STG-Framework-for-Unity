using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
[AddComponentMenu("东方STG框架/弹幕设计/重要组件/触发器")]
public class Trigger : STGComponent
{
    [FoldoutGroup("触发器设置")]
    [LabelText("使用触发器")]
    public bool Use = true;
    public enum TriggerType { Box = 0, Circle = 1, Line = 2 }
    [FoldoutGroup("触发器设置")]
    [LabelText("检测玩家")]
    public bool checkPlayer = false;
    [FoldoutGroup("触发器设置")]
    [LabelText("碰撞类型")]
    public TriggerType Type = TriggerType.Circle;
    public delegate void TriggerDelegate(Bullet BulletRef, Trigger TriggerRef);
    public delegate void TriggerStayDelegate(Bullet BulletRef, Trigger TriggerRef, float stayTime,int enterTime);
    public delegate void TriggerSelfDelegate(Trigger TriggerRef);
    public delegate void TriggerBulletsInteractionDelegate(Bullet BulletRef, Bullet Bullet2Ref, Trigger TriggerRef);

    public delegate IEnumerator TriggerStayDelegateDelay(Bullet BulletRef, Trigger TriggerRef, float stayTime, int enterTime);
    public delegate IEnumerator TriggerDelegateDelay(Bullet BulletRef, Trigger TriggerRef);
    public delegate IEnumerator TriggerSelfDelegateDelay(Trigger TriggerRef);

    public event TriggerStayDelegate TriggerEventWhenStay;
    public event TriggerStayDelegate TriggerEventWhenExit;
    public event TriggerStayDelegate TriggerEventWhenEnter;

    public event TriggerBulletsInteractionDelegate TriggerBulletsEventWhenStay;
    public event TriggerBulletsInteractionDelegate TriggerBulletsEventWhenExit;
    public event TriggerBulletsInteractionDelegate TriggerBulletsEventWhenEnter;

    public event TriggerStayDelegateDelay TriggerEventWhenStayDelay;
    public event TriggerDelegateDelay TriggerEventWhenExitDelay;
    public event TriggerDelegateDelay TriggerEventWhenEnterDelay;
    public event TriggerSelfDelegate TriggerUsing;
    public event TriggerSelfDelegate TriggerDestroy;
    public event TriggerSelfDelegate TriggerStart;

    public event TriggerSelfDelegateDelay TriggerUsingDelay;
    public event TriggerSelfDelegateDelay TriggerDestroyDelay;
    public event TriggerSelfDelegateDelay TriggerStartDelay;

    [FoldoutGroup("触发器设置")]
    [LabelText("触发器新循环使用开始事件")]
    public bool loopStartEvent = true;
    [FoldoutGroup("触发器设置")]
    [LabelText("调用停留事件")]

    public bool AllowStayEvent = true;
    [FoldoutGroup("触发器设置")]
    [LabelText("额外检查子弹")]
    public bool CheckExtraBullet;
    [FoldoutGroup("触发器设置")]
    [LabelText("额外检查子弹Tag")]
    [ShowIf("CheckExtraBullet")]
    public string ExtraBulletTag = "";
    [FoldoutGroup("触发器设置")]
    [LabelText("触发器圆形半径")]
    [ShowIf("Type", TriggerType.Circle)]
    public float Radius = 0.3f;
    [FoldoutGroup("触发器设置")]
    [LabelText("对角线长度")]
    [ShowIf("Type", TriggerType.Box)]
    public Vector2 SquareLength;
    [FoldoutGroup("触发器设置")]
    [LabelText("辅助线起始")]
    public GameObject AuxiliaryLinesStart;
            [FoldoutGroup("触发器设置")]
    [LabelText("辅助线终止")]
    public GameObject AuxiliaryLinesEnd;
    [FoldoutGroup("触发器运动")]
    [LabelText("触发器速度")]
    public float TriggerSpeed = 0;
    [FoldoutGroup("触发器运动")]
    [LabelText("触发器加速度")]
    public float TriggerAcceleratedSpeed = 0;
    [FoldoutGroup("触发器运动")]
    [LabelText("触发器角速度")]
    public float TriggerAccelerRotation = 0;
    [FoldoutGroup("触发器设置")]
    [LabelText("可复用")]
    public bool reusable = true;
    [HideInInspector]

    [LabelText("组件年龄")]
    public float TotalLiveFrame = 0;
    [FoldoutGroup("触发器设置")]
    [LabelText("只使用一次")]
    public bool OnceTime = false;
    [FoldoutGroup("触发器设置")]
    [LabelText("检查子弹有效性")]
    public bool CheckVailed = true;
    [FoldoutGroup("触发器设置")]
    [LabelText("最大使用次数")]
    public int MaxUseTime = -1;
    [FoldoutGroup("触发器运动")]
    [LabelText("全局运动")]
    public Vector2 GlobalPositionOffset = Vector2.zero;

    static public List<Trigger> TriggerList = new List<Trigger>();
    [LabelText("受影响的额外检测子弹")]
    public List<TriggerReceiver> Extra = new List<TriggerReceiver>();
    [HideInInspector]
    public Bullet masterBullet;
    [HideInInspector]
    public bool bulletChecker;
    Character Triggertarget;
    [HideInInspector]
    public float PlayerStayTime = 0;
    [HideInInspector]
    public int PlayerEnterTime = 0;
    void OnDrawGizmos()
    {
        if (transform == null)
            return;
        Gizmos.DrawIcon(transform.position, "Trigger");
        if (Type == TriggerType.Circle)
            Gizmos.DrawWireSphere(transform.position, Radius);
        if (Type == TriggerType.Box)
            Gizmos.DrawWireCube(transform.position, SquareLength);
        if (AuxiliaryLinesStart != null && AuxiliaryLinesEnd != null)
            Gizmos.DrawLine(AuxiliaryLinesStart.transform.position, AuxiliaryLinesEnd.transform.position);
    }

    public void UseStayEvent(Bullet bullet, float t,int enterTime)
    {
        if (AllowStayEvent == false)
            return;

        if (TriggerEventWhenStay != null)
            TriggerEventWhenStay(bullet, this, t, enterTime);
        if (TriggerEventWhenStayDelay != null)
            StartCoroutine(TriggerEventWhenStayDelay(bullet, this, t, enterTime));
    }
    public void UseStayEvent(Bullet bullet, Bullet bullet2)
    {
        if (bullet.TotalLiveFrame > bullet.MaxLiveFrame && CheckVailed)
            return;
        if (AllowStayEvent == false)
            return;

        if (TriggerBulletsEventWhenStay != null)
            TriggerBulletsEventWhenStay(bullet, bullet2, this);
    }
    public void OnBulletExitFromTrigger(Bullet bullet, Bullet bullet2 = null,float StayTime=0,int Entertime=0)
    {
        if (bullet != null)
        {
            if (bullet.TotalLiveFrame > bullet.MaxLiveFrame && CheckVailed)
                return;
        }
        if (bullet2 != null && CheckExtraBullet)
        {
            if (TriggerBulletsEventWhenExit != null)
                TriggerBulletsEventWhenExit(bullet, bullet2, this);
        }
        else
        {
            if (TriggerEventWhenExit != null)
                TriggerEventWhenExit(bullet, this, StayTime, Entertime);
            if (TriggerEventWhenExitDelay != null)
                StartCoroutine(TriggerEventWhenExitDelay(bullet, this));
        }
    }
    public void OnBulletEnterIntoTrigger(Bullet bullet, Bullet bullet2 = null, float StayTime=0, int Entertime=0)
    {
       
        if (bullet != null)
        {
        
            if (bullet.TotalLiveFrame > bullet.MaxLiveFrame && CheckVailed)
            {
            //    Debug.Log("dsf3");
                return;
            }
        }
        if (bullet2 != null && CheckExtraBullet)
        {
           // Debug.Log("dsf4");
            if (TriggerBulletsEventWhenEnter != null)
                TriggerBulletsEventWhenEnter(bullet, bullet2, this);
        }
        else
        {
         //   Debug.Log(TriggerEventWhenEnter==null);
          //  Debug.Log("dsf5");
            if (TriggerEventWhenEnter != null)
            {
             //   Debug.Log("dsf6");
                TriggerEventWhenEnter(bullet, this, StayTime, Entertime);
            }
            if (TriggerEventWhenEnterDelay != null)
                StartCoroutine(TriggerEventWhenEnterDelay(bullet, this));
        }
     //   Debug.Log("dsf7");
    }
    public void OnTriggerUsing(Trigger Trig)
    {
        if (TriggerUsing != null)
            TriggerUsing(Trig);
        if (TriggerUsingDelay != null)
            StartCoroutine(TriggerUsingDelay(Trig));
    }
    public void OnTriggerDestroy(Trigger Trig)
    {
        if (TriggerDestroy != null)
            TriggerDestroy(Trig);
        if (TriggerDestroyDelay != null)
            StartCoroutine(TriggerDestroyDelay(Trig));
    }
    public void OnTriggerStart(Trigger Trig)
    {
        if (TriggerStart != null)
            TriggerStart(Trig);
        if (TriggerStartDelay != null)
            StartCoroutine(TriggerStartDelay(Trig));
    }
    void Start()
    {
        Triggertarget = Global.PlayerObject.GetComponent<Character>();
        OnTriggerStart(this);
        if (checkPlayer)
        {
            TriggerList.Add(this);
            Triggertarget.inTrigger.Add(false);
        }
        masterBullet = GetComponentInParent<Bullet>();
        if (masterBullet != null)
            bulletChecker = true;
    }
    void OnDestroy()
    {
        try
        {
            int index = TriggerList.FindIndex(x => x == this);
            TriggerList.RemoveAt(index);
            Triggertarget.inTrigger.RemoveAt(index);
        }
        catch
        {

        }
    }
    public void CheckExtra()
    {
        // 去除无效元素
        List<TriggerReceiver> triggers = new List<TriggerReceiver>();
        for (int i = 0; i < ObjectPool.ExtraChecking.Count; ++i)
        {
            if (ObjectPool.ExtraChecking[i] == null)
                triggers.Add(ObjectPool.ExtraChecking[i]);

        }
        foreach (var a in triggers)
        {
            ObjectPool.ExtraChecking.Remove(a);
        }

        for (int i = 0; i < ObjectPool.ExtraChecking.Count; ++i)
        {
            if (ObjectPool.ExtraChecking[i].bullet.DestroyMode)
                continue;
            if (ExtraBulletTag != ObjectPool.ExtraChecking[i].Tag)
                continue;
            if (Type == Trigger.TriggerType.Box)
            {
                bool Result = Bullet.Intersection(gameObject.transform.position, SquareLength, ObjectPool.ExtraChecking[i].bullet.gameObject.transform.position, Radius);
                if (Result == true && !Extra.Contains(ObjectPool.ExtraChecking[i]) && Use == true)
                {
                    Debug.Log(Result);
                    Extra.Add(ObjectPool.ExtraChecking[i]);


                    OnBulletEnterIntoTrigger(ObjectPool.ExtraChecking[i].bullet, masterBullet);
                    //Debug.Log("Extra Out!");
                    if (OnceTime)
                    {
                        Use = false;
                    }
                }
                if (Result && Use == true)
                {

                    UseStayEvent(ObjectPool.ExtraChecking[i].bullet, masterBullet);
                    // Debug.Log("Extra Stay!");
                }
                if (Result == false && Extra.Contains(ObjectPool.ExtraChecking[i]) && Use == true)
                {
                    Extra.Remove(ObjectPool.ExtraChecking[i]);

                    OnBulletExitFromTrigger(ObjectPool.ExtraChecking[i].bullet, masterBullet);
                    // Debug.Log("Extra In!");
                    if (OnceTime)
                    {
                        Use = false;
                    }
                }

                //inTrigger[i] = Result;
            }
            if (Type == Trigger.TriggerType.Circle)
            {
                float t = Vector2.Distance(gameObject.transform.position, ObjectPool.ExtraChecking[i].bullet.gameObject.transform.position);

                float a = Radius + ObjectPool.ExtraChecking[i].bullet.Radius;
                bool Result = (a > t);
                if (Result == true && !Extra.Contains(ObjectPool.ExtraChecking[i]) && Use == true)
                {
                    Extra.Add(ObjectPool.ExtraChecking[i]);
                    OnBulletEnterIntoTrigger(ObjectPool.ExtraChecking[i].bullet, masterBullet);
                 

                    if (OnceTime)
                    {
                        Use = false;
                    }

                }
                if (Result && Use == true)
                {

                    UseStayEvent(ObjectPool.ExtraChecking[i].bullet, masterBullet);

                }
                if (Result == false && Extra.Contains(ObjectPool.ExtraChecking[i]) && Use == true)
                {

                    Extra.Remove(ObjectPool.ExtraChecking[i]);
                    OnBulletExitFromTrigger(ObjectPool.ExtraChecking[i].bullet, masterBullet);

                    if (OnceTime)
                    {
                        Use = false;
                    }

                }

                //inTrigger[i] = Result;
            }
            if (Type == Trigger.TriggerType.Line)
            {

                float t = Vector2.Dot(AuxiliaryLinesEnd.transform.position - ObjectPool.ExtraChecking[i].transform.position, (AuxiliaryLinesEnd.transform.position - AuxiliaryLinesStart.transform.position));
                float ang = Mathf.Abs(Vector2.Angle(AuxiliaryLinesEnd.transform.position - ObjectPool.ExtraChecking[i].transform.position, (AuxiliaryLinesEnd.transform.position - AuxiliaryLinesStart.transform.position)));
                float final = t * Mathf.Tan(ang * Mathf.Deg2Rad);

                float a = Radius + ObjectPool.ExtraChecking[i].bullet.Radius;
                bool Result = (a > final);
                if (Result == true && !Extra.Contains(ObjectPool.ExtraChecking[i]) && Use == true)
                {
                    Extra.Add(ObjectPool.ExtraChecking[i]);

                    OnBulletEnterIntoTrigger(ObjectPool.ExtraChecking[i].bullet, masterBullet);

                    if (OnceTime)
                    {
                        Use = false;
                    }

                }
                if (Result && Use == true)
                {

                    UseStayEvent(ObjectPool.ExtraChecking[i].bullet, masterBullet);

                }
                if (Result == false && Extra.Contains(ObjectPool.ExtraChecking[i]) && Use == true)
                {
                    Extra.Remove(ObjectPool.ExtraChecking[i]);
                    OnBulletExitFromTrigger(ObjectPool.ExtraChecking[i].bullet, masterBullet);

                    if (OnceTime)
                    {
                        Use = false;
                    }

                }

                //inTrigger[i] = Result;
            }
        }
    }
    private void Update()
    {
        if (UpdateWithSelfComponent)
            UpdateInfo();
    }
    public override void UpdateInfo()
    {
        if (Global.WrttienSystem && MaxLiveFrame < TotalLiveFrame)
            return;
        TotalLiveFrame += 1 * Global.GlobalSpeed;
        if (MaxLiveFrame == TotalLiveFrame)
        {
            if (TriggerDestroy != null)
                TriggerDestroy(this);
        }
        if (CheckExtraBullet)
            CheckExtra();
        if (TriggerUsing != null)
            TriggerUsing(this);
        if (TriggerAccelerRotation != 0)
            transform.Rotate(new Vector3(0, 0, TriggerAccelerRotation));
        if (TriggerAcceleratedSpeed != 0)
            TriggerSpeed += TriggerAcceleratedSpeed;
        if (TriggerSpeed != 0)
            transform.Translate(Vector2.up * TriggerSpeed * Global.GlobalSpeed / 100 * (Application.isEditor ? 1 : Global.GlobalBulletSpeed), Space.Self);
        if (GlobalPositionOffset != Vector2.zero)
            transform.Translate(GlobalPositionOffset * Global.GlobalSpeed * (Application.isEditor ? 1 : Global.GlobalBulletSpeed), Space.World);
        if (reusable && TotalLiveFrame >= MaxLiveFrame)
        {
            TotalLiveFrame = 0;
            if (loopStartEvent)
                OnTriggerStart(this);
        }
        else if (TotalLiveFrame >= MaxLiveFrame)
        {
            try
            {
                int index = TriggerList.FindIndex(x => x == this);
                TriggerList.RemoveAt(index);
                Triggertarget.inTrigger.RemoveAt(index);
            }
            catch
            {

            }
            enabled = false;
        }
    }
}
