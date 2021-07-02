using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasedEvent_Explore : TriggerEvent {
    public GameObject ResetingShooting;
    public override void OnBulletEnterIntoTrigger (Bullet Which, Trigger Target, float f, int enterTime)
    {
        Shooting[] Shoter = ResetingShooting.GetComponentsInChildren<Shooting>();
        CameraShake.shake = 0.20f;
        foreach (Shooting a in Shoter) {
            ResetingShooting.transform.position = Which.gameObject.transform.position;

            a.gameObject.SetActive (true);
            Shooting.ResetShooting (a);
        }
    }

}
