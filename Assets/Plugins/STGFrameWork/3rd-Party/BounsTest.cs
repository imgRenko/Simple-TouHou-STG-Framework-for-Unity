using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounsTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Bouns.SetBouns(2, Vector3.zero, Bouns.BounsType.Score);
	}
}
