using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Sirenix.OdinInspector;
namespace 变量
{
	public class 数组长度 : 数组类型处理
	{

		public 数组.ValueType 变量类型;
		[Output] public int 长度;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		[Button]
		public void 刷新()
		{
		
			Debug.Log(变量类型);
			if (GetPort("输入") != null)
			{
				RemoveDynamicPort("输入");

			}
			AddDmyInputPort(变量类型, "输入");
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			
			switch (变量类型)
			{
				case 数组.ValueType.双精度浮点:
					return GetInputValue<List<double>>("输入").Count;

				case 数组.ValueType.浮点数:
					return GetInputValue<List<float>>("输入").Count;

				case 数组.ValueType.整数:
					return GetInputValue<List<int>>("输入").Count;

				case 数组.ValueType.长整数:
					return GetInputValue<List<long>>("输入").Count;

				case 数组.ValueType.短整数:
					return GetInputValue<List<short>>("输入").Count;

				case 数组.ValueType.布尔值:
					return GetInputValue<List<bool>>("输入").Count;

				case 数组.ValueType.文本:
					return GetInputValue<List<string>>("输入").Count;

				case 数组.ValueType.发射器:
					return GetInputValue<List<Shooting>>("输入").Count;

				case 数组.ValueType.力场:
					return GetInputValue<List<Force>>("输入").Count;

				case 数组.ValueType.发射器曲线事件:

					return GetInputValue<List<BasedEvent_ShootingLocomotion>>("输入").Count;


				case 数组.ValueType.向量:
					return GetInputValue<List<Vector3>>("输入").Count;

				case 数组.ValueType.子弹曲线事件:
					return GetInputValue<List<BasedEvent_BulletLocomotion>>("输入").Count;

				case 数组.ValueType.敌人:

					return GetInputValue<List<Enemy>>("输入").Count;

			

				case 数组.ValueType.自绘图案:
					return GetInputValue<List<CustomedPattern>>("输入").Count;

				case 数组.ValueType.角色:
					return GetInputValue<List<Character>>("输入").Count;

				case 数组.ValueType.触发器:
					return GetInputValue<List<Trigger>>("输入").Count;

				case 数组.ValueType.贝塞尔曲线:
					return GetInputValue<List<BezierDrawer>>("输入").Count;

				case 数组.ValueType.颜色:
					return GetInputValue<List<Color>>("输入").Count;
				case 数组.ValueType.精灵纹理:
					return GetInputValue<List<Sprite>>("输入").Count;
				case 数组.ValueType.关卡流程控制器:
					return GetInputValue<List<StageControl>>("输入").Count;
				case 数组.ValueType.动画播放系统:
					return GetInputValue<List<Animator>>("输入").Count;
				case 数组.ValueType.声音播放系统:
					return GetInputValue<List<AudioSource>>("输入").Count;
				case 数组.ValueType.弹幕连接器:
					return GetInputValue<List<Link>>("输入").Count;
				case 数组.ValueType.新版剧情系统:
					return GetInputValue<List<DialogSystemInit>>("输入").Count;
				case 数组.ValueType.旧版剧情系统:
					return GetInputValue<List<WrittenControlNew>>("输入").Count;
				case 数组.ValueType.符卡系统:
					return GetInputValue<List<SpellCard>>("输入").Count;
	

			}
			return 0;
		}
	}
}