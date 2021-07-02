using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
public class ParmPanelController : MonoBehaviour {
    public Text Title;
    public FieldInfo fInfoSelected;
    public GameObject vector3Object;
    public GameObject vector2Object;
    public GameObject floatObject;
    public GameObject intObject;
    public GameObject stringObject;
    public GameObject boolObject;
    public Object a;
    public bool Use = false;
	// Use this for initialization
	void Start () {
		
	}
    public void DisplayParm(){
        if (fInfoSelected.FieldType == typeof(Vector3)) {
            Vector3 t = (Vector3)fInfoSelected.GetValue (a);
            InputField[] filled = GetComponentsInChildren<InputField> ();
            int i = 0;
            foreach (InputField textFilled in filled) {
                i++;
                if (i == 1)
                    textFilled.text = t.x.ToString ();
                if (i == 2)
                    textFilled.text = t.y.ToString ();
                if (i == 3)
                    textFilled.text = t.z.ToString ();
            }
            vector3Object.SetActive (true);
        }
        if (fInfoSelected.FieldType == typeof(Vector2)) {
            Vector2 t = (Vector2)fInfoSelected.GetValue (a);
            InputField[] filled = GetComponentsInChildren<InputField> ();
            int i = 0;
            foreach (InputField textFilled in filled) {
                i++;
                if (i == 1)
                    textFilled.text = t.x.ToString ();
                if (i == 2)
                    textFilled.text = t.y.ToString ();
            }
            vector3Object.SetActive (true);
        }
        if (fInfoSelected.FieldType == typeof(float)) {
            float t = (float)fInfoSelected.GetValue (a);
            InputField filled = floatObject.GetComponent<InputField> ();
            filled.text = t.ToString();
            floatObject.SetActive (true);
        }
        if (fInfoSelected.FieldType == typeof(int)) {
            int t = (int)fInfoSelected.GetValue (a);
            InputField filled = intObject.GetComponent<InputField> ();
            filled.text = t.ToString();
            intObject.SetActive (true);
        }
        if (fInfoSelected.FieldType == typeof(string)) {
            string t = (string)fInfoSelected.GetValue (a);
            InputField filled = stringObject.GetComponent<InputField> ();
            filled.text = t;
            stringObject.SetActive (true);
        }
        if (fInfoSelected.FieldType == typeof(bool)) {
            bool t = (bool)fInfoSelected.GetValue (a);
            Toggle boolmark = boolObject.GetComponentInChildren<Toggle> ();
            boolmark.isOn = t;
            boolObject.SetActive (true);
        }
        Use = true;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
