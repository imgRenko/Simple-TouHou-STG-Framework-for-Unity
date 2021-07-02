using UnityEngine;
/// <summary>
/// 只能搭载在搭载有Animator的对象，否则会报空引用异常，如脚本名，它用于控制游戏暂停是否还继续播放Animator所控制的动画。
/// </summary>
 [AddComponentMenu("东方STG框架/框架核心/界面显示/同游戏暂停动画控制器设置器")]
public class PauseControl : MonoBehaviour {
    public Animator t;
   
    float a = 0;
	void Start () {
        if (t == null)
            t = gameObject.GetComponent<Animator> ();
        a = t.speed;
	}
	void Update () {
        if (Global.GamePause)
            t.speed = 0;
        else
            t.speed = a * Global.GlobalSpeed;
	}
}
