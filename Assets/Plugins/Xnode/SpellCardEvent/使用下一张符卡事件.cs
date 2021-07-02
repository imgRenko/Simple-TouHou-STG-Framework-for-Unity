using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace 基础事件.符卡系统事件
{
	
	public class 使用下一张符卡事件 : Node
	{
		

		[Output] public FunctionProgress 下一步;
		[Output]
		public List<Shooting> 发射器集合;
			[Output]
		public Enemy 敌人;
			[Output]
		public SpellCard 符卡;
		Enemy enemy;
		List<Shooting> shootings;
		SpellCard spellcards;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		public override void FunctionDo(string PortName,List<object> param = null) 
		{
		
			shootings = (List<Shooting> )param[0];
				enemy = (Enemy )param[1];
					spellcards = (SpellCard )param[1];	
			ConnectDo("下一步");
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
				if(port.fieldName =="发射器集合")
				if (shootings != null)
					return shootings; // Replace this
			if (port.fieldName == "敌人")
				if (enemy != null)
				return enemy;
			if (port.fieldName == "符卡")
				if (spellcards != null)
				return spellcards;
			return null;
		}
	}
}