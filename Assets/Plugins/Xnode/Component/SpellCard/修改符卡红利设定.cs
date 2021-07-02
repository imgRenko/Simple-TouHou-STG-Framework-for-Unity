using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.符卡系统
{
	public class 修改符卡红利设定 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public SpellCard 符卡;
		[Input] public long 目的值;
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
        public override void FunctionDo(string PortName, List<object> param = null)
        {
			符卡 = GetInputValue("符卡", 符卡);
			目的值 = GetInputValue("目的值", 目的值);
		
			switch (类型) {
				case Type.Max:
					符卡.MaxBouns = 目的值;
					break;
				case Type.Min:
					符卡.MinBouns = 目的值;
					break;
			}
			ConnectDo("退出节点");
        }
  
	}
}