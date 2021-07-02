using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class TrackBulletEvent : BulletEvent {
    [SerializeField]
    public List<TrackInfo> trackInfos = new List<TrackInfo>();

    [LabelText("使用同批次子弹序号处理 ")]
    public bool bulletIndex = true;

    [LabelText("时间处理图层")]
    public TimeLayout timeLayout;

    [LabelText("时间比例自变量")]
    public bool timePercent = false;
    private void OnDrawGizmos()
    {
        foreach (var a in trackInfos)
        {
            if (a.displayInGizmos)
            {
                Gizmos.DrawWireSphere(a.patternCenter, 0.1f);
                for (int i = 0; i != a.trackDatas.Count; ++i)
                {
                    if (i + 1 <= a.trackDatas.Count - 1)
                    {
                        Gizmos.DrawLine(a.trackDatas[i].targetPos , a.trackDatas[i+1].targetPos);

                    }
                }
            }
        }
    }
    public override void OnBulletMoving(Bullet Target)
    {
        float t = 0;

        if (timePercent)
            t = Target.TotalLiveFrame / Target.MaxLiveFrame;
        else
            t = Target.TotalLiveFrame;
        
        List<TrackData> trackUsingDatas = new List<TrackData>();
        for (int i = 0; i != trackInfos.Count; ++i) {
            if (t > trackInfos[i].UsingTime && !Target.trackInfoSign[i])
            {
                trackUsingDatas = trackInfos[i].trackDatas;
                TrackInfo Info = new TrackInfo();
                List<float> x = new List<float>();
                List<float> y = new List<float>();
                Info = trackInfos[i];
                if (trackUsingDatas.Count == 0)
                    return;
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
                if (bulletIndex)
                    Target.trackData = trackUsingDatas[Target.BulletIndex];
                else
                    Target.trackData = trackUsingDatas[Target.shootingIndex];

                Target.originalPatternPos = trackInfos[i].patternCenter;

                Target.MoveTargetLerp();

                Target.trackInfoSign[i] = true;
            }

        }

    }
    public override void OnBulletCreated(Bullet Target)
    {
        Target.OriSpd = Target.Speed;
        trackInfos.Sort((TrackInfo x, TrackInfo y) => { return x.UsingTime.CompareTo(y.UsingTime); });
        foreach (var a in trackInfos) {
            Target.trackInfoSign.Add(false);
        }
    }
}
