using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("东方STG框架/弹幕设计/开发者调试工具/轨迹显示器")]
public class ShootingTrackDisplayer : MonoBehaviour
{
    public int Display = 0;
    public bool InfluenceByRotation = true;
    public List<Vector3> trackPoint = new List<Vector3>();
    [HideInInspector]
    public List<Color32> colorSet = new List<Color32>();
    Transform a;
    // Use this for initialization
    void OnDrawGizmosSelected()
    {
        for (int i = 0; i != trackPoint.Count; ++i)
        {
            if (i < colorSet.Count - 1) 
                Gizmos.color = colorSet[i];
            if (a == null)
                a = gameObject.transform;
            if (i + 1 <= trackPoint.Count - 1)
            {
                if (InfluenceByRotation)
                    Gizmos.DrawLine((a.position + a.localRotation * trackPoint[i]), (a.position + a.localRotation * trackPoint[i + 1]));
                else
                    Gizmos.DrawLine((a.position + trackPoint[i]), (a.position + trackPoint[i + 1]));
            }
            if (Display > trackPoint.Count - 1 || Display < 0)
                continue;
            if (InfluenceByRotation)
                Gizmos.DrawWireSphere(a.position + a.localRotation * trackPoint[Display], 0.1f);
            else
                Gizmos.DrawWireSphere(a.position + trackPoint[Display], 0.1f);

        }
    }

}
