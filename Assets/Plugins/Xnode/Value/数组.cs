using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNode;
namespace 变量
{
	public class 数组 : 数组类型处理
	{
		[Input] public FunctionProgress 进入节点;
		public string 名称;

		public enum ValueType
		{
			浮点数 = 0,
			整数 = 1,
			文本 = 2,
			双精度浮点 = 3,
			长整数 = 4,
			短整数 = 5,
			布尔值 = 6,
			发射器 = 7,
			敌人 = 8,
			向量 = 9,
			颜色 = 10,
			子弹曲线事件 = 11,
			发射器曲线事件 = 12,
			角色 = 13,
			触发器 = 14,
			贝塞尔曲线 = 15,
			自绘图案 = 16,
			游戏对象 = 28,
			力场= 18,
			精灵纹理 = 19,
			旧版剧情系统 = 20,
			新版剧情系统 = 21,
			关卡流程控制器 = 22,
			符卡系统 = 23,
			动画播放系统 = 24,
			声音播放系统 = 25,
			激光 = 26,
			弹幕连接器 = 27
		}
		public ValueType 变量类型;
		[HideInInspector]
		public List<float> floatList = new List<float>();
		[HideInInspector]
		public List<double> doubleList = new List<double>();
		[HideInInspector]
		public List<short> shortList = new List<short>();
		[HideInInspector]
		public List<bool> boolList = new List<bool>();
		[HideInInspector]
		public List<int> intList = new List<int>();
		[HideInInspector]
		public List<string> stringList = new List<string>();
		[HideInInspector]
		public List<long> longList = new List<long>();
		[HideInInspector]
		public List<Vector3> Vector3List = new List<Vector3>();
		[HideInInspector]
		public List<Color> colorList = new List<Color>();
		[HideInInspector]
		public List<BasedEvent_BulletLocomotion> bulletLocomotion = new List<BasedEvent_BulletLocomotion>();
		[HideInInspector]
		public List<BasedEvent_ShootingLocomotion> shootingLocomotion = new List<BasedEvent_ShootingLocomotion>();
		[HideInInspector]
		public List<Character> Characters = new List<Character>();
		[HideInInspector]
		public List<Trigger> Triggers = new List<Trigger>();
		[HideInInspector]
		public List<BezierDrawer> Beziers = new List<BezierDrawer>();
		[HideInInspector]
		public List<CustomedPattern> Patterns = new List<CustomedPattern>();
		//[HideInInspector]
		//public List<EnemyShooting> EnemyShootings = new List<EnemyShooting>();
		[HideInInspector]
		public List<Force> Forces = new List<Force>();
		[HideInInspector]
		public List<Enemy> Enemys = new List<Enemy>();
		[HideInInspector]
		public List<Sprite> Sprites = new List<Sprite>();
		[HideInInspector]
		public List<Shooting> Shootings = new List<Shooting>();
		[HideInInspector]
		public List<WrittenControlNew> WrittensNew = new List<WrittenControlNew>();
		[HideInInspector]
		public List<DialogSystemInit> dialogSys = new List<DialogSystemInit>();
		[HideInInspector]
		public List<StageControl> stageControl = new List<StageControl>();
		[HideInInspector]
		public List< SpellCard> splCar = new List<SpellCard>();
		[HideInInspector]
		public List<Animator> animators = new List<Animator>();
		[HideInInspector]
		public List<AudioSource> audiosource = new List<AudioSource>();
		[HideInInspector]
		public List<Link> links = new List<Link>();
		[HideInInspector]
		public List<GameObject> gameObjects = new List<GameObject>();
		[HideInInspector]
		public int NodeIndex = 0;
		[Output] public FunctionProgress 退出节点;
		// Use this for initialization
		protected override void Init()
		{
			floatList.Clear();
			doubleList.Clear();
			shortList.Clear();
			boolList.Clear();
			intList.Clear();
			stringList.Clear();
			longList.Clear();
			Vector3List.Clear();
			colorList.Clear();
			bulletLocomotion.Clear();
			shootingLocomotion.Clear();
			Characters.Clear();
			Triggers.Clear();
			Beziers.Clear();
			Patterns.Clear();
	
			Forces.Clear();
			Enemys.Clear();
			Shootings.Clear();
			Sprites.Clear();
			WrittensNew.Clear();
			gameObjects.Clear();
			NodeIndex = 0;
			
			foreach (var a in graph.nodes)
			{
				if (this == a)
					break;
				NodeIndex++;
			}
			
				graph.valueNode.Add(名称, NodeIndex);
			Debug.Log("变量初始化完成,Index:" + NodeIndex.ToString());
			
		}
		[Button]
		public void 刷新 (){
			变量类型 = GetInputValue< ValueType>("变量类型", 变量类型);
			if (GetPort("变量值") != null && GetPort("初始化数组") != null)
			{
				RemoveDynamicPort("变量值");
				RemoveDynamicPort("初始化数组");

			}
			AddDmyOutputPort(变量类型,"变量值");
			AddDmyInputPort(变量类型, "初始化数组");
			graph.valueNode.Remove(名称);
			Init(); 

		}
		public object GetList() {
			switch (变量类型)
			{
				case ValueType.双精度浮点:
					return doubleList;

				case ValueType.浮点数:
					return floatList;

				case ValueType.整数:
					return intList;

				case ValueType.长整数:
					return longList;

				case ValueType.短整数:
					return shortList;

				case ValueType.布尔值:
					return boolList;

				case ValueType.文本:
					return stringList;
				case ValueType.力场:
					return Forces;
				case ValueType.发射器:
					return Shootings;
				case ValueType.发射器曲线事件:
					return shootingLocomotion;
				case ValueType.向量:
					return Vector3List;
				case ValueType.子弹曲线事件:
					return bulletLocomotion;
				case ValueType.敌人:
					return Enemys;
				
				case ValueType.自绘图案:
					return Patterns;
				case ValueType.角色:
					return Characters;
				case ValueType.触发器:
					return Triggers;
				case ValueType.贝塞尔曲线:
					return Beziers;
				case ValueType.颜色:
					return colorList;
				case ValueType.精灵纹理:
					return Sprites;
				case 数组.ValueType.关卡流程控制器:
					return stageControl;
					
				case 数组.ValueType.动画播放系统:
					return animators;
				case 数组.ValueType.声音播放系统:
					return audiosource;
				case 数组.ValueType.弹幕连接器:
					return links;
				case 数组.ValueType.新版剧情系统:
					return dialogSys;
				case 数组.ValueType.旧版剧情系统:
					return WrittensNew;
				case 数组.ValueType.符卡系统:
					return splCar;
				case 数组.ValueType.游戏对象:
					return gameObjects;

			}
			return null;
		}
		public void SetList( object Obj)
		{
			switch (变量类型)
			{
				case ValueType.双精度浮点:
					 doubleList = (List<double>)Obj;
					break;
				case ValueType.浮点数:
					  floatList = (List<float>)Obj;
					break;
				case ValueType.整数:
					  intList = (List<int>)Obj;
					break;
				case ValueType.长整数:
					  longList = (List<long>)Obj;
					break;
				case ValueType.短整数:
					  shortList = (List<short>)Obj;
					break;
				case ValueType.布尔值:
					  boolList = (List<bool>)Obj;
					break;
				case ValueType.文本:
					  stringList = (List<string>)Obj; break;
				case ValueType.力场:
					  Forces = (List<Force>)Obj; break;
				case ValueType.发射器:
					  Shootings = (List<Shooting>)Obj; break;
				case ValueType.发射器曲线事件:
					  shootingLocomotion = (List<BasedEvent_ShootingLocomotion>)Obj; break;
				case ValueType.向量:
					  Vector3List = (List<Vector3>)Obj; break;
				case ValueType.子弹曲线事件:
					  bulletLocomotion = (List<BasedEvent_BulletLocomotion>)Obj; break;
				case ValueType.敌人:
					  Enemys = (List<Enemy>)Obj; break;
			
				case ValueType.自绘图案:
					  Patterns = (List<CustomedPattern>)Obj; break;
				case ValueType.角色:
					  Characters = (List<Character>)Obj; break;
				case ValueType.触发器:
					  Triggers = (List<Trigger>)Obj; break;
				case ValueType.贝塞尔曲线:
					  Beziers = (List<BezierDrawer>)Obj; break;
				case ValueType.颜色:
					  colorList = (List<Color>)Obj; break;
				case ValueType.精灵纹理:
					  Sprites = (List<Sprite>)Obj; break;
				case 数组.ValueType.关卡流程控制器:
					 stageControl = (List<StageControl>)Obj; break;

				case 数组.ValueType.动画播放系统:
					 animators = (List<Animator>)Obj; break;
				case 数组.ValueType.声音播放系统:
					 audiosource = (List<AudioSource>)Obj; break;
				case 数组.ValueType.弹幕连接器:
					 links = (List<Link>)Obj; break;
				case 数组.ValueType.新版剧情系统:
					 dialogSys = (List<DialogSystemInit>)Obj; break;
				case 数组.ValueType.旧版剧情系统:
					 WrittensNew = (List<WrittenControlNew>)Obj; break;
				case 数组.ValueType.符卡系统:
					 splCar = (List<SpellCard>)Obj; break;
				case 数组.ValueType.游戏对象:
					gameObjects = (List<GameObject>)Obj; break;
			}
	
		}
		// Return the correct value of an output port when requested
		public override void FunctionDo(string PortName,List<object> param = null) 
		{
			object r = GetInputValue<object>("初始化数组");
			if (r != null)
				SetList(r);
			ConnectDo("退出节点");
			
		}
        public override object GetValue(NodePort port)
        {
			return GetList();
		}
    }
}