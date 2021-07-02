using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Sirenix.OdinInspector;
namespace 基础事件.子弹事件
{
	
	public class 子弹刷新时事件Node : Node
	{
		[InfoBox("在低性能CPU中若使用此事件的子弹数量过多会导致严重的性能损耗，请适量使用。",InfoMessageType = InfoMessageType.Warning)]

		[Output] public FunctionProgress 下一步;
		[Output] public Bullet 子弹;
		[Output] public float 子弹年龄;
		object bullet;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionFreshDo(string PortName,object param = null) 
		{
			
			bullet = param;
			ConnectDo("下一步");
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			if (port.fieldName == "子弹")
			{
				if (bullet != null)
					return bullet; // Replace this
			}
			else
			{
				if (bullet != null)
					return ((Bullet)bullet).TotalLiveFrame;
			}
			return null;

		}
	}
}