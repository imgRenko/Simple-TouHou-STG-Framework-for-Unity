using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ComponentPanelController : MonoBehaviour {
    public int GroupIndex;
    public float Height = 0;
    public GameObject GroupParent;
    public GameObject ParmParent;
    public Text Title;
    public List<GroupPanelController> GroupList = new List<GroupPanelController>();
    protected RectTransform _rect;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
