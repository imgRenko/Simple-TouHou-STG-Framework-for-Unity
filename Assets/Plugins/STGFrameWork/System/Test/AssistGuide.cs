using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistGuide : MonoBehaviour
{
    public Camera ab;
    // Use this for initialization
    void Start()
    {
        
    }
    void OnDrawGizmos()
    {
        if (ab == null) return;
        Vector3 a1 = Vector3.zero, b = Vector3.zero, c = Vector3.zero, d = Vector3.zero;
        a1.x = 0;
        a1.y = 0;
        c.x = 1366;
        d.y = 768;
        b.x = 1366;
        b.y = 768;
        Gizmos.DrawLine(ab.ScreenToWorldPoint(a1), ab.ScreenToWorldPoint(c));
        Gizmos.DrawLine(ab.ScreenToWorldPoint(a1), ab.ScreenToWorldPoint(d));
        Gizmos.DrawLine(ab.ScreenToWorldPoint(d), ab.ScreenToWorldPoint(b));
        Gizmos.DrawLine(ab.ScreenToWorldPoint(c), ab.ScreenToWorldPoint(b));
    }
}