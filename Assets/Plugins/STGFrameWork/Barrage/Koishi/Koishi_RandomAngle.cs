using UnityEngine;

public class Koishi_RandomAngle : ShootingEvent {
	 public override void AfterShootingFinishedShooting(Shooting Target,Bullet bullet)
	{
	
		Target.Angle = Random.Range (0, 360);
	}
}
