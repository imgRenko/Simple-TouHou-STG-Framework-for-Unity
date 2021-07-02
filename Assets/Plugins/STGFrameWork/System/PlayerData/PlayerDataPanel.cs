using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerDataPanel : MonoBehaviour {
    static public PlayerDataPanel MainObject;
    public Text ScoreDisplayer;
    public RectTransform Base;
	// Use this for initialization
	void Start () {
        Global.GamePause = true;
        if (MainObject == null)
            MainObject = new PlayerDataPanel();
        MainObject.ScoreDisplayer = ScoreDisplayer;
        MainObject.Base = Base;
	}
    static public void EnablePlayerDataDisplayer(long Score){
        MainObject.gameObject.SetActive (true);
        MainObject.ScoreDisplayer.text = Score.ToString ();
       // GameObject t = Instantiate (Base.gameObject,Base.transform.parent,false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
