using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Sirenix.OdinInspector;
namespace 变量
{
	public class 纹理常量 : Node
	{
		[PreviewField(80, ObjectFieldAlignment.Left)]
		public Sprite 常量初始值;
		[Output] public Sprite 输出;


		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return 常量初始值; // Replace this
		}
	}
}