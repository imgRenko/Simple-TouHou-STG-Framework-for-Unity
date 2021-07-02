using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrialTimeRemaining : MonoBehaviour {
    public Text TitleText;
    public string title ="重新挑战";
    public Button TryButton;
	void Update () {
        TryButton.interactable = Global.TrialTime != 0;
        if (!TryButton.interactable)
        {
            TitleText.text = "已不可再续关";
            return;
        }
           TitleText.text = Global.TrialTime > 1 ? title + string.Format("\n(点数：{0})", Global.TrialTime) : "你还有最后一次机会";
	}
}
