public class Koishi_TriggerClosetoPlayer : TriggerEvent {
	public Shooting shoot;
	public int shottimer = 10; 
	float _count = 0;
	public override void OnBulletStayInTrigger (Bullet Which, Trigger Target,float a, int enterTime)
	{

		_count += 1 * Global.GlobalSpeed;
		if (_count > shottimer) {
			_count = 0;
			if (Which == null)
				Shooting.RecoverShooting (shoot, true,false);
		}
	}
	public override void OnBulletExitFromTrigger (Bullet Which, Trigger Target, float a, int enterTime)
	{
		
		_count = 0;
	}
}
