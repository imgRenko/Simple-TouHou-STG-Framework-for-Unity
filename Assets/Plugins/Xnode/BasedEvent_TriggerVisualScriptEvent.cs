using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
[AddComponentMenu("东方STG框架/弹幕设计/可视化脚本/适用于触发器的可视化脚本事件")]
public class BasedEvent_TriggerVisualScriptEvent : TriggerEvent
{
	
    public NodeGraph graph;
    [HideInInspector] public Node OnStay;
    [HideInInspector] public Node OnExit;
    [HideInInspector] public Node OnEnter;
    [HideInInspector] public Node OnUse;
    [HideInInspector] public Node OnUsing;

    [HideInInspector] public Node OnDestroy;
    [HideInInspector] public Node OnStart;
    [HideInInspector] public Node OnStayBullet;
    [HideInInspector] public Node OnExitBullet;
    [HideInInspector] public Node OnEnterBullet;
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
    public void DoSingleEvent(Node a ,Trigger trigger) {
        if (a == null)
            return;
        List<object> objects = new List<object>();
        objects.Add(trigger);
        a.FunctionDo("下一步", objects);

    }
    public void DoDoubleEvent(Node a, Trigger trigger,Bullet bullet)
    {
        if (a == null)
            return;
        List<object> objects = new List<object>();
        objects.Add(trigger);
        objects.Add(bullet);
        a.FunctionDo("下一步", objects);

    }

    public void DoFourEvent(Node a, Trigger trigger, Bullet bullet,float stayTime,int enterTime)
    {
        if (a == null)
            return;
        List<object> objects = new List<object>();
        objects.Add(trigger);
        objects.Add(bullet);
        objects.Add(stayTime);
        objects.Add(enterTime);
        a.FunctionDo("下一步", objects);

    }
 
    public void DoThirdEvent(Node a, Trigger trigger, Bullet bullet, Bullet bullet2)
    {
        if (a == null)
            return;
        List<object> objects = new List<object>();
        objects.Add(trigger);
        objects.Add(bullet);
        objects.Add(bullet2);
        a.FunctionDo("下一步", objects);

    }
    public void DoThirdEvent(Node a, Trigger trigger, Bullet bullet, int EnterTime)
    {
        if (a == null)
            return;
        List<object> objects = new List<object>();
        objects.Add(trigger);
        objects.Add(bullet);
        objects.Add(EnterTime);
        a.FunctionDo("下一步", objects);

    }
    public override void OnBulletEnterIntoTrigger(Bullet Which, Trigger Target, float StayTime, int enterTime)
    {
        DoFourEvent(OnEnter, Target, Which, StayTime, enterTime);

    }
    public override void OnBulletExitFromTrigger(Bullet Which, Trigger Target, float StayTime, int enterTime)
    {
        DoFourEvent(OnExit, Target, Which, StayTime, enterTime);

    }
    public override void OnBulletStayInTrigger(Bullet Which, Trigger Target,float StayTime,int enterTime)
    {
        DoFourEvent(OnStay, Target,Which, StayTime,enterTime);
    }
    public override void OnTriggerDestroy(Trigger Target)
    {
        DoSingleEvent(OnDestroy, Target);

    }
    public override void OnTriggerStart(Trigger Target)
    {
        DoSingleEvent(OnUse, Target);
    }
    public override void OnTriggerUsing(Trigger Target)
    {
        DoSingleEvent(OnUsing, Target);
    }
    public override void OnExtraBulletEnterIntoTrigger(Bullet bullet, Bullet bullet2, Trigger Target)
    {
        DoThirdEvent(OnEnterBullet, Target, bullet, bullet2);
    }
    public override void OnExtraBulletExitFromTrigger(Bullet bullet, Bullet bullet2, Trigger Target)
    {
        DoThirdEvent(OnExitBullet, Target, bullet, bullet2);
    }
    public override void OnExtraBulletStayInTrigger(Bullet bullet, Bullet bullet2, Trigger Target)
    {
        DoThirdEvent(OnStayBullet,Target, bullet, bullet2);
    }
    public override void EventStart(Trigger Target)
    {
        if (graph == null)
        {
            enabled = false;
            return;
        }
        OnStart = SearchNode("触发器创建时事件");
        OnUsing = SearchNode("触发器使用时事件");
        OnDestroy = SearchNode("触发器销毁时事件");

        OnUse = SearchNode("触发器新循环事件");

        OnStay = SearchNode("子弹在触发器内时事件");
        OnEnter = SearchNode("子弹进入触发器时事件");
        OnExit = SearchNode("子弹退出触发器时事件");

        OnStayBullet = SearchNode("额外子弹在触发器内时事件");
        OnEnterBullet = SearchNode("额外子弹进入触发器时事件");
        OnExitBullet = SearchNode("额外子弹退出触发器时事件");

        DoSingleEvent(OnStart, Target);
        
    }
}
