using UnityEngine;
[AddComponentMenu("东方STG框架/框架核心/事件基础/敌人事件基类")]
public class EnemyEvent : MonoBehaviour {
    [HideInInspector]
    public Enemy T;
    [Multiline]
    public string Note = "于此处输入对该脚本的注释。";
    // Use this for initialization
    void Start () {

        T = GetComponent<Enemy> ();
		EventStart (T);
        T.WhenEnemyMoving += OnMoving;
        T.WhenEnemyStopMoving += OnStopMoving;
		T.WhenEnemyDamaged += OnDamaged;
		T.WhenEnemyKilled += OnKilled;
		T.WhenEnemyStarts += OnStarts;
		T.WhenEnemyStartsBarrage += OnStartsBarrage;
    }
    /// <summary>
    /// 敌人开始移动的事件，需要覆写，否则无效。
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="Spell"></param>
    /// <param name="Character"></param>
    public virtual void OnMoving (Enemy E)
    {

    }
    /// <summary>
    /// 敌人停止移动的事件，需要覆写，否则无效。
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="Spell"></param>
    /// <param name="Character"></param>
    public virtual void OnStopMoving (Enemy E)
    {

    }
	public virtual void OnKilled (Enemy E)
	{

	}
	public virtual void OnDamaged (Enemy E)
	{

	}
	public virtual void OnStarts (Enemy E)
	{

	}
	public virtual void OnStartsBarrage (Enemy E)
	{

	}
	public virtual void EventStart (Enemy E)
	{

	}
}
