using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
[AddComponentMenu("东方STG框架/弹幕设计/可视化脚本/适用于敌人事件的可视化脚本")]
public class BasedEvent_EnemyVisualScriptEvent : EnemyEvent
{
    public NodeGraph graph;
    [HideInInspector]
    public Node damageEvent;
    [HideInInspector] public Node startEvent;
    [HideInInspector] public Node destroyEvent;
    [HideInInspector] public Node barrageEvent;
    [HideInInspector] public Node movingEvent;
    [HideInInspector] public Node stopingEvent;
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
    public void DoEvent(Node a , Enemy Target) {
        if (a == null)
            return;
        List<object> objects = new List<object>();
        objects.Add(Target);
        a.FunctionDo("下一步", objects);

    }
    public override void OnStarts(Enemy E)
    {
        if (graph == null)
        {
            enabled = false;
            return;
        }
        DoEvent(SearchNode("敌人启用时事件"), E);
        damageEvent = SearchNode("敌人受击事件");
        destroyEvent = SearchNode("敌人被击倒时事件");
        barrageEvent = SearchNode("敌人使用弹幕时事件");
        movingEvent = SearchNode("敌人移动时事件");
        stopingEvent = SearchNode("敌人停下时事件");
    }
    public override void OnDamaged(Enemy E)
    {
        DoEvent(damageEvent, E);
    }
    public override void OnKilled(Enemy E)
    {
        DoEvent(destroyEvent, E);
    }
    public override void OnStartsBarrage(Enemy E)
    {
        DoEvent(barrageEvent, E);
    }
    public override void OnMoving(Enemy E)
    {
        DoEvent(movingEvent, E);
    }

    public override void OnStopMoving(Enemy E)
    {
        DoEvent(stopingEvent, E);
    }
}
