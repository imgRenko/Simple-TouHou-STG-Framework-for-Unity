using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("东方STG框架/弹幕设计/剧情系统/新剧情系统(漫画式)/剧情文本显示器")]
public class DialogSystemTextMessage : MonoBehaviour
{
    public RectTransform Movement;
    public RectTransform Scale;
    public Text MessageContent;
    public Animator AnimCtrl;
    public Image BalloonImage;

    public float ShowTime = 0.03f;
    public int index;
    public Vector2 TargetPos;

    private int mCurPos;
    private string mTypingText;
    private string mTempTypingText;

    public void StartType(string content) 
    {
        CancelInvoke("Typing");
        MessageContent.text = "";
        mTempTypingText = "";
        mTypingText = content;
        mCurPos = 0;
        InvokeRepeating("Typing", 0, ShowTime);
        DialosSystemTextManager.typing = true;
    }
    private void Typing()
    {
        if (mTypingText.Length - 1 == mCurPos)
        {
            CancelInvoke("Typing");
            DialosSystemTextManager.typing = false;
        }
        mTempTypingText += mTypingText.Substring(mCurPos, 1);
        MessageContent.text = mTempTypingText + "<color=#13171800>" + mTypingText.Substring(mCurPos + 1) + "</color>";
        mCurPos++;
    }
    private void Update()
    {
        Movement.anchoredPosition = Vector2.Lerp(Movement.anchoredPosition, TargetPos, 0.45f);
        if (DialosSystemTextManager.typing == true && Input.GetButtonUp("Submit")) {
            CancelInvoke("Typing");
            DialosSystemTextManager.typing = false;
            MessageContent.text = mTypingText;
        }
    }
    private void Start()
    {
        AnimCtrl = GetComponent<Animator>();
        TargetPos = Movement.anchoredPosition;
    }
    public void Destroy()
    {
        StartCoroutine(DestroyProcess());
    }
    private IEnumerator DestroyProcess() {
        AnimCtrl.PlayInFixedTime("BalloonFloat_0", 0, 0);
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
        DialosSystemTextManager.TextCollection.Remove(index);
    }
}
