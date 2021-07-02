///  TouHou Project Unity STG FrameWork
///  Renko's Code 
///  2019 Created.
using System.Collections;
using UnityMonoBehaviour = UnityEngine.MonoBehaviour;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[RequireComponent(typeof(BezierSpline))]
[ExecuteInEditMode]
[AddComponentMenu("东方STG框架/弹幕设计/弹幕特殊化/贝塞尔曲线轨迹绘画器")]
public class BezierDrawer : MonoBehaviour
{
 //   public LineRenderer lineRenderer;
    [LabelText("绘画精度")]
    public int vertexCount = 500;
    [LabelText("曲线循环一周时长")]
    public float maxFrame = 50;
    [LabelText("自动获取发射器")]
    public bool autoGet = true;


    [LabelText("不显示演算过程")]
    public bool HideCalutateProgress;
    [LabelText("作为发射形状设计器")]
    public bool AsTrackSpawn;
    [ShowIf("AsTrackSpawn")]
    [LabelText("动画曲线")]
    public AnimationCurve animationCurve;
    [ShowIf("AsTrackSpawn")]
    [LabelText("以帧数计算")]
    public bool calculateFrame;
    [LabelText("规整图像")]
    [ShowIf("AsTrackSpawn")]
    public bool regularPattern;

    public BezierSpline bezierSpline;
    /// <summary>
    /// 生成轨迹数据
    /// </summary>
    [Button]
    [ShowIf("AsTrackSpawn")]
    public void SpawnTrackData()
    {
        if (AsTrackSpawn == false)
        {
            Debug.LogError("必须先允许此组件作为发射形状设计器，并完成相关设置以后继续");
            return;
        }
        TrackSpawn spawn = GetComponent<TrackSpawn>();
        if (spawn == null)
            spawn = gameObject.AddComponent<TrackSpawn>();
        TrackInfo trackInfo = new TrackInfo();
        spawn.trackInfos.Add(trackInfo);
        pointList = GetPointList();
        foreach (var a in pointList) {
            trackInfo.trackDatas.Add(new TrackData
            {
                targetPos = a,
                animationCurve = animationCurve,
                calculateFrame = calculateFrame,
                regularPattern = regularPattern
            }) ;
        }
    }
    [Button]
    [ShowIf("AsTrackSpawn")]
    public void SpawnBulletTrackData()
    {

        TrackBulletEvent spawn = GetComponent<TrackBulletEvent>(); //= new TrackBulletEvent();
        if (spawn == null)
            spawn = gameObject.AddComponent<TrackBulletEvent>();
        TrackInfo trackInfo = new TrackInfo();
        pointList = GetPointList();
        spawn.trackInfos.Add(trackInfo);
        foreach (var a in pointList)
        {
            trackInfo.trackDatas.Add(new TrackData
            {
                targetPos = a,
                animationCurve = animationCurve,
                calculateFrame = calculateFrame,
                regularPattern = regularPattern
            });
        }
    }
    private List<Vector3> GetPointList() {
        List<Vector3> pos = new List<Vector3>();
        bezierSpline = GetComponent<BezierSpline>();
        if (bezierSpline == null)
            return null;
        for (int i = 0; i != vertexCount; ++i) {
            float e = 1.0f / (float)vertexCount;
            pos.Add(bezierSpline.GetPoint(e * i));


        }
        return pos;
    }
    private Shooting[] TargetShooting;
    private List<Vector3> pointList;
    private void Start()
    {
        if (autoGet)
            TargetShooting = GetComponents<Shooting>();
        pointList = new List<Vector3>();
        bezierSpline = GetComponent<BezierSpline>();
    }
    private void Update()
    {

        if (TargetShooting != null)
        foreach (Shooting a in TargetShooting) {
                a.BezierCurveController = this;
        }
        //lineRenderer.positionCount = pointList.Count;
        //lineRenderer.SetPositions(pointList.ToArray());
    }
 
}