using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
[AddComponentMenu("东方STG框架/弹幕设计/可视化脚本/适用于发射器事件的可视化脚本")]
public class BasedEvent_ShootingVisualScriptEvent : ShootingEvent
{
    public NodeGraph graph;
    [HideInInspector] public Node beforeShooting;
    [HideInInspector] public Node startShooting;
    [HideInInspector] public Node shootingUpdate;
    [HideInInspector] public Node afterShooting;
    [HideInInspector] public Node shootingDestroy;
    [HideInInspector] public Node shootingEachTime;
    [HideInInspector] public Node shootingStartNewLoop;
    [HideInInspector] public Node shootingFinishAllTask;
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
    public void DoEvent(Node a , Shooting Target,Bullet bullet = null,bool Addbullet = false) {
        if (a == null)
            return;
        List<object> objects = new List<object>();
        objects.Add(Target);
        if (Addbullet)
            objects.Add(bullet);
        a.FunctionDo("下一步", objects);

    }
    public override void EventStart(Shooting[] Target)

    {
        if (graph == null)
        {
            enabled = false;
            return;
        }
        startShooting = SearchNode("发射器初始化时");
        shootingUpdate = SearchNode("发射器进行更新时");
        beforeShooting = SearchNode("发射器发射前");
        afterShooting = SearchNode("发射器发射后");
        shootingDestroy = SearchNode("发射器销毁时");
        shootingEachTime = SearchNode("发射器处理每条子弹时");
        shootingStartNewLoop = SearchNode("发射器启用新循环时");
        shootingFinishAllTask = SearchNode("完成全部发射任务时");
        DoEvent(startShooting, Target[0]);
    }
    public override void OnShootingDestroy(Shooting Target)
    {
        DoEvent(shootingDestroy, Target);
    }
    public override void AfterShootingFinishedShooting(Shooting Target,Bullet bullet)
    {

        DoEvent(shootingEachTime, Target,bullet,true);
    }
    public override void OnShootingFinishAllShotTasks(Shooting Target)
    {
        DoEvent(shootingFinishAllTask, Target);
    }
    public override void BeforeShooting(Shooting Target)
    {
        DoEvent(beforeShooting, Target);
    }

    public override void OnShootingUsing(Shooting Target)
    {
        DoEvent(shootingUpdate, Target);
    }
    public override void StartNewLoop(Shooting Target)
    {
        DoEvent(shootingStartNewLoop, Target);
    }
}
