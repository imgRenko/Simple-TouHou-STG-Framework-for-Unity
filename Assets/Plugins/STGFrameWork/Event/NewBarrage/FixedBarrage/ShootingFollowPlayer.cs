using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingFollowPlayer : ShootingEvent {
    public bool X, Y;
    public ShootingFollowPlayer() {
        Note = "制作发射器位置跟随玩家的效果。";
    }
    public override void OnShootingUsing(Shooting Target)
    {
        if (Global.PlayerObject == null) return;
        if (X && Y)
        {
            Target.transform.position = new Vector3(Global.PlayerObject.transform.position.x, Global.PlayerObject.transform.position.y, 0);
            return;
        }
        if (Y)
            Target.transform.position = new Vector3 (transform.position.x, Global.PlayerObject.transform.position.y,0);
        if (X)
            Target.transform.position = new Vector3( Global.PlayerObject.transform.position.x, transform.position.y, 0);
    }
}
