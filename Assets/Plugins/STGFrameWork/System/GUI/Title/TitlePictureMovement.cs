using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitlePictureMovement : MonoBehaviour {
    public Image titlePic;
    public Vector2 maxRange = Vector2.one;
    public Vector2 minRange = -Vector2.one;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        titlePic.rectTransform.anchoredPosition =Vector2.Lerp(titlePic.rectTransform.anchoredPosition, new Vector2(Mathf.Lerp(minRange.x, maxRange.x, Input.mousePosition.x / Screen.width), Mathf.Lerp(minRange.y, maxRange.y, Input.mousePosition.y / Screen.height)),0.1f);
    }
}
