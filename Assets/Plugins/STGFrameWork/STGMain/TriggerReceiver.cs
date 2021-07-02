using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("东方STG框架/弹幕设计/重要组件/触发器额外探测器")]
public class TriggerReceiver : MonoBehaviour {
    [InfoBox("在触发器组件处勾选\"进行额外检查\"，并保证这里的子弹Tag，于触发器那边的Tag相同，这样才能正常进行额外检测。然后，在发射器指定的\"子弹需要携带的子弹对象\"上挂载本组件。本组件常使用于子弹之间互动，这样的互动要损耗一部分性能。")]
    public string Tag;
    [HideInInspector]
    public Bullet bullet;
   
	void Awake () {
        bullet = GetComponentInParent<Bullet>();
        ObjectPool.ExtraChecking.Add(this);
	}

    void OnDestroy()
    { 
     ObjectPool.ExtraChecking.Remove(this);
    }

   }
