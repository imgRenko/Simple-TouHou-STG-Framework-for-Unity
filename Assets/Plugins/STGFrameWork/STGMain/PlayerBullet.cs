using UnityEngine;
[AddComponentMenu("东方STG框架/框架核心/自机类/自机子弹")]
public class PlayerBullet : MonoBehaviour
{
    public int SpriteIndex = 0;
    public AnimationCurve bulletCurve;
    public float Speed = 0;
	public float MaxLiveFrame = 200;
	public float TotalLiveFrame = 0;
    public bool Follow;
    public string DestroyAnimName = "PlayerBulletDestroy";
    public int AttackNumber;
    public bool attacked = false;
    public Vector3 OriginalPos;
    public SpriteRenderer BulletSprite;
    public Transform transformObject;
    public bool Used;
    public Animator animControl;
    public static Vector3 defPos;
   
    private void Awake()
    {
        transformObject = this.transform;
        defPos = new Vector3(9999, 9999, 0);
    }
    // Update is called once per frame
    public void Disabled(){
        transformObject.position = defPos;
        TotalLiveFrame = 0;
         attacked = false;
        Used = false;
    }
    public void ResetPos(Vector3 pos) {
        transformObject.position = pos;
        transformObject.eulerAngles = Vector3.zero;
    }
    void Update()
    {
        
        if (Global.GamePause == true || !Used) {
			return;
		}
     
        TotalLiveFrame += 1 * Global.GlobalSpeed;
        if (Follow)
        {
            
            if (Global.AllEnemy.Count != 0)
            {
                AttackNumber = -1;
                for (int i = 0; i != Global.AllEnemy.Count; ++i)
                {
                    if (Global.AllEnemy[i].HP != 0 && Global.AllEnemy[i].enemyState.EnemyStateNow == EnemyState.State.STATE_ALIVE)
                    {
                        AttackNumber = i;
                        break;
                    }
                }
                if (AttackNumber != -1)
                {
                    transformObject.position = (Vector2)Vector3.Slerp(OriginalPos, Global.AllEnemy[AttackNumber].gameObject.transform.position, bulletCurve.Evaluate(TotalLiveFrame));
                    transformObject.eulerAngles = new Vector3 (0, 0, Math2D.GetAimToObjectRotation (Global.AllEnemy[AttackNumber].gameObject, gameObject) - 180);
                }
                else
                    Follow = false;
            }
            else
                Follow = false;
        }
        else
        {

           
            transformObject.Translate(Vector3.up * Speed * Global.GlobalSpeed, Space.Self);
            transformObject.eulerAngles = new Vector3 (0, 0, Mathf.Lerp (gameObject.transform.eulerAngles.z,0,0.05f * Global.GlobalSpeed));
        }
        if (TotalLiveFrame > MaxLiveFrame)
        {
            Disabled();

        }
    }

}
