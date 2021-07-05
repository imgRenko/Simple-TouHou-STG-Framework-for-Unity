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
    void Cosplay()
    {
        trackFollower.transform.Translate(new Vector2(0, Tracker.Radius), Space.Self);
        _temp.Speed = Tracker.Speed;
        ///_temp.Rotation = Tracker.Angle;
        BulletEvent[] _l = GetComponents<BulletEvent>();
        foreach (BulletEvent a in _l)
        {
            a.AddEvent();
            BulletEventWhenBulletCreate += a.OnBulletCreated;
            BulletEvent += a.OnBulletMoving;
        }
        _temp.defSpeed = Vector2.up / 100 * Global.GlobalSpeed * Global.GlobalBulletSpeed;
        Tracker.SetBulletState(_temp);
        if (BulletEventWhenBulletCreate != null)
            BulletEventWhenBulletCreate(_temp);
        for (int i = 0; i != _temp.MaxLiveFrame; ++i)
        {
            if (BulletEvent != null)
                BulletEvent(_temp);
            _temp.TotalLiveFrame++;

            _temp.UpdateState();
            displayer.trackPoint.Add(_temp.gameObject.transform.localPosition);
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
        // t.transform.eulerAngles = new Vector3(0, 0, index * addRotation);
        _temp.Rotation = Tracker.Angle+(index +1)* addRotation+ Tracker.AngleIncreament *index; ;
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
