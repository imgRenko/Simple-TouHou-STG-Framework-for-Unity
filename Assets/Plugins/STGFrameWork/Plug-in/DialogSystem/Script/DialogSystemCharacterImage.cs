using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("东方STG框架/弹幕设计/剧情系统/新剧情系统(漫画式)/角色图片控制器")]
public class DialogSystemCharacterImage : MonoBehaviour {
    [Header("Image Ref")]
    public Image CoverImage;
    public Image CharacterImage;
    public Image OutlineImage;
    public Image BackgroundImage;
    public int index;
    public Vector2 TargetImgPos;
    public Vector2 TargetImgScale;
    public Vector2 TargetCharPos;
    public Color32 TargetMaskColor;
    public float Speed = 0.75f;
    public float TargetAlpha = 255;

    public RectTransform Movement;
    public Vector2 orginalScale;
    void Start(){
        orginalScale = TargetImgScale;
    }
    private void Update()
    {
        CharacterImage.rectTransform.anchoredPosition = TargetCharPos;
        Movement.anchoredPosition = Vector2.Lerp(Movement.anchoredPosition, TargetImgPos, Speed);
        Movement.localScale = Vector2.Lerp(Movement.localScale, TargetImgScale, Speed);
        if (CoverImage.enabled )
        BackgroundImage.color = Color.Lerp(BackgroundImage.color, TargetMaskColor, 0.2f);
        else CharacterImage.color = Color.Lerp(CharacterImage.color, TargetMaskColor, 0.2f);
    }
    public void Destroy()
    {
        StartCoroutine(DestroyProcess());
    }
    public IEnumerator DestroyProcess()
    {
        TargetImgScale = Vector2.zero;
        yield return new WaitForSeconds(1);
        DialosSystemTextManager.CharImgCollection.Remove(index);
        Destroy(gameObject);
        
    }
}
