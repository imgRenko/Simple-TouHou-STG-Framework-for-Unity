using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("东方STG框架/弹幕设计/实用工具/游戏同启动对象设置器")]
public class LaunchObjectActivilty : MonoBehaviour {
    public GameObject Target;
    public bool isActive = false;
	void Start () {
        Target.SetActive(isActive);
	}
}
