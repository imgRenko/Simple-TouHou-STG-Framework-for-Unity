using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovePosition : BulletEvent {
    public Trigger TargetTrigger;
    public override void OnBulletMoving(Bullet Target)
    {
        Target.transform.position = Vector3.SlerpUnclamped(Target.transform.position, TargetTrigger.transform.position, 0.05f);       
    }
}
