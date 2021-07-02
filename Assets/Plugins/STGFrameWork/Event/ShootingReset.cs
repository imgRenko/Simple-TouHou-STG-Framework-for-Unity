using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[AddComponentMenu("东方STG框架/弹幕设计/常见事件/发射器事件/发射器重置器(调试期间)")]
public class ShootingReset : MonoBehaviour {
    [Button]
    public void ResetShooting() {
        foreach (var a in GetComponentsInChildren<Shooting>()) {
            a.ResetCountTime();
            a.enabled = true;
        }
    }
}
