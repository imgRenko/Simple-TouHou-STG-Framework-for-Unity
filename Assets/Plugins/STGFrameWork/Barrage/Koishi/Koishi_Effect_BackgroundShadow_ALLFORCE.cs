using UnityEngine;

public class Koishi_Effect_BackgroundShadow_ALLFORCE : BulletEvent {
    Color32 _t ;
	public override void OnBulletMoving (Bullet Target)
	{
        Target.Scale = Vector2.one *( 1f * (Target.TotalLiveFrame / Target.MaxLiveFrame) + 1);
        _t.a = (byte)(255 -  255.0f * (Target.TotalLiveFrame / Target.MaxLiveFrame));
		Target.BulletSpriteRenderer.color = _t;
	}
}
