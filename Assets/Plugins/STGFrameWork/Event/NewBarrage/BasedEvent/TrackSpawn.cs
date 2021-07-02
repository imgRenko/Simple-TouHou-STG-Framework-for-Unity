using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[AddComponentMenu("东方STG框架/弹幕设计/常见事件/发射器事件/发射器事件(轨迹数据)")]
public class TrackSpawn : ShootingEvent {
	[LabelText("以发射器序号应用子弹轨迹")]
	public bool NoBatch = true;
    [LabelText("子弹轨迹数据")]
    [SerializeField]
    public List<TrackInfo> trackInfos = new List<TrackInfo>();

    [LabelText("时间处理图层")]
    public TimeLayout timeLayout;

    [LabelText("时间比例自变量")]
    public bool timePercent = false;

    int te = 0;
   // int re = 0;
    public override IEnumerator OnShootingFinishAllShotTasksDelay(Shooting Target)
    {
        return base.OnShootingFinishAllShotTasksDelay(Target);
    }
    // Use this for initialization
    public override void OnShootingFinishAllShotTasks(Shooting Target)
    {

        float t = 0;
        foreach (var info in trackInfos)
        {
            if (timeLayout != null)
                continue;
            if (timePercent)
                t = Target.TotalFrame / Target.MaxLiveFrame;
            else
                t = Target.TotalFrame;
        }
        List<TrackData> trackUsingDatas = new List<TrackData>();
        TrackInfo Info = new TrackInfo();
        List<float> x = new List<float>();
        List<float> y = new List<float>();
        /// 需要保证trackinfo从大到小排序
        foreach (var info in trackInfos)
        {
            if (t > info.UsingTime)
            {

                trackUsingDatas = info.trackDatas;
                Info = info;
                continue;
            }

        }
        if (Info.AutoGetCenter)
        {
            foreach (var a in Info.trackDatas)
            {
                x.Add(a.targetPos.x);
                y.Add(a.targetPos.y);
            }
            float totalX = 0, totalY = 0;
            foreach (var a in x)
            {
                totalX += a;
            }
            foreach (var b in y)
            {
                totalY += b;
            }
            totalX /= x.Count;
            totalY /= y.Count;
            Info.patternCenter = new Vector2(totalX, totalY);
        }

        if (trackUsingDatas.Count == 0)
            return;
        if (NoBatch)
        {

            foreach (var bullet in Target.ShotBullet)
            {
                bullet.trackData = trackUsingDatas[bullet.BulletIndex];
                bullet.originalPatternPos = Info.patternCenter;
                bullet.MoveTargetLerp();
            }
            //   Debug.Log(Target.bulletIndexChecking);
        }
        else
        {
            foreach (var bullet in Target.ShotBullet)
            {

                if (te > trackUsingDatas.Count - 1)
                    te = 0;
                //  if (re >= Target.ShotBullet.Count - 1)
                //      re -= Target.ShotBullet.Count - 1;
                bullet.trackData = trackUsingDatas[te];

                bullet.originalPatternPos = Info.patternCenter;

                bullet.MoveTargetLerp();

                //   Debug.Log(te);
                te++;
                //  re++;
            }
        }
    }
    private void OnDrawGizmos()
    {
        foreach (var a in trackInfos)
        {
            if (a.displayInGizmos)
            Gizmos.DrawSphere(a.patternCenter, 0.1f);
        }
    }
 
    public override void EventStart(Shooting[] Target)
    {
        trackInfos.Sort((TrackInfo x, TrackInfo y) => { return x.UsingTime.CompareTo(y.UsingTime); });
    }
}
[System.Serializable]
public class TrackInfo {
    [SerializeField]
    public List<TrackData> trackDatas = new List<TrackData>();
    public float UsingTime = 0;
    public bool AutoGetCenter = true;
    [HideIf("AutoGetCenter")]
    public Vector3 patternCenter;

    public bool displayInGizmos = false;
}
