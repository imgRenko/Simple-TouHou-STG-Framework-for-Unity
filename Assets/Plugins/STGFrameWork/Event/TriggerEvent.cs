using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
/// <summary>
/// Trigger event.当子弹进入触发器的时候，触发其中的事件，虽然有一点比较奇葩，但还是需要提一下，在所有事件中，若子弹引用为NULL，则指的是玩家进入了该触发器，不过不需担心，
/// 在目前脚本内所有的定义中仅有子弹类要使用触发器的事件，其他的均没有使用，因此脚本开发中，要尽量在触发器事件中避免NULL情况的发生
/// </summary>
public class TriggerEvent : SerializedMonoBehaviour
{
    [HideInInspector]
    public Trigger TargetTrigger;
    [FoldoutGroup("触发器事件初始化", expanded: false)]
    [LabelText("注释")]
    [Multiline]
    public string Note = "（于此处输入注解）";
    void Start ()
    {
        Debug.Log("[Info] Trigger Event Added");
        TargetTrigger = GetComponent<Trigger> ();
		EventStart (TargetTrigger);
		TargetTrigger.TriggerUsing += OnTriggerUsing;
		TargetTrigger.TriggerStart += OnTriggerStart;
		TargetTrigger.TriggerDestroy += OnTriggerDestroy;
        TargetTrigger.TriggerEventWhenStay += OnBulletStayInTrigger;
		TargetTrigger.TriggerEventWhenEnter += OnBulletEnterIntoTrigger;
		TargetTrigger.TriggerEventWhenExit += OnBulletExitFromTrigger;
        TargetTrigger.TriggerUsingDelay += OnTriggerUsingDelay;
        TargetTrigger.TriggerStartDelay += OnTriggerStartDelay;
        TargetTrigger.TriggerDestroyDelay += OnTriggerDestroyDelay;
        TargetTrigger.TriggerBulletsEventWhenStay += OnExtraBulletStayInTrigger;
        TargetTrigger.TriggerBulletsEventWhenExit += OnExtraBulletExitFromTrigger;
        TargetTrigger.TriggerBulletsEventWhenEnter += OnExtraBulletEnterIntoTrigger;
        TargetTrigger.TriggerEventWhenStayDelay += OnBulletStayInTriggerDelay;
        TargetTrigger.TriggerEventWhenEnterDelay += OnBulletEnterIntoTriggerDelay;
        TargetTrigger.TriggerEventWhenExitDelay += OnBulletExitFromTriggerDelay;
    }
    public virtual void OnBulletStayInTrigger (Bullet Which,Trigger Target,float StayTime, int enterTimes)
    {

    }
	public virtual void OnBulletExitFromTrigger(Bullet Which,Trigger Target, float StayTime, int enterTimes)
	{

	}
	public virtual void OnBulletEnterIntoTrigger(Bullet Which,Trigger Target, float StayTime, int enterTimes)
	{

	}
    public virtual void OnExtraBulletStayInTrigger(Bullet bullet, Bullet bullet2,Trigger Target)
    {

    }
    public virtual void OnExtraBulletExitFromTrigger(Bullet bullet, Bullet bullet2, Trigger Target)
    {

    }
    public virtual void OnExtraBulletEnterIntoTrigger(Bullet bullet, Bullet bullet2, Trigger Target)
    {

    }
    public virtual void OnTriggerUsing (Trigger Target)
	{

	}
	public virtual void OnTriggerDestroy (Trigger Target)
	{

	}
	public virtual void OnTriggerStart (Trigger Target)
	{

	}
	public virtual void EventStart( Trigger Target){}
    public virtual IEnumerator OnBulletStayInTriggerDelay(Bullet Which, Trigger Target, float StayTime,int enterTimes)
    {
        yield return null;
    }
    public virtual IEnumerator OnBulletExitFromTriggerDelay(Bullet Which, Trigger Target)
    {
        yield return null;
    }
    public virtual IEnumerator OnBulletEnterIntoTriggerDelay(Bullet Which, Trigger Target)
    {
        yield return null;
    }
    public virtual IEnumerator OnTriggerUsingDelay(Trigger Target)
    {
        yield return null;
    }
    public virtual IEnumerator OnTriggerDestroyDelay(Trigger Target)
    {
        yield return null;
    }
    public virtual IEnumerator OnTriggerStartDelay(Trigger Target)
    {
        yield return null;
    }
    public virtual IEnumerator EventStartDelay(Trigger Target) { 
        yield return null; 
    }
}
