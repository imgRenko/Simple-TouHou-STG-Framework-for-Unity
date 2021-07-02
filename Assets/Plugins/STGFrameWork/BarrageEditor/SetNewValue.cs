using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetNewValue : MonoBehaviour {
    public ParmPanelController ParmSetter;
    public InputField VectorX;
    public InputField VectorY;
    public InputField VectorZ;
    public InputField InputSetter;
    public Toggle BooleanSetter;
	// Use this for initialization
	void Start () {
		
	}
    public void SetNewParm(){
        if (InputSetter != null && InputSetter.text == string.Empty) return;
        if (ParmSetter.Use == false)
            return;
        if ( ParmSetter.fInfoSelected.FieldType == typeof(float)) {
            ParmSetter.fInfoSelected.SetValue (ParmSetter.a, System.Convert.ToSingle (InputSetter.text));
        }
        if (ParmSetter.fInfoSelected.FieldType == typeof(int)) {
            ParmSetter.fInfoSelected.SetValue (ParmSetter.a, System.Convert.ToInt16 (InputSetter.text));
        }
        if (ParmSetter.fInfoSelected.FieldType == typeof(string)) {
            ParmSetter.fInfoSelected.SetValue (ParmSetter.a, InputSetter.text);
        }
        if (ParmSetter.fInfoSelected.FieldType == typeof(bool)) {
            ParmSetter.fInfoSelected.SetValue (ParmSetter.a, BooleanSetter.isOn);
        }
        if (ParmSetter.fInfoSelected.FieldType == typeof(Vector2)) {
            if (VectorX.text == string.Empty || VectorY.text == string.Empty)
                return;
            Vector2 a = new Vector2 (System.Convert.ToSingle (VectorX.text), System.Convert.ToSingle (VectorY.text));
            ParmSetter.fInfoSelected.SetValue (ParmSetter.a, a);
        }
        if (ParmSetter.fInfoSelected.FieldType == typeof(Vector3)) {
            if (VectorX.text == string.Empty || VectorY.text == string.Empty|| VectorZ.text == string.Empty)
                return;
            Vector3 a = new Vector3 (System.Convert.ToSingle (VectorX.text), System.Convert.ToSingle (VectorY.text),System.Convert.ToSingle (VectorZ.text));
            ParmSetter.fInfoSelected.SetValue (ParmSetter.a, a);
        }

    }
	// Update is called once per frame
	void Update () {
		
	}
}
