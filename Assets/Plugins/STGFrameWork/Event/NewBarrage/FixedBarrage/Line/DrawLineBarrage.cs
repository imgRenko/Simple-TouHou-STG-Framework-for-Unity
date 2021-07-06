using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Sirenix.OdinInspector;
[Serializable]
public class DrawLineCondition {
    public bool isEnabled = true;
    public float lerpParm = 0;
}
[AddComponentMenu("东方STG框架/弹幕设计/弹幕特殊化/两点直线动态弹幕")]
public class DrawLineBarrage : MonoBehaviour {
    public Shooting targetShooting;
    [FoldoutGroup("绘画参数", expanded: false)] [Tooltip("发射起始点")] public Vector2 startPoint, endPoint;
    [FoldoutGroup("绘画参数", expanded: false)] [Tooltip("随机平移，向量X代表左右平移，Y代表上下平移")] public Vector2 Offset;
    [FoldoutGroup("绘画参数", expanded: false)] [Tooltip("线性插值步数")] public int stepAmount = 30;
    [FoldoutGroup("绘画参数", expanded: false)]  public float lerpAlpha = 0;
    [FoldoutGroup("绘画参数", expanded: false)] [Tooltip("发射间隔")] public float ShotTimer = 30;
    [FoldoutGroup("绘画参数", expanded: false)] [Tooltip("最大使用时间")] public float MaxFrame = 300;
    [FoldoutGroup("绘画参数", expanded: false)] [Tooltip("初次使用延迟")] public float Delay = 0;
    [FoldoutGroup("绘画条件", expanded: false)] [Tooltip("如果勾选下栏的条件功能无效，就是按照stepAmount来生成不同的条件，以便实现随机弹幕")] public bool randomCondition = false;
    [FoldoutGroup("绘画条件", expanded: false)] [SerializeField] public List<DrawLineCondition> Condition;

    private float _timer = 0, _Total = 0;
    private float _singleStep = 0;
    private Vector2 defaultStartPoint, defaultEndPoint;
    void Start(){
        SortCondition();
    }
    
    DrawLineCondition FindRange(float step) {
        foreach (DrawLineCondition a in Condition) {
            if (a.lerpParm > step)
                return a;
        }
        return null;
    }
    public void SortCondition() {
        Condition.Sort((DrawLineCondition left, DrawLineCondition right) =>
        {
            if (left.lerpParm > right.lerpParm)
                return 1;
            else if (left.lerpParm == right.lerpParm)
                return 0;
            else
                return -1;
        });
    }
    public void AddCondition(float step,bool isEnabled) {
        DrawLineCondition _Condition = new DrawLineCondition();
        _Condition.lerpParm = step;
        _Condition.isEnabled = isEnabled;
        Condition.Add(_Condition);
        SortCondition();
    }
    void Update () {
        _Total += 1 * Global.GlobalSpeed;
        if (_Total < Delay)
            return;
        _timer += 1 * Global.GlobalSpeed;
        if (_Total > MaxFrame + Delay)
            enabled = false;
        if (_timer >= ShotTimer) {
            _timer = 0;  _singleStep = 1.0f / (float)stepAmount;
            float _Alpha = 0; Vector2 ShotPos = Vector2.zero;
            Vector2 _X = new Vector2(UnityEngine.Random.Range(-Offset.x, Offset.x), 0);
            Vector2 _Y = new Vector2(0, UnityEngine.Random.Range(-Offset.y, Offset.y));
            for (int i = 0; i != stepAmount; ++i) {
                if (!randomCondition){
                    DrawLineCondition _Condition = FindRange(_Alpha);
                    _Alpha += _singleStep;
                    if (_Condition == null || _Condition.isEnabled)
                    {

                        ShotPos = Vector2.Lerp(startPoint + _X + _Y, endPoint + _X + _Y, _Alpha);

                        targetShooting.ShotOneBullet(ShotPos);
                    }
                }
                else {
                    ShotPos = Vector2.Lerp(startPoint + _X + _Y, endPoint + _X + _Y, UnityEngine.Random.Range(0, 1f));
                    targetShooting.ShotOneBullet( ShotPos);
                }
             }

        }


    }
}
