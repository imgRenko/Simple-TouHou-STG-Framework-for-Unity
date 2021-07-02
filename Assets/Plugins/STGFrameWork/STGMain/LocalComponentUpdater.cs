using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[AddComponentMenu("东方STG框架/弹幕设计/可视化脚本/局部可视化脚本执行顺序编辑器")]
public class LocalComponentUpdater : MonoBehaviour
{
    [Title("这是局部组件执行顺序编辑器")]
    [InfoBox("请为你所有的组件指定一个父级对象，然后将此编辑器放置在那个父级对象上。注意，若需要发射器携带那些受到本编辑器影响的组件时，需要将本编辑器一同携带过去，否则将无法绕开Unity的随机调用机制。被携带过去的本编辑器在全局执行顺序中是最后执行的。\r\n若需要在游戏启动前指定本编辑器在全局脚本的执行顺序，在Hierarchy处前往STGMainComponent->GameAction",InfoMessageType = InfoMessageType.Warning)]
    [LabelText("编辑执行顺序")]
    public List<STGComponent> components = new List<STGComponent>();
    [Button]
    public void Distinct() {
        List<STGComponent> newList = components.Distinct(new Comparer()).ToList();
        components = newList;
    }
    void Awake()
    {
        List<STGComponent> newList = components.Distinct(new Comparer()).ToList();
        components = newList;

        List<STGComponent> r = new List<STGComponent>();
        for (int i = 0; i != components.Count; ++i)
        {
            if (components[i] == null)
                r.Add(components[i]);

        }
    
        foreach (var a in r) {
            components.Remove(a);
        }
        foreach (var a in components)
        {
            a.UpdateWithSelfComponent = false;
        }
        if (ComponentOrderEditor.updateComponents.Contains(this) == false)
            ComponentOrderEditor.updateComponents.Add(this);
    }
    public void DoUpdate() {
        foreach (var a in components) {
          if(a.gameObject.activeInHierarchy)
            a.UpdateInfo();
        }
    }
}
public class Comparer : IEqualityComparer<STGComponent> {
    public bool Equals(STGComponent a, STGComponent b)
    {
        return a == b;
    }

    public int GetHashCode(STGComponent obj)
    {
        return obj.GetHashCode();
    }
}
