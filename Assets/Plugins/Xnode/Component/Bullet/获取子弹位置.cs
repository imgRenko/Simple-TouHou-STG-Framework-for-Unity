using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.子弹
{
	public class 获取子弹位置 : Node
	{
		[Input] public Bullet 子弹;
		[Output] public Vector3 位置;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			子弹 = GetInputValue("子弹",子弹);
			if (子弹 == null)
				return Vector3.zero;
			return 子弹.BulletTransform.position; // Replace this
		}
	}
}