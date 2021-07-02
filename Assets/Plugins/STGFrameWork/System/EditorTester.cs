using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorTester : MonoBehaviour {
    public GameObject Defplayer;
    public Global.Rank rank;
	// Use this for initialization
	void Start () {
        if (Application.isEditor && Global.CommandString == "") {
            Defplayer.SetActive (true);
            Global.GameRank = rank;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
