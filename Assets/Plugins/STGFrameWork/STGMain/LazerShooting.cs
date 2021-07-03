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
public class LazerMovementInfo
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
    public LazerMovementInfo Clone() {
        return (LazerMovementInfo)this.MemberwiseClone();
    }
}
[AddComponentMenu("东方STG框架/弹幕设计/重要组件/激光发射器")]
public class LazerShooting : STGComponent
{
    [FoldoutGroup("总体控制", expanded: false)]
    public float totalFrames = 0;

    [FoldoutGroup("总体控制", expanded: false)]
    public float Interval = 30;

    public int Way = 1;

    public float rotationRange = 360;

    public AdditionalProperty Angle;

    public float LazerColliderRadius = 0.1f;

    [FoldoutGroup("总体控制", expanded: false)]
    [PreviewField(50, ObjectFieldAlignment.Left)]
    public Texture2D lazerTexture;

    [FoldoutGroup("总体控制", expanded: false)]
    public LazerMovementInfo lazerMovementInfo, selfMovementInfo;

    private float intervalCount = 0;


    public LazerShooting() {

        lazerMovementInfo = new LazerMovementInfo
        {
            Speed = new AdditionalProperty { Property = 2 },
            maxFrame = 300,
            trailTime = 2
        };
        selfMovementInfo = new LazerMovementInfo();

    }
  

    public void RecoverAndShot() { 
    
    }
    

    public void Shot() {
        if (Way < 0 || Global.PlayerCharacterScript.DestroyingBullet)
            return;
        for (int i = 0; i != Way; ++i)
        {
            LazerMovement Lazer = Global.GameObjectPool_A.ApplyLazerMovement();
            LazerMovementInfo Info = lazerMovementInfo.Clone();
            
            Info.Rotation = rotationRange / Way * i + Angle.Property;
            Lazer.transform.position = this.transform.position;
            Lazer.RefleshTrail();
            Lazer.movementInfo = Info;
            Lazer.MaxLiveFrame = lazerMovementInfo.maxFrame;
            Lazer.LazerColliderRadius = LazerColliderRadius;
            Lazer.OccupieLazer(lazerTexture, Info.trailTime);
        }
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

        // 处理发射

        intervalCount += Global.GlobalSpeed;

        if (intervalCount > Interval) {
            intervalCount = 0;
            Shot();
        }

    }



    
}
