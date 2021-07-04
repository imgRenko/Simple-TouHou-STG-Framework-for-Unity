using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Sirenix.OdinInspector;
namespace 变量
{
	public class 声明敌人变量 : Node
	{
		[Input] public FunctionProgress 进入节点;
		[Input] public string 变量名称;
		[Input] public Enemy 敌人;

		[Output] public FunctionProgress 退出节点;
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
			力场,
			敌人,曲线
		}
		public ValueType 类型;
		public bool 不声明数组 = true;
		[Button]
		public void 刷新()
		{
			if (GetInputPort("输入") != null)
				RemoveDynamicPort("输入");
			switch (类型)
			{
				case ValueType.三维向量:
					if (不声明数组)
						AddDynamicInput(typeof(Vector3), ConnectionType.Override, TypeConstraint.None, "输入");
					else
						AddDynamicInput(typeof(List<Vector3>), ConnectionType.Override, TypeConstraint.None, "输入");
					break;
				case ValueType.整数:
					if (不声明数组)
						AddDynamicInput(typeof(int), ConnectionType.Override, TypeConstraint.None, "输入");
					else
						AddDynamicInput(typeof(List<int>), ConnectionType.Override, TypeConstraint.None, "输入");
					break;
				case ValueType.文本:
					if (不声明数组)
						AddDynamicInput(typeof(string), ConnectionType.Override, TypeConstraint.None, "输入");
					else
						AddDynamicInput(typeof(List<string>), ConnectionType.Override, TypeConstraint.None, "输入");
					break;
				case ValueType.激光:
					if (不声明数组)
						AddDynamicInput(typeof(LaserShooting), ConnectionType.Override, TypeConstraint.None, "输入");
					else
						AddDynamicInput(typeof(List<LaserShooting>), ConnectionType.Override, TypeConstraint.None, "输入");

					break;
				case ValueType.发射器:
					if (不声明数组)
						AddDynamicInput(typeof(Shooting), ConnectionType.Override, TypeConstraint.None, "输入");
					else
						AddDynamicInput(typeof(List<Shooting>), ConnectionType.Override, TypeConstraint.None, "输入");
					break;
				case ValueType.子弹:
					if (不声明数组)
						AddDynamicInput(typeof(Bullet), ConnectionType.Override, TypeConstraint.None, "输入");
					else
						AddDynamicInput(typeof(List<Bullet>), ConnectionType.Override, TypeConstraint.None, "输入");
					break;
				case ValueType.布尔:
					if (不声明数组)
						AddDynamicInput(typeof(bool), ConnectionType.Override, TypeConstraint.None, "输入");
					else
						AddDynamicInput(typeof(List<bool>), ConnectionType.Override, TypeConstraint.None, "输入");
					break;
				case ValueType.触发器:
					if (不声明数组)
						AddDynamicInput(typeof(Trigger), ConnectionType.Override, TypeConstraint.None, "输入");
					else
						AddDynamicInput(typeof(List<Trigger>), ConnectionType.Override, TypeConstraint.None, "输入");
					break;
				case ValueType.力场:
					if (不声明数组)
						AddDynamicInput(typeof(Force), ConnectionType.Override, TypeConstraint.None, "输入");
					else
						AddDynamicInput(typeof(List<Force>), ConnectionType.Override, TypeConstraint.None, "输入");
					break;
				case ValueType.浮点数:
					if (不声明数组)
						AddDynamicInput(typeof(float), ConnectionType.Override, TypeConstraint.None, "输入");
					else
						AddDynamicInput(typeof(List<float>), ConnectionType.Override, TypeConstraint.None, "输入");
					break;
				case ValueType.敌人:
					if (不声明数组)
						AddDynamicInput(typeof(Enemy), ConnectionType.Override, TypeConstraint.None, "输入");
					else
						AddDynamicInput(typeof(List<Enemy>), ConnectionType.Override, TypeConstraint.None, "输入");
					break;
				case ValueType.曲线:
					if (不声明数组)
						AddDynamicInput(typeof(AnimationCurve), ConnectionType.Override, TypeConstraint.None, "输入");
					else
						AddDynamicInput(typeof(List<AnimationCurve>), ConnectionType.Override, TypeConstraint.None, "输入");
					break;
			}
		}
		protected override void Init()
		{


		}

		public override void FunctionDo(string PortName, List<object> param = null)
		{敌人 = GetInputValue("敌人",敌人);
			STGComponent component = (STGComponent)敌人;
			object GetValue = GetInputValue<object>("输入");
			变量名称 = GetInputValue("变量名称", 变量名称);
			switch (类型)
			{

				case ValueType.三维向量:
					if (不声明数组)
					{
						if (component.tempVector3Pairs.ContainsKey(变量名称) == false)
							component.tempVector3Pairs.Add(变量名称, (Vector3)GetValue);
						else
						{
							component.tempVector3Pairs.Remove(变量名称);
							component.tempVector3Pairs.Add(变量名称, (Vector3)GetValue);
						}
					}
					else
					{
						if (component.tempVector3ListPairs.ContainsKey(变量名称) == false)
							component.tempVector3ListPairs.Add(变量名称, (List<Vector3>)GetValue);
						else
						{
							component.tempVector3ListPairs.Remove(变量名称);
							component.tempVector3ListPairs.Add(变量名称, (List<Vector3>)GetValue);
						}
					}
					break;
				case ValueType.整数:
					if (不声明数组)
					{
						if (component.tempIntPairs.ContainsKey(变量名称) == false)
							component.tempIntPairs.Add(变量名称, (int)GetValue);
						else
						{
							component.tempIntPairs.Remove(变量名称);
							component.tempIntPairs.Add(变量名称, (int)GetValue);
						}
					}
					else
					{
						if (component.tempIntListPairs.ContainsKey(变量名称) == false)
							component.tempIntListPairs.Add(变量名称, (List<int>)GetValue);
						else
						{
							component.tempIntListPairs.Remove(变量名称);
							component.tempIntListPairs.Add(变量名称, (List<int>)GetValue);
						}
					}
					break;
				case ValueType.文本:
					if (不声明数组)
					{
						if (component.tempStringPairs.ContainsKey(变量名称) == false)
							component.tempStringPairs.Add(变量名称, (string)GetValue);
						else
						{
							component.tempStringPairs.Remove(变量名称);
							component.tempStringPairs.Add(变量名称, (string)GetValue);
						}
					}
					else
					{
						if (component.tempStringListPairs.ContainsKey(变量名称) == false)
							component.tempStringListPairs.Add(变量名称, (List<string>)GetValue);
						else
						{
							component.tempStringListPairs.Remove(变量名称);
							component.tempStringListPairs.Add(变量名称, (List<string>)GetValue);
						}
					}
					break;
				case ValueType.激光:
					if (不声明数组)
					{
						if (component.tempLazerPairs.ContainsKey(变量名称) == false)
							component.tempLazerPairs.Add(变量名称, (LaserShooting)GetValue);
						else
						{
							component.tempLazerPairs.Remove(变量名称);
							component.tempLazerPairs.Add(变量名称, (LaserShooting)GetValue);
						}
					}
					else
					{
						if (component.tempLazerLists.ContainsKey(变量名称) == false)
							component.tempLazerLists.Add(变量名称, (List<LaserShooting>)GetValue);
						else
						{
							component.tempLazerLists.Remove(变量名称);
							component.tempLazerLists.Add(变量名称, (List<LaserShooting>)GetValue);
						}
					}

					break;
				case ValueType.发射器:
					if (不声明数组)
					{
						if (component.tempShootingPairs.ContainsKey(变量名称) == false)
							component.tempShootingPairs.Add(变量名称, (Shooting)GetValue);
						else
						{
							component.tempShootingPairs.Remove(变量名称);
							component.tempShootingPairs.Add(变量名称, (Shooting)GetValue);
						}
					}
					else
					{
						if (component.tempShootingListPairs.ContainsKey(变量名称) == false)
							component.tempShootingListPairs.Add(变量名称, (List<Shooting>)GetValue);
						else
						{
							component.tempShootingListPairs.Remove(变量名称);
							component.tempShootingListPairs.Add(变量名称, (List<Shooting>)GetValue);
						}
					}

					break;
				case ValueType.子弹:
					if (不声明数组)
					{
						if (component.tempBulletPairs.ContainsKey(变量名称) == false)
							component.tempBulletPairs.Add(变量名称, (Bullet)GetValue);
						else
						{
							component.tempBulletPairs.Remove(变量名称);
							component.tempBulletPairs.Add(变量名称, (Bullet)GetValue);
						}
					}
					else
					{
						if (component.tempBulletListPairs.ContainsKey(变量名称) == false)
							component.tempBulletListPairs.Add(变量名称, (List<Bullet>)GetValue);
						else
						{
							component.tempBulletListPairs.Remove(变量名称);
							component.tempBulletListPairs.Add(变量名称, (List<Bullet>)GetValue);
						}
					}
					break;
				case ValueType.布尔:
					if (不声明数组)
					{
						if (component.tempBoolPairs.ContainsKey(变量名称) == false)
							component.tempBoolPairs.Add(变量名称, (bool)GetValue);
						else
						{
							component.tempBoolPairs.Remove(变量名称);
							component.tempBoolPairs.Add(变量名称, (bool)GetValue);
						}
					}
					else
					{
						if (component.tempBoolListPairs.ContainsKey(变量名称) == false)
							component.tempBoolListPairs.Add(变量名称, (List<bool>)GetValue);
						else
						{
							component.tempBoolListPairs.Remove(变量名称);
							component.tempBoolListPairs.Add(变量名称, (List<bool>)GetValue);
						}
					}
					break;
				case ValueType.触发器:
					if (不声明数组)
					{
						if (component.tempTriggerPairs.ContainsKey(变量名称) == false)
							component.tempTriggerPairs.Add(变量名称, (Trigger)GetValue);
						else
						{
							component.tempTriggerPairs.Remove(变量名称);
							component.tempTriggerPairs.Add(变量名称, (Trigger)GetValue);
						}

					}
					else
					{
						if (component.tempTriggerListPairs.ContainsKey(变量名称) == false)
							component.tempTriggerListPairs.Add(变量名称, (List<Trigger>)GetValue);
						else
						{
							component.tempTriggerListPairs.Remove(变量名称);
							component.tempTriggerListPairs.Add(变量名称, (List<Trigger>)GetValue);
						}
					}
					break;
				case ValueType.力场:
					if (不声明数组)
					{
						if (component.tempForcePairs.ContainsKey(变量名称) == false)
							component.tempForcePairs.Add(变量名称, (Force)GetValue);
						else
						{
							component.tempForcePairs.Remove(变量名称);
							component.tempForcePairs.Add(变量名称, (Force)GetValue);
						}
					}
					else
					{
						if (component.tempForceListPairs.ContainsKey(变量名称) == false)
							component.tempForceListPairs.Add(变量名称, (List<Force>)GetValue);
						else
						{
							component.tempForceListPairs.Remove(变量名称);
							component.tempForceListPairs.Add(变量名称, (List<Force>)GetValue);
						}
					}
					break;
				case ValueType.浮点数:
					if (不声明数组)
					{
						if (component.tempFloatPairs.ContainsKey(变量名称) == false)
							component.tempFloatPairs.Add(变量名称, (float)GetValue);
						else
						{
							component.tempFloatPairs.Remove(变量名称);
							component.tempFloatPairs.Add(变量名称, (float)GetValue);
						}
					}
					else
					{
						if (component.tempFloatListPairs.ContainsKey(变量名称) == false)
							component.tempFloatListPairs.Add(变量名称, (List<float>)GetValue);
						else
						{
							component.tempFloatListPairs.Remove(变量名称);
							component.tempFloatListPairs.Add(变量名称, (List<float>)GetValue);
						}
					}
					break;
				case ValueType.敌人:
					if (不声明数组)
					{
						if (component.tempEnemyPairs.ContainsKey(变量名称) == false)
							component.tempEnemyPairs.Add(变量名称, (Enemy)GetValue);
						else
						{
							component.tempEnemyPairs.Remove(变量名称);
							component.tempEnemyPairs.Add(变量名称, (Enemy)GetValue);
						}
					}
					else
					{
						if (component.tempEnemyListPairs.ContainsKey(变量名称) == false)
							component.tempEnemyListPairs.Add(变量名称, (List<Enemy>)GetValue);
						else
						{
							component.tempEnemyListPairs.Remove(变量名称);
							component.tempEnemyListPairs.Add(变量名称, (List<Enemy>)GetValue);
						}
					}
					break;
				case ValueType.曲线:
					if (不声明数组)
					{
						if (component.tempCurvePairs.ContainsKey(变量名称) == false)
							component.tempCurvePairs.Add(变量名称, (AnimationCurve)GetValue);
						else
						{
							component.tempCurvePairs.Remove(变量名称);
							component.tempCurvePairs.Add(变量名称, (AnimationCurve)GetValue);
						}
					}
					else
					{
						if (component.tempCurveListPairs.ContainsKey(变量名称) == false)
							component.tempCurveListPairs.Add(变量名称, (List<AnimationCurve>)GetValue);
						else
						{
							component.tempCurveListPairs.Remove(变量名称);
							component.tempCurveListPairs.Add(变量名称, (List<AnimationCurve>)GetValue);
						}
					}
					break;
			}
			ConnectDo("退出节点");

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}
	}
}