using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 变量
{
	public class 从另一个表获取变量 : Node
	{
		public STGTriggerGraph STG触发事件表;
		public string 变量名称;
		[Output] public Anything 输出值;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			int NodeIndex = 0;
			if (STG触发事件表 == null)
				return null;
			STG触发事件表.valueNode.TryGetValue(变量名称, out NodeIndex);
			NodePort Port = STG触发事件表.nodes[NodeIndex].GetPort("变量值");
			if (port == null)
				return null;
			object r = Port.GetOutputValue();
			if (r != null)
				return r;
			else
				return null; // Replace this
		}
	}
}