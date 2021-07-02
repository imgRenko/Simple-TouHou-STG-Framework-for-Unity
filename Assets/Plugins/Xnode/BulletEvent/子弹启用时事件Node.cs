using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.子弹事件
{
	[CreateAssetMenu(fileName = "STGTriggerGraph", menuName = "基本事件/子弹启用事件")]
	public class 子弹启用时事件Node : Node
	{
		

		[Output] public FunctionProgress 下一步;
		[Output]
		public Bullet 子弹;
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
			if (bullet != null)
				return bullet; // Replace this
			return null;
		}
	}
}