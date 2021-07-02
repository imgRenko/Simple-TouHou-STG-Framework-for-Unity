using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("东方STG框架/框架核心/自机类/自机火力设置器")]
public class PowerSystem : MonoBehaviour {  
    public List<GameObject> PowerVeryLowShooting;
    public List<GameObject> PowerLowShooting;
    public List<GameObject> PowerHightShooting;  
    public List<GameObject> PowerVeryHeightShooting;
	// Update is called once per frame
	void Update () {
        if ( Global.Power >= 0 && Global.Power < 1) 
        {
            for ( int i = 0; i != PowerVeryLowShooting.Count; ++i ) 
            {
                PowerVeryLowShooting[i].SetActive(true);
            }
            for ( int i = 0; i != PowerLowShooting.Count; ++i )
            {
                PowerLowShooting[i].SetActive (false);
            }
            for ( int i = 0; i != PowerHightShooting.Count; ++i )
            {
                PowerHightShooting[i].SetActive (false);
            }
            for ( int i = 0; i != PowerVeryHeightShooting.Count; ++i )
            {
                PowerVeryHeightShooting[i].SetActive (false);
            }
        }
        if ( Global.Power >= 1 && Global.Power < 2 )
        {
            for ( int i = 0; i != PowerVeryLowShooting.Count; ++i )
            {
                PowerVeryLowShooting[i].SetActive (true);
            }
            for ( int i = 0; i != PowerLowShooting.Count; ++i )
            {
                PowerLowShooting[i].SetActive (true);
            }
            for ( int i = 0; i != PowerHightShooting.Count; ++i )
            {
                PowerHightShooting[i].SetActive (false);
            }
            for ( int i = 0; i != PowerVeryHeightShooting.Count; ++i )
            {
                PowerVeryHeightShooting[i].SetActive (false);
            }
        }
        if ( Global.Power >= 2 && Global.Power < 3 )
        {
            for ( int i = 0; i != PowerVeryLowShooting.Count; ++i )
            {
                PowerVeryLowShooting[i].SetActive (true);
            }
            for ( int i = 0; i != PowerLowShooting.Count; ++i )
            {
                PowerLowShooting[i].SetActive (true);
            }
            for ( int i = 0; i != PowerHightShooting.Count; ++i )
            {
                PowerHightShooting[i].SetActive (true);
            }
            for ( int i = 0; i != PowerVeryHeightShooting.Count; ++i )
            {
                PowerVeryHeightShooting[i].SetActive (false);
            }
        }
        if ( Global.Power >= 3 )
        {
            for ( int i = 0; i != PowerVeryLowShooting.Count; ++i )
            {
                PowerVeryLowShooting[i].SetActive (true);
            }
            for ( int i = 0; i != PowerLowShooting.Count; ++i )
            {
                PowerLowShooting[i].SetActive (true);
            }
            for ( int i = 0; i != PowerHightShooting.Count; ++i )
            {
                PowerHightShooting[i].SetActive (true);
            }
            for ( int i = 0; i != PowerVeryHeightShooting.Count; ++i )
            {
                PowerVeryHeightShooting[i].SetActive (true);
            }
        }
    }
}
