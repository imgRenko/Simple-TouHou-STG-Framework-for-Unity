using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class MotionTable :  SerializedMonoBehaviour{
    public List<MotionTableTarget> frameGroup = new List<MotionTableTarget>();
    public enum MotionMatters { 
        Lerp  = 0,
        Slerp = 1,
        UncalmSlerp = 2
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnDrawGizmos()
    {

        Gizmos.DrawIcon(transform.position, "ShootingTitle");
        Gizmos.color = Color.red;
        if (transform.parent != null)
            Gizmos.DrawLine(transform.position, transform.parent.transform.position);
        for (int i = 0; i != frameGroup.Count; i++) {
            Gizmos.DrawLine((i == 0 ? (Vector2)gameObject.transform.position : frameGroup[i-1].Point), frameGroup[i].Point); 
        }
    }
}
[System.Serializable]
public class MotionTableTarget
{
    public float KeepOn = 2 ;
    public bool Used;
    public MotionTable.MotionMatters MotionWays;
    public Vector2 Point;

}
