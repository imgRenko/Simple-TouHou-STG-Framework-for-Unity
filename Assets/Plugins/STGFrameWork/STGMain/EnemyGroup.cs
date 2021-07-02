using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("东方STG框架/框架核心/敌人类/敌人组(已被废弃)")]
[System.Obsolete]
public class EnemyGroup 
{
    public List<EnemyState> Enemylist = new List<EnemyState>();

    public void add(EnemyState t)
    {
        Enemylist.Add(t);
    }

    public bool IsAllDead () {
	    for (int i = 0; i < Enemylist.Count; i++)
	    {
	        if (Enemylist[i].EnemyStateNow == EnemyState.State.STATE_KILL || Enemylist[i].EnemyStateNow == EnemyState.State.STATE_NO_USING) continue;
	        return false;
	    }
	    return true;
	}
}
