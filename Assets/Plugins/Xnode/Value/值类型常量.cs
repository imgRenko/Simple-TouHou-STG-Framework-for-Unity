using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 变量
{
	public class 值类型常量 : Node
	{
		[Input] public string 值;
		public enum ValueType
		{
			浮点数 = 0,
			整数 = 1,
			文本 = 2,
			双精度浮点 = 3,
			长整数 = 4,
			短整数 = 5,
			布尔值 = 6
		}
		public ValueType 类型;
		[Output] public Anything 输出;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			 值 = GetInputValue<string>("值",值);
			object a = 值;
			switch (类型)
			{
				case ValueType.双精度浮点:
					a = System.Convert.ToDouble(值);
					break;
				case ValueType.浮点数:
					a = System.Convert.ToSingle(值);
					break;
				case ValueType.整数:
					a = System.Convert.ToInt32(值);
					break;
				case ValueType.长整数:
					a = System.Convert.ToInt64(值);
					break;
				case ValueType.短整数:
					a = System.Convert.ToInt16(值);
					break;
				case ValueType.布尔值:
					a = System.Convert.ToBoolean(值);
					break;
				case ValueType.文本:
					a = 值;
					break;
			}

			return a;
		}
	}
}