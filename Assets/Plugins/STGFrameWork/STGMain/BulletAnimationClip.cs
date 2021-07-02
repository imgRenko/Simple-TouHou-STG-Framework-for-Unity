using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAnimationClip : MonoBehaviour
{
    public Bullet bullet;
    public bool DestroyAnimation = true;
    public bool SpawnAnimation = true;

    Transform bulletTransform;
    // Start is called before the first frame update
    void Awake()
    {
        bullet = this.GetComponent<Bullet>();
        bulletTransform = bullet.transform;
        if (DestroyAnimation)
            bullet.BulletDestroyAnimation += DestroyAnimationProcess;
        if (SpawnAnimation)
            bullet.BulletCreateAnimation += SpawnAnimationProcess;

    }

    // Update is called once per frame
    public virtual void DestroyAnimationProcess(float curr, float end, Bullet bullet)
    {
        if (curr >= end)
        {
          
            bullet.DestroyBulletETC();
            return;
        }
        float playFrameInterval = 3.0f;
        //  AnimationControl.speed =  GlobalSpeed;
        if ((int)(curr / playFrameInterval) < bullet.DestroyAnimSprites.Count - 1 && bullet.NoDestroyAnimation == false)
            bullet.BulletSpriteRenderer.sprite = bullet.DestroyAnimSprites[(int)(curr / playFrameInterval)];
        else
        {
          
            bullet.DestroyBulletETC();
        }

    }
    public virtual void SpawnAnimationProcess(float curr, float end, Bullet bullet)
    {
        if (curr > end)
        {
            bullet.animationController.PlayAnimation();
            return;
        }
        float process = (curr - bullet.signedFrame) / (end - bullet.signedFrame);
        bulletTransform.localScale = Vector3.one * (2 - process);
        Color p = bullet.BulletSpriteRenderer.color;
        p.a = curr / end;
        bullet.BulletSpriteRenderer.color = p;
    }
}