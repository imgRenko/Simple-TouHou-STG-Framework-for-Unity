using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.对象
{

	public class 获取搭载本组件的游戏对象 : Node
	{
		[Input] public Anything 组件;
		[Output] public GameObject 游戏对象;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			Component 组件 = GetInputValue<Component>("组件");
			if (组件 == null)
				return null;

			return 组件.gameObject; // Replace this
		}
	}
}