using System.Collections;
using UnityEngine;
/// <summary>
///  这个类与BulletEvent类不一样，这个类是专门给被作为Enemy Movement的子弹使用的
/// 此外，该类不具有给多个被作为Movement的子弹使用的功能。
/// </summary>
[AddComponentMenu("东方STG框架/框架核心/事件基础/敌人运动子弹事件基类")]
public class BulletEventForSingle : MonoBehaviour
{
	
    [HideInInspector]
    public Bullet T;
    [Multiline]
    public string note="（于此处输入注解）";
    // Use this for initialization
    void Start ()
	{
	    T = this.gameObject.GetComponent<Bullet>();
		if (T == null)
			return;
	    T.BulletEvent += OnBulletMoving;
	    T.BulletEventDestroy += OnBulletDestroy;
        T.BulletEventDelay += OnBulletMovingDelay;
        T.BulletEventDestroyDelay += OnBulletDestroyDelay;
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
    /// 这是当子弹销毁时使用的事情，它需要覆写，否则无效。
    /// （OnBulletCreated Function. 
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target"></param>
    public virtual void OnBulletDestroy (Bullet Target)
    {

    }
    /// <summary>
    /// 这是当子弹移动时使用的事情（一帧使用一次），它需要覆写，否则无效。
    /// （OnBulletMoving Function.
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target"></param>
    public virtual IEnumerator OnBulletMovingDelay(Bullet Target)
    {
        yield return null;
    }
    /// <summary>
    /// 这是当子弹销毁时使用的事情，它需要覆写，否则无效。
    /// （OnBulletCreated Function. 
    /// Override required.Or it is invaild.）
    /// </summary>
    /// <param name="Target"></param>
    public virtual IEnumerator OnBulletDestroyDelay(Bullet Target)
    {
        yield return null;
    }
}
