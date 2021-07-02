using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
[AddComponentMenu("东方STG框架/框架核心/事件基础/子弹事件基类")]
public class BulletEvent :  SerializedMonoBehaviour{
    [FoldoutGroup("子弹事件初始化", expanded: false)]
    [LabelText("目的发射器")]
    public Shooting[] TargetShooting;
    [FoldoutGroup("子弹事件初始化", expanded: false)]
    [LabelText("自动获取目的发射器")]
    public bool autoGet = true;
    [FoldoutGroup("子弹事件初始化", expanded: false)]
    [LabelText("自动获取本对象子弹")]
    public bool autoGetBullet = false;

    [FoldoutGroup("子弹事件初始化", expanded: false)]
    [LabelText("注释")]
    [Multiline]
    public string Note="（于此处输入注解）";
    [FoldoutGroup("子弹事件初始化", expanded: false)]
    [LabelText("设置目的发射器以第一个被发现的发射器")]
    public bool ForOnlyOne = false;
	/// <summary>
	/// 在发射器每一次发射操作中，这颗子弹的序号数。
	/// </summary>

    void Start () {
        AddEvent();
    }
    void Awake(){
        OnBulletActive ();
    }
    public void AddEvent ()
    {
        if (autoGet)
            TargetShooting = GetComponents<Shooting> ();
        if ( ForOnlyOne )
        {
            TargetShooting[0].BulletEvent += OnBulletMoving;
            TargetShooting[0].BulletEventWhenBulletCreate += OnBulletCreated;
            TargetShooting[0].BulletEventWhenBulletDestroy += OnBulletDestroy;
			TargetShooting[0].BulletEventWhenBulletRestoreMainLevel += OnBulletRecoverMainLevel;
            TargetShooting[0].BulletEventDelay += OnBulletMovingDelay;
            TargetShooting[0].BulletEventWhenBulletCreateDelay += OnBulletCreatedDelay;
            TargetShooting[0].BulletEventWhenBulletDestroyDelay += OnBulletDestroyDelay;
            TargetShooting[0].BulletEventWhenBulletRestoreMainLevelDelay += OnBulletRecoverMainLevelDelay;
            TargetShooting[0].BulletEventOnDestroyingPlayerDelay += OnBulletDestroyPlayerDelay;
            TargetShooting[0].BulletEventOnDestroyingPlayer += OnBulletDestroyPlayer;
        }
        else
        {
            for ( int i = 0; i != TargetShooting.Length; ++i )
            {
                TargetShooting[i].BulletEvent += OnBulletMoving;
                TargetShooting[i].BulletEventWhenBulletCreate += OnBulletCreated;
                TargetShooting[i].BulletEventWhenBulletDestroy += OnBulletDestroy;
				TargetShooting[i].BulletEventWhenBulletRestoreMainLevel += OnBulletRecoverMainLevel;
                TargetShooting[i].BulletEventDelay += OnBulletMovingDelay;
                TargetShooting[i].BulletEventWhenBulletCreateDelay += OnBulletCreatedDelay;
                TargetShooting[i].BulletEventWhenBulletDestroyDelay += OnBulletDestroyDelay;
                TargetShooting[i].BulletEventWhenBulletRestoreMainLevelDelay += OnBulletRecoverMainLevelDelay;
                TargetShooting[i].BulletEventOnDestroyingPlayerDelay += OnBulletDestroyPlayerDelay;
                TargetShooting[i].BulletEventOnDestroyingPlayer += OnBulletDestroyPlayer;

            }
        }
    }
    /// <summary>
    /// 这是当子弹移动时使用的事情（一帧使用一次），它需要覆写，否则无效。
    /// （OnBulletMoving Function.
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target"></param>
    public virtual void OnBulletMoving (Bullet Target)
    { 
    
    }
    /// <summary>
    /// 这是当子弹被設置為活躍狀態时使用的事情它需要覆写，否则无效。
    /// （OnBulletMoving Function.
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target"></param>
    public virtual void OnBulletActive ()
    { 

    }
    /// <summary>
    /// 这是当子弹创建时使用的事情，它需要覆写，否则无效。
    /// （OnBulletCreated Function. 
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target"></param>
    public virtual void OnBulletCreated (Bullet Target)
    {

    }
    /// <summary>
    /// 这是当子弹销毁时使用的事情，它需要覆写，否则无效。
    /// （OnBulletCreated Function. 
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target"></param>
    public virtual void OnBulletDestroy (Bullet Target)
    {

    }
	/// <summary>
	/// 这是当子弹恢复主层级时使用的事情，它需要覆写，否则无效。
	/// （OnBulletCreated Function. 
	/// Override required.Or it is invaild.）
	/// </summary>
	/// <param name="Target"></param>
	public virtual void OnBulletRecoverMainLevel(Bullet Target,Bullet ParentBullet)
	{

	}
    /// <summary>
    /// 玩家被子弹击中的场合使用的事件，不允许可以使用延迟函数。
    /// </summary>
    /// <param name="Target">Target.</param>
    public virtual void OnBulletDestroyPlayer(Bullet Target)
    {
        
    }
    public virtual IEnumerator OnBulletMovingDelay(Bullet Target)
    {
        yield return null;
    }
    /// <summary>
    /// 这是当子弹创建时使用的事情，它需要覆写，否则无效。（这个函数是线程函数，允许在此处进行延迟）
    /// （OnBulletCreated Function. 
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target"></param>
    public virtual IEnumerator OnBulletCreatedDelay(Bullet Target)
    {
        yield return null;
    }
    /// <summary>
    /// 这是当子弹销毁时使用的事情，它需要覆写，否则无效。（这个函数是线程函数，允许在此处进行延迟）
    /// （OnBulletCreated Function. 
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target"></param>
    public virtual IEnumerator OnBulletDestroyDelay(Bullet Target)
    {
        yield return null;
    }
    /// <summary>
    /// 这是当子弹恢复主层级时使用的事情，它需要覆写，否则无效。（这个函数是线程函数，允许在此处进行延迟）
    /// （OnBulletCreated Function. 
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target"></param>
    public virtual IEnumerator OnBulletRecoverMainLevelDelay(Bullet Target)
    {
        yield return null;
    }
    /// <summary>
    /// 玩家被子弹击中的场合使用的事件，可以使用延迟函数。
    /// </summary>
    /// <param name="Target">Target.</param>
    public virtual IEnumerator OnBulletDestroyPlayerDelay(Bullet Target){
        yield return null;
    }
}
