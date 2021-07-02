using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("东方STG框架/弹幕设计/实用工具/对象筛选器(以游戏难度筛选)")]
public class RankSelect : MonoBehaviour {
    public List<GameObject> Easy = new List<GameObject>();
    public bool ActiveEasy = true;
    public List<GameObject> Normal = new List<GameObject>();
    public bool ActiveNormal = true;
    public List<GameObject> Lunatic = new List<GameObject>();
    public bool ActiveLunatic = true;
    void Start () {
        if (Global.GameRank == Global.Rank.EASY)
            foreach (GameObject a in Easy) {
                a.SetActive (ActiveEasy);
            }
        if (Global.GameRank == Global.Rank.NORMAL)
            foreach (GameObject a in Normal) {
                a.SetActive (ActiveNormal);
            }
        if (Global.GameRank == Global.Rank.LUNATIC)
            foreach (GameObject a in Lunatic) {
                a.SetActive (ActiveLunatic);
            }

    }
}
