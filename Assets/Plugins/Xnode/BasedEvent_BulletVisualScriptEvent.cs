using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
[AddComponentMenu("东方STG框架/弹幕设计/可视化脚本/适用于子弹事件的可视化脚本")]
public class BasedEvent_BulletVisualScriptEvent : BulletEvent
{
    public NodeGraph graph;
    [HideInInspector] public Node updateEvent;
    [HideInInspector] public Node startEvent;
    [HideInInspector] public Node destroyEvent;
    [HideInInspector] public Node restoreMainEvent;
    [HideInInspector] public Node destroyPlayer;
    public bool isUpdateEventExist = false;
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
    public void DoEvent(Node a ,Bullet Target) {
        if (a == null)
            return;
        //List<object> objects = new List<object>();
        //objects.Add(Target);
        a.FunctionFreshDo("下一步", Target);


    }
    public void DoSecondEvent(Node a, Bullet Target,Bullet ParentBullet)
    {
        if (a == null)
            return;
        List<object> objects = new List<object>();
        objects.Add(Target);
        objects.Add(ParentBullet);
        a.FunctionDo("下一步", objects);


    }
    public override void OnBulletCreated(Bullet Target)
    {
        if (graph == null)
        {
            enabled = false;
            return;
        }
        DoEvent(SearchNode("子弹启用时事件"), Target);
        updateEvent = SearchNode("子弹刷新时事件");
        destroyEvent = SearchNode("子弹销毁时事件");
        restoreMainEvent = SearchNode("子弹改变父级时事件");
        destroyPlayer = SearchNode("子弹击中玩家时事件");
        if (updateEvent != null)
            isUpdateEventExist = true;
    }
    public override void OnBulletMoving(Bullet Target)
    {
        if (isUpdateEventExist)
        DoEvent(updateEvent, Target);
    }
    public override void OnBulletDestroy(Bullet Target)
    {

        DoEvent(destroyEvent, Target);
    }
    public override void OnBulletRecoverMainLevel(Bullet Target,Bullet ParentBullet)
    {
        DoSecondEvent(restoreMainEvent, Target, ParentBullet);
    }

    public override void OnBulletDestroyPlayer(Bullet Target)
    {
        DoEvent(destroyPlayer, Target);
    }
}
