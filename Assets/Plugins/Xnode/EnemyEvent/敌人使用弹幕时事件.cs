using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.敌人事件
{
	[CreateAssetMenu(fileName = "STGTriggerGraph", menuName = "基本事件/敌人启用事件")]
	public class 敌人使用弹幕时事件 : Node
	{
	

		[Output] public FunctionProgress 下一步;
		[Output]
		public Enemy 敌人;
		Enemy enemy;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName,List<object> param = null) 
		{
		
			enemy = (Enemy)param[0];
			ConnectDo("下一步");
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			if (enemy != null)
				return enemy; // Replace this
			return null;
		}
	}
}