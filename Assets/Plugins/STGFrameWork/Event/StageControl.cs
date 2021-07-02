using System.Collections;
using UnityEngine;
[AddComponentMenu("东方STG框架/框架核心/事件基础/关卡进程类基类")]
public class StageControl : MonoBehaviour
{
    [Multiline]
    public string Note = "这是控制关卡流程的脚本。";
    public GameObject[] UsableShooting;
    public bool ControlStage = false;
    public string stageName, stageDescription;
    public Sprite stageSprite;

	/// <summary>
	///  场景控制，该函数需要复写，否则该函数是没有任何作用的。
	/// </summary>
	/// <returns>IE协同程序</returns>
    public virtual  IEnumerator SceneControl()
    {
         yield return null;
    }
	/// <summary>
	///  在完成了该关卡场景的任何设置之后，使用该函数开始进行对关卡流程的控制
	/// </summary>
    public void StartControl()
    {
        StartCoroutine(SceneControl());
		StartCoroutine (WhenStartControlingStage ());
    }
    void Start(){
        if (ControlStage)
            StartControl();
    }
	/// <summary>
	/// 利用该函数来抖动相机，若Global类没有ShakeCamera类的引用，该函数不能运行或者对于其他相机不起作用。
	/// </summary>
	/// <param name="shake">Shake.</param>
	/// <param name="shakeAmount">Shake amount.</param>
	/// <param name="decreaseFactor">Decrease factor.</param>
    public void ShakeCamera(float shake = 0,float shakeAmount = 0.7f,float decreaseFactor = 1)
    {
        CameraShake.shake = shake;
        CameraShake.shakeAmount = shakeAmount;
        CameraShake.decreaseFactor = decreaseFactor;
    }
        
	/// <summary>
	/// 当使用了StartControl 函数后使用。（StartControl 函数的描述： 在完成了该关卡场景的任何设置之后
	/// ，使用该函数开始进行对关卡流程的控制），该函数是一个协同程序，并且需要复写，否则该函数是
	/// 无效的。
	/// </summary>
	/// <returns>start controling stage.</returns>
    public virtual IEnumerator WhenStartControlingStage ()
    {
        yield return null;
    }
	/// <summary>
	/// 当使用了StageClear函数后使用。（WhenFinishingStage 函数的描述：
	/// 当玩家已经通过该关卡的时候使用的函数，该函数是一个协同程序，并且需要复写，否则该函数是
	/// 无效的。）
	/// </summary>
	/// <returns>Finish stage.</returns>
	public virtual IEnumerator WhenFinishingStage ()
	{
		yield return null;
	}
	/// <summary>
	///  利用该函数来完成关卡流程，进入下一个关卡，或进入下一个场景。（这是第一个重载）
	/// Score参数来控制完成关卡后的奖分数量
	/// Next stage需求一个StageControl （继承StageControl的子类）由此来进入下一个关卡
	/// StartControlSceneNow如果为true在进入下一个关卡之后立即控制下一个关卡，否则不会，需要手动进行
	/// </summary>
	public void StageClear(int Score,StageControl NextStage,bool StartControlSceneNow = false)
    {
        Global.stageClear_A.gameObject.SetActive(true);
		Global.AddPlayerScore (Score);
		//Global.AddScore.text = "+" + (Score*10).ToString ();
		//Global.AddScore.GetComponent<Animator> ().PlayInFixedTime ("ScoreAddScore",0,0);
        Global.stageClear_A.ani_statecontrol.Play("StageClear");
        Global.stageClear_A.ClearText.text = (Score* 10).ToString ();
        Global.StageIndex++;
        NextStage.enabled = true;
        this.enabled = false;
        if ( StartControlSceneNow )
            NextStage.StartControl();
		
    }
	/// <summary>
	///  利用该函数来完成关卡流程，进入下一个关卡，或进入下一个场景。（这是第二个重载）
	/// Score参数来控制完成关卡后的奖分数量
	/// 该函数重载仅有一个参数，这意味着使用该函数后不会进入新的关卡，若要进入新的关卡使用另一个重载
	/// </summary>
    public void StageClear (long Score)
    {
        Global.stageClear_A.gameObject.SetActive (true);
		Global.AddPlayerScore (Score);
		//Global.AddScore.text = "+" +( Score).ToString ();
		//Global.AddScore.GetComponent<Animator> ().PlayInFixedTime ("ScoreAddScore",0,0);
        Global.stageClear_A.ani_statecontrol.Play ("StageClear");
        Global.stageClear_A.ClearText.text = ( Score  ).ToString ();
        Global.StageIndex++;
        this.enabled = false;
		StartCoroutine (WhenFinishingStage ());
    }
    static public void StageClearStatic(int Score)
    {
        Global.stageClear_A.gameObject.SetActive(true);
        Global.AddPlayerScore(Score);
        //Global.AddScore.text = "+" +( Score).ToString ();
        //Global.AddScore.GetComponent<Animator> ().PlayInFixedTime ("ScoreAddScore",0,0);
        Global.stageClear_A.ani_statecontrol.Play("StageClear");
        Global.stageClear_A.ClearText.text = (Score).ToString();
        Global.StageIndex++;
    }
}
