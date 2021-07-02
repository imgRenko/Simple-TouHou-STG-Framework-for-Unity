using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]
public class CharacterInfo{
    public string Char,CharSubName,Destription;
    public string LocomotionSpd,Damage,Radius;
    public Sprite CharPic;
}
public class Title_ChangeCharcterSelection : MonoBehaviour {
    public Animator AnimController,BGController;
    public Image CharPic,SelectPoint;
    public Text Locomiton, Damage, Radius,Char,CharSub;
    public GameObject MainPanel;
    public List<Vector2> Pos = new List<Vector2> ();
    private int index = 0;
    private int PanelIndex = 0;
    private int RankSelectionPoint = 0;
    private bool isDestroying = false;
    private bool Playable = true;

    [SerializeField]
    public List<CharacterInfo> InfoList = new List<CharacterInfo> ();

    public void NextCharacter (){
        if (index >= InfoList.Count )
            index = 0;
        if (index < 0)
            index = InfoList.Count - 1;
        Locomiton.text = InfoList[index].LocomotionSpd;
        Damage.text = InfoList[index].Damage;
        Radius.text = InfoList[index].Radius;
        Char.text = InfoList[index].Char;
        CharSub.text = InfoList[index].CharSubName;
       // Destription.text = InfoList[index].Destription;
        CharPic.sprite = InfoList [index].CharPic;
    }
    void Start(){
        NextCharacter ();
    }
    public void DestroyPanel(){
        MainPanel.SetActive (true);
        gameObject.SetActive (false);
        isDestroying = false;
    }
    public void RankPanelIndex (){
        PanelIndex = 1;
    }
    public void ReturnCharacterSelectionPanel (){
        PanelIndex = 0;
    }
    public void RankPanel(){
        AnimController.PlayInFixedTime ("NextRank", 0, 0);
    }
    public void RankPanelReturn(){
        AnimController.PlayInFixedTime ("NextRankBack", 0, 0);
    }
    void Update(){
        AnimatorStateInfo info =AnimController.GetCurrentAnimatorStateInfo(0);    
        if (!Playable || info.normalizedTime < 1.0f)
            return;
        if (Input.GetButtonDown ("Submit") && PanelIndex == 1) {
            if (RankSelectionPoint == 0)
                Global.GameRank = Global.Rank.EASY;
            if (RankSelectionPoint == 1)
                Global.GameRank = Global.Rank.NORMAL;  
            if (RankSelectionPoint == 2)
                Global.GameRank = Global.Rank.LUNATIC;  
            if (index == 0)
                Global.CommandString = "Reimu";
            if (index == 1)
                Global.CommandString = "Marisa";
            if (index == 2)
                Global.CommandString = "Sanae";
            BGController.gameObject.SetActive (true);
            BGController.PlayInFixedTime ("EnterScene",0,0);
            Playable = false;
        }
        if (Input.GetButtonDown ("Submit") && PanelIndex == 0) {
            RankPanel ();
        }
        if (Input.GetButtonDown ("Cancel") && PanelIndex == 1) {
            RankPanelReturn ();
        }
        if (Input.GetButtonDown ("Cancel") && isDestroying == false && PanelIndex == 0) {
            AnimController.PlayInFixedTime ("CharacterSelectionBack", 0, 0);
            isDestroying = true;
        }
        SelectPoint.rectTransform.anchoredPosition = 
            Vector2.Lerp( SelectPoint.rectTransform.anchoredPosition,Pos [RankSelectionPoint],0.2f);
        if (!Input.GetButtonDown ("Horizontal"))
            return;
        if (PanelIndex == 0)
            AnimController.PlayInFixedTime ("NextCharacterSelection", 0, 0);
        if (Input.GetAxis ("Horizontal") > 0 && PanelIndex == 0) {
            index++;
        } else if (Input.GetAxis ("Horizontal") < 0 && PanelIndex == 0){
            index--;
        }
        if (Input.GetAxis ("Horizontal") > 0 && PanelIndex == 1) {
            RankSelectionPoint++;
            if (RankSelectionPoint > 2 )
                RankSelectionPoint = 0;

        } else if (Input.GetAxis ("Horizontal") < 0 && PanelIndex == 1){
            RankSelectionPoint--;
            if (RankSelectionPoint < 0)
                RankSelectionPoint = 2;
        }

    }
}
