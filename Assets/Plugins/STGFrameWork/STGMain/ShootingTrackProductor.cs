using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("东方STG框架/弹幕设计/开发者调试工具/轨迹显示控制中心")]
public class ShootingTrackProductor : MonoBehaviour
{
    public Shooting Tracker;
    public GameObject trackFollower;
    public event Shooting.BulletDelegate BulletEvent;
    public event Shooting.BulletDelegate BulletEventWhenBulletCreate;
    private Bullet _temp;
    private Vector3 Origin;
    private ShootingTrackDisplayer displayer;
    void ResetTimeLayout(TimeLayout[] Layouts)
    {

        foreach (var t in Layouts)
        {
                t.totalFrame = 0;
        }
    }
    void UpdateTimeLayout(TimeLayout[] Layouts) {
     
        foreach (var t in Layouts)
        {
            t.totalFrame++;
            if (t.loop && t.totalFrame >= t.maxFrame)
                t.totalFrame = 0;
        }
    }
    void Cosplay()
    {
        trackFollower.transform.Translate(new Vector2(0, Tracker.Radius), Space.Self);
       // _temp.Speed = Tracker.Speed;
        ///_temp.Rotation = Tracker.Angle;
        BulletEvent[] _l = GetComponents<BulletEvent>();
        TimeLayout[] _timeLayout = GetComponents<TimeLayout>();
        TimeLayout[] _timeLayoutPar = GetComponentsInParent<TimeLayout>(true);
        TimeLayout[] _timeLayoutChlids = GetComponentsInChildren<TimeLayout>(true);
        ResetTimeLayout(_timeLayout);
        ResetTimeLayout(_timeLayoutPar);
        ResetTimeLayout(_timeLayoutChlids);
        foreach (BulletEvent a in _l)
        {
            a.AddEvent();
            BulletEventWhenBulletCreate += a.OnBulletCreated;
            BulletEvent += a.OnBulletMoving;
        }
        _temp.defSpeed = Vector2.up / 100;
        Tracker.SetBulletState(_temp);
        if (BulletEventWhenBulletCreate != null)
            BulletEventWhenBulletCreate(_temp);
        for (int i = 0; i != _temp.MaxLiveFrame; ++i)
        {
            UpdateTimeLayout(_timeLayout);
            UpdateTimeLayout(_timeLayoutPar);
            UpdateTimeLayout(_timeLayoutChlids);
            if (BulletEvent != null)
                BulletEvent(_temp);
            _temp.TotalLiveFrame++;

            if (_temp.TotalLiveFrame > _temp.MaxLiveFrame)
                break;
            _temp.UpdateState();
            displayer.trackPoint.Add(_temp.gameObject.transform.position);
            float tempSpeed = _temp.Speed;
            if (tempSpeed > 0)
            {
                displayer.colorSet.Add(Color32.Lerp(Color.white, Color.red, _temp.Speed / 6));

            }else
                displayer.colorSet.Add(Color32.Lerp(Color.white, Color.blue, -_temp.Speed / 6));

        }
        displayer.transform.localPosition = Vector3.zero;


        BulletEvent = null;
        BulletEventWhenBulletCreate = null;
        DestroyImmediate(_temp.gameObject);
    
    }

    public void StartCaluating(int index)
    {
        trackFollower = (GameObject)GameObject.Instantiate(Resources.Load("Prefeb/EmptyObject"));
        trackFollower.transform.parent = this.transform;
        Tracker.BulletBase = GameObject.Find("TrackBullet");
        _temp = Instantiate(Tracker.BulletBase).GetComponent<Bullet>();
        _temp.BulletTransform = _temp.gameObject.transform;
        if (_temp == null)
        {
            Debug.LogError("未提供子弹原型，请提供子弹原型来计算轨迹！");
            return;
        }
        _temp.gameObject.transform.parent = this.transform;
        _temp.gameObject.transform.localPosition = Vector3.zero;
        displayer = trackFollower.AddComponent<ShootingTrackDisplayer>();
        _temp.gameObject.SetActive(true);
        float addRotation = Tracker.Range / Tracker.Way;
        _temp.BulletIndex = index;
        trackFollower.transform.localPosition = Vector3.zero;
        _temp.Speed = Tracker.Speed;
        // t.transform.eulerAngles = new Vector3(0, 0, index * addRotation);
        _temp.Rotation = Tracker.Angle+(index +1)* addRotation;
        Vector2 _positon = _temp.transform.position;
        Vector2 original =  Tracker.transform.position;
   

        _temp.transform.localRotation = Quaternion.Euler(0, 0, _temp.Rotation);
        if (!Tracker.useEllipse)
            _temp.transform.Translate(Vector2.up * (Tracker.Radius + Tracker.RadiusWayIncrement * index), Space.Self);
        else
        {
            _positon = Quaternion.AngleAxis(Tracker.ellipseRotation, original) * _positon;
            _positon.x = Tracker.ellipseSize.x * (1 + Tracker.Radius) * Mathf.Cos(_temp.Rotation * Mathf.Deg2Rad);
            _positon.y = Tracker.ellipseSize.y * (1 + Tracker.Radius) * Mathf.Sin(_temp.Rotation * Mathf.Deg2Rad);
            _positon = Quaternion.Euler(0, 0, Tracker.transform.rotation.eulerAngles.z + Tracker.ellipseRotation) * _positon;
            _positon.Scale(new Vector3(Tracker.ellipseScale + 1, Tracker.ellipseScale + 1, 1));
            _temp.Rotation = Math2D.GetAimToTargetRotation((Vector2)original, (Vector2)original + _positon) + Tracker.RadiusDirection - Tracker.Angle;
            _temp.TargetRotation = _temp.Rotation;
            _temp.BulletTransform.position = _positon;
            _temp.Speed = (Tracker.Speed + index * Tracker.SpeedIncreament) * Vector2.Distance(original, original + _positon);

           

        }
         Cosplay();
    }
    public void DoPlay()
    {
        for (int i = 0; i !=Tracker.Way; i++)
        {
            StartCaluating(i);
            
        }
      
        DestroyImmediate(this);
    }
}
