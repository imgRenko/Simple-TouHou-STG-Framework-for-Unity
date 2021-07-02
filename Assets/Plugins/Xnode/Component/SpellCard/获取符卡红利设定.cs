using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.符卡系统
{
	public class 获取符卡红利设定 : Node
	{
		
		[Input] public SpellCard 符卡;
		[Output] public long 目的值;
		public enum Type { 
			Min,Max
		}
		public Type 类型;
		[Output] public FunctionProgress 退出节点;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
       
   public override object GetValue(NodePort port)
		{
				符卡 = GetInputValue("符卡", 符卡);
			
		if (符卡 == null)
			return null;
			switch (类型) {
				case Type.Max:
					return 符卡.MaxBouns;
					
				case Type.Min:
					return  符卡.MinBouns; 
			}
			return null;
		}
	}
}