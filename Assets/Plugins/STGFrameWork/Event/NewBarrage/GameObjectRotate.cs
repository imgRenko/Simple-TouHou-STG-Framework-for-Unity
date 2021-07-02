using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectRotate : MonoBehaviour {
    public GameObject Tar;
    public float Spd = 2;
	// Use this for initialization
	void Start () {
        if (Tar == null)
        Tar = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (!Global.GamePause)
        Tar.transform.Rotate (Vector3.back, Spd);
	}
}
