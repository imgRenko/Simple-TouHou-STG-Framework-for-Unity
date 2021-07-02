using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddedScore : MonoBehaviour {
    public Text ScoreDisplayer;
    public void ClearTempScore(){
        Global.Score += Global.AddedScore / 10;
        Global.AddedStore = 0;
    }
    void Update(){
        if (Global.AddedStore != 0)
            ScoreDisplayer.text = "+ " + (Global.AddedStore * 10).ToString ();
    }
}
