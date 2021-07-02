using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("东方STG框架/弹幕设计/剧情系统/新剧情系统(漫画式)/对话图片显示器")]
public class DialogSystemImage : MonoBehaviour {
    public Color32 targetColor = Color.white;
    [HideInInspector]
    public Image _TargetImage;

    private Color32 _CurrentColor;
    private void Start(){
        _TargetImage = GetComponent<Image>();
        if (_TargetImage == null)
            enabled = false;
        _CurrentColor = targetColor;
    }
    private void Update(){
        _CurrentColor = Color.Lerp(_CurrentColor, targetColor, 0.25f);
        _TargetImage.color = _CurrentColor;
    }
    public void SetColor(Color c) 
    {
        targetColor = c;
        _CurrentColor = c;
    }
}
