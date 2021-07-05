using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[System.Serializable]
public class AdditionalProperty
{
    public float Property;
    public float acceleratedProperty;
    public Vector2 randomVector;
    
    private void DoRandom() {
        if (randomVector != Vector2.zero)
        Property = Random.Range(randomVector.x, randomVector.y);
    }

    private void DoAccelerated() {
        Property += acceleratedProperty;
    }

    public void DoUpdate() {
        DoRandom();
        DoAccelerated();
    }
    public AdditionalProperty Clone()
    {
        return (AdditionalProperty)this.MemberwiseClone();
    }
}
[System.Serializable]
public class LaserMovementInfo
{
    public AdditionalProperty Speed;
    public AdditionalProperty Angular;
    public float Rotation;
    public float maxFrame;
    public float trailTime;

    /// <summary>
    /// 返回一个速度向量（世界坐标）
    /// </summary>
    /// <returns></returns>
    public Vector2 DoUpdate()
    {
       Speed.DoUpdate();
       Angular.DoUpdate();

       Rotation += Angular.Property;

        Vector2 SpeedVector = Quaternion.Euler(0, 0, Rotation) * (Vector2.up * Speed.Property) / 60.0f;

        return SpeedVector;

    }
    public LaserMovementInfo Clone() {
        return (LaserMovementInfo)this.MemberwiseClone();
    }
}
[AddComponentMenu("东方STG框架/弹幕设计/重要组件/激光发射器")]
public class LaserShooting : STGComponent
{
    [FoldoutGroup("总体控制", expanded: false)]
    public float totalFrames = 0;

    [FoldoutGroup("总体控制", expanded: false)]
    public float Interval = 30;

    public int Way = 1;

    public float rotationRange = 360;

    public AdditionalProperty Angle;

    public float laserColliderRadius = 0.1f;

    [HideInInspector]
    public List<LaserMovement> laserMovements = new List<LaserMovement>();

    [FoldoutGroup("总体控制", expanded: false)]
    [PreviewField(50, ObjectFieldAlignment.Left)]
    public Texture2D laserTexture;

    [FoldoutGroup("总体控制", expanded: false)]
    public LaserMovementInfo laserMovementInfo, selfMovementInfo;

    private float intervalCount = 0;

    public delegate void LaserShootingShotTask(LaserShooting laserShooting, LaserMovement laserMovement);

    public delegate void LaserShootingEvent(LaserShooting laserShooting);

    public event LaserShootingShotTask  AfterLaserShootingFinishedShooting;


    public event LaserShootingEvent LaserShootingUsing, LaserShootingBeforeShooting, LaserShootingFinishShotTask;


    public LaserShooting() {

        laserMovementInfo = new LaserMovementInfo
        {
            Speed = new AdditionalProperty { Property = 2 },
            maxFrame = 300,
            trailTime = 2
        };
        selfMovementInfo = new LaserMovementInfo();

    }
  

    public void RecoverAndShot() { 
    
    }
    

    public void Shot() {
        if (Way < 0 || Global.PlayerCharacterScript.DestroyingBullet)
            return;
        if (LaserShootingBeforeShooting != null)
            LaserShootingBeforeShooting(this);
        laserMovements.Clear();
        for (int i = 0; i != Way; ++i)
        {
            LaserMovement Laser = Global.GameObjectPool_A.ApplyLaserMovement();
            LaserMovementInfo Info = laserMovementInfo.Clone();
            
            Info.Rotation = rotationRange / Way * i + Angle.Property;
            Laser.transform.position = this.transform.position;
            Laser.RefleshTrail();
            Laser.movementInfo = Info;
            Laser.MaxLiveFrame = laserMovementInfo.maxFrame;
            Laser.LaserColliderRadius = laserColliderRadius;
            Laser.OccupieLaser(laserTexture, Info.trailTime);
            laserMovements.Add(Laser);
            if (LaserShootingFinishShotTask != null)
            AfterLaserShootingFinishedShooting(this, Laser);

        }
        if (LaserShootingFinishShotTask != null)
            LaserShootingFinishShotTask(this);
    }

    void Update()
    {
        if (UpdateWithSelfComponent)
            UpdateBase();
        
    }

    void UpdateBase() {

        if (Global.GamePause || Global.WrttienSystem) { return; }

        // 处理自运动

        Angle.DoUpdate();

        if (totalFrames > MaxLiveFrame)
            enabled = false;

        totalFrames += Global.GlobalSpeed;

        transform.Translate(selfMovementInfo.DoUpdate(), Space.World);

        if (LaserShootingUsing != null)
            LaserShootingUsing(this);


        // 处理发射

        intervalCount += Global.GlobalSpeed;

        if (intervalCount > Interval) {
            intervalCount = 0;
            Shot();
        }

    }



    
}
