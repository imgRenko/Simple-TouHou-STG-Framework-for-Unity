using UnityEngine;
using UnityEngine.UI;
public class GamePause : MonoBehaviour
{
	public GameObject MainMemu;
	public Animator Ani_control;
    public AudioSource Audio;
    public Button a;
	public void PauseGame ()
    {
        if (Global.SpellCardExpressing)
            return;
        Global.GamePause = true;
        Global.BGM.Pause ();
        //Time.timeScale = 0;
    }
	public void Continue(){
		Global.GamePause = false;
        Global.BGM.Play ();
       // Time.timeScale = 1;

    }
    public void ResetTrialTime (int count){
        Global.TrialTime = count;
    }
	void Update ()
	{
        if (Input.GetButton("Pause") && Global.GamePause == false)
        {
            //a.Select ();
            if (Global.SpellCardExpressing || Global.dialoging || Global.GamePause || Global.isGameover)
                return;
          
            MainMemu.SetActive(true);
            a.Select ();
            Audio.Play();
           
            MainMemu.GetComponent<Animator>().PlayInFixedTime("GamePause");
            if (Global.GamePause == true )
                MainMemu.GetComponent<Animator>().PlayInFixedTime("GamePause_Back");
            
        }
    }
}
