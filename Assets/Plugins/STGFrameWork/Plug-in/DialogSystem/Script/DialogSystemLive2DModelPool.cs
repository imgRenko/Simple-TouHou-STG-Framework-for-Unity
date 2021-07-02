using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Live2D.Cubism.Core;
using Live2D.Cubism.Rendering;

public class DialogSystemLive2DModelPool : MonoBehaviour {
	public static CubismModel[] Model;
	// Use this for initialization
	void Start () {
		Model = gameObject.GetComponentsInChildren<CubismModel>(true);
		
	}
	public static CubismModel GetModel(string name) {
		foreach (var a in Model) {
			if (name == a.name)
				return a;
		}
		return null;
	}

}
