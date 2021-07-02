using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullStub : MonoBehaviour {
	public float maxTime = 0;
	public bool Died = false;
	float Count;
	public XNode.Node node;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Global.GamePause || Global.WrttienSystem || Died)
			return;
		Count += Global.GlobalSpeed;
		if (Count > maxTime) {
			node.ConnectDo("退出节点");

			Died = true;
			DestroyImmediate(this.gameObject);
		}
	}
}
