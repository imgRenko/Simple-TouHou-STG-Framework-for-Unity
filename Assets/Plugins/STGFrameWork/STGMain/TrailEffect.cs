using UnityEngine;
[AddComponentMenu("东方STG框架/弹幕设计/常见事件/子弹事件/拖尾效果演算")]
public class TrailEffect : BulletEvent {
    public override void OnBulletMoving (Bullet Target)
    {
        Color32 _t = new Color32((byte)255,(byte)255,(byte)255, (byte)(150f * (1 - Target.TotalLiveFrame / Target.MaxLiveFrame)));

        Target.BulletSpriteRenderer.color = _t;
    }
}
