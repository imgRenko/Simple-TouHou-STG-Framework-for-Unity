using UnityEngine;
using UnityEngine.UI;
public class ProgressPower : MonoBehaviour {
	public Slider Target;
	void Update () {
		Target.value = Global.Power / Global.maxPower_A;
	}
}
