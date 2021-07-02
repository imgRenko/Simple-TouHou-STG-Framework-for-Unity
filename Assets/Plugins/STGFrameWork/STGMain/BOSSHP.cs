
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("东方STG框架/框架核心/界面显示/BOSS 血量显示器")]
public class BOSSHP : MonoBehaviour {
    public Enemy Boss;
    public Image FrontImage;
    public Image BackImage;
    public SpellCard Spellcard;
    public Camera Camera;
    public Text Name;
    public Text HPText;
    public Text SpellCardNumber;
    public bool ShowMagicPicture = false;
    public bool showHPText = true;
    public bool showCurrentSpellCardHP = false;
    public Image MagicPicture;
    public Camera _cam;
    public bool showAll =false;
    float TotalHP = 0,nowHP = 0;

    public void InstallHPBar (){
        if (Boss == null)
            return;
        TotalHP = 0;
        if (showAll)
            foreach (SpellCard a in Boss.SpellCardList) {
                TotalHP += a.MaxHP;
            }
    }
	void Update () {
        if (MagicPicture != null)
            MagicPicture.gameObject.SetActive(ShowMagicPicture);
        if (Spellcard != null)
        {
            Name.text = Boss.Name;
            SpellCardNumber.text = "x " + Boss.SpellCardList.Count;
            BackImage.rectTransform.position = _cam.WorldToScreenPoint(Boss.transform.position);

            if (!showAll)
            {
                FrontImage.fillAmount = Mathf.LerpUnclamped(FrontImage.fillAmount, Boss.HP / Boss.MaxHP, 0.2f);
                HPText.text = string.Format("{0}/{1}", (Boss.HP), Boss.MaxHP);
            }
            else
            {
                nowHP = 0;
                foreach (SpellCard a in Boss.SpellCardList)
                {
                    if (a == Global.SpellCardNow)
                        continue;
                    nowHP += a.MaxHP;
                }
                float p = (nowHP + Boss.HP) / TotalHP;
                FrontImage.fillAmount = Mathf.LerpUnclamped(FrontImage.fillAmount, p, 0.2f);
                if (!showCurrentSpellCardHP)
                    HPText.text = string.Format("{0}/{1} - ({2}%)", (int)(nowHP + Boss.HP), (int)TotalHP, Mathf.CeilToInt(FrontImage.fillAmount * 100));
                else
                    HPText.text = string.Format("{0}/{1} - ({2}%)", (int)(Boss.HP), (int)Boss.MaxHP, Mathf.CeilToInt(FrontImage.fillAmount* 100));


            }
            if (!showHPText)
                HPText.text = string.Empty;
        }
        

    }
}
