using UnityEngine;
using UnityEngine.SceneManagement;
[AddComponentMenu("东方STG框架/弹幕设计/实用工具/场景选择器(只用于动画事件)")]
public class ScenesChooser : MonoBehaviour {
    public int ScenesCount = 0;
    /// <summary>
    /// 需要使用读取界面才使用这个变量。
    /// </summary>
    static public int BeyondSceneCount = 0;
    public int BeyondSceneCount_Setting = 0;
    void Start(){
        BeyondSceneCount = BeyondSceneCount_Setting;
    }
	public void ToScenes () {
		SceneManager.LoadScene (ScenesCount);
	}

}
