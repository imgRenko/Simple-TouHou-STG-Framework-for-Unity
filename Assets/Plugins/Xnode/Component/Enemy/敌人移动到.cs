using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.敌人
{
	public class 敌人移动到 : Node
	{
		[Sirenix.OdinInspector.InfoBox("不与敌人移动控制器相兼容，在敌人移动控制器生效的情况下，可能会出现瞬移的情况。此组件不阻止此矛盾行为发生，请在出现问题以后视情况使用本节点。")]
		[Input] public FunctionProgress 进入节点;
		[Input] public Enemy 敌人;
		public Enemy.MoveCurves 移动方式;
		public float 移动时长= 30;
		public bool 使用曲线;
		public AnimationCurve 曲线;
		[Input] public Vector2 要移动到的点;
		[Output] public FunctionProgress 退出节点;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
        public override void FunctionDo(string PortName, List<object> param = null)
        {
			敌人 = GetInputValue("敌人", 敌人);
			要移动到的点 = GetInputValue("要移动到的点", 要移动到的点);
			敌人.Move(要移动到的点, false, -1, false);
			敌人.MoveType = 移动方式;
			敌人.RunTime = 移动时长;
			if (使用曲线)
				敌人.animationCurve = 曲线;
			ConnectDo("退出节点");

		}
        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}
	}
}