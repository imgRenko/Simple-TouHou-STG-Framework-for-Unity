using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLaserManager 
{
    public static FollowLaserManager managerInstance {
        get
        {
            if (managerInstancePrivate == null)
                managerInstancePrivate = new FollowLaserManager();
            return managerInstancePrivate;
        }
    }

    private static FollowLaserManager managerInstancePrivate;

    public FollowLaserProcessor ApplyNewProcessor(List<GameObject> trackObjects,AnimationCurve widthCtrlCurve,int maxLiveFrame = 200) {
        FollowLaserProcessor followLaserProcessor = Global.GameObjectPool_A.ApplyFollowLaserProcessor();
        if (followLaserProcessor == null)
            return null; 

        followLaserProcessor.widthAnimationCurve = widthCtrlCurve;
        followLaserProcessor.SetTrackObject(trackObjects);
        followLaserProcessor.Use = true;
        followLaserProcessor.MaxLiveFrame = maxLiveFrame;
        return followLaserProcessor;

    }

}
