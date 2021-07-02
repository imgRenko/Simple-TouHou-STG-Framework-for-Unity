using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("东方STG框架/弹幕设计/剧情系统/新剧情系统(漫画式)/剧情文本管理系统")]
public class DialosSystemTextManager : MonoBehaviour {
    public static Dictionary<int, DialogSystemCharacterImage> CharImgCollection = new Dictionary<int, DialogSystemCharacterImage>();
    public static Dictionary<int, DialogSystemTextMessage> TextCollection = new Dictionary<int, DialogSystemTextMessage>();
    public static Dictionary<int, DialogLive2DController> Live2DModels = new Dictionary<int, DialogLive2DController>();
    public static bool typing = false;
    public static DialogSystemInit Now;
    public void Skip(){
        Now.EndPlotSystem ();
    }
    public void Reload()
    {
        Now.Reload();
    }
}
