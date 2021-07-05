using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class LaserShootingEvent : SerializedMonoBehaviour
{
    [FoldoutGroup("发射器事件初始化", expanded: false)]
    [LabelText("发射器事件目的对象")]
    public LaserShooting[] TargetShooting;
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
    void Start()
    {
        if (autoGet)
            TargetShooting = GetComponents<LaserShooting>();
        EventStart(TargetShooting);
        if (ForOnlyOne)
        {
            TargetShooting[0].AfterLaserShootingFinishedShooting += AfterLaserShootingFinishedShooting;
            TargetShooting[0].LaserShootingUsing += OnLaserShootingUsing;
            TargetShooting[0].LaserShootingFinishShotTask += OnLaserShootingFinishAllShotTasks;
            TargetShooting[0].LaserShootingBeforeShooting += BeforeLaserShootingShot;
        }
        else
        {
            for (int i = 0; i != TargetShooting.Length; ++i)
            {
                TargetShooting[i].AfterLaserShootingFinishedShooting += AfterLaserShootingFinishedShooting;
                TargetShooting[i].LaserShootingUsing += OnLaserShootingUsing;
                TargetShooting[i].LaserShootingFinishShotTask += OnLaserShootingFinishAllShotTasks;
                TargetShooting[i].LaserShootingBeforeShooting += BeforeLaserShootingShot;
            }
        }
    }
    /// <summary>
    /// 这是当发射器正在使用时使用的事情，它需要覆写，否则无效。
    /// （OnShootingDestroy Function.
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target"></param>
    /// 
    public virtual void OnLaserShootingUsing(LaserShooting Target) { }
    /// <summary>
    /// Raises the shooting finish all shot tasks event.
    /// 在发射器完成所有的发射操作后，才会执行这一个事件。
    /// （例如Way为60，則发射60次，发射完60次后执行一次。）
    /// </summary>
    /// <param name="Target">Target.</param>
	public virtual void OnLaserShootingFinishAllShotTasks(LaserShooting Target) { }
    /// <summary>
    /// 当发射器开始使用的时候，才会使用该事件。
    /// </summary>
    /// <param name="Target">Target.</param>
	public virtual void EventStart(LaserShooting[] Target) { }
    /// <summary>
    /// 在发射器發射一次前执行的事件。（例如Way为60，則发射60次，每次发射前后执行一次。）
    /// </summary>
    /// <param name="Target">Target.</param>
    public virtual void AfterLaserShootingFinishedShooting(LaserShooting Target,LaserMovement laserMovement) { }
    /// <summary>
    /// 发射器发射前使用一次这个事件
    /// </summary>
    /// <param name="Target">Target.</param>
    public virtual void BeforeLaserShootingShot(LaserShooting Target) { }
}

