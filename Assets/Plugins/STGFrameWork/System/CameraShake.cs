using UnityEngine;
using Sirenix.OdinInspector;
public class CameraShake: MonoBehaviour
{
    public Transform camTransform;
    static public float shake = 0f;
    static public float shakeAmount = 0.7f;
    static public float decreaseFactor = 1.0f;
    [LabelText("振幅")]
    public float _def_shake = 0f;
    [LabelText("震动总值")]
    public float _def_shakeAmount = 0.7f;
    [LabelText("震动消退速度")]
    public float _def_decreaseFactor = 1.0f;
    Vector3 originalPos;
    Vector3 _t;
 
    void Awake ()
    {
        if ( camTransform == null )
        {
            camTransform = GetComponent (typeof (Transform)) as Transform;
        }
        shake = _def_shake;
        shakeAmount = _def_shakeAmount;
        decreaseFactor = _def_decreaseFactor;

    }
    void OnEnable ()
    {
        originalPos = camTransform.localPosition;
    }
    void Update ()
    {
        if (Global.GamePause || Global.WrttienSystem)
            return;

        if ( shake > 0 )
        {
            
         
                _t = originalPos + Random.insideUnitSphere * shakeAmount * Global.GlobalSpeed;
                

            camTransform.localPosition = Vector3.Lerp(camTransform.localPosition,_t,0.5f);

            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0f;

            camTransform.localPosition = Vector3.Lerp(camTransform.localPosition,originalPos,0.5f);;
        }
    }
}
