using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.场景.获取对象引用
{
	public class 获得场景引用Node : Node
	{
		public string 引用对象名称;
		[Output] public GameObject 引用对象;
		[HideInInspector]
		public GameObject refObj;
		[HideInInspector]
		public string Objname;
		// Use this for initialization
		protected override void Init(
			)
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			if (Objname != 引用对象名称 || 引用对象 == null)
			{
				Objname =引用对象名称 ;
				Debug.Log("对象索引更新");
				引用对象 = GameObject.Find(引用对象名称);
			}
			if (引用对象 == null)
				return null;
			return 引用对象; // Replace this
		}
	}
}