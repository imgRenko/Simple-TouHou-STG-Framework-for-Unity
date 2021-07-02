using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour {
    public GameObject AuxiliaryLinesStart, AuxiliaryLinesEnd,tar;

    void OnDrawGizmos()
    {
        if (AuxiliaryLinesStart != null && AuxiliaryLinesEnd != null)
            Gizmos.DrawLine(AuxiliaryLinesStart.transform.position,AuxiliaryLinesEnd.transform.position);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       /*
        float t = Vector2.Dot (AuxiliaryLinesEnd.transform.position - tar.transform.position, (AuxiliaryLinesEnd.transform.position - AuxiliaryLinesStart.transform.position));
        float ang = Mathf.Abs (Vector2.Angle (AuxiliaryLinesEnd.transform.position - tar.transform.position, (AuxiliaryLinesEnd.transform.position - AuxiliaryLinesStart.transform.position)));
        float final = t * Mathf.Tan (ang * Mathf.Deg2Rad);
*/
        float ang = Vector2.Angle (Vector2.up, (AuxiliaryLinesEnd.transform.position - AuxiliaryLinesStart.transform.position));
        Debug.Log (ang);
    }
}
