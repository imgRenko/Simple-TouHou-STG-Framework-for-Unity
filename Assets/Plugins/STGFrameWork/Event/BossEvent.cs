using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("东方STG框架/弹幕设计/常见事件/符卡事件/左侧信息栏显示BOSS立绘")]
public class BossEvent :  SpellCardEvent{
    public bool Display = false;
    public override void OnSpellCardUsage (List<Shooting> Target, SpellCard Spell, Enemy Character)
    {
        if (Character.isBoss && !Display) {
            Character.AsBoss ();
            Display = true;
        }
    }
}
