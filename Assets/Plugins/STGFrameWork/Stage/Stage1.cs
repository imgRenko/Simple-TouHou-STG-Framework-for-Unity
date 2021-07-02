using System.Collections;
using UnityEngine;
public class Stage1: StageControl
{
    public GameObject AssistPositionGuide;
    public override IEnumerator WhenFinishingStage ()
    {
        return base.WhenFinishingStage ();
    }
	IEnumerator WindowsEvent(){
		Global.WindowDialog_A.Hide ();
		Global.GamePause = false;
		yield return null;
	}
    public override IEnumerator SceneControl()
    {
        EnemyState _enemy = Global.GameObjectPool_A.ApplyEnemy();
        _enemy.SetEnemy(AssistPositionGuide.transform.position, UsableShooting[0], 0, 6);
        int IDT = _enemy.Target.ID;
        _enemy.Movement.AimToPlayerObject();
        _enemy.Movement.Speed = 0;
        _enemy.Movement.ChangeSpeedSmoothly = true;
        _enemy.Movement.TargetSpeed = 3;
        _enemy.Movement.MaxLiveFrame = 120;
        
        yield return new WaitForSeconds(5f);
        if (IDT != _enemy.Target.ID)
        {
            _enemy.ShootingList[0].enabled = false;
            _enemy.Movement.enabled = true;
            _enemy.Movement.Reusable = true;
            _enemy.Target.destroyWhenOutScreen = true;
            _enemy.Movement.Speed = 0;
            _enemy.Movement.TargetSpeed = 4;
            _enemy.Movement.Rotation = 110f;
        }
        yield return null;
    }
}
