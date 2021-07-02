using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameTip : MonoBehaviour {
    public Text Title;
    public Gradient TitleGradient;
    public AudioSource AS;
    public void SetTitle(AudioClip SoundCilp,string display = ""){
        Title.gameObject.SetActive (false);
        Title.text = display;
        AS.clip = SoundCilp;
    
        Title.gameObject.SetActive (true);
        Play ();
    }
    public void setOff(){
        gameObject.SetActive (false);
    }
    public void Play(){
        AS.Play ();
    }
}
