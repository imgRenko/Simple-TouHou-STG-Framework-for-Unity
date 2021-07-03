using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 除了Bullet类，此类是触发器、力场、敌人、发射器、激光、玩家、玩家子弹的基类，代表这些类是STG组件
/// </summary>
public class STGComponent : MonoBehaviour
{
    [FoldoutGroup("STG组件更新")]
    [LabelText("用自身组件更新")]
    public bool UpdateWithSelfComponent = true;
    [FoldoutGroup("STG组件更新")]
    [LabelText("组件最大使用时长")]
    public int MaxLiveFrame = 200;

    [HideInInspector]
    public bool ControledByTimeLayout = false;

    public virtual void UpdateInfo() { }

    public Dictionary<string, int> tempIntPairs = new Dictionary<string, int>();

    public Dictionary<string, Shooting> tempShootingPairs = new Dictionary<string, Shooting>();

    public Dictionary<string, Trigger> tempTriggerPairs = new Dictionary<string, Trigger>();

    public Dictionary<string, string> tempStringPairs = new Dictionary<string, string>();

    public Dictionary<string, Force> tempForcePairs = new Dictionary<string, Force>();

    public Dictionary<string, float> tempFloatPairs = new Dictionary<string, float>();

    public Dictionary<string, bool> tempBoolPairs = new Dictionary<string, bool>();

    public Dictionary<string, Vector3> tempVector3Pairs = new Dictionary<string, Vector3>();

    public Dictionary<string, Bullet> tempBulletPairs = new Dictionary<string, Bullet>();

    public Dictionary<string, Enemy> tempEnemyPairs = new Dictionary<string, Enemy>();

    public Dictionary<string, List<LazerShooting>> tempLazerLists = new Dictionary<string, List<LazerShooting>>();

    public Dictionary<string, List<int>> tempIntListPairs = new Dictionary<string, List<int>>();

    public Dictionary<string, List<Shooting>> tempShootingListPairs = new Dictionary<string, List<Shooting>>();

    public Dictionary<string, List<Trigger>> tempTriggerListPairs = new Dictionary<string, List<Trigger>>();

    public Dictionary<string, List<string>> tempStringListPairs = new Dictionary<string, List<string>>();

    public Dictionary<string, List<Force>> tempForceListPairs = new Dictionary<string, List<Force>>();

    public Dictionary<string, List<float>> tempFloatListPairs = new Dictionary<string, List<float>>();

    public Dictionary<string, List<bool>> tempBoolListPairs = new Dictionary<string, List<bool>>();

    public Dictionary<string, LazerShooting> tempLazerPairs = new Dictionary<string, LazerShooting>();

    public Dictionary<string, List<Vector3>> tempVector3ListPairs = new Dictionary<string, List<Vector3>>();

    public Dictionary<string, List<Bullet>> tempBulletListPairs = new Dictionary<string, List<Bullet>>();

    public Dictionary<string, List<Enemy>> tempEnemyListPairs = new Dictionary<string, List<Enemy>>();

    public Dictionary<string, List<AnimationCurve>> tempCurveListPairs = new Dictionary<string, List<AnimationCurve>>();
    public Dictionary<string, AnimationCurve> tempCurvePairs = new Dictionary<string,AnimationCurve> ();

    public void ClearAll() {
        tempIntPairs.Clear();
        tempShootingPairs.Clear();
        tempTriggerPairs.Clear();
        tempStringPairs.Clear();
        tempForcePairs.Clear();
        tempFloatPairs.Clear();
        tempBoolPairs.Clear();
        tempLazerLists.Clear();
        tempIntListPairs.Clear();
        tempShootingListPairs.Clear();
        tempTriggerListPairs.Clear();
        tempStringListPairs.Clear();
        tempForceListPairs.Clear();
        tempFloatListPairs.Clear();
        tempCurvePairs.Clear();
        tempCurveListPairs.Clear();
        tempBoolListPairs.Clear();
        tempLazerPairs.Clear();
        tempVector3ListPairs.Clear();
        tempVector3Pairs.Clear();
        tempBulletListPairs.Clear();
        tempBulletPairs.Clear();
        tempEnemyPairs.Clear();
        tempEnemyListPairs.Clear();
    }
}
