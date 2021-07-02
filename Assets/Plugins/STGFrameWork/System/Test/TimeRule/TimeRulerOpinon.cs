using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeRulerOpinon : MonoBehaviour {
    public float Width = 400;
    public float LeftBorder = 0;
    public float RightBorder = 0;
    public Image TimeTarget;
    public Text RecentTime;
    public SpellCard BarrageCollection;
    public GameObject ShootingInfoDisplay;
    public List<GameObject> ShootingInfoDisplayList = new List<GameObject>();
    public bool Updated = false;
    void Update (){
        Vector2 p = TimeTarget.rectTransform.position;
        if (Global.SpellCardNow != null) {
            p.x = Mathf.Lerp(LeftBorder,RightBorder,Global.SpellCardNow.ResetTotalFrame / Global.SpellCardNow.ResetTime);
            TimeTarget.rectTransform.position = p;
            RecentTime.text = Global.SpellCardNow.ResetTotalFrame.ToString ();
        }
        if (Updated == false){
            int a = Global.SpellCardNow.Credentials.DataList.Count;
            Vector2 l = ShootingInfoDisplay.GetComponent<RectTransform>().position;
            for (int i = 0; i != a;++i){
                GameObject T = Instantiate (ShootingInfoDisplay);
                T.transform.SetParent (ShootingInfoDisplay.transform.parent.transform);
                RectTransform re = T.GetComponent<RectTransform> ();
                re.sizeDelta.Set (653.4f * Global.SpellCardNow.Credentials.DataList [i].Ref.MaxLiveFrame / Global.SpellCardNow.ResetTime, 17.7f);
                Vector2 lt = l;
                lt.x =  Mathf.Lerp(LeftBorder+ re.sizeDelta.x/2,RightBorder+re.sizeDelta.x/2,Global.SpellCardNow.Credentials.DataList [i].Ref.Delay / Global.SpellCardNow.ResetTime) ;
                re.position = lt;
                T.SetActive (true);
                ShootingInfoDisplayList.Add (T);
                Updated = true;
            }

        }
    }
}
