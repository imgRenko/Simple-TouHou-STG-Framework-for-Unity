using UnityEngine;

public class SpellChange : ShootingEvent
{
    public Enemy target;
    public override void OnShootingUsing (Shooting Target)
    {
        base.OnShootingUsing (Target);
        if (target == null)
            return;;
        Global.GlobalSpeed = Mathf.Clamp( target.HP / target.MaxHP + 0.1f,0,1f);
          
    }
}

