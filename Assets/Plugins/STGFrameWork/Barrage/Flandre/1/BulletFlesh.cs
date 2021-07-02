using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace STGBarrage.Flandre{
	public class BulletFlesh : BulletEvent
	{
        public Sprite NewSprite;
        public override void OnBulletMoving(Bullet Target)
        {

            if (Target.TotalLiveFrame > 200)
            {
                Target.ChangeSprite(NewSprite);
            }
        }
    }
}