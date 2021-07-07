using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using Unity.Collections;
// ---------------------- 对象池 （ObjectPool） ----------------------
// 对象池技术限制内存输出，对需要不断创建对象的STG，具有良好的性能优化效果
// 2015 年 Kutaka
// ------------------------------------------------------------------
[AddComponentMenu("东方STG框架/框架核心/对象池")]
public class ObjectPool : MonoBehaviour
{
    public int MaxBulletNumber = 3000;
    public int BounsNumber = 300;
    public int EnemyNumber = 45;
    public int PlayerBulletNumbet = 45;
    public int LaserAmount = 50;
    public GameObject BulletBased;     // 子弹的原型。 
    public GameObject BounsBased;     // 奖励的原型。 
    public GameObject PlayerBulletBased;
    public GameObject LaserBased;
    public GameObject FollowLaserBased;
    public EnemyState EnemyBased;

    public bool UseJob = true;
    public int JobBatch = 1;
    [HideInInspector]
    public Queue<Bullet> BulletList = new Queue<Bullet>();
    [HideInInspector]
    public List<Bullet> BulletList_State = new List<Bullet>();
    [HideInInspector]
    public List<Bouns> BounsList = new List<Bouns>();
    [HideInInspector]
    public List<EnemyState> EnemyList = new List<EnemyState>();
    [HideInInspector]
    public List<LaserMovement> LaserMovementList = new List<LaserMovement>();
    [HideInInspector]
    public List<PlayerBullet> PlayerBulletList = new List<PlayerBullet>();
    public List<TriggerReceiver> ExtraCheckingTse = new List<TriggerReceiver>();
    public List<FollowLaserProcessor> FollowLaserProcessorList = new List<FollowLaserProcessor>();
    public static List<TriggerReceiver> ExtraChecking = new List<TriggerReceiver>();

    //int index = 0;
    // Use this for initialization
    // 此部分在编辑器模式不适用，在玩家在打包游戏后游玩的过程中，用于减少玩家读取时间。
    T CreateObj<T>(bool Chlid, string Name, GameObject based, float z = 0)
    {
        GameObject New = Instantiate(based);
        New.name = Name;
        New.SetActive(true);
        New.gameObject.transform.position = new Vector3(999, 999, z);

        New.transform.parent = based.transform.parent;
        T c;
        if (!Chlid)
            c = New.GetComponent<T>();
        else
            c = New.GetComponentInChildren<T>(true);
        return c;
    }


    IEnumerator InitFrameBullet()
    {
        // 一帧30个子弹
        for (int p = 0; p != MaxBulletNumber / 10; ++p)
        {
            for (int i = 0; i != 10; ++i)
            {
                Bullet c = CreateObj<Bullet>(false, "Bullet" + p * 30 + i.ToString(), BulletBased);
                c.ID = p * 5 + i;
                c.Use = false;
                Global.GameObjectPool_A.BulletList.Enqueue(c);
                Global.GameObjectPool_A.BulletList_State.Add(c);

            }
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator InitBouns()
    {

        for (int p = 0; p != BounsNumber / 5; ++p)
        {
            for (int i = 0; i != 5; ++i)
            {
                Bouns c = CreateObj<Bouns>(true, "Bouns" + p * 5 + i.ToString(), BounsBased, 0 - (0.1f * i));
                Global.GameObjectPool_A.BounsList.Add(c);

            }
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator InitBullet()
    {

        for (int p = 0; p != PlayerBulletNumbet / 10; ++p)
        {
            for (int i = 0; i != 10; ++i)
            {
                PlayerBullet c = CreateObj<PlayerBullet>(false, "PlayerBullet" + p * 10 + i.ToString(), PlayerBulletBased);
                Global.GameObjectPool_A.PlayerBulletList.Add(c);
            }
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator InitLaser()
    {

        for (int p = 0; p != LaserAmount / 5; ++p)
        {
            for (int i = 0; i != 5; ++i)
            {
                LaserMovement c = CreateObj<LaserMovement>(false, "LaserMovement" + p*5+i.ToString(), LaserBased);
                Global.GameObjectPool_A.LaserMovementList.Add(c);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator InitFollowLaser()
    {

        for (int p = 0; p != LaserAmount / 5; ++p)
        {
            for (int i = 0; i != 5; ++i)
            {
                FollowLaserProcessor c = CreateObj<FollowLaserProcessor>(false, "FollowLaserInfo" + p * 5 + i.ToString(), FollowLaserBased);
                Global.GameObjectPool_A.FollowLaserProcessorList.Add(c);
            }
            yield return new WaitForEndOfFrame();
        }
    }
    void Start()
    {
#if UNITY_EDITOR
        for (int i = 0; i != MaxBulletNumber; ++i)
        {
            Bullet c = CreateObj<Bullet>(false, "Bullet" + i.ToString(), BulletBased);
            c.ID = i;
            c.Use = false;
            Global.GameObjectPool_A.BulletList.Enqueue(c);
            Global.GameObjectPool_A.BulletList_State.Add(c);
        }
        for (int i = 0; i != PlayerBulletNumbet; ++i)
        {
            PlayerBullet c = CreateObj<PlayerBullet>(false, "PlayerBullet" + i.ToString(), PlayerBulletBased);
            Global.GameObjectPool_A.PlayerBulletList.Add(c);
        }
        for (int i = 0; i != BounsNumber; ++i)
        {
            Bouns c = CreateObj<Bouns>(true, "Bouns" + i.ToString(), BounsBased, 0 - (0.1f * i));
            Global.GameObjectPool_A.BounsList.Add(c);
        }
        for (int i = 0; i != LaserAmount; ++i)
        {
            LaserMovement c = CreateObj<LaserMovement>(false, "LaserMovement" + i.ToString(), LaserBased);

            Global.GameObjectPool_A.LaserMovementList.Add(c);
        }
        for (int i = 0; i != LaserAmount; ++i)
        {
            FollowLaserProcessor c = CreateObj<FollowLaserProcessor>(false, "FollowLaserInfo" + i.ToString(), FollowLaserBased);
            Global.GameObjectPool_A.FollowLaserProcessorList.Add(c);
        }


#else
        StartCoroutine(InitFrameBullet());
        StartCoroutine(InitBouns());
        StartCoroutine(InitBullet());
           StartCoroutine(InitLaser());
                 StartCoroutine(InitFollowLaser());
#endif
        for (int i = 0; i != EnemyNumber; ++i)
        {
            EnemyState c = CreateObj<EnemyState>(true, "Enemy" + i.ToString(), EnemyBased.gameObject);
            c.gameObject.SetActive(false);
            Global.GameObjectPool_A.EnemyList.Add(c);
        }

    }
    /// <summary>
    /// 申请子弹，并将所得的子弹返回。
    /// </summary>
    /// <returns></returns>
    public Bullet ApplyBullet()
    {
        if (BulletList.Count <= 0) return null;
        return BulletList.ElementAt(0);
    }
    public Bouns ApplyBouns()
    {
        for (int i = 0; i != BounsList.Count; ++i)
        {
            if (BounsList[i] == null)
                continue;
            if (BounsList[i].Use == false)
                return BounsList[i];
        }
        return null;
    }
    public EnemyState ApplyEnemy()
    {
        for (int i = 0; i != EnemyList.Count; ++i)
        {
            if (EnemyList[i] == null)
                continue;
            if (EnemyList[i].Used == false)
            {
                EnemyList[i].UseAnimationController(EnemyList[i].ClipFileIndex);
                return EnemyList[i];
            }
        }
        return null;
    }
    public LaserMovement ApplyLaserMovement()
    {
        for (int i = 0; i != LaserMovementList.Count; ++i)
        {
            if (LaserMovementList[i] == null)
                continue;
            if (LaserMovementList[i].GetOccupiedState() == false)
            {
                return LaserMovementList[i];
            }
        }
        return null;
    }
    public PlayerBullet ApplyPlayerBullet()
    {
        for (int i = 0; i != PlayerBulletList.Count; ++i)
        {
            if (PlayerBulletList[i] == null)
                continue;
            if (PlayerBulletList[i].Used == false)
            {
                return PlayerBulletList[i];
            }
        }
        return null;
    }
    public FollowLaserProcessor ApplyFollowLaserProcessor()
    {
        for (int i = 0; i != FollowLaserProcessorList.Count; ++i)
        {
            if (FollowLaserProcessorList[i] == null)
                continue;
            if (FollowLaserProcessorList[i].Use == false)
            {
                return FollowLaserProcessorList[i];
            }
        }
        return null;
    }
    public void PlayerGetAllBouns()
    {
        for (int i = 0; i != BounsList.Count; ++i)
        {
            if (BounsList[i] == null)
                continue;
            if (BounsList[i].Use == false)
                continue;
            BounsList[i].ClosePlayer = true;
            BounsList[i].Fast = true;
        }
        return;
    }

    public IEnumerator DestroyBullets(bool NoCondition, bool bouns = false, float delay = 0f)
    {
        int o = 0;
        yield return new WaitForSeconds(delay);
        for (int i = 0; i != BulletList_State.Count; ++i)
        {
            if (NoCondition)
            {
                if (BulletList_State[i].Use == false || BulletList_State[i].InvaildDestroy == true)
                    continue;
            }
            else
            {
                if (BulletList_State[i].Use == false || BulletList_State[i].DonDestroy || BulletList_State[i].InvaildDestroy == true)
                    continue;
            }
            if (NoCondition)
                ++o;
            BulletList_State[i].DestroyBullet();
        }
        if (NoCondition && bouns)
            Global.AddPlayerScore(o * 200);
    }
    void Update()
    {

        ExtraCheckingTse = ExtraChecking;

        if (Global.WrttienSystem || Global.GamePause || Global.GlobalSpeed == 0)
            return;
        if (!UseJob)
            return;
    }
    public void DestroyEnemy()
    {
        for (int i = 0; i != EnemyList.Count; ++i)
        {

            if (EnemyList[i].EnemyStateNow == EnemyState.State.STATE_NO_USING || EnemyList[i].Used == false)
            {
                continue;
            }
            EnemyList[i].Target.HP = -1;
            Global.AddPlayerScore(5000);
        }
    }
}
