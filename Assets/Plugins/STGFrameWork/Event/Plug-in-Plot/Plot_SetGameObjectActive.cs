using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot_SetGameObjectActive : DialogSystemEvent {

    public GameObject Target;
    public bool State = true;
    public Plot_SetGameObjectActive(){
        Note = "修改State的值允許你讓激活或者關閉某一個遊戲對象。";
    }
    public override void WhenPlotEnded ()
    {
        Target.SetActive (State);
    }
}
