using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ShootingEventList{
    public List<GameObject> enabledList = new List<GameObject>();
    public float HPpercent = 0.5f;
    public bool ActiveObject = true;
    public bool Enabled = true;
}
[AddComponentMenu("东方STG框架/弹幕设计/实用工具/敌人转阶段控制器")]
public class EnemyStage : MonoBehaviour {
    public Enemy who;
    [SerializeField]
    public List<ShootingEventList> StageSetting = new List<ShootingEventList>();
    public bool turnStageDestroyBullets = true;
	void Update () {
        if (who == null && Global.SpellCardExpressing && Global.WrttienSystem)
            return;
        if (who.HP / who.MaxHP >= 0.99f)
            foreach (ShootingEventList e in StageSetting) {
                e.Enabled = true;
                foreach (GameObject b in e.enabledList) {
                    b.SetActive (!e.ActiveObject);
                }
            }
        foreach (ShootingEventList e in StageSetting) {
            if (who.HP / who.MaxHP < e.HPpercent && e.Enabled) {
                e.Enabled = false;
                StartCoroutine(Global.GameObjectPool_A.DestroyBullets (true));
                foreach (GameObject b in e.enabledList) {
                    b.SetActive (e.ActiveObject);
                }
            }
        }
	}
}
