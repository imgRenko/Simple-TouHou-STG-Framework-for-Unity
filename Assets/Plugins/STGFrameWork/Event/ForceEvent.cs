using UnityEngine;
[AddComponentMenu("东方STG框架/框架核心/事件基础/力场事件基类")]
public class ForceEvent : MonoBehaviour {
    [HideInInspector]
    public Force[] ForceList;
    [Multiline]
    public string Note = "（于此处输入注解）";
    public bool ForOnlyOne = false;
    void Awake ()
    {
        ForceList = GetComponents<Force> ();
		EventStart (ForceList);
        if ( ForOnlyOne )
        {
            ForceList[0].OnUsing += OnForceUsing;
            ForceList[0].OnDestroy += OnForceDestroy;
			ForceList [0].OnUsage += OnForceStarting;
        }
        else
        {
            for ( int i = 0; i != ForceList.Length; ++i ) {
                ForceList[i].OnUsing += OnForceUsing;
                ForceList[i].OnDestroy += OnForceDestroy;
				ForceList [i].OnUsage += OnForceStarting;
            }
        }
    }

    public virtual void OnForceDestroy(Force F)
    {
    }
    public virtual void OnForceUsing (Force F)
    {
    }
	public virtual void OnForceStarting(Force F)
	{
	}
	public virtual void EventStart (Force[] E)
	{

	}
}
