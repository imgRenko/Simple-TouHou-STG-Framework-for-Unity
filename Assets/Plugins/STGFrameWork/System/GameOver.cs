using UnityEngine;
using System.Collections;
public class GameOver : MonoBehaviour
{
    public AudioSource GameOverBGM;
	public Animator GameOverGUI;
    public GameObject GameOverPanel;
	public delegate IEnumerator GameOverEvent ();
	public event GameOverEvent EnableEvent,DisableEvent;
    public void EnableGameOver() {
        Global.isGameover = true;
        Global.GamePause = true;
        GameOverPanel.SetActive (true);
        GameOverBGM.Play();
        Global.BGM.Pause ();
        WhenEnable();
    }
    public void DisableGameOver() {
        Global.isGameover = false;
        Global.GamePause = false;
        GameOverBGM.Stop();
        Global.BGM.Play ();
        WhenDisable();
    }
    public void ResetSpellCard(){
        
       
        Global.Score = 0;
        Global.TrialTime--;
        Global.MaxBounsScore = 0;
        Global.graze = 0;
        
        //Global.SpellFailed = true;
        Global.PlayerLive_A = 2;
        Global.Power = 0.1f;
        Bouns.SetBouns (1, Vector2.zero, Bouns.BounsType.FullPower);
         DisableGameOver ();
        if (Global.SpellCardNow != null)
            Global.SpellCardNow.ReuseSpellCard (true);
    }
    public void WhenEnable()
    {
        if (EnableEvent != null)
		StartCoroutine (EnableEvent ());
    }
	public void WhenDisable()
    {
        if (DisableEvent != null)
		StartCoroutine (DisableEvent ());
    }

}


