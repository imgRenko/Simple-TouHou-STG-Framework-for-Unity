using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Obsolete]
public class AlphaControl : MonoBehaviour
{
    public List<Text> Texts = new List<Text>();
    public List<Image> Images = new List<Image>();
    private float alpha = 255;
	void Update () {
	    if (Global.MainCamera.WorldToScreenPoint(Global.PlayerObject.transform.position).y > 600)
	    {
	        for (var i = 0; i < Texts.Count; i++)
	        {
	            alpha = Mathf.Lerp(alpha, 128, 0.03f);

                Texts[i].color = new Color(255,255,255, alpha);
	        }

        }
	    else if (Global.MainCamera.WorldToScreenPoint(Global.PlayerObject.transform.position).y <= 200)
	    {

	        for (var i = 0; i < Images.Count; i++)
	        {
	            alpha = Mathf.Lerp (alpha, 128, 0.03f);
                Images[i].color = new Color(Images[i].color.r, Images[i].color.g, Images[i].color.b,
                    alpha);
	        }
	    }
	    else
	    {
	        for ( var i = 0; i < Texts.Count; i++ )
	        {
	            alpha = Mathf.Lerp (alpha, 255, 0.03f);

                Texts[i].color = new Color (255, 255, 255, alpha);
	        }
	        for ( var i = 0; i < Images.Count; i++ )
	        {
	            alpha = Mathf.Lerp (alpha, 255, 0.03f);

                Images[i].color = new Color (Images[i].color.r, Images[i].color.g, Images[i].color.b,
                    alpha);
	        }
        }
	}
}
