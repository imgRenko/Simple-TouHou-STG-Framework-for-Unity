using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MatchCondition{
    public string Tag;
    public GameObject gameObj;
    public bool Active;
}
[AddComponentMenu("东方STG框架/弹幕设计/实用工具/对象筛选器(以全局变量筛选)")]
public class TagObjectSelection : MonoBehaviour {
    [SerializeField]
    public List<MatchCondition> fillForm = new List<MatchCondition>();
    public bool AwakeFunction = true;
    void Awake () {
        if (AwakeFunction){
            foreach (MatchCondition a in fillForm) {
                if (a.Tag == Global.CommandString)
                    a.gameObj.SetActive (a.Active);
            }
        }
    }
    void Start () {
        foreach (MatchCondition a in fillForm) {
            if (a.Tag == Global.CommandString)
                a.gameObj.SetActive (a.Active);
        }
    }
}
