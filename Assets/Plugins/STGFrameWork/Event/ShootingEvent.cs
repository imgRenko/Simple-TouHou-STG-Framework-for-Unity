using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;
[AddComponentMenu("东方STG框架/框架核心/事件基础/发射器事件基类")]
public class ShootingEvent: SerializedMonoBehaviour
{
    [FoldoutGroup("发射器事件初始化", expanded: false)]
    [LabelText("发射器事件目的对象")]
    public Shooting[] TargetShooting;
    [FoldoutGroup("发射器事件初始化", expanded: false)]
    [LabelText("自动取得目的对象")]
    public bool autoGet = true;
    [FoldoutGroup("发射器事件初始化", expanded: false)]
    [LabelText("注释")]
    [Multiline]
    public string Note = "（于此处输入注解）";
    [FoldoutGroup("发射器事件初始化", expanded: false)]
    [LabelText("仅要求一个目的对象")]
    public bool ForOnlyOne = false;
    void Start ()
    {
        if (autoGet)
            TargetShooting = GetComponents<Shooting> ();
		EventStart (TargetShooting);
        if (ForOnlyOne) {
            TargetShooting [0].ShootingEvent += AfterShootingFinishedShooting;
            TargetShooting [0].ShootingDestroy += OnShootingDestroy;
            TargetShooting [0].ShootingUsingEvent += OnShootingUsing;
            TargetShooting [0].ShootingFinishAllShotTask += OnShootingFinishAllShotTasks;
            TargetShooting [0].ShootingEventDelay += AfterShootingFinishShootingDelay;
            TargetShooting [0].ShootingDestroyDelay += OnShootingDestroyDelay;
            TargetShooting [0].ShootingUsingDelay += OnShootingUsingDelay;
            TargetShooting [0].ShootingFinishAllShotTaskDelay += OnShootingFinishAllShotTasksDelay;
            TargetShooting [0].WhenRollBack += OnShootingRollBackDelay;
            TargetShooting [0].BeforeShooting += BeforeShooting;
            TargetShooting [0].BeforeShootingDelay += BeforeShootingDelay;
            TargetShooting[0].StartNewLoop += StartNewLoop;
        }
        else
        {
            for (int i = 0; i != TargetShooting.Length; ++i) {
                TargetShooting [i].ShootingEvent += AfterShootingFinishedShooting;
                TargetShooting [i].ShootingDestroy += OnShootingDestroy;
                TargetShooting [i].ShootingUsingEvent += OnShootingUsing;
                TargetShooting [i].ShootingFinishAllShotTask += OnShootingFinishAllShotTasks;
                TargetShooting [i].ShootingEventDelay += AfterShootingFinishShootingDelay;
                TargetShooting [i].ShootingDestroyDelay += OnShootingDestroyDelay;
                TargetShooting [i].ShootingUsingDelay += OnShootingUsingDelay;
                TargetShooting [i].ShootingFinishAllShotTaskDelay += OnShootingFinishAllShotTasksDelay;
                TargetShooting [i].WhenRollBack += OnShootingRollBackDelay;
                TargetShooting [i].BeforeShooting += BeforeShooting;
                TargetShooting [i].BeforeShootingDelay += BeforeShootingDelay;
                TargetShooting[i].StartNewLoop += StartNewLoop;
            }
        }
    }
    /// <summary>
    /// 这是当发射器发射完成时使用的事情（一次发射使用一次,），它需要覆写，否则无效。（例如Way為60，則發射60次，每次發射執行一次。）
    /// （AfterShootingFinishShooting Function.
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target"></param>
    public virtual void AfterShootingFinishedShooting (Shooting Target, Bullet bullet)
    {

    }
    /// <summary>
    /// 这是当发射器销毁时(可生存时间为零的时候也会使用这个事件)使用的事情，它需要覆写，否则无效。
    /// （OnShootingDestroy Function.
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target"></param>
    /// 
    public virtual void OnShootingDestroy (Shooting Target) { }
    /// <summary>
    /// 这是当发射器正在使用时使用的事情，它需要覆写，否则无效。
    /// （OnShootingDestroy Function.
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target"></param>
    /// 
    public virtual void OnShootingUsing (Shooting Target) { }
    /// <summary>
    /// Raises the shooting finish all shot tasks event.
    /// 在发射器完成所有的发射操作后，才会执行这一个事件。
    /// （例如Way為60，則發射60次，發射完60次後執行一次。）
    /// </summary>
    /// <param name="Target">Target.</param>
	public virtual void OnShootingFinishAllShotTasks (Shooting Target) { }
    /// <summary>
    /// 当发射器开始使用的时候，才会使用该事件。
    /// </summary>
    /// <param name="Target">Target.</param>
	public virtual void EventStart (Shooting[] Target) { }
    /// <summary>
    /// 在发射器發射一次前执行的事件。（例如Way為60，則發射60次，每次發射前執行一次。）
    /// </summary>
    /// <param name="Target">Target.</param>
    public virtual void BeforeShooting(Shooting Target) {}
    /// <summary>
    /// 仅仅在发射器完成一次生命周期时使用。
    /// </summary>
    /// <param name="Target">Target.</param>
    public virtual void  StartNewLoop(Shooting Target) { }
    /// <summary>
    /// 这是当发射器发射完成时使用的事情（一次发射使用一次），它需要覆写，否则无效。（这个函数是线程函数，允许在此处进行延迟）
    /// （AfterShootingFinishShooting Function.
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target"></param>
    public virtual IEnumerator AfterShootingFinishShootingDelay(Shooting Target)
    {
        yield return null;
    }
    /// <summary>
    /// 这是当发射器销毁时(可生存时间为零的时候也会使用这个事件)使用的事情，它需要覆写，否则无效。（这个函数是线程函数，允许在此处进行延迟）
    /// （OnShootingDestroy Function.
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target"></param>
    /// 
    public virtual IEnumerator OnShootingDestroyDelay(Shooting Target) { yield return null; }
    /// <summary>
    /// 这是当发射器正在使用时使用的事情，它需要覆写，否则无效。（这个函数是线程函数，允许在此处进行延迟）
    /// （OnShootingDestroy Function.
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target"></param>
    /// 
    public virtual IEnumerator OnShootingUsingDelay(Shooting Target) { yield return null; }
    /// <summary>
    /// Raises the shooting finish all shot tasks event.
    /// 在发射器完成所有的发射操作后，才会执行这一个事件。（这个函数是线程函数，允许在此处进行延迟）
    /// （例如Way為60，則發射60次，發射完60次後執行一次。）
    /// </summary>
    /// <param name="Target">Target.</param>
	public virtual IEnumerator OnShootingFinishAllShotTasksDelay(Shooting Target) { yield return null; }
	public virtual IEnumerator OnShootingRollBackDelay(Shooting Target) { yield return null; }
    public virtual IEnumerator BeforeShootingDelay(Shooting Target) { yield return null; }

}
