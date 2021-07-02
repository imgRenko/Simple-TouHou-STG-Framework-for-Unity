using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RankSpriteDisplayer : MonoBehaviour {
    public List<Sprite> RankSprite = new List<Sprite>();
    public Image Display;
	void Update () {
        Display.sprite = RankSprite [(int)Global.GameRank];
	}
}
