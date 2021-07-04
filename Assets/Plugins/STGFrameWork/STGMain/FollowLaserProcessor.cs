using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLaserProcessor : STGComponent
{
    public bool Use = false;

    public AnimationCurve widthAnimationCurve = AnimationCurve.EaseInOut(0,0,0.2f,1);

    public float totalFrames;

    private List<Vector3> Positions = new List<Vector3>();

    private List<GameObject> trackObject = new List<GameObject>();

    private LineRenderer lineRenderer;

    private void OnDrawGizmos()
    {
        if (trackObject.Count <= 1)
            return;
        for (int i = 1; i != trackObject.Count; ++i)
        {

            Vector2 a = trackObject[i - 1].transform.position;
            Vector2 b = trackObject[i].transform.position;
            Gizmos.DrawLine(a, b);

        }
    }

    public void SetTrackObject(List<GameObject> trackObjs)
    {
        trackObject = trackObjs;
        InitPos();
    }

    public void AddTrackGameObject(GameObject gameObject)
    {
        if (!trackObject.Contains(gameObject))
        {
            trackObject.Add(gameObject);
            Positions.Add(gameObject.transform.position);
        }
    }

    public void RemoveTrackGameObject(GameObject gameObject)
    {
        if (trackObject.Contains(gameObject))
        {
            int Index = trackObject.FindIndex((GameObject x) => { return x == gameObject; });
            trackObject.Remove(gameObject);
            Positions.RemoveAt(Index);
        }
    }

    private void InitPos() {
        Positions.Clear();
        foreach (var Obj in trackObject)
        {
            Positions.Add(Obj.transform.position);
        }
    }

    public void ClearAllTrackObject()
    {
        trackObject.Clear();
    }

    public void DestroyLaser() {
        Use = false;
        trackObject.Clear();
        UpdatePos();
        widthAnimationCurve = AnimationCurve.EaseInOut(0, 0, 0.2f, 1);
        lineRenderer.positionCount = 0;
        totalFrames = 0;
        lineRenderer.SetPositions(Positions.ToArray());
    }

    private void UpdatePos() {

        for (int i = 0; i != trackObject.Count; ++i) {
            Positions[i] = trackObject[i].transform.position;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
       
        InitPos();
        UpdatePos();
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.GamePause || Global.WrttienSystem || Use == false) {
            return;
        }

        totalFrames += Global.GlobalSpeed;
        if (totalFrames > MaxLiveFrame || trackObject.Count <= 1)
            DestroyLaser();
        lineRenderer.widthMultiplier = widthAnimationCurve.Evaluate(totalFrames/MaxLiveFrame);
        UpdatePos();
        lineRenderer.positionCount = trackObject.Count;
        lineRenderer.SetPositions(Positions.ToArray());
        CheckCollider();
    }

    public void CheckCollider() {
        if (Positions.Count <= 1)
            return;
        Character Player = Global.PlayerCharacterScript;
        Vector2 charPos = Player.gameObject.transform.position;
        float radius = Player.Radius;
        for (int i = 1; i != Positions.Count; ++i) {

            Vector2 a = Positions[i - 1];
            Vector2 b = Positions[i];
            bool isIntersect = Math2D.IsCircleIntersectLineSeg(charPos.x, charPos.y, Player.Radius, a.x, a.y, b.x, b.y);
            if (isIntersect)
                Player.Die();
            
        }
    }
}
