using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace STGBarrage.Flandre{
	public class BulletRotation : ShootingEvent
	{
        public float additionalRadius = 15;
        public BulletTrackProduct trackData;
        public override void BeforeShooting(Shooting Target)
        {
            Target.RadiusDirection += additionalRadius;
           
        }
        public override void OnShootingUsing(Shooting Target)
        {
            Target.Radius = 1.2f * Mathf.Sin(Time.time);

        }
        public override void AfterShootingFinishedShooting(Shooting Target, Bullet bullet)
        {
            
            bullet.UseSimpleTrack(trackData);
            Timer.MinTime += 16;
        }
       
    }
}