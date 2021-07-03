﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerCollider {
    public float Radius = 0.1f;
    public Vector2 Postion;
    public bool Graze = false;
}

public class LazerMovement : MonoBehaviour
{
    public LazerMovementInfo movementInfo;

    public float LazerColliderRadius = 0.1f;

    public float MaxLiveFrame = 200;

    private float totalFrames = 0;

    private float trailLength;

    private bool Use = false;

    private bool stopCalc = false;

    private LineRenderer trailRenderer;

    private Transform thisTransform;

    private List<Vector3> Postions = new List<Vector3>();

    private List<LazerCollider> lazerColliders = new List<LazerCollider>();

    public void DoSpriteChange(Texture2D Texture)
    {
        trailRenderer.material.SetTexture("_MainTex", Texture);
        thisTransform = transform;
    }

    public bool GetOccupiedState()
    {
        return Use;
    }

    public void OccupieLazer(Texture2D initTexture, float trailTime)
    {
        DoSpriteChange(initTexture);
        Use = true;
        totalFrames = 0;
        trailLength = trailTime;
    }
    public void RefleshTrail()
    {
        Postions.Clear();
        lazerColliders.Clear();
        trailRenderer.positionCount = 0;
        trailRenderer.SetPositions(Postions.ToArray());
    }
    private void Unoccupied()
    {
        thisTransform.position = new Vector3(999, 999, 0);
        RefleshTrail();
        Use = false;
        stopCalc = false;
    }

    public void UnoccupieLazer() {
        stopCalc = true;
    }

    // Start is called before the first frame update
    private void Start()
    {
        trailRenderer = GetComponent<LineRenderer>();
    }
    public void AddLazerCollider(Vector2 Pos, float Radius)
    {
        LazerCollider newOne = new LazerCollider();
        newOne.Postion = Pos;
        newOne.Radius = Radius;
        lazerColliders.Add(newOne);
    }
    private void OnDrawGizmos()
    {
        foreach (var Collider in lazerColliders) {
            Gizmos.DrawSphere(Collider.Postion, Collider.Radius);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Global.GamePause || Global.WrttienSystem || !Use) { return; }

        if (Global.PlayerCharacterScript.DestroyingBullet)
            Unoccupied();

        bool finishCalc = totalFrames > MaxLiveFrame || stopCalc;

        if (totalFrames % 2 == 0)
        {
            Vector3 lazerPos = thisTransform.position;
            if (!finishCalc)
                Postions.Add(lazerPos);

            if (totalFrames > trailLength * 60 && Postions.Count != 0)
            {
                Postions.RemoveAt(0);
                //检查碰撞器，如果激光尾部已经移走，检查碰撞器是否有效
                if (lazerColliders.Count > 0 && Postions.Count > 0)
                {
                    LazerCollider a = lazerColliders[0];
                    if (Vector2.Distance(a.Postion, Postions[0]) > a.Radius )
                    {
                        lazerColliders.RemoveAt(0);
                    }
                }
            }

            if (Postions.Count == 0 && finishCalc)
            {
                Unoccupied();
                return;
            }

            // 添加碰撞器，并检查碰撞器是否重合，重合就不添加。
            if (lazerColliders.Count > 1)
            {
                LazerCollider a = lazerColliders[lazerColliders.Count - 1];
                if (Vector2.Distance(a.Postion, lazerPos) > a.Radius)
                {
                    AddLazerCollider(lazerPos, LazerColliderRadius);
                }
            }
            else
            {
                AddLazerCollider(lazerPos, LazerColliderRadius);

            }

            trailRenderer.positionCount = Postions.Count;
            trailRenderer.SetPositions(Postions.ToArray());
        }

        // 检查玩家是否碰到激光
        foreach (var Collider in lazerColliders) {

            Vector2 charPos = Global.PlayerObject.transform.position;
            Character Player = Global.PlayerCharacterScript;
            float radiusTotal = Collider.Radius + Player.Radius;

            if (Vector2.Distance(Collider.Postion, charPos) < radiusTotal)
            {
                Player.Die();
            }
            if (!Collider.Graze && Vector2.Distance(Collider.Postion, charPos) < radiusTotal + 0.4f )
            {
                Player.Controller.UseGrazedEvent();
                Collider.Graze = true;
            }
        }


        totalFrames += Global.GlobalSpeed;
        if (!finishCalc)
            thisTransform.Translate(movementInfo.DoUpdate(), Space.World);

    }
}
