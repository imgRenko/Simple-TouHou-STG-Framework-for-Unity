using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using XNode;
using System;

namespace 变量
{
	public class 数组删改操作 : 数组类型处理
	{
		
		public enum Operation { 
			增加 = 0,
			删除= 1,
			清空 = 2
		}
		[Input] public FunctionProgress 进入节点;
		public 数组.ValueType 变量类型;
		public Operation 操作;
		[Output] public FunctionProgress 退出节点;
		public object r;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		[Button]
		public void 刷新()
		{
			变量类型 = GetInputValue<数组.ValueType>("变量类型", 变量类型);
			if (GetPort("输入") != null && GetPort("对应值") != null)
			{
				RemoveDynamicPort("输入");
				RemoveDynamicPort("对应值");
			}
			AddDmyInputPort(变量类型, "输入");
			AddDmyInputNotListPort(变量类型, "对应值");
			if (GetPort("输出") != null)
				RemoveDynamicPort("输出");
			AddDmyOutputPort(变量类型,"输出");
			//Init(); 

		}

		public override void FunctionDo(string PortName,List<object> param = null) 
		{
			变量类型 = GetInputValue<数组.ValueType>("变量类型", 变量类型);
			switch (变量类型)
			{
				case 数组.ValueType.双精度浮点:
					List<double> doubleList = GetInputValue<List<double>>("输入");
					r =  doubleList;
					switch (操作) {	
						case Operation.删除:
							doubleList.Remove(GetInputValue<double>("对应值"));
							break;
						case Operation.增加:
							doubleList.Add(GetInputValue<double>("对应值"));
							break;
						case Operation.清空:
							doubleList.Clear();
							break;
					}
					break;
				case 数组.ValueType.浮点数:
					List<float> floatList = GetInputValue<List<float>>("输入");
					r = floatList;
					switch (操作)
					{
						case Operation.删除:
							floatList.Remove(GetInputValue<float>("对应值"));
							break;
						case Operation.增加:
							floatList.Add(GetInputValue<float>("对应值"));
							break;
						case Operation.清空:
							floatList.Clear();
							break;
					}
					break;
				case 数组.ValueType.整数:
					List<int> Int = GetInputValue<List<int>>("输入");
					r = Int;
					switch (操作)
					{
						case Operation.删除:
							Int.Remove(GetInputValue<int>("对应值"));
							break;
						case Operation.增加:
							Int.Add(GetInputValue<int>("对应值"));
							break;
						case Operation.清空:
							Int.Clear();
							break;
					}
					break;
				case 数组.ValueType.长整数:
					List<long> Long = GetInputValue<List<long>>("输入");
					r = Long;
					switch (操作)
					{
						case Operation.删除:
							Long.Remove(GetInputValue<long>("对应值"));
							break;
						case Operation.增加:
							Long.Add(GetInputValue<long>("对应值"));
							break;
						case Operation.清空:
							Long.Clear();
							break;
					}
					break;
				case 数组.ValueType.短整数:
					List<short> Short = GetInputValue<List<short>>("输入");
					r = Short;
					switch (操作)
					{
						case Operation.删除:
							Short.Remove(GetInputValue<short>("对应值"));
							break;
						case Operation.增加:
							Short.Add(GetInputValue<short>("对应值"));
							break;
						case Operation.清空:
							Short.Clear();
							break;
					}
					break;
				case 数组.ValueType.布尔值:
					List<bool> Bool = GetInputValue<List<bool>>("输入");
					r = Bool;
					switch (操作)
					{
						case Operation.删除:
							Bool.Remove(GetInputValue<bool>("对应值"));
							break;
						case Operation.增加:
							Bool.Add(GetInputValue<bool>("对应值"));
							break;
						case Operation.清空:
							Bool.Clear();
							break;
					}
					break;
				case 数组.ValueType.文本:
					List<string> String = GetInputValue<List<string>>("输入");
					r = String;
					switch (操作)
					{
						case Operation.删除:
							String.Remove(GetInputValue<string>("对应值"));
							break;
						case Operation.增加:
							String.Add(GetInputValue<string>("对应值"));
							break;
						case Operation.清空:
							String.Clear();
							break;
					}
					break;
				case 数组.ValueType.发射器:
					List<Shooting> shooting = GetInputValue<List<Shooting>>("输入");
					r = shooting;
					switch (操作)
					{
						case Operation.删除:
							shooting.Remove(GetInputValue<Shooting>("对应值"));
							break;
						case Operation.增加:
							shooting.Add(GetInputValue<Shooting>("对应值"));
							break;
						case Operation.清空:
							shooting.Clear();
							break;
					}
					break;
				case 数组.ValueType.力场:
					List<Force> force = GetInputValue<List<Force>>("输入");
					r = force;
					switch (操作)
					{
						case Operation.删除:
							force.Remove(GetInputValue<Force>("对应值"));
							break;
						case Operation.增加:
							force.Add(GetInputValue<Force>("对应值"));
							break;
						case Operation.清空:
							force.Clear();
							break;
					}
					break;
				case 数组.ValueType.发射器曲线事件:

					List<BasedEvent_ShootingLocomotion> BasedEvent_shootingLocomotion = GetInputValue<List<BasedEvent_ShootingLocomotion>>("输入");
					r = BasedEvent_shootingLocomotion;
					switch (操作)
					{
						case Operation.删除:
							BasedEvent_shootingLocomotion.Remove(GetInputValue<BasedEvent_ShootingLocomotion>("对应值"));
							break;
						case Operation.增加:
							BasedEvent_shootingLocomotion.Add(GetInputValue<BasedEvent_ShootingLocomotion>("对应值"));
							break;
						case Operation.清空:
							BasedEvent_shootingLocomotion.Clear();
							break;
					}
					break;
				case 数组.ValueType.向量:
					List<Vector3> vector3 = GetInputValue<List<Vector3>>("输入");
					r = vector3;
					switch (操作)
					{
						case Operation.删除:
							vector3.Remove(GetInputValue<Vector3>("对应值"));
							break;
						case Operation.增加:
							vector3.Add(GetInputValue<Vector3>("对应值"));
							break;
						case Operation.清空:
							vector3.Clear();
							break;
					}
					break;
				case 数组.ValueType.子弹曲线事件:
	
					List<BasedEvent_BulletLocomotion> BasedEvent_bulletLocomotion = GetInputValue<List<BasedEvent_BulletLocomotion>>("输入");
					r = BasedEvent_bulletLocomotion;
					switch (操作)
					{
						case Operation.删除:
							BasedEvent_bulletLocomotion.Remove(GetInputValue<BasedEvent_BulletLocomotion>("对应值"));
							break;
						case Operation.增加:
							BasedEvent_bulletLocomotion.Add(GetInputValue<BasedEvent_BulletLocomotion>("对应值"));
							break;
						case Operation.清空:
							BasedEvent_bulletLocomotion.Clear();
							break;
					};
					break;
				case 数组.ValueType.敌人:

					List<Enemy> enemy = GetInputValue<List<Enemy>>("输入");
					r = enemy;
					switch (操作)
					{
						case Operation.删除:
							enemy.Remove(GetInputValue<Enemy>("对应值"));
							break;
						case Operation.增加:
							enemy.Add(GetInputValue<Enemy>("对应值"));
							break;
						case Operation.清空:
							enemy.Clear();
							break;
					};
					break;

				case 数组.ValueType.自绘图案:
					List<CustomedPattern > CustomPattern = GetInputValue<List<CustomedPattern>>("输入");
					r = CustomPattern;
					switch (操作)
					{
						case Operation.删除:
							CustomPattern.Remove(GetInputValue<CustomedPattern>("对应值"));
							break;
						case Operation.增加:
							CustomPattern.Add(GetInputValue<CustomedPattern>("对应值"));
							break;
						case Operation.清空:
							CustomPattern.Clear();
							break;
					};
					break;
				case 数组.ValueType.角色:
					List<Character> character = GetInputValue<List<Character>>("输入");
					r = character;
					switch (操作)
					{
						case Operation.删除:
							character.Remove(GetInputValue<Character>("对应值"));
							break;
						case Operation.增加:
							character.Add(GetInputValue<Character>("对应值"));
							break;
						case Operation.清空:
							character.Clear();
							break;
					};
					break;
				case 数组.ValueType.触发器:
					List<Trigger> trigger = GetInputValue<List<Trigger>>("输入");
					r = trigger;
					switch (操作)
					{
						case Operation.删除:
							trigger.Remove(GetInputValue<Trigger>("对应值"));
							break;
						case Operation.增加:
							trigger.Add(GetInputValue<Trigger>("对应值"));
							break;
						case Operation.清空:
							trigger.Clear();
							break;
					};
					break;
				case 数组.ValueType.贝塞尔曲线:
					List<BezierDrawer> draw = GetInputValue<List<BezierDrawer>>("输入");
					r = draw;
					switch (操作)
					{
						case Operation.删除:
							draw.Remove(GetInputValue<BezierDrawer>("对应值"));
							break;
						case Operation.增加:
							draw.Add(GetInputValue<BezierDrawer>("对应值"));
							break;
						case Operation.清空:
							draw.Clear();
							break;
					};
					break;
				case 数组.ValueType.颜色:
					List<Color> color = GetInputValue<List<Color>>("输入");
					r = color;
					switch (操作)
					{
						case Operation.删除:
							color.Remove(GetInputValue<Color>("对应值"));
							break;
						case Operation.增加:
							color.Add(GetInputValue<Color>("对应值"));
							break;
						case Operation.清空:
							color.Clear();
							break;
					};
					break;
				case 数组.ValueType.精灵纹理:
					List<Sprite> sprite = GetInputValue<List<Sprite>>("输入");
					r = sprite;
					switch (操作)
					{
						case Operation.删除:
							sprite.Remove(GetInputValue<Sprite>("对应值"));
							break;
						case Operation.增加:
							sprite.Add(GetInputValue<Sprite>("对应值"));
							break;
						case Operation.清空:
							sprite.Clear();
							break;
					};
					break;
				case 数组.ValueType.符卡系统:
					List<SpellCard> spl = GetInputValue<List<SpellCard>>("输入");
					r = spl;
					switch (操作)
					{
						case Operation.删除:
							spl.Remove(GetInputValue<SpellCard>("对应值"));
							break;
						case Operation.增加:
							spl.Add(GetInputValue<SpellCard>("对应值"));
							break;
						case Operation.清空:
							spl.Clear();
							break;
					};
					break;
				case 数组.ValueType.关卡流程控制器:
					List<StageControl> ste = GetInputValue<List<StageControl>>("输入");
					r = ste;
					switch (操作)
					{
						case Operation.删除:
							ste.Remove(GetInputValue<StageControl>("对应值"));
							break;
						case Operation.增加:
							ste.Add(GetInputValue<StageControl>("对应值"));
							break;
						case Operation.清空:
							ste.Clear();
							break;
					};
					break;
				case 数组.ValueType.动画播放系统:
					List<Animator> anim = GetInputValue<List<Animator>>("输入");
					r = anim;
					switch (操作)

					{
						case Operation.删除:
							anim.Remove(GetInputValue<Animator>("对应值"));
							break;
						case Operation.增加:
							anim.Add(GetInputValue<Animator>("对应值"));
							break;
						case Operation.清空:
							anim.Clear();
							break;
					};
					break;
				case 数组.ValueType.声音播放系统:
					List<AudioSource> source = GetInputValue<List<AudioSource>>("输入");
					r = source;
					switch (操作)
					{
						case Operation.删除:
							source.Remove(GetInputValue<AudioSource>("对应值"));
							break;
						case Operation.增加:
							source.Add(GetInputValue<AudioSource>("对应值"));
							break;
						case Operation.清空:
							source.Clear();
							break;
					};
					break;
				case 数组.ValueType.弹幕连接器:
					List<Link> link = GetInputValue<List<Link>>("输入");
					r = link;
					switch (操作)
					{
						case Operation.删除:
							link.Remove(GetInputValue<Link>("对应值"));
							break;
						case Operation.增加:
							link.Add(GetInputValue<Link>("对应值"));
							break;
						case Operation.清空:
							link.Clear();
							break;
					};
					break;
				case 数组.ValueType.新版剧情系统:
					List<DialogSystemInit> init = GetInputValue<List<DialogSystemInit>>("输入");
					r = init;
					switch (操作)
					{
						case Operation.删除:
							init.Remove(GetInputValue<DialogSystemInit>("对应值"));
							break;
						case Operation.增加:
							init.Add(GetInputValue<DialogSystemInit>("对应值"));
							break;
						case Operation.清空:
							init.Clear();
							break;
					};
					break;
				case 数组.ValueType.旧版剧情系统:
					List<WrittenControlNew> initN = GetInputValue<List<WrittenControlNew>>("输入");
					r = initN;
					switch (操作)
					{
						case Operation.删除:
							initN.Remove(GetInputValue<WrittenControlNew>("对应值"));
							break;
						case Operation.增加:
							initN.Add(GetInputValue<WrittenControlNew>("对应值"));
							break;
						case Operation.清空:
							initN.Clear();
							break;
					};
					break;
			}
			ConnectDo("退出节点");
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return r; // Replace this
		}
	}
}