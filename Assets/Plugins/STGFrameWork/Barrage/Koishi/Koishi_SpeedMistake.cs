public class Koishi_SpeedMistake : ShootingEvent {
	public Shooting next;
	 public override void AfterShootingFinishedShooting(Shooting Target,Bullet bullet)
	{
		
		Target.Speed += 0.7f;
		Target.Angle += 24f;
	}
	public override void OnShootingDestroy (Shooting Target)
	{
		base.OnShootingDestroy (Target);
		Target.Speed = 5f;
		Target.Angle = 0;
		if (next != null) {
			Shooting.RecoverShooting (next,true);
		}
	}
	public override System.Collections.IEnumerator OnShootingRollBackDelay (Shooting Target)
	{
		return base.OnShootingRollBackDelay (Target);

	}
}
