using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
[AddComponentMenu("东方STG框架/弹幕设计/可视化脚本/适用于符卡系统的可视化脚本")]
public class BasedEvent_SpellCardVisualScriptEvent : SpellCardEvent
{
	
    public NodeGraph graph;
    [HideInInspector] public Node OnUsage;
    [HideInInspector] public Node OnUsing;
    [HideInInspector] public Node OnDestory;
    [HideInInspector] public Node OnNext;

    public Node SearchNode(string Name) {
     
        foreach (var a in graph.nodes)
        {
           
            if (a != null && a.name == Name)
            {
                
                return a;

            }
        }
        return null;
    }
    public void DoEvent(Node a , List<Shooting> Target, Enemy Character = null, SpellCard Spell = null) {
        if (a == null)
            return;
        List<object> objects = new List<object>();
        objects.Add(Target);
        objects.Add(Character);
        objects.Add(Spell);

        a.FunctionDo("下一步", objects);

    }
    public override void OnSpellCardUsage(List<Shooting> Target, SpellCard Spell, Enemy Character)
    {
        if (graph == null)
        {
            enabled = false;
            return;
        }
        OnUsage = SearchNode("符卡系统初始化时事件");
        OnUsage = SearchNode("符卡系统使用时事件");
        OnDestory = SearchNode("符卡系统销毁时事件");
        OnNext = SearchNode("使用下一张符卡事件");
        DoEvent(OnUsage, Target, Character, Spell);
    }

    public override void BeforeNextCard(List<Shooting> Target, SpellCard Spell, Enemy Character)
    {
        DoEvent(OnNext, Target, Character, Spell);
    }
    public override void OnSpellCardDestroy(List<Shooting> Target, SpellCard Spell, Enemy Character)
    {
        DoEvent(OnDestory, Target, Character, Spell);
    }
    public override void OnSpellCardUsing(List<Shooting> Target, SpellCard Spell, Enemy Character)
    {
        DoEvent(OnUsing, Target, Character, Spell);
    }

}
