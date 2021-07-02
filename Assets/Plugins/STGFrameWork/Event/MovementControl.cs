using System.Collections;
using System.Collections.Generic;
public class MovementControl : SpellCardEvent {
    public MoveMethod motionMethod;
    public bool BeforeEventEnabled = false;
    public bool Enabled = false;
    ~MovementControl()
    {
        Note = "当符卡被销毁的时候，将敌人的移动脚本根据一个布尔值来启用或弃用。";
    }
    public override void OnSpellCardDestroy(List<Shooting> Target, SpellCard Spell, Enemy Character)
    {
        if (!BeforeEventEnabled)
            motionMethod.enabled = Enabled;
    }
    public override void OnSpellCardUsage (List<Shooting> Target, SpellCard Spell, Enemy Character)
    {
        if (BeforeEventEnabled)
        motionMethod.enabled = Enabled;
    }
}
