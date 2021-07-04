using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Sirenix.OdinInspector;
namespace 变量
{
	public class 获得敌人域变量 : Node
	{
		[Input] public FunctionProgress 进入节点;
		 public string 变量名称;
		[Input] public Enemy 敌人;
	

		[Output] public FunctionProgress 退出节点;

		public object getValue;
		public enum ValueType
		{
			浮点数,
			整数,
			文本,
			激光,
			发射器,
			三维向量,
			子弹,
			布尔,
			触发器,
			力场, 敌人,曲线
		}
		public ValueType 类型;
		public bool 不声明数组 = true;
		[Button]
		public void 刷新()
		{
			if (GetOutputPort("输出") != null)
				RemoveDynamicPort("输出");
			switch (类型)
			{
				case ValueType.三维向量:
					if (不声明数组)
						AddDynamicOutput(typeof(Vector3), ConnectionType.Override, TypeConstraint.None, "输出");
					else
						AddDynamicOutput(typeof(List<Vector3>), ConnectionType.Override, TypeConstraint.None, "输出");
					break;
				case ValueType.整数:
					if (不声明数组)
						AddDynamicOutput(typeof(int), ConnectionType.Override, TypeConstraint.None, "输出");
					else
						AddDynamicOutput(typeof(List<int>), ConnectionType.Override, TypeConstraint.None, "输出");
					break;
				case ValueType.文本:
					if (不声明数组)
						AddDynamicOutput(typeof(string), ConnectionType.Override, TypeConstraint.None, "输出");
					else
						AddDynamicOutput(typeof(List<string>), ConnectionType.Override, TypeConstraint.None, "输出");
					break;
				case ValueType.激光:
					if (不声明数组)
						AddDynamicOutput(typeof(LaserShooting), ConnectionType.Override, TypeConstraint.None, "输出");
					else
						AddDynamicOutput(typeof(List<LaserShooting>), ConnectionType.Override, TypeConstraint.None, "输出");

					break;
				case ValueType.发射器:
					if (不声明数组)
						AddDynamicOutput(typeof(Shooting), ConnectionType.Override, TypeConstraint.None, "输出");
					else
						AddDynamicOutput(typeof(List<Shooting>), ConnectionType.Override, TypeConstraint.None, "输出");
					break;
				case ValueType.子弹:
					if (不声明数组)
						AddDynamicOutput(typeof(Bullet), ConnectionType.Override, TypeConstraint.None, "输出");
					else
						AddDynamicOutput(typeof(List<Bullet>), ConnectionType.Override, TypeConstraint.None, "输出");
					break;
				case ValueType.布尔:
					if (不声明数组)
						AddDynamicOutput(typeof(bool), ConnectionType.Override, TypeConstraint.None, "输出");
					else
						AddDynamicOutput(typeof(List<bool>), ConnectionType.Override, TypeConstraint.None, "输出");
					break;
				case ValueType.触发器:
					if (不声明数组)
						AddDynamicOutput(typeof(Trigger), ConnectionType.Override, TypeConstraint.None, "输出");
					else
						AddDynamicOutput(typeof(List<Trigger>), ConnectionType.Override, TypeConstraint.None, "输出");
					break;
				case ValueType.力场:
					if (不声明数组)
						AddDynamicOutput(typeof(Force), ConnectionType.Override, TypeConstraint.None, "输出");
					else
						AddDynamicOutput(typeof(List<Force>), ConnectionType.Override, TypeConstraint.None, "输出");
					break;
				case ValueType.浮点数:
					if (不声明数组)
						AddDynamicOutput(typeof(float), ConnectionType.Override, TypeConstraint.None, "输出");
					else
						AddDynamicOutput(typeof(List<float>), ConnectionType.Override, TypeConstraint.None, "输出");
					break;
				case ValueType.曲线:
					if (不声明数组)
						AddDynamicOutput(typeof(AnimationCurve), ConnectionType.Override, TypeConstraint.None, "输出");
					else
						AddDynamicOutput(typeof(List<AnimationCurve>), ConnectionType.Override, TypeConstraint.None, "输出");
					break;
			}
		}
		protected override void Init()
		{


		}

		public override void FunctionDo(string PortName, List<object> param = null)
		{
			ConnectDo("退出节点");
			


		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			敌人 = GetInputValue("敌人", 敌人);
			if (!Application.isPlaying)
				return null;
			STGComponent component = (STGComponent)敌人;
			//getValue = GetInputValue<object>("输出");
			//变量名称 = GetInputValue("变量名称", 变量名称);
			switch (类型)
			{

				case ValueType.三维向量:
					Vector3 r = Vector3.zero;
					List<Vector3> ra = new List<Vector3>();
					if (不声明数组)
					{
						if (component.tempVector3Pairs.ContainsKey(变量名称) == true)
						{
							component.tempVector3Pairs.TryGetValue(变量名称, out r);
							getValue = r;
						}
					}
					else
					{
						if (component.tempVector3ListPairs.ContainsKey(变量名称) == true)
						{
							component.tempVector3ListPairs.TryGetValue(变量名称, out ra);
							getValue = ra;
						}
					}
					break;
				case ValueType.整数:
					int rInt = 0;
					List<int> raInt = new List<int>();
					if (不声明数组)
					{
						if (component.tempIntPairs.ContainsKey(变量名称) == true)
						{
							component.tempIntPairs.TryGetValue(变量名称, out rInt);
							getValue = rInt;
						}
					}
					else
					{
						if (component.tempIntListPairs.ContainsKey(变量名称) == true)
						{
							component.tempIntListPairs.TryGetValue(变量名称, out raInt);
							getValue = raInt;
						}
					}
					break;
				case ValueType.文本:
					string rString = "";
					List<string> raString = new List<string>();
					if (不声明数组)
					{
						if (component.tempStringPairs.ContainsKey(变量名称) == true)
						{
							component.tempStringPairs.TryGetValue(变量名称, out rString);
							getValue = rString;
						}
					}
					else
					{
						if (component.tempStringListPairs.ContainsKey(变量名称) == true)
						{
							component.tempStringListPairs.TryGetValue(变量名称, out raString);
							getValue = raString;
						}
					}
					break;
				case ValueType.激光:
					LaserShooting rLazerShooting = null;
					List<LaserShooting> raLazerShooting = new List<LaserShooting>();
					if (不声明数组)
					{
						if (component.tempLazerPairs.ContainsKey(变量名称) == true)
						{
							component.tempLazerPairs.TryGetValue(变量名称, out rLazerShooting);
							getValue = rLazerShooting;
						}
					}
					else
					{
						if (component.tempLazerLists.ContainsKey(变量名称) == true)
						{
							component.tempLazerLists.TryGetValue(变量名称, out raLazerShooting);
							getValue = raLazerShooting;
						}
					}

					break;
				case ValueType.发射器:
					Shooting rShooting = null;
					List<Shooting> raShooting = new List<Shooting>();
					if (不声明数组)
					{
						if (component.tempShootingPairs.ContainsKey(变量名称) == true)
						{
							component.tempShootingPairs.TryGetValue(变量名称, out rShooting);
							getValue = rShooting;
						}
					}
					else
					{
						if (component.tempShootingListPairs.ContainsKey(变量名称) == true)
						{
							component.tempShootingListPairs.TryGetValue(变量名称, out raShooting);
							getValue = raShooting;
						}
					}

					break;
				case ValueType.子弹:
					Bullet rBullet = null;
					List<Bullet> raBullet = new List<Bullet>();
					if (不声明数组)
					{
						if (component.tempBulletPairs.ContainsKey(变量名称) == true)
						{
							component.tempBulletPairs.TryGetValue(变量名称, out rBullet);
							getValue = rBullet;
						}
					}
					else
					{
						if (component.tempBulletListPairs.ContainsKey(变量名称) == true)
						{
							component.tempBulletListPairs.TryGetValue(变量名称, out raBullet);
							getValue = raBullet;
						}
					}
					break;
				case ValueType.布尔:
					bool rBool = true;
					List<bool> raBool = new List<bool>();
					if (不声明数组)
					{
						if (component.tempBoolPairs.ContainsKey(变量名称) == true)
						{
							component.tempBoolPairs.TryGetValue(变量名称, out rBool);
							getValue = rBool;
						}
					}
					else
					{
						if (component.tempBoolListPairs.ContainsKey(变量名称) == true)
						{
							component.tempBoolListPairs.TryGetValue(变量名称, out raBool);
							getValue = raBool;
						}
					}
					break;
				case ValueType.触发器:
					Trigger rTrigger = null;
					List<Trigger> raTrigger = new List<Trigger>();
					if (不声明数组)
					{
						if (component.tempTriggerPairs.ContainsKey(变量名称) == true)
						{
							component.tempTriggerPairs.TryGetValue(变量名称, out rTrigger);
							getValue = rTrigger;
						}
					}
					else
					{
						if (component.tempTriggerListPairs.ContainsKey(变量名称) == true)
						{
							component.tempTriggerListPairs.TryGetValue(变量名称, out raTrigger);
							getValue = raTrigger;
						}
					}
					break;
				case ValueType.力场:
					Force rForce = null;
					List<Force> raForce = new List<Force>();
					if (不声明数组)
					{
						if (component.tempForcePairs.ContainsKey(变量名称) == true)
						{
							component.tempForcePairs.TryGetValue(变量名称, out rForce);
							getValue = rForce;
						}
					}
					else
					{
						if (component.tempForceListPairs.ContainsKey(变量名称) == true)
						{
							component.tempForceListPairs.TryGetValue(变量名称, out raForce);
							getValue = raForce;
						}
					}
					break;
				case ValueType.浮点数:
					float rfloat = 0;
					List<float> rafloat = new List<float>();
					if (不声明数组)
					{
						if (component.tempFloatPairs.ContainsKey(变量名称) == true)
						{
							component.tempFloatPairs.TryGetValue(变量名称, out rfloat);
							getValue = rfloat;
						}
					}
					else
					{
						if (component.tempFloatListPairs.ContainsKey(变量名称) == true)
						{
							component.tempFloatListPairs.TryGetValue(变量名称, out rafloat);
							getValue = rafloat;
						}
					}
					break;
				case ValueType.敌人:
					Enemy rEnemy = null;
					List<Enemy> raEnemy = new List<Enemy>();
					if (不声明数组)
					{
						if (component.tempEnemyPairs.ContainsKey(变量名称) == true)
						{
							component.tempEnemyPairs.TryGetValue(变量名称, out rEnemy);
							getValue = rEnemy;
						}
					}
					else
					{
						if (component.tempEnemyListPairs.ContainsKey(变量名称) == true)
						{
							component.tempEnemyListPairs.TryGetValue(变量名称, out raEnemy);
							getValue = raEnemy;
						}
					}
					break;
				case ValueType.曲线:
					AnimationCurve rCurve = null;
					List<AnimationCurve> raCurve = new List<AnimationCurve>();
					if (不声明数组)
					{
						if (component.tempCurvePairs.ContainsKey(变量名称) == true)
						{
							component.tempCurvePairs.TryGetValue(变量名称, out rCurve);
							getValue = rCurve;
						}
					}
					else
					{
						if (component.tempCurveListPairs.ContainsKey(变量名称) == true)
						{
							component.tempCurveListPairs.TryGetValue(变量名称, out raCurve);
							getValue = raCurve;
						}
					}
					break;
			}
			if (getValue != null)
				return getValue;
			return null; // Replace this
		}
	}
}