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
    public GameObject BulletBased;     // 子弹的原型。 
    public GameObject BounsBased;     // 奖励的原型。 
    public GameObject PlayerBulletBased;
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
    public List<PlayerBullet> PlayerBulletList = new List<PlayerBullet>();
    public List<TriggerReceiver> ExtraCheckingTse = new List<TriggerReceiver>();
    public static List<TriggerReceiver> ExtraChecking = new List<TriggerReceiver>();

    //int index = 0;
    // Use this for initialization
    // 此部分在编辑器模式不适用，在玩家在打包游戏后游玩的过程中，用于减少玩家读取时间。
    IEnumerator InitFrameBullet() {
        // 一帧30个子弹
        for (int p = 0; p != MaxBulletNumber / 10; ++p)
        {
            for (int i = 0; i != 10; ++i)
            {

                GameObject New = Instantiate(BulletBased);

                New.SetActive(true);
                New.gameObject.transform.position = new Vector2(999, 999);

                New.transform.parent = BulletBased.transform.parent;
                Bullet c = New.GetComponent<Bullet>();

                c.Use = false;
                c.ID = i;
                New.name = "Bullet" + p*30+i.ToString();
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
                GameObject New = Instantiate(BounsBased);
                //  New.tag = "Chlid";
                Transform trans = New.transform;
                Vector3 Position = trans.position;
                Position.z = 0 - (0.1f * p*5+i);
                trans.position = Position;

                New.SetActive(false);
                trans.parent = BounsBased.transform.parent;
                New.name = "Bouns" + p*5+i.ToString();
                Global.GameObjectPool_A.BounsList.Add(New.GetComponentInChildren<Bouns>());
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
                GameObject New = Instantiate(PlayerBulletBased);
                Transform trans = New.transform;
                //   New.tag = "Chlid";
                Vector3 Position = trans.position;
                New.SetActive(true);
                trans.position = Position;
                trans.parent = PlayerBulletBased.transform.parent;
                New.name = "PlayerBullet" + i.ToString();

                Global.GameObjectPool_A.PlayerBulletList.Add(New.GetComponent<PlayerBullet>());
            }
            yield return new WaitForEndOfFrame();
        }
    }
    void Start()
    {
        StartCoroutine(InitFrameBullet());

        StartCoroutine(InitBouns());
        StartCoroutine(InitBullet());

        for (int i = 0; i != EnemyNumber; ++i)
        {
            GameObject New = Instantiate(EnemyBased.gameObject);
            Transform trans = New.transform;
         //   New.tag = "Chlid";
            Vector3 Position = trans.position;
            trans.position = Position;
            New.SetActive(false);
            trans.parent = EnemyBased.transform.parent;
            New.name = "Enemy" + i.ToString();
            Global.GameObjectPool_A.EnemyList.Add(New.GetComponent<EnemyState>());

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
                if (BulletList_State[i].Use == false || BulletList_State[i].noDestroy == true)
                    continue;
            }
            else
            {
                if (BulletList_State[i].Use == false || BulletList_State[i].DonDestroy || BulletList_State[i].noDestroy == true)
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
