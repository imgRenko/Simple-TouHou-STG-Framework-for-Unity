
using UnityEngine;
using UnityEngine.UI;
public class PlayerScore : MonoBehaviour {
    public Text Playerscore,PlayerScoreMicro;
    public Text Graze;
    public Text Power;
    public Text Bouns;
    public Text MissCount;
    public Text MaxScore;
    public Text LifeCount, SpellCardCount;
    public Image PlayerLifeBased;
    public long Score;
    public Animator ScoreAnimator;
    private float _Blend = 0;
    public AudioClip cilp;
    public bool Once;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Global.GamePause || Global.GlobalSpeed == 0)
            return;
        Bouns.text = (Global.MaxBounsScore * 10 ).ToString();
        Power.text = string.Format("{0:F}",Global.Power) +"/" + Global.maxPower_A.ToString("F");
        if (Score > Global.MaxScore && Once == false) {
            Global.Tip.AS.clip = cilp;
            Once = true;
            Global.Tip.SetTitle (cilp, "The Highest Score Record Updated!");
        }
        if (Score < Global.MaxScore) {
            MaxScore.text = ((long)Global.MaxScore * 10).ToString ();
            Once = false;
        } else {
            MaxScore.text = ((long)Score * 10).ToString ();
            Global.MaxScore = Score;
        }
        LifeCount.text = Global.PlayerLive_A.ToString();
        SpellCardCount.text = Global.SpellCard.ToString();

        if (Global.PlayerObject.transform.position.y < -2f &&Global.PlayerObject.transform.position.x < -1f && Global.isBossPictureShowing) {
                _Blend = Mathf.Lerp (_Blend, 1, 0.2f);
                ScoreAnimator.SetFloat ("Alpha", _Blend);
            } else {
                _Blend = Mathf.Lerp (_Blend, 0, 0.2f);
                ScoreAnimator.SetFloat ("Alpha", _Blend);
            }

        Score += (Global.Score-(Score) + 4) /5;
        if (Score > Global.Score)
            Score = Global.Score;


        PlayerScoreMicro.text =((long)Score* 10).ToString();
        Graze.text =  Global.graze .ToString ();
        MissCount.text = Global.MissCount.ToString ();

        Playerscore.text =((long)Score* 10).ToString();


        int _length = Playerscore.text.Length - 1;
 
        if (_length < 0)
            return;
        int _blackCount = 10 - _length;
        string a = string.Empty;
        if (_blackCount < 0)
        {
            Playerscore.text = Playerscore.text;
            return;
        }
        for (int i = 0; i != _blackCount; ++i) {
            a += "0";
        }
        string _final = "<color=black>" + a +"</color>" +  Playerscore.text;
        Playerscore.text = _final;
    }
}
