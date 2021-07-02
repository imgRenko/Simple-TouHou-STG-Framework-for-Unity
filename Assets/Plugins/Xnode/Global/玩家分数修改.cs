using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using XNode;
namespace 基础事件.全局.玩家{
public class 玩家分数修改: Node {
	[Output] public long 分数;
		[Input] public FunctionProgress 进入节点;
		[Input] public long 分数值;
		[Input] public bool 增加 = true;
		[Input] public bool 显示增分提示 = true;
		[Output] public FunctionProgress 退出节点;
		// Use this for initialization
		protected override void Init() {
		base.Init();
		
	}
	public override void FunctionDo(string PortName, List<object> param = null)
		{
			Global.AddPlayerScore(分数值, 显示增分提示);
			ConnectDo("退出节点");
		}
	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null;//return 返回最大分数 ? Global.MaxScore : Global.Score; // Replace this
	}
}}