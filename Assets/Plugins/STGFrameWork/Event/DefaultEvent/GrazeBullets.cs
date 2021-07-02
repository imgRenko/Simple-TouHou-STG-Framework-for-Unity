using UnityEngine;
public class GrazeBullets : CharacterEvent
{
    public override void OnCharacterGrazes (Character Target)
    {
        base.OnCharacterGrazes (Target);
        ++Global.graze;
        Global.AddPlayerScore (2000);
        //Global.AddScore.text = "+" + 2000.ToString ();
       // Global.AddScore.GetComponent<Animator> ().PlayInFixedTime ("ScoreAddScore",0,0);
        Global.MaxBounsScore ++;
        Target.PlaySound(2,0);
    }
}

