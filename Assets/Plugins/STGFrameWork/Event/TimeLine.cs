using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLine : TriggerEvent {
    float frameCount;
    public Vector2 pos_left,pos_right;
    public override void OnTriggerUsing(Trigger Target)
    {
        frameCount += 1 * Global.GlobalSpeed;
        if (frameCount >= 80) 
        {
            frameCount = 0;
            int a = Random.Range(0, 1);
            gameObject.transform.position = ((a == 1) ? pos_left : pos_right);
        }
    }
}
