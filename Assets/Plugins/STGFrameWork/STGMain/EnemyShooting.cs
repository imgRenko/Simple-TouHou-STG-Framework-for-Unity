using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[AddComponentMenu("东方STG框架/作废组件/敌人发射器")]
[System.Obsolete]
public class EnemyShooting : Shooting  {
 
    EnemyShooting(){
        CaluateDelayToPercent = true;
    }
    void OnDrawGizmosSelected()
    {
        if (transform == null)
            return;
        Gizmos.DrawIcon(transform.position, "ShootingTitle");
        Gizmos.color = Color.red;
        if (transform.parent != null)
            Gizmos.DrawLine(transform.position, transform.parent.transform.position);

        Gizmos.color = Color.yellow;
        Vector2 _tar = Vector2.left;
        _tar = Quaternion.Euler(new Vector3(0, 0, Angle)) * (Vector2)Vector2.up;
        Gizmos.DrawRay(transform.position, _tar * Speed / 5);
        Gizmos.color = Color.blue;
        _tar = Quaternion.Euler(new Vector3(0, 0, MoveDirection)) * (Vector2)Vector2.up;
        Gizmos.DrawRay(transform.position, _tar * ShootingSpeed * 0.4f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Radius);
        if (BulletShootingObject != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, BulletShootingObject.transform.position);
        }

    }
    void Update()
    {
        if (Global.GamePause)
            return;
        if (Global.SpellCardExpressing && ignoreSCExpression == false)
            return;
        if (Global.WrttienSystem && IgnorePlot == false)
            return;
        if (lineIndex > Way)
            lineIndex = Way - 1;
        if (lineIndex < 0)
            lineIndex = 0;
        _soundTimeCount++;
        TotalFrame = TotalFrame + 1 * Global.GlobalSpeed;
        _shootingTimer += 1 * Global.GlobalSpeed;
        UseShootingUsingEvent ();
        if (MoveShooting)
        {
            if (ShootingAcceleratedSpeed != 0)
                ShootingSpeed += ShootingAcceleratedSpeed;
            if (ShootingSpeed != 0)
                gameObject.transform.Translate(Vector3.up * ShootingSpeed / 100 * Global.GlobalSpeed, Space.Self);
            if (RotationSpeed != 0)
                MoveDirection += RotationSpeed;
            gameObject.transform.eulerAngles = new Vector3(0, 0, MoveDirection);
        }
        if (_shootingTimer >= Timer / Global.GlobalSpeed && TotalFrame > Delay)
        {
            if (FollowPlayer)
                Angle = -_angle;
            _shootingTimer = 0;
            if (SpecialBounsShoot + 1 < 0)
                return;
            for (int i = 0; i != SpecialBounsShoot + 1; ++i)
            {
                ShotEnemy(Way, _forceshotRequire);
                if (_forceshotRequire)
                    _forceshotRequire = false;
            }

        }

        if (TotalFrame >= MaxLiveFrame + Delay)
        {
            if (rollBack)
            {
                if (order == EventOrder.BEFORE)
                    ReturnOriginalData();
            }
            if (rollBack)
            {
                if (order == EventOrder.AFTER)
                    ReturnOriginalData();
            }
            if (Reusable == false)
            {
                enabled = false;
                UnprocessingOrginalData = false;
            }
            else
            {
                CountTime = 0;
                UnprocessingOrginalData = false;
                _pass = 0;
                CanShoot = true;
                TotalFrame = 0;
            }
        }
    }
    public List<Bullet> ShotEnemy(int way, bool forceShot = false)
    {
        if (CountTime > ShootingShotMaxTime && ShootingShotMaxTime != -1)
            return null;
        InverseRotation = true;
        BulletRotation = 0;
        if (way < 0)
        {
            Debug.LogError("发射的Way数小于0,这是个非常危险的操作，现已经被拦截。");
            return null;
        }
        if (UseRandomOffset)
        {
            _xOffsetTemp = Random.Range(-XOffset, XOffset);
            _yOffsetTemp = Random.Range(-YOffset, YOffset);
        }
        else
        {
            _xOffsetTemp = 0;
            _yOffsetTemp = 0;
        }
        List<Bullet> bulletCollection = new List<Bullet>();
        if (UseRandomBulletRotation)
        {
            float rotation = Random.Range(-BulletRotationOffset, BulletRotationOffset);
            BulletRotation = rotation;
        }
        if (RandomAcceleratedSpeed)
        {
            float acceleratedSpeedoffset = Random.Range(-AcceleratedSpeedOffset, AcceleratedSpeedOffset);
            AcceleratedSpeed = acceleratedSpeedoffset;
        }
        if (RandomSpeed)
        {
            float speedoffset = Random.Range(-SpeedOffset + Speed, SpeedOffset + Speed);
            Speed = speedoffset;
            Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);
        }
        if (UseRandomWay)
        {
            int i = Random.Range(1, WayOffset);
            Way = i;
            Way = Mathf.Clamp(Way, 1, Way);
        }

        float addRotation = Range / Way;
        if (UseRandomRadius)
        {
            float radius = Random.Range(-RadiusOffset, RadiusOffset);
            Radius = radius;
        }
        if (UseRandomAngle)
        {
            float rotation = Random.Range(-AngleOffset + Angle, AngleOffset + Angle);
            Angle = rotation;
            Angle = Mathf.Clamp(Angle, MinAngle, MaxAngle);
        }
        Angle = Angle % 360;
        Radius += RadiusShotIncrement;
        float radiusT = Radius;
        if (AngleIncreament != 0 && UseRandomAngle == false)
            Angle += AngleIncreament;
        ellipseSize = Quaternion.Euler(new Vector3(0, 0, ellipseScale)) * (Vector3)ellipseSize;
        for (int i = 0; i != way; ++i)
        {
            if (useEllipse == false)
            {
                radiusT += RadiusWayIncrement;
            }
            if (Global.GameObjectPool_A == null)
                break;
            Vector3 postion = this.transform.position;
            if (AvoidPlayer)
            {
                if (Vector2.Distance(postion, Global.PlayerObject.gameObject.transform.position) < AvoidRange)
                    continue;
            }
            EnemyState newEnemy = Global.GameObjectPool_A.ApplyEnemy();
            if (newEnemy == null) continue;
            Bullet newBullet = newEnemy.gameObject.GetComponent<Bullet>();
            if (newBullet == null) continue;
            if (Global.GameObjectPool_A.BulletList.Count != 0)
                Global.GameObjectPool_A.BulletList.Dequeue();
            if (newBullet == null)
                continue;
            if (FollowPlayer)
            {
                Angle = Math2D.GetAimToObjectRotation(gameObject, Global.PlayerObject);
                if (Way % 2 == 0)
                    Angle = Angle + addRotation / 2;
                if (middleIndex)
                {
                    lineIndex = Way / 2;
                    firstLine = false;
                }

                if (firstLine == false)
                    Angle += addRotation * -lineIndex;
            }

            if (UseCustomSprite)
                newBullet.BulletSprite = CustomSprite;
            newBullet.gameObject.transform.position = postion;

            Vector2 _orginal = postion;
            newBullet.Rotation = Angle + i  * addRotation;
            if (useEllipse)
            {
                Vector2 _positon = postion;
                _positon = Quaternion.AngleAxis(ellipseRotation, thisTransform.position) * _positon;
                _positon.x += ellipseSize.x * (1 + Radius) * Mathf.Cos(newBullet.Rotation * Mathf.Deg2Rad);
                _positon.y += ellipseSize.y * (1 + Radius) * Mathf.Sin(newBullet.Rotation * Mathf.Deg2Rad);
                _positon = Quaternion.Euler(0, 0, thisTransform.rotation.eulerAngles.z + ellipseRotation) * _positon;
                _positon.Scale(new Vector3(ellipseScale + 1, ellipseScale + 1, 1));
                newBullet.Rotation = Math2D.GetAimToTargetRotation((Vector2)postion, (Vector2)_positon) + RadiusDirection + Random.Range(-RandomRadiusDirection, RandomRadiusDirection) - Angle;
                newBullet.TargetRotation = newBullet.Rotation;
                newBullet.BulletTransform.position = _positon;
                newBullet.Speed = (Speed + i * SpeedIncreament) * Vector2.Distance(_orginal, _positon);
            }


            if (_Masterbullet != null)
            {
                newBullet.InverseRotation = true;
                newBullet.Rotation = _Masterbullet.Rotation;
            }
            newBullet.gameObject.transform.eulerAngles = new Vector3(0, 0, newBullet.Rotation);
            if (useEllipse == false)
            {
                if (Radius != 0)
                    newBullet.gameObject.transform.Translate(Vector2.up * radiusT, Space.Self);
                newBullet.Speed = Speed + i * SpeedIncreament * GlobalSpeedPercent;
                newBullet.TargetSpeed = BulletTargetSpeed;
            }

            //newBullet.gameObject.SetActive(true);
            //newBullet.AnimationControl.enabled = true;
            if (keepRegularEllipse == true && useEllipse == true)
            {
                Vector2 _point = _orginal;
                _point.x += ellipseSize.x * (1 + Radius) * Mathf.Cos((Angle + i * addRotation) * Mathf.Deg2Rad);
                _point.y += ellipseSize.y * (1 + Radius) * Mathf.Sin((Angle + i * addRotation) * Mathf.Deg2Rad);
                newBullet.Speed = Speed + i * SpeedIncreament + Vector2.Distance(_orginal, _point);
                newBullet.TargetSpeed = BulletTargetSpeed;
            }
            if (Vector2.Distance(Global.PlayerObject.gameObject.transform.position, gameObject.transform.position) <
                         Radius && FollowPlayer == true && DisallowInverse == false)
                newBullet.Speed = -newBullet.Speed;
            if (BulletShootingObject != null)
            {
                Shooting[] temp = Instantiate(BulletShootingObject, newBullet.gameObject.transform).GetComponents<Shooting>();
                newBullet.BulletShooting = temp;
                for (int c = 0; c != temp.Length; ++c)
                {
                    if (temp != null)
                    {
                        temp[c].enabled = true;
                        if (MakeSubBullet)
                        {

                            temp[c].ShootSubBullet = true;
                            temp[c].MasterBulletObject = newBullet.gameObject;
                        }
                       
                        newBullet.BulletShooting[c].gameObject.transform.position = newBullet.gameObject.transform.position;
                        temp[c].gameObject.SetActive(true);
                    }
                    if (FollowAngle)
                    {
                        temp[c].Angle = Angle;
                        newBullet.AddShootingRef(temp[c]);
                    }
                }
            }
            if (ShootSubBullet && MasterBulletObject != null)
            {
                newBullet.gameObject.transform.parent = MasterBulletObject.transform;

                newBullet.AsSubBullet = true;
            }
            AddBulletEvent (newBullet);
            SetBulletState(newBullet);
            if (MaterialIndex >= 0)
                newBullet.BulletSpriteRenderer.material = Global.MaterialCollections_A[MaterialIndex];
   

            newBullet.BrokenBulletColor = BrokenBulletColor;
            if (UseCustomCollisionGroup)
            {
                newBullet.UseCustomCollisionGroup = UseCustomCollisionGroup;
                GameObject T = Instantiate(CustomCollisionGroup);
                newBullet.CustomCollisionGroup = T.GetComponentsInChildren<CustomCollision>();
                T.transform.parent = newBullet.gameObject.transform;
                T.gameObject.transform.localPosition = Vector3.zero;
                T.SetActive(true);
                newBullet.CustomCollisionGroupMainController = T;
            }
            if (newBullet.gameObject.activeSelf && NoCreateAnimation == false)
            {

                newBullet.BulletSpriteRenderer.sprite = CreatingCustomSprite;
                newBullet.BulletSpriteRenderer.color = CreateBulletColor;

                newBullet.BulletSpriteRenderer.gameObject.GetComponent<Animator>().PlayInFixedTime(BulletCreatingAnimationName, 0, 0);
            }

            if (NoCreateAnimation)
            {
                newBullet.BulletSpriteRenderer.gameObject.GetComponent<AnimationController>().PlayAnimation();
            }
            UseCreateEvent (newBullet);
            newBullet.ChangeInverseRotation = ChangeInverseRotation;
            newBullet.BulletBreakingAnimationName = BulletBreakingAnimationName;
            newBullet.Scale = Scale;
            newBullet.BulletSpriteRenderer.gameObject.transform.localScale = Scale;
            Vector3 a = newBullet.transform.position;
            a.x += _xOffsetTemp;
            a.y += _yOffsetTemp;
            Enemy t = newEnemy.SetEnemy(a,ShootingObject,AnimationIndex,EnemyHP);
            t.BounsScoreNumber = BounsScoreNumber;
            t.BounsPowerNumber = BounsPowerNumber;
            t.BounsFullPowerNumber = BounsFullPowerNumber;
            t.BounsLivePieceNumber = BounsLivePieceNumber;

            t.gameObject.SetActive (true);
        }
        return bulletCollection;
    }
}
