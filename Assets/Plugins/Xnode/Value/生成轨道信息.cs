using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.子弹
{
	public class 生成轨道信息 : Node
	{
		public BulletTrackProduct 轨道信息;
		[Output] public BulletTrackProduct 输出;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			 BulletTrackProduct r = 轨道信息.Clone(); // Replace this
			return r;
		}
	}
}