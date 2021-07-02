using UnityEngine;

public class Koishi_Force_BulletScaleChange : BulletEvent {
	public override void OnBulletMoving (Bullet Target)
	{
		base.OnBulletMoving (Target);
		Target.Scale *= 0.93f;

		Color _t = new Color(Target.BulletColor.r,Target.BulletColor.g,Target.BulletColor.b, (10 * Target.TotalLiveFrame));
		Target.BulletSpriteRenderer.color = _t;
	}
	public override void OnBulletCreated (Bullet Target)
	{
		base.OnBulletCreated (Target);
		Target.InverseRotateDirection = Random.Range (0, 360f);
	}
}
