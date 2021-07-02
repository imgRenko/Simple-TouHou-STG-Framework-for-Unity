using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("东方STG框架/弹幕设计/实用工具/对象筛选器(依据难度)")]
public class RankFilter : MonoBehaviour
{
    public List<GameObject> EasyRanklist;
    public List<GameObject> NormalRanklist;
    public List<GameObject> HardRanklist;
    public List<GameObject> LunaticRanklist;
     void Awake()
    {
        StartFilting();    
    }
    public void StartFilting()
    {
        if (Global.GameRank == Global.Rank.EASY)
        {
        for (int i = 0; i < EasyRanklist.Count; i++)
        {
            EasyRanklist[i].gameObject.SetActive(true);
        }
            for ( int i = 0; i < NormalRanklist.Count; i++ )
            {
                NormalRanklist[i].gameObject.SetActive (false);
            }
            for ( int i = 0; i < HardRanklist.Count; i++ )
            {
                HardRanklist[i].gameObject.SetActive (false);
            }
            for ( int i = 0; i < LunaticRanklist.Count; i++ )
            {
                LunaticRanklist[i].gameObject.SetActive (false);
            }
        }
        if ( Global.GameRank == Global.Rank.NORMAL )
        {
            for ( int i = 0; i < EasyRanklist.Count; i++ )
            {
                EasyRanklist[i].gameObject.SetActive (false);
            }
            for ( int i = 0; i < NormalRanklist.Count; i++ )
            {
                NormalRanklist[i].gameObject.SetActive (true);
            }
            for ( int i = 0; i < HardRanklist.Count; i++ )
            {
                HardRanklist[i].gameObject.SetActive (false);
            }
            for ( int i = 0; i < LunaticRanklist.Count; i++ )
            {
                LunaticRanklist[i].gameObject.SetActive (false);
            }
        }
        if ( Global.GameRank == Global.Rank.HARD )
        {
            for ( int i = 0; i < EasyRanklist.Count; i++ )
            {
                EasyRanklist[i].gameObject.SetActive (false);
            }
            for ( int i = 0; i < NormalRanklist.Count; i++ )
            {
                NormalRanklist[i].gameObject.SetActive (false);
            }
            for ( int i = 0; i < HardRanklist.Count; i++ )
            {
                HardRanklist[i].gameObject.SetActive (true);
            }
            for ( int i = 0; i < LunaticRanklist.Count; i++ )
            {
                LunaticRanklist[i].gameObject.SetActive (false);
            }
            
        }
        if (Global.GameRank == Global.Rank.LUNATIC)
        {
            for (int i = 0; i < EasyRanklist.Count; i++)
            {
                EasyRanklist[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < NormalRanklist.Count; i++)
            {
                NormalRanklist[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < HardRanklist.Count; i++)
            {
                HardRanklist[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < LunaticRanklist.Count; i++)
            {
                LunaticRanklist[i].gameObject.SetActive(true);
            }
        }
    }

}
