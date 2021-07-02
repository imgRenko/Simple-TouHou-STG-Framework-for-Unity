﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
[AddComponentMenu("东方STG框架/框架核心/自机类/自机数据表格")]
public class Character : MonoBehaviour
{
    [Multiline]
    public string CharacterName = "蕾米莉亚";
    public float Speed = 1;
    public float Radius = 0.05f;
    public float BulletDamage = 1f;
    public float ExtraDamage = 0.0f;
    public float DamageDecline = 1.0f;
    public static string PlayerTag = "Rem";
    public List<AudioSource> SoundSource = new List<AudioSource>();
    public List<AudioClip> EffectSound = new List<AudioClip>();
    public Sprite Normal, Deadly, Break;
    public SpriteRenderer PlayerPicture;
    public Animator AnimationController;
    public Vector2 PictrueMaskOffset;
    public Shooting deadEffect;
    public int invincibleTime = 150;
    public bool Invincible = false;
    public bool countGrazeWhenNobody = false;
    public bool ControlAniSpeed = true;
    public bool GameOverRightNow = true;
    [HideInInspector]
    public ObjectPool ObjectPool;
    public float CollectionAllBounsHeight = 1.24f;
    public bool DestroyingBullet = false;
    float invincibleFrame = 0;
    public List<bool> inTrigger = new List<bool>();
    [HideInInspector]
    public PlayerController Controller;
    public delegate IEnumerator PlayerEvent(Character a);
    public event PlayerEvent PlayerDie;
    public float defaultCollisionRadius = 0;
    private float DeadTime;
    [HideInInspector]
    public bool isGone;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
    public void Die()
    {
        if (Invincible == false && Global.WrttienSystem == false)
        {
            --Global.PlayerLive_A;
            if (Global.PlayerLive_A < 0)
            {
                Global.Power = 0;
                if (PlayerDie != null)
                    StartCoroutine(PlayerDie(this));
                if (GameOverRightNow)
                    Global.GameOverScript_A.EnableGameOver();
            }
            isGone = true;
            Global.PlayerMask.Set(CharacterName, Break);
            Global.SpellCard = 3;
            PlaySound(0, 0);
            DestroyingBullet = true;
            invincibleFrame = 0;
            Invincible = true;
            Global.MissCount++;
            if (deadEffect != null)
            {
                Shooting.RecoverShooting(deadEffect, true);
            }
            //Miss后，掉P的代码
            if (Global.Power <= 0)
                return;
            if (Global.Power > 0.1f)
            {
                for (int i = 0; i != 10; i++)
                {
                    Bouns Temp = ObjectPool.ApplyBouns();

                    if (Temp == null)
                        continue;

                    Temp.bounsType = Bouns.BounsType.Power;
                    Vector3 Positon = Vector3.zero;

                    Temp.gameObject.transform.parent.gameObject.SetActive(true);
                    Temp.UseBouns(Positon);
                }
            }
            else
            {
                for (int i = 0; i >= Global.Power / 0.01f; i++)
                {
                    Bouns Temp = ObjectPool.ApplyBouns();

                    if (Temp == null)
                        continue;

                    Temp.bounsType = Bouns.BounsType.Power;
                    Vector3 Positon = Vector3.zero;

                    Temp.gameObject.transform.parent.gameObject.SetActive(true);
                    Temp.UseBouns(Positon);
                }
            }
            Global.Power -= Global.maxPower_A / 8;
            return;
        }
    }
    public void PlaySound(int index, ulong delay)
    {
        if (index > EffectSound.Count)
            return;
        if (EffectSound[index] == null)
            return;
        SoundSource[index].clip = EffectSound[index];
        AudioQueue.Play(SoundSource[index]);
    }
    void Update()
    {
        if (Global.GamePause || Global.isGameover)
            return;
        if (DestroyingBullet)
            invincibleFrame += 1 * Global.GlobalSpeed;
        if (Global.GamePause == false)
            AnimationController.speed = ControlAniSpeed ? Global.GlobalSpeed : 1;
        else
            AnimationController.speed = 0;

        if (invincibleFrame <= invincibleTime / Global.GlobalSpeed && DestroyingBullet)
            StartCoroutine(ObjectPool.DestroyBullets(false));
        else if (invincibleFrame > invincibleTime / Global.GlobalSpeed && DestroyingBullet)
        {
            invincibleFrame = 0;
            DestroyingBullet = false;
            Invincible = false;
        }
        if (isGone)
        {
            DeadTime += 1 * 0.016667f  * Global.GlobalSpeed;
            if (DeadTime >= 3)
            {
                isGone = false;
                DeadTime = 0;
                Global.PlayerMask.Set(CharacterName, Global.PlayerLive_A > 1 ? Normal : Deadly);
            }
        }
        if (gameObject.transform.position.y > CollectionAllBounsHeight)
            ObjectPool.PlayerGetAllBouns();
        if (Global.Power <= 0)
            Global.Power = 0;
        #region  检测触发器
        if (Trigger.TriggerList.Count != 0)
        {
            for (int i = 0; i != Trigger.TriggerList.Count; ++i)
            {
                if (Trigger.TriggerList[i] == null) continue;
                if (Trigger.TriggerList[i].Type == Trigger.TriggerType.Box)
                {
                    bool Result = Bullet.Intersection(gameObject.transform.position, Trigger.TriggerList[i].SquareLength, Trigger.TriggerList[i].gameObject.transform.position, Radius);

                    if (Result && Trigger.TriggerList[i].Use == true)
                    {
                        Trigger.TriggerList[i].PlayerStayTime++;
                        Trigger.TriggerList[i].UseStayEvent(null, Trigger.TriggerList[i].PlayerStayTime, Trigger.TriggerList[i].PlayerEnterTime);
                        Debug.Log("Stay");
                    }
                    if (Result == false && inTrigger[i] == true && Trigger.TriggerList[i].Use == true)
                    {
                        Trigger.TriggerList[i].OnBulletExitFromTrigger(null, null, Trigger.TriggerList[i].PlayerStayTime, Trigger.TriggerList[i].PlayerEnterTime);
                        Trigger.TriggerList[i].PlayerStayTime = 0;
                        if (Trigger.TriggerList[i].OnceTime)
                        {
                            Trigger.TriggerList[i].Use = false;

                        }
                        Debug.Log("Exit");
                    }
                    if (Result == true && inTrigger[i] == false && Trigger.TriggerList[i].Use == true)
                    {
                        Trigger.TriggerList[i].PlayerEnterTime++;
                        Trigger.TriggerList[i].OnBulletEnterIntoTrigger(null, null, Trigger.TriggerList[i].PlayerStayTime, Trigger.TriggerList[i].PlayerEnterTime);

                        if (Trigger.TriggerList[i].OnceTime)
                        {
                            Trigger.TriggerList[i].Use = false;
                        }
                        Debug.Log("Enter");
                    }
                    inTrigger[i] = Result;
                }
                if (Trigger.TriggerList[i].Type == Trigger.TriggerType.Circle)
                {

                    bool Result = Radius + Trigger.TriggerList[i].Radius > Vector2.Distance(gameObject.transform.position, Trigger.TriggerList[i].gameObject.transform.position);
                    if (Result && Trigger.TriggerList[i].Use == true)
                    {
                        Trigger.TriggerList[i].PlayerStayTime++;
                        Trigger.TriggerList[i].UseStayEvent(null, Trigger.TriggerList[i].PlayerStayTime, Trigger.TriggerList[i].PlayerEnterTime);
                        Debug.Log("Stay");
                    }
                    if (Result == false && (inTrigger[i] == true) && Trigger.TriggerList[i].Use == true)
                    {
                        
                        Trigger.TriggerList[i].OnBulletExitFromTrigger(null,null, Trigger.TriggerList[i].PlayerStayTime, Trigger.TriggerList[i].PlayerEnterTime);
                        Trigger.TriggerList[i].PlayerStayTime = 0;
                        if (Trigger.TriggerList[i].OnceTime)
                        {
                            Trigger.TriggerList[i].Use = false;
                        }
                        Debug.Log("Exit");
                    }
                    if (Result == true && inTrigger[i] == false && Trigger.TriggerList[i].Use == true)
                    {
                        Trigger.TriggerList[i].PlayerEnterTime++;
                        Trigger.TriggerList[i].OnBulletEnterIntoTrigger(null, null, Trigger.TriggerList[i].PlayerStayTime, Trigger.TriggerList[i].PlayerEnterTime);

                        if (Trigger.TriggerList[i].OnceTime)
                        {
                            Trigger.TriggerList[i].Use = false;
                        }
                        Debug.Log("Enter");
                    }
                    inTrigger[i] = Result;
                }
                if (Trigger.TriggerList[i].Type == Trigger.TriggerType.Line)
                {
                    float t = Vector2.Dot(Trigger.TriggerList[i].AuxiliaryLinesEnd.transform.position - transform.position,
                        (Trigger.TriggerList[i].AuxiliaryLinesEnd.transform.position - Trigger.TriggerList[i].AuxiliaryLinesStart.transform.position));
                    float ang = Mathf.Abs(Vector2.Angle(Trigger.TriggerList[i].AuxiliaryLinesEnd.transform.position - transform.position,
                        (Trigger.TriggerList[i].AuxiliaryLinesEnd.transform.position - Trigger.TriggerList[i].AuxiliaryLinesStart.transform.position)));
                    float final = t * Mathf.Tan(ang * Mathf.Deg2Rad);
                    bool Result = Radius + Trigger.TriggerList[i].Radius > final;

                    if (Result && Trigger.TriggerList[i].Use == true)
                    {
                        Trigger.TriggerList[i].PlayerStayTime++;
                        Trigger.TriggerList[i].UseStayEvent(null, Trigger.TriggerList[i].PlayerStayTime, Trigger.TriggerList[i].PlayerEnterTime);

                    }
                    if (Result == false && inTrigger[i] == true && Trigger.TriggerList[i].Use == true)
                    {
                        inTrigger[i] = false;
                     
                        Trigger.TriggerList[i].OnBulletExitFromTrigger(null, null, Trigger.TriggerList[i].PlayerStayTime, Trigger.TriggerList[i].PlayerEnterTime);
                        Trigger.TriggerList[i].PlayerStayTime = 0;
                        if (Trigger.TriggerList[i].OnceTime)
                        {
                            Trigger.TriggerList[i].Use = false;
                        }
                    }
                    if (Result == true && inTrigger[i] == false && Trigger.TriggerList[i].Use == true)
                    {
                        Trigger.TriggerList[i].PlayerEnterTime++;
                        Trigger.TriggerList[i].OnBulletEnterIntoTrigger(null, null, Trigger.TriggerList[i].PlayerStayTime, Trigger.TriggerList[i].PlayerEnterTime); 
                        inTrigger[i] = true;
                        if (Trigger.TriggerList[i].OnceTime)
                        {
                            Trigger.TriggerList[i].Use = false;
                        }
                    }
                    //inTrigger[i] = Result;
                }
            }
        }

        #endregion
    }

    void Awake()
    {
        PlayerTag = CharacterName;
        defaultCollisionRadius = Radius;
        Global.PlayerObject = this.gameObject;
        ObjectPool = Global.GameObjectPool_A;
        Global.BulletOrigin.PlayerCharacter = this;
        Global.BulletOrigin.Player = Controller;
        AnimationController = gameObject.GetComponent<Animator>();
        ObjectPool = GameObject.Find("GameAction").GetComponent<ObjectPool>();
        Global.PlayerMask.Offset = PictrueMaskOffset;
        Global.PlayerMask.Set(CharacterName, Normal);
       
    }
    public void WritePlayerData2XML(bool success = false, string XmlfileName = "PlayerData.xml")
    {
        XmlDocument document = new XmlDocument();
        document.AppendChild(document.CreateXmlDeclaration("1.0", "UTF-8", null));
        XmlElement Root = document.CreateElement("PLAYER_DATA");
        document.AppendChild(Root);
        XmlElement Book = document.CreateElement("GameData");
        Book.SetAttribute("Score", Global.Score.ToString());
        Book.SetAttribute("Date", System.DateTime.Now.ToLongDateString());
        Book.SetAttribute("StageIndex", Global.StageIndex.ToString());
        Book.SetAttribute("Character", CharacterName);
        Root.AppendChild(Book);
        document.Save(Application.streamingAssetsPath + @"\PlayerData\" + XmlfileName);
    }
}