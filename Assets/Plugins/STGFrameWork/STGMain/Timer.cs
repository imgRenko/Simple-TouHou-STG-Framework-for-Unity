using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("东方STG框架/框架核心/界面显示/倒计时显示器")]
public class Timer : MonoBehaviour {
    public Text SecondText;
    public Text MinSecondText;
    static public float Second;
	static public float MinSecond;
	static public float MaxSecond;
	static public float MaxMinSecond;
    public bool Count = false;
	public int maxTime = 20;
    static public float MinTime = 0;
    public AudioSource TimeOut;
    public AudioSource TimeOuted;
    public bool TimeWillUp;
    const double secondDis = 16.66666666666667f;
    int TimeCount = 0;
    // Use this for initialization
    void Start () {
        MinTime = MinSecond * 10;
    }
    
    void TimeSound()
    {
        TimeCount = 0;
        if (Second <= 5)
        {
            gameObject.GetComponent<Animator>().Play("Timer_TimeUp",0,0);
            TimeOuted.Play();
        }
        if (Second <= 10 && Second > 5)
        {
			gameObject.GetComponent<Animator>().Play("Timer_TimeWillUp", 0, 0);
            TimeOut.Play();
        }
    }
	// Update is called once per frame
	void Update () {

		if (Count == false || Global.WrttienSystem || Global.GamePause == true)
            return;
        if (TimeWillUp)
            ++TimeCount;

        MinTime = (int)(MinTime - secondDis * Global.GlobalSpeed * Global.SpellCardTimeSpeed);
        MinSecond = MinTime / 10;
        if (MinTime <= 0)
        {
            MinTime = 1000 + MinTime;
            Second -= 1;
            if (Second == -1)
                TimeWillUp = false;
            if (TimeWillUp)
                TimeSound();
        }
        if (MinTime >= 1000)
        {
            MinTime -= 1000 ;
            Second += 1;
            if (Second == -1)
                TimeWillUp = false;
            if (TimeWillUp)
                TimeSound();
        }
        if (Second <= -1 && MinSecond <= 0)
        {
            TimeWillUp = false;
            TimeCount = 0;
            Second = -1;
            SecondText.text = "00";
            MinSecondText.text = ".00";
           MinSecond = 0;
            Count = !Count;
        }
        if (Second == 10)
            TimeWillUp = true;
        if ( MinSecond < 10)
            MinSecondText.text = ".0" + ((int)Mathf.Clamp(MinSecond, 0, 99)).ToString();
        else
            MinSecondText.text = "." + ((int)Mathf.Clamp(MinSecond, 0, 99)).ToString();
        if (Second < 10)
        {
            SecondText.text = "0" + Mathf.Clamp(Second, 0, 9).ToString();
        }
        else
            SecondText.text = Mathf.Clamp(Second,0,maxTime).ToString();
		if (maxTime != -1) {
			if (Second > maxTime) {
				SecondText.text = maxTime.ToString ();
				MinSecondText.text = ".99";
                if (maxTime < 10)
                {
                    SecondText.text = "0" + Mathf.Clamp(maxTime, 0, 9).ToString();
                }
                return;
			}
		}
    }
}
