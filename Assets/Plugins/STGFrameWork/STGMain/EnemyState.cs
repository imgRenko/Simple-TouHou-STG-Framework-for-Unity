using System.Collections.Generic;
using  UnityEngine;
[AddComponentMenu("东方STG框架/框架核心/敌人类/敌人状态")]
public class EnemyState : MonoBehaviour
{
    public Bullet Movement;
    public bool Used = false;
    public Enemy Target;
    public Animator AnimationControl;
    public GameObject ShootingObject;

    public Shooting[] ShootingList;
    [HideInInspector]
    public int ClipFileIndex;
    public State EnemyStateNow = State.STATE_NO_USING;
    public enum  State
    {
        STATE_NO_USING = 0,
        STATE_ALIVE = 1,
        STATE_KILL = 2
    }
    /// <summary>
    /// 使用定义在Global类的AnimationControlFile相应序号对应的动画控制器的文件。
    /// </summary>
    /// <param name="Index"></param>
    public void UseAnimationController(int Index)
    {
		if (Global.AnimationControlFile_A == null)
			return;
		if (AnimationControl == null)
			return;
		if (Index > Global.AnimationControlFile_A.Length - 1)
			return;
        AnimationControl.runtimeAnimatorController = Global.AnimationControlFile_A[Index];
    }

	public Enemy SetEnemy(Vector2 point ,GameObject ShootingObject, int AnimationControllerIndex = 0 ,int HPmax = 300)
    {
        Target.gameObject.transform.parent.gameObject.SetActive(true);
        Target.gameObject.transform.parent.gameObject.transform.position = point;
        UseAnimationController(AnimationControllerIndex);
        Used = true;

        Target.ID = Random.Range(0, 9999999);
        Target.MaxHP = HPmax;
		Target.HP = Target.MaxHP;
        Movement.enabled = true;
        Movement.TotalLiveFrame = 0;
        EnemyStateNow = EnemyState.State.STATE_ALIVE;
        if (ShootingObject == null)
            return Target;
        
        GameObject Copy = Instantiate(ShootingObject, Target.gameObject.transform);
        Copy.transform.localPosition = Vector2.zero;
        Copy.SetActive(true);
        ShootingObject = Copy;
        ShootingList = Copy.GetComponentsInChildren<Shooting>();
        return Target;
    }
    private void Start()
    {
      
    }
}
