using UnityEngine;
public class CharacterEvent : MonoBehaviour {
	[HideInInspector]
	public PlayerController Controller;
	[Multiline]
	public string Note="（于此处输入注解）";
	void Start () {
		AddEvent();
	}
	public void AddEvent ()
	{

		Controller = GetComponent<PlayerController> ();
		EventStart (Controller);
		Controller.OnCharacterMoving += OnCharacterMoving;
		Controller.OnCharacterShooting += OnCharacterShooting;
		Controller.OnCharacterSlowDown += OnCharacterSlowDown;
		Controller.OnCharacterStoping += OnCharacterStoping;
		Controller.OnCharacterUpdating += OnCharacterUpdating;
        Controller.OnCharacterGrazed += OnCharacterGrazes;
	}
	/// <summary>
	/// 当玩家进行移动的时候所使用的事件（一帧使用一次），它需要覆写，否则无效。
	/// （OnCharacterMoving Function.
	/// Override required.Or it is invaild.）
	/// </summary>
	/// <param name="Target"></param>
	public virtual void OnCharacterMoving (Character Target)
	{ 

	}
	/// <summary>
	/// 玩家进行发射子弹时使用的事件，它需要覆写，否则无效。
	/// （OnCharacterShooting Function. 
	/// Override required.Or it is invaild.）
	/// </summary>
	/// <param name="Target"></param>
	public virtual void OnCharacterShooting (Character Target)
	{

	}
	/// <summary>
	/// 玩家减速时使用的事件，它需要覆写，否则无效。
	/// （OnCharacterSlowDown Function. 
	/// Override required.Or it is invaild.）
	/// </summary>
	/// <param name="Target"></param>
	public virtual void OnCharacterSlowDown (Character Target)
	{

	}
	/// <summary>
	/// 玩家停止时的事件，它需要覆写，否则无效。
	/// （OnCharacterStoping Function. 
	/// Override required.Or it is invaild.）
	/// </summary>
	/// <param name="Target"></param>
	public virtual void OnCharacterStoping (Character Target)
	{

	}
	/// <summary>
	/// 玩家类更新所使用的事件，它需要覆写，否则无效。
	/// （OnCharacterUpdating Function. 
	/// Override required.Or it is invaild.）
	/// </summary>
	/// <param name="Target"></param>
	public virtual void OnCharacterUpdating (Character Target)
	{

	}
    /// <summary>
    /// 玩家擦弹所使用的事件，它需要覆写，否则无效。
    /// （OnCharacterGrazes Function. 
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target">Target.</param>
    public virtual void OnCharacterGrazes(Character Target)
    {

    }
    /// <summary>
    /// Events the start.
    /// </summary>
    /// <param name="Target">Target.</param>
	public virtual void EventStart (PlayerController Target)
	{

	}
}
