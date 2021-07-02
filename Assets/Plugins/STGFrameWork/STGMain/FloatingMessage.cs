using System.Collections;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("东方STG框架/框架核心/界面显示/临时文本显示器")]
public class FloatingMessage : MonoBehaviour
{
    public Text OrginalText;

    public int WaitingTime = 5;
    [Multiline] public string showntext = "Hello!STG!";
    public int FontSize = 32;
    public Color32 color;
    public Vector2 Point;
    public GameObject Copy;
    public bool allowShowAtfrist = false;
    private Text TextCopy;
    // Use this for initialization
	void Start ()
	{
        
	    OrginalText = Global.CommandWidelyUsed_A.GetComponent<Text> ();

        if (allowShowAtfrist == false)
	        setText(showntext, color, Point, WaitingTime);

    }

    static public FloatingMessage addMessagetoGameObject(GameObject t)
    {
        return t.AddComponent<FloatingMessage>();
    }

    public void setText(string content, Color TextColor, Vector2 point, int Waitingtime = 0)
    {

        showntext = content;
        WaitingTime = Waitingtime;
        color = TextColor;
        Point = point;
        Copy = Instantiate(OrginalText.gameObject, OrginalText.gameObject.transform.parent);
        Copy.SetActive(true);
        TextCopy = Copy.GetComponent<Text>();
        TextCopy.fontSize = FontSize;
        TextCopy.color = color;

        StartCoroutine(showText());
    }

    IEnumerator showText()
    {

        TextCopy.text = showntext;
        TextCopy.rectTransform.position = Point;
        Copy.GetComponent<Animator>().Play("FloatingTextApproaching");
        yield return new WaitForSeconds(0.3f);
        yield return new WaitForSeconds(WaitingTime);
        Copy.GetComponent<Animator>().Play("FloatingTextDestroy");
        yield return new WaitForSeconds(0.3f);
        Destroy(Copy);
        Destroy(this);
    }
}
