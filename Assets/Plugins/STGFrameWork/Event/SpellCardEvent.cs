using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("东方STG框架/框架核心/事件基础/符卡事件基类")]
public class SpellCardEvent : MonoBehaviour {
    [HideInInspector]
    public SpellCard TargetSpellCard;
    [Multiline]
    public string Note ="（于此处输入注解）";
    void Start ()
    {
        TargetSpellCard = GetComponent<SpellCard> ();
		EventStart (TargetSpellCard, TargetSpellCard.Character);
        TargetSpellCard.OnSpellCardUsage += OnSpellCardUsage;
        TargetSpellCard.OnSpellCardUsing += OnSpellCardUsing;
        TargetSpellCard.OnSpellCardDestroy += OnSpellCardDestroy;
		TargetSpellCard.BeforeNextSpellCard += BeforeNextCard;
    }
    /// <summary>
    /// 符卡被使用时候的事件，需要覆写，否则无效。
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="Spell"></param>
    /// <param name="Character"></param>
    public virtual void OnSpellCardUsage (List<Shooting> Target,SpellCard Spell,Enemy Character) {}
    /// <summary>
    /// 符卡正在使用时的事件，需要覆写，否则无效。
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="Spell"></param>
    /// <param name="Character"></param>
    public virtual void OnSpellCardUsing (List<Shooting> Target, SpellCard Spell, Enemy Character) {}
    /// <summary>
    /// 符卡被破坏的事件，需要覆写，否则无效。
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="Spell"></param>
    /// <param name="Character"></param>
    public virtual void OnSpellCardDestroy (List<Shooting> Target, SpellCard Spell, Enemy Character) {}
	public virtual void BeforeNextCard(List<Shooting> Target, SpellCard Spell, Enemy Character) {}
	public virtual void EventStart( SpellCard Spell, Enemy Character) {}
}
