using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerPicMask : MonoBehaviour {
    public Image PlayerPicture;
    public Text PlayerName;
    public Vector2 Offset;
    private Vector2 Default;
    private Animator anim;
    private Sprite tar;
    void Awake(){
        anim = GetComponent<Animator> ();
        Default = PlayerPicture.rectTransform.anchoredPosition;
    }
    public void SetTargetSprite (){
        PlayerPicture.sprite = tar;
    }
    public void ClearSprite (){
        PlayerPicture.sprite = null;
        PlayerName.text = string.Empty;

    }
    public void EnterIn(){
        Global.isBossPictureShowing = true;
    }
    public void Back(){
        Global.isBossPictureShowing = false;
    }
    /// <summary>
    /// 显示BOSS立绘用函数
    /// </summary>
    /// <param name="n"></param>
    /// <param name="l"></param>
    public void Set(string n,Sprite l){

        tar = l;
        PlayerName.text = n;
        PlayerPicture.rectTransform.anchoredPosition = Default + Offset;
        if (anim != null)
        anim.PlayInFixedTime ("ChangeSprite", 0, 0);
    }

}
