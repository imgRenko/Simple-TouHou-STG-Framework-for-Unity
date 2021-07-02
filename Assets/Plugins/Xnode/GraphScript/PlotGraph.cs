using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using 基础事件.新剧情系统;

[CreateAssetMenu]
public class PlotGraph : NodeGraph {
    PlotGraph() {
        Name = "游戏角色剧情图表设计器";
        AddNode(typeof(新剧情系统XML生成过程));
    }
    
}