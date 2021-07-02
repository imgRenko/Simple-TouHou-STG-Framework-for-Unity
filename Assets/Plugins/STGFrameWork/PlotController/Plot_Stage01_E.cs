using UnityEngine;
using System.Collections;
public class Plot_Stage01_E : MonoBehaviour
{
	public WrittenControl P;
    public StageClear Sc;
    public GameObject GameFinished;

	public string ce;
	bool used = false;
	void Update ()
	{
	
			if (P.StepNow == -1 && P.PlayerAppear == false) {
				P.MakePlayerAppear ();
			}
			if (P.StepNow == 1 && P.PlayerAppear == false) {
				P.MakeOppositeAppear ();
			}

        if (P.StepNow >= P.MaxStep - 1 && !used) {
				Global.WrttienSystem = false;
                used = true;
                Global.ShowCharCard ();
                StartCoroutine(Delay());
              

			}
		
	}
    IEnumerator Delay() {
        yield return new WaitForSeconds(3);
        Sc.gameObject.SetActive(true);
        Global.AddPlayerScore(200000000);
        //Global.AddScore.text = "+" +( Score).ToString ();
        //Global.AddScore.GetComponent<Animator> ().PlayInFixedTime ("ScoreAddScore",0,0);
        Sc.ani_statecontrol.Play("StageClear");
        Sc.ClearText.text = (200000000).ToString();
        yield return new WaitForSeconds(4);
        GameFinished.SetActive(true);
        Destroy(this);
    }
}

