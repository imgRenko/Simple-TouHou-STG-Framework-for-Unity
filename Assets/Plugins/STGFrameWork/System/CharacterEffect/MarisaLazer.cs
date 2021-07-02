using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaLazer : MonoBehaviour {
    

    void OnCollisionEnter2D(Collision2D C)
    {
        Debug.Log ("CK");
        if (C.gameObject.tag == "Enemy" )
         {
            Enemy _e = C.gameObject.GetComponent<Enemy> ();
            if (_e != null) {
                _e.HP-=50;
            }
         }
    }
}
