using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("东方STG框架/弹幕设计/剧情系统/新剧情系统(漫画式)/角色对话事件绑定器(需要继承直接使用无效)")]
public class DialogSystemEvent : MonoBehaviour {
    public string Note = "在這裡輸入相關注釋內容";
    public DialogSystemInit target;
	void Start () {
        target.DialogEnded += WhenPlotEnded;
	}
    public virtual void WhenPlotEnded(){}
}
