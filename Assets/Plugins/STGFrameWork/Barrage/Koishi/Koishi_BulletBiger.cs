using UnityEngine;

public class Koishi_BulletBiger : BulletEvent {
	public override void OnBulletMoving (Bullet Target)
	{
		base.OnBulletMoving (Target);
        Target.Scale = Vector2.one *( 0.4f * (Target.TotalLiveFrame / Target.MaxLiveFrame) + 1);
        Color32 _t = new Color32((byte)255,(byte)255,(byte)255, (byte)(255 -  (Target.TotalLiveFrame / Target.MaxLiveFrame)));
		Target.BulletSpriteRenderer.color = _t;
	}
}
