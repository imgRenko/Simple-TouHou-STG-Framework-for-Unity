using UnityEngine;
using System.Collections;
using System.IO;
[DisallowMultipleComponent]
public class Plot_Stage01_G : MonoBehaviour
{
    public WrittenControl P;
    public Enemy Boss;
    public Sprite _window_Sprite_help;
    public AudioSource BGM;
    //public GameObject orginalBGM;
    bool played = false;
    bool showed = false;
    IEnumerator WindowsDismiss()
    {
        Global.GamePause = false;
        Global.WindowDialog_A.Hide();
        Destroy(this);
        yield return null;
    }
    void Update()
    {
       
            if (P.StepNow == 1 && P.PlayerAppear == false)
            {
                P.MakePlayerAppear();
            }
            if (P.StepNow == 0 && P.OppoAppear == false)
            {
                P.MakeOppositeAppear();
            }
            if (P.StepNow == 25 && played == false)
            {
                played = true;
                BGM.enabled = true;
                BGM.Play();
           // orginalBGM.SetActive (false);
            }
        if (P.StepNow >= P.MaxStep - 1 && !showed)
            {
                Global.WrttienSystem = false;
                Global.ShowCharCard ();
            showed = true;
                Boss.UseBarrages();
                string a = Application.streamingAssetsPath + @"\merry.renko";
                if (File.Exists(a) == true)
                {
                    Global.PlayerObject.GetComponent<Character>().Invincible = true;
                    Global.GamePause = true;
                    Global.WindowDialog_A.Show(_window_Sprite_help, "你将会变强", "发现merry.renko在StreamAssets文件夹内，玩家的子弹判定将无效。", "Window_Show");
                    Global.WindowDialog_A.eventDriver[0].ButtonBlindEvent += WindowsDismiss;
                }
                else {
                    Destroy(this);
                }
            }


    }
}

