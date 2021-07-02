using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("东方STG框架/框架核心/界面显示/玩家残机符卡数量显示器")]
public class LifePointer : MonoBehaviour
{
    public List<Image> LifeImage = new List<Image>();
    public Text PriceText;
    public bool displaySpellCard = false;
    public Color DarkColor = new Color(68, 68, 68, 255);
    public Color NormalColor = new Color(68, 68, 68, 255);
    private int nowLive, nowSpell;
    void Update()
    {
        //借助一个临时变量，如果发生玩家残机数、符卡数改变就执行一次。
        if (nowLive == Global.PlayerLive_A && nowSpell == Global.SpellCard)
            return;

        nowLive = Global.PlayerLive_A;
        nowSpell = Global.SpellCard;

        int a = 0;
        int b = 0;

        if (displaySpellCard)
        {
            a = Global.SpellCard;
            b = Global.SpellCardPrice;
        }
        else
        {
            a = Global.PlayerLive_A;
            b = Global.LivePrice;
        }
        if (a < 8)
            PriceText.text = "Price " + b.ToString() + "/" + Global.MaxLivePrice.ToString();
        else
            PriceText.text = " - - ";

        for (var i = 0; i < 8; ++i)
            LifeImage[i].color = DarkColor;

        if (a > 0)
        {
            for (var i = 0; i < Mathf.Clamp(a, 0, 8); ++i)
                LifeImage[i].color = NormalColor;
        }
    }
}

