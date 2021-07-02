public class Koishi_Effect_Background_ChangeSprite : ShootingEvent {
	public Enemy enemyCharacter;
	public override void OnShootingFinishAllShotTasks (Shooting Target)
	{
		base.OnShootingFinishAllShotTasks (Target);
		Target.CustomSprite = enemyCharacter.SpriteRender.sprite;
	}
}
