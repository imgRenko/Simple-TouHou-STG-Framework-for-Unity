using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class FuncitonProduct{
	public delegate void FunctionEvent();
	public FuncitonProduct FuncitonProducts;
	public Dictionary<string, FuncitonProduct> functions = new Dictionary<string, FuncitonProduct>();


}
/// <summary>
/// 这个类可以调用一些常用的函数。
/// </summary>
[AddComponentMenu("东方STG框架/弹幕设计/常见事件/触发器事件/简单触发器事件调用器")]
public class SimplyEvent : MonoBehaviour {
    [ShowIf("ShootingEvent")]
    public Shooting shooting;
    public bool ShootingEvent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
