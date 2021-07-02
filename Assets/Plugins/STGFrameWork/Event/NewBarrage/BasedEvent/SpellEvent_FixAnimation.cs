using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEvent_FixAnimation : SpellCardEvent {
    public Animator tarAnim;
    public string WhichParm = "Default";
    public bool setValue = false;
    public bool UsedDestroy = false;
    public override void OnSpellCardUsage (List<Shooting> Target, SpellCard Spell, Enemy Character)
    {
        if (!UsedDestroy)
            tarAnim.SetBool (WhichParm, setValue);
    }
    public override void OnSpellCardDestroy (List<Shooting> Target, SpellCard Spell, Enemy Character)
    {
        if (UsedDestroy)
            tarAnim.SetBool (WhichParm, setValue);
    }
}
