using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[AddComponentMenu("东方STG框架/弹幕设计/重要组件/力场控制器")]
public class Force : STGComponent
{
    [FoldoutGroup("力场细节", expanded: false)]
    [LabelText("允许回滚")]
    public bool EnableRollBack = true;

    public enum ForceType { Box = 0, Circle = 1 }
    [FoldoutGroup("力场细节", expanded: false)]
    [LabelText("力场判据形状")]
    public ForceType Type = ForceType.Circle;
    [FoldoutGroup("力场细节", expanded: false)]
    [LabelText("力场强度")]
    public float ForceStrength = 1;
    [FoldoutGroup("力场应用范围", expanded: false)]
    [LabelText("对玩家生效")]
    public bool ForPlayer = true;
    [FoldoutGroup("力场应用范围", expanded: false)]
    [LabelText("对子弹生效")]
    public bool ForBullet = false;
    [ShowIf("ForBullet")]
    [FoldoutGroup("力场应用范围", expanded: false)]
    [LabelText("生效的子弹Tag")]
    [Multiline] public string EnableForTag = "";
    [FoldoutGroup("力场细节", expanded: false)]
    [LabelText("力场方向为吸引")]
    public bool Attractive = true;
    [HideInInspector]
    public Vector3 GlobalVector = new Vector3(0, 1, 0);
    [FoldoutGroup("力场应用范围", expanded: false)]
    [LabelText("圆形半径")]
    public float Radius = 0.3f;
    [FoldoutGroup("力场应用范围", expanded: false)]
    [LabelText("对角线向量")]
    public Vector2 SquareLength;
    [FoldoutGroup("力场移动器", expanded: false)]
    [LabelText("力场速度")]
    public float ForceSpeed = 1;
    [FoldoutGroup("力场移动器", expanded: false)]
    [LabelText("力场加速度")]
    public float ForceAcceleratedSpeed = 0;
    [FoldoutGroup("力场移动器", expanded: false)]
    [LabelText("力场角速度")]
    public float ForceAccelerRotation = 0;
    [FoldoutGroup("力场移动器", expanded: false)]
    [LabelText("力场全局偏移")]
    public Vector2 GlobalPositionOffset = Vector2.zero;


    private int TotalFrame = 0;
    [FoldoutGroup("力场细节", expanded: false)]
    [LabelText("可复用力场")]
    public bool Reusable = false;
    public delegate void ForceEvent(Force F);

    public ForceEvent OnUsing;
    public ForceEvent OnDestroy;
    public ForceEvent OnUsage;
    //public List<int> BulletList = new List<int>();
    public struct ForceParm
    {
        public float ForceStrength;
        public string EnableForTag;
        public bool Attractive;
        public bool ForPlayer;
        public bool ForBullet;
        public Vector3 GlobalVector;
        public float Radius;
        public Vector2 SquareLength;
        public float ForceSpeed;
        public float ForceAcceleratedSpeed;
        public float ForceAccelerRotation;
        public Vector2 GlobalPositionOffer;
    };
    public ForceParm ForceOrginal;
    public void RollBack()
    {
        ForceStrength = ForceOrginal.ForceStrength;
        EnableForTag = ForceOrginal.EnableForTag;
        Attractive = ForceOrginal.Attractive;
        ForPlayer = ForceOrginal.ForPlayer;
        ForBullet = ForceOrginal.ForBullet;
        GlobalVector = ForceOrginal.GlobalVector;
        Radius = ForceOrginal.Radius;
        SquareLength = ForceOrginal.SquareLength;
        ForceSpeed = ForceOrginal.ForceSpeed;
        ForceAcceleratedSpeed = ForceOrginal.ForceAcceleratedSpeed;
        ForceAccelerRotation = ForceOrginal.ForceAccelerRotation;
        GlobalPositionOffset = ForceOrginal.GlobalPositionOffer;
    }
    public void SetOrginal()
    {
        ForceOrginal.ForceStrength = ForceStrength;
        ForceOrginal.EnableForTag = EnableForTag;
        ForceOrginal.Attractive = Attractive;
        ForceOrginal.ForPlayer = ForPlayer;
        ForceOrginal.ForBullet = ForBullet;
        ForceOrginal.GlobalVector = GlobalVector;
        ForceOrginal.Radius = Radius;
        ForceOrginal.SquareLength = SquareLength;
        ForceOrginal.ForceSpeed = ForceSpeed;
        ForceOrginal.ForceAcceleratedSpeed = ForceAcceleratedSpeed;
        ForceOrginal.ForceAccelerRotation = ForceAccelerRotation;
        ForceOrginal.GlobalPositionOffer = GlobalPositionOffset;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;


        Gizmos.DrawIcon(transform.position, "Force");
        if (Type == ForceType.Circle)
            Gizmos.DrawWireSphere(transform.position, Radius);
        if (Type == ForceType.Box)
            Gizmos.DrawWireCube(transform.position, SquareLength);
    }
    public Vector3 RotateVector(Vector3 position, float angle)
    {
        Vector3 point = Quaternion.AngleAxis(angle, Vector3.forward) * GlobalVector;
        return point;
    }
    void Awake()
    {
        SetOrginal();
    }
    void Start()
    {
        if (OnUsage != null)
            OnUsage(this);
    }
    private void Update()
    {
        if (UpdateWithSelfComponent)
            UpdateInfo();
    }
    public override void UpdateInfo()
    {
        if (Global.GamePause || Global.WrttienSystem)
            return;

        if (MaxLiveFrame <= TotalFrame)
        {
            if (OnDestroy != null)
                OnDestroy(this);

            if (Reusable)
            {
                if (EnableRollBack)
                    RollBack();

                TotalFrame = 0;
            }

            return;
        }

        if (OnUsing != null)
            OnUsing(this);

        if (ForPlayer)
        {
            var distance = Vector2.Distance(Global.PlayerObject.transform.position, this.gameObject.transform.position);

            if (distance > 0.1f)
            {
                Global.PlayerObject.transform.Translate(
                    (Application.isEditor ? 1 : Global.GlobalBulletSpeed) * (Attractive
                        ? RotateVector(GlobalVector, Bullet.GetAimToObjectRotation(this.gameObject, Global.PlayerObject)) * Global.GlobalSpeed * ForceStrength / 1000.0f
                        : -RotateVector(GlobalVector, Bullet.GetAimToObjectRotation(this.gameObject, Global.PlayerObject)) * Global.GlobalSpeed * ForceStrength / 1000.0f)
                        ,Space.World);
            }

        }

        ++TotalFrame;

        if (ForceAccelerRotation != 0)
            transform.Rotate(new Vector3(0, 0, ForceAccelerRotation));

        if (ForceAcceleratedSpeed != 0)
            ForceSpeed += ForceAcceleratedSpeed;

        if (ForceSpeed != 0)
            transform.Translate(Vector2.up * ForceSpeed / 100.0f * Global.GlobalSpeed * (Application.isEditor ? 1 : Global.GlobalBulletSpeed), Space.Self);

        if (GlobalPositionOffset != Vector2.zero)
            transform.Translate(GlobalPositionOffset * Global.GlobalSpeed * (Application.isEditor ? 1 : Global.GlobalBulletSpeed), Space.World);
    }
    public void BulletUpdate(Bullet a)
    {
        if (EnableForTag != a.Tag) return;

        if (a.Use == false) return;

        a.gameObject.transform.Translate((Application.isEditor ? 1 : Global.GlobalBulletSpeed) * (Attractive
                ? RotateVector(GlobalVector, Bullet.GetAimToObjectRotation(this.gameObject, a.gameObject)) * Global.GlobalSpeed * ForceStrength /
                  1000
                : -RotateVector(GlobalVector, Bullet.GetAimToObjectRotation(this.gameObject, a.gameObject)) * Global.GlobalSpeed * ForceStrength /
            1000),
            Space.World);
    }
}
