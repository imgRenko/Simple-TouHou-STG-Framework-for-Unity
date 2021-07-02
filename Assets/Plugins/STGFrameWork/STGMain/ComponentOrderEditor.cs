using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[AddComponentMenu("东方STG框架/弹幕设计/可视化脚本/全局可视化脚本执行顺序编辑器")]
public class ComponentOrderEditor : MonoBehaviour
{
    [Title("这是全局组件执行顺序编辑器")]
    [InfoBox("将局部组件顺序编辑器组件拖入此处，被此局部组件编辑器所涉及的STG组件，将不受Unity同一组件随机调用的机制影响。将组件拖入此处后，从原理上将不再自动调用组件的StateUpdate函数（但Unity的Update函数有效），将由此组件代替调用，所以在游戏过程中，如果此组件被删除，将会影响到所有涉及本组件的STG组件。\r\n附：在本列表中，被放置在最上边的，是最先被执行的。多个组件之间的刷新调用拥有先后顺序性质，不能同时调用。\r\n确认你的执行顺序正确以后，点击Apply确认你的操作，然后将更新执行顺序列表。")]
    [LabelText("组件执行顺序编辑")]
    public List<LocalComponentUpdater> components = new List<LocalComponentUpdater>();

    public static List<LocalComponentUpdater> updateComponents = new List<LocalComponentUpdater>();


    // Start is called before the first frame update
    [Button]
    public void Apply() {
        updateComponents = components;
    }
}
