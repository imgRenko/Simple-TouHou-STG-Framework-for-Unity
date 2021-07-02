using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("东方STG框架/弹幕设计/常见事件/子弹事件/拖尾效果风格设置")]
public class TrailStyle : ShootingEvent {
    public Bullet Orginal;
    public override void BeforeShooting(Shooting Target)
    {
        Target.CustomSprite = Orginal.BulletSpriteRenderer.sprite;
        Target.Angle = Orginal.Rotation;
        Target.Timer = Orginal.TrailUpdate;
        Target.MaxLiveFrame = Orginal.TrailLength;
    }
}
