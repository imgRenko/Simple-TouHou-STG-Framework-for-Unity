using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace STGBarrage.Flandre{
	public class BulletFlesh : BulletEvent
	{
        public Sprite NewSprite;
        public Shooting d;
        public BulletTrackProduct trackData;
        public override void OnBulletCreated(Bullet Target)
        {
            Target.UseSimpleTrack(trackData);
        }
        public override void OnBulletMoving(Bullet Target)
        {
          //  Target.TrailUpdate = 0;
            if (Target.TotalLiveFrame > 200)
            {
                Target.ChangeSprite(NewSprite);
            }
        }
    }
}