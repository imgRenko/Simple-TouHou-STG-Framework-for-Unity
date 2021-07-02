using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Condition{
    public string Tag;
    public GameObject gameObj;
    public bool Active;
}
[AddComponentMenu("东方STG框架/弹幕设计/实用工具/对象筛选器(以自机种类筛选)")]
public class PlayerTagSelection : MonoBehaviour {
    public List<Condition> fillForm = new List<Condition>();
	void Start () {
        foreach (Condition a in fillForm) {
            if (a.Tag == Character.PlayerTag)
                a.gameObj.SetActive (a.Active);
        }
	}
}
