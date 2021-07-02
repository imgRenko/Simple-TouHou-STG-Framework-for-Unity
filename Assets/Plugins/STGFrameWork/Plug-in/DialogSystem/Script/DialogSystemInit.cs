using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using Live2D.Cubism.Core;
using XNode;
using Sirenix.OdinInspector;
using Live2D.Cubism.Rendering;

[AddComponentMenu("东方STG框架/弹幕设计/剧情系统/新剧情系统(漫画式)/剧情系统控制中心")]
public class DialogSystemInit : MonoBehaviour
{
    [Header("File Info")]
    public bool useOriginalXML = true;
    public string XmlName = "Plot_01.xml";
    public PlotGraph Graph;

    [Header("Setting")]
    public DialogSystemInstance dialogSystemInstance;
    public delegate void DialogSystemEvent();
    public event DialogSystemEvent DialogEnded;
   
    private XmlDocument _PlotFile;
    private XmlNodeList _PlotElementPoint;
    public XmlNode _CurrentNode;
    public Sprite XMLErrorFace;
    public Camera Live2DCamera;
    public static string createdXMLFileContent;
    private void Start()
    {
        createdXMLFileContent = string.Empty;
        dialogSystemInstance.Main.SetActive(true);
        dialogSystemInstance.Live2DCamera.SetActive(true);
        Global.WrttienSystem = true;
        LoadPlotXml();
        DialosSystemTextManager.Now = this;
        if (_PlotFile != null)
            StartCoroutine("ProductPlot");
    }
    public Node SearchNode(string Name)
    {

        foreach (var a in Graph.nodes)
        {
            if (a == null)
                continue;
            if (a.name == Name)
            {

                return a;

            }
        }
        return null;
    }
    public void CreateXMLFile() {
        Node root = SearchNode("新剧情系统XML生成过程");
        if (root != null)
        {
            root.FunctionDo("继续", null);
            string Content = "\r\n</Event>";
            DialogSystemInit.createdXMLFileContent += Content;
            Debug.Log(createdXMLFileContent);
            _PlotFile = new XmlDocument();
            _PlotFile.LoadXml(createdXMLFileContent);

            _PlotElementPoint = _PlotFile.FirstChild.ChildNodes;
        }
        else
            Debug.Log("不存在XML节点");
    }

    void LoadPlotXml()
    {
        try
        {
            if (useOriginalXML)
            {
                string path = Application.streamingAssetsPath + @"\Plot\" + XmlName;
                _PlotFile = new XmlDocument();
                _PlotFile.Load(path);
                _PlotElementPoint = _PlotFile.FirstChild.ChildNodes;
            }
            else {
                CreateXMLFile();
            }
        }
        catch (XmlException ex)
        {
            Global.WindowDialog_A.Show(XMLErrorFace, "XML异常", "在如下的XML文件中发现了以下问题，剧情系统终止" + "\n<size=10>" + ex.ToString() + "</size>", "Window_Show");
            Global.WindowDialog_A.eventDriver[0].ButtonBlindEvent += DialogSystemErrorEnd;
            Global.GamePause = true;
        }
    }
    IEnumerator DialogSystemErrorEnd()
    {
        if (DialogEnded != null)
            DialogEnded();
        Start();
        Global.WindowDialog_A.Hide();
        yield return null;
    }
    string TryGetValue(string key)
    {
        for (int i = 0; i != dialogSystemInstance.FunctionSetting.Count; ++i)
        {
            if (dialogSystemInstance.FunctionSetting[i].Key == key)
                return dialogSystemInstance.FunctionSetting[i].Content;
        }
        return null;
    }
    /// <summary>
    /// 在这里开始遍历所有的元素，依次检测元素内容，并根据元素内容制作游戏效果
    /// </summary>
    public IEnumerator ProductPlot()
    {
        Global.GamePause = true;
        Global.WrttienSystem = true;
        for (int i = 0; i != _PlotElementPoint.Count; i++)
        {
            string _FunctionName;
            _CurrentNode = _PlotElementPoint[i];
            _FunctionName = TryGetValue(_CurrentNode.Name);
            Debug.Log(_CurrentNode.Name + "|" + _FunctionName);
            if (_CurrentNode.Name == "WaitInput")
            {
                yield return new WaitUntil(() => (DialosSystemTextManager.typing == false));
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => (Input.GetButtonUp("Submit")));
                if (_PlotElementPoint[i].Attributes["delay"] != null)
                {
                    float t = System.Convert.ToSingle(_CurrentNode.Attributes["delay"].InnerText);
                    yield return new WaitForSeconds(t);
                }
                continue;
            }
            if (_CurrentNode.Name == "Wait")
            {
                float t = System.Convert.ToSingle(_CurrentNode.Attributes["time"].InnerText);
                yield return new WaitForSeconds(t);
            }
            if (_FunctionName != "null")
            {
                Invoke(_FunctionName, 0);
                yield return new WaitForSeconds(0);
            }
        }
        EndPlotSystem();
        Global.GamePause = false;
        Global.WrttienSystem = false;
        yield return null;
    }
    void ChangeImage()
    {
        Debug.Log(_CurrentNode.Name);
        string src = _CurrentNode.Attributes["src"].InnerText;
        string colorInfoOrginal = _CurrentNode.Attributes["color"].InnerText;
        string[] colorInfo = colorInfoOrginal.Split(',');
        Sprite image = Resources.Load<Sprite>("Background/" + src);
        if (image == null)
        {
            Debug.LogError("获取到了空图片"); return;
        }
        if (colorInfo.Length < 4)
        {
            Debug.LogError("读取不到ColorInfo"); return;
        }
        dialogSystemInstance.backGround._TargetImage.sprite = image;
        Color FinalColor = String2Color(colorInfo);
        dialogSystemInstance.backGround.targetColor = FinalColor;
    }

    void ChangeBGColor()
    {
        Debug.Log(_CurrentNode.Name);
        string colorInfoOrginal = _CurrentNode.Attributes["color"].InnerText;
        string[] colorInfo = colorInfoOrginal.Split(',');
        if (colorInfo.Length < 4)
        {
            Debug.LogError("读取不到ColorInfo"); return;
        }
        Color FinalColor = String2Color(colorInfo);
        dialogSystemInstance.backGround.targetColor = FinalColor;
    }
    static Color String2Color(string[] groups)
    {
        float r, g, b, a;
        r = System.Convert.ToSingle(groups.Length >= 1 ? groups[0] : "0");
        g = System.Convert.ToSingle(groups.Length >= 2 ? groups[1] : "0");
        b = System.Convert.ToSingle(groups.Length >= 3 ? groups[2] : "0");
        a = System.Convert.ToSingle(groups.Length >= 4 ? groups[3] : "0");
        return new Color(r, g, b, a);
    }
    static Vector2 String2Vector2(string[] Group)
    {
        if (Group.Length == 1)
            return new Vector2(System.Convert.ToSingle(Group[0]), 0);
        if (Group.Length == 0)
            return new Vector2(0, 0);
        return new Vector2(System.Convert.ToSingle(Group[0]), System.Convert.ToSingle(Group[1]));
    }
    void Wait() { Debug.Log(_CurrentNode.Name); }
    void End(bool reload = false)
    {
        Debug.LogWarning("剧情系统结束");
        foreach (KeyValuePair<int, DialogSystemCharacterImage> a in DialosSystemTextManager.CharImgCollection)
        {
            Destroy(a.Value.gameObject);
        }
        DialosSystemTextManager.CharImgCollection.Clear();
        foreach (KeyValuePair<int, DialogSystemTextMessage> a in DialosSystemTextManager.TextCollection)
        {
            Destroy(a.Value.gameObject);
        }
        foreach (var a in DialosSystemTextManager.Live2DModels)
        {
            a.Value.gameObject.SetActive(false);
            a.Value.transform.localPosition = a.Value.OriginalPos;

            a.Value.GetComponent<CubismRenderController>().Opacity = 0;
            
            Destroy(a.Value);
        }
        dialogSystemInstance.dialogSystemLive2DCameraController.tarSize = 0.6f;
        dialogSystemInstance.dialogSystemLive2DCameraController.tarPos = Vector2.zero;
       DialosSystemTextManager.Live2DModels.Clear();
        DialosSystemTextManager.TextCollection.Clear();
        DialosSystemTextManager.CharImgCollection.Clear();
        dialogSystemInstance.Main.SetActive(false);
        dialogSystemInstance.Live2DCamera.SetActive(false);
        DialosSystemTextManager.TextCollection.Clear();
        if (!reload)
            this.gameObject.SetActive(false);
        dialogSystemInstance.backGround.targetColor = new Color(0, 0, 0, 0);
        Global.GamePause = false;
        Global.WrttienSystem = false;
        if (DialogEnded != null)
            DialogEnded();
    }
    public void EndPlotSystem()
    {
        End();
    }
    void Zoom() {
        float zoomed = System.Convert.ToSingle(_CurrentNode.Attributes["zoom"].InnerText);
        dialogSystemInstance.dialogSystemLive2DCameraController.tarSize = zoomed;
    }
    void CameraPos()
    {
        string[] pos = (_CurrentNode.Attributes["pos"].InnerText).Split(',');

        dialogSystemInstance.dialogSystemLive2DCameraController.tarPos = String2Vector2(pos); 
    }
    void CharacterLighted()
    {
        int index = System.Convert.ToInt32(_CurrentNode.Attributes["index"].InnerText);
        DialogSystemCharacterImage result;
        if (DialosSystemTextManager.CharImgCollection.TryGetValue(index, out result))
        {
            if (result.CoverImage.enabled)
            {
                result.TargetMaskColor = new Color32(0, 0, 0, 0); result.TargetImgScale = result.orginalScale;
            }
            else {
                result.TargetMaskColor = new Color32(255, 255, 255, 255);
            }
           
        }
    }
    void CharacterDarked()
    {
        int index = System.Convert.ToInt32(_CurrentNode.Attributes["index"].InnerText);
        DialogSystemCharacterImage result;
        if (DialosSystemTextManager.CharImgCollection.TryGetValue(index, out result))
        {
            if (result.CoverImage.enabled)
            {
                result.TargetMaskColor = new Color32(0, 0, 0, 40); result.TargetImgScale = result.orginalScale * 0.85f;
            }
            else
            {
                result.TargetMaskColor = new Color32(165, 165, 165, 255);
            }
          
        }
    }
    void CharacterImage()
    {
        string img_src = _CurrentNode.Attributes["img-src"].InnerText;
        string masked = _CurrentNode.Attributes["masked"].InnerText;

        string cover_src = "";
        string[] maskColor = { "0,0,0"};

        if (masked == "true")
        {
            cover_src = _CurrentNode.Attributes["cover-src"].InnerText;
           
        }
        maskColor = _CurrentNode.Attributes["mask-color"].InnerText.Split(',');
        string[] orginal_pos = _CurrentNode.Attributes["original-pos"].InnerText.Split(',');
        string[] target_pos = _CurrentNode.Attributes["target-pos"].InnerText.Split(',');
        string[] orginal_scale = _CurrentNode.Attributes["original-scale"].InnerText.Split(',');
        string[] target_scale = _CurrentNode.Attributes["target-scale"].InnerText.Split(',');
        string[] charpos = _CurrentNode.Attributes["char-pos"].InnerText.Split(',');
       
        
        int index = System.Convert.ToInt32(_CurrentNode.Attributes["index"].InnerText);
        int ZLayout = System.Convert.ToInt32(_CurrentNode.Attributes["zlayout"].InnerText);
        float Speed = System.Convert.ToSingle(_CurrentNode.Attributes["transform-speed"].InnerText);
        
        Sprite CharImg = Resources.Load<Sprite>("Dialog/Character/" + img_src);
        Sprite Cover = Resources.Load<Sprite>("Dialog/Texture/" + cover_src);
        Vector2 orginal = String2Vector2(orginal_pos);
        Vector2 target = String2Vector2(target_pos);
        Vector2 orginalScale = String2Vector2(orginal_scale);
        Vector2 targetScale = String2Vector2(target_scale);
        Vector2 Charpos = String2Vector2(charpos);
        Color MaskColor = String2Color(maskColor);
        DialogSystemCharacterImage result;
        if (DialosSystemTextManager.CharImgCollection.TryGetValue(index, out result))
        {
            result.Movement.anchoredPosition = orginal;
            result.gameObject.transform.localScale = orginalScale;
            result.Speed = Speed;
            result.TargetImgScale = targetScale;
            if (masked == "true")
            {
                result.CoverImage.sprite = Cover;
                result.CoverImage.enabled = true;
            }
            else
            {
                result.CoverImage.enabled = false;
            }
            result.TargetCharPos = Charpos;
            result.CharacterImage.sprite = CharImg;
            result.TargetImgPos = target;
        //    if (masked == "true")
                result.TargetMaskColor = MaskColor;
            result.gameObject.transform.SetSiblingIndex(ZLayout);
            return;
        }

        GameObject Temp = Instantiate(dialogSystemInstance.CharImgBased.gameObject, dialogSystemInstance.CharImgBased.gameObject.transform.parent);
    
        DialogSystemCharacterImage Temp_Image = Temp.GetComponent<DialogSystemCharacterImage>();
        Temp_Image.Movement.anchoredPosition = orginal;
        Temp_Image.gameObject.transform.localScale = orginalScale;
        Temp_Image.Speed = Speed;
        Temp_Image.TargetImgScale = targetScale;
        if (masked == "true")
        {
            Temp_Image.CoverImage.sprite = Cover;
            Temp_Image.CoverImage.enabled = true;
        }
        else
        {
            Temp_Image.CoverImage.enabled = false;
        }
        Temp_Image.index = index;
        Temp_Image.TargetCharPos = Charpos;
        Temp_Image.CharacterImage.sprite = CharImg;
        Temp_Image.TargetImgPos = target;
       
        Temp.transform.SetSiblingIndex(ZLayout);
    //    if (masked == "true")
            Temp_Image.TargetMaskColor = MaskColor;
        Temp.transform.SetParent(dialogSystemInstance.CharImgBased.gameObject.transform.parent.transform);
        DialosSystemTextManager.CharImgCollection.Add(index, Temp_Image);
        Temp.SetActive(true);

    }
    void CharacterLive2D()
    {
        string model = _CurrentNode.Attributes["src"].InnerText;
        string[] orginal_pos = _CurrentNode.Attributes["original-pos"].InnerText.Split(',');
        string[] target_pos = _CurrentNode.Attributes["target-pos"].InnerText.Split(',');
        string orginal_alpha = _CurrentNode.Attributes["original-alpha"].InnerText;
        string target_alpha = _CurrentNode.Attributes["target-alpha"].InnerText;
        int index = System.Convert.ToInt32(_CurrentNode.Attributes["index"].InnerText);
        int ZLayout = System.Convert.ToInt32(_CurrentNode.Attributes["zlayout"].InnerText);
        float Speed = System.Convert.ToSingle(_CurrentNode.Attributes["transform-speed"].InnerText);

        string motion = string.Empty;
        
            motion = _CurrentNode.Attributes["motion"].InnerText;
        Vector2 orginal = String2Vector2(orginal_pos);
        Vector2 target = String2Vector2(target_pos);

        DialogLive2DController result;
        if (DialosSystemTextManager.Live2DModels.TryGetValue(index, out result))
        {
            result.Movement.localPosition = orginal;

            result.Speed = Speed;
            result.originalAlpha = System.Convert.ToSingle(orginal_alpha);
            result.targetAlpha = System.Convert.ToSingle(target_alpha);
            result.OriginalPos = orginal;
            result.TargetPos = target;

            result.gameObject.transform.SetSiblingIndex(ZLayout);
            if (motion != string.Empty) {

               
                    Animator r = result.gameObject.GetComponent<Animator>();
                    r.CrossFade(motion, System.Convert.ToSingle(0.24f));


                
            }
            return;


        }

        GameObject CharImg = DialogSystemLive2DModelPool.GetModel(model).gameObject;
        CharImg.transform.SetParent(dialogSystemInstance.Live2DModelPlace.gameObject.transform);
        CharImg.transform.localPosition = orginal;
        DialogLive2DController Temp_Image = CharImg.AddComponent<DialogLive2DController>();
        Temp_Image.originalAlpha = System.Convert.ToSingle(orginal_alpha);
        Temp_Image.controller = Temp_Image.gameObject.GetComponent<Live2D.Cubism.Rendering.CubismRenderController>();
        Temp_Image.controller.Opacity = System.Convert.ToSingle(orginal_alpha);
        Temp_Image.modelInfo = CharImg.GetComponent<CubismModel>();
        Temp_Image.BreathParam = FindParams(Temp_Image.modelInfo.Parameters, "breath");
        Temp_Image.BreathCurve = dialogSystemInstance.breathCurve;
        Temp_Image.Movement = gameObject.transform;
      

        

        Temp_Image.OriginalPos = orginal;
        Temp_Image.targetAlpha = System.Convert.ToSingle(target_alpha);

        Temp_Image.Speed = Speed;

        Temp_Image.index = index;
        Temp_Image.TargetPos = target;
        CharImg.transform.SetSiblingIndex(ZLayout);
     
            DialosSystemTextManager.Live2DModels.Add(index, Temp_Image);
        CharImg.SetActive(true);
        if (motion != string.Empty)
        {


            Animator r = Temp_Image.gameObject.GetComponent<Animator>();
            r.CrossFade(motion, System.Convert.ToSingle(0.24f));



        }
    }
    IEnumerator DisplayModel(GameObject a)
    {
        yield return new WaitForEndOfFrame();
        a.SetActive(true);

    }
    void PlayMotion()
    {
        int index = System.Convert.ToInt32(_CurrentNode.Attributes["index"].InnerText);
        string Motion = _CurrentNode.Attributes["motion"].InnerText;
        string fade = _CurrentNode.Attributes["fade"].InnerText;
   
        DialogLive2DController result;
        if (DialosSystemTextManager.Live2DModels.TryGetValue(index, out result))
        {
            Animator r = result.gameObject.GetComponent<Animator>();
            
             r.CrossFade(Motion, System.Convert.ToSingle(fade));


        }
    }

    public void Reload()
    {
        StopCoroutine("ProductPlot");
        StopAllCoroutines();
        End(true);
        Start();
    }
    void CharacterText()
    {
        bool noSrc, noPos, noFilpX;
        noSrc = _CurrentNode.Attributes["src"] == null;
        noPos = _CurrentNode.Attributes["pos"] == null;
        noFilpX = _CurrentNode.Attributes["filp"] == null;

        string src = noSrc ? string.Empty : _CurrentNode.Attributes["src"].InnerText;
        string text = _CurrentNode.Attributes["text"].InnerText;
        string filp = string.Empty;
        string[] orginal_pos = { };
        if (!noFilpX)
            filp = _CurrentNode.Attributes["filp"].InnerText;
        if (!noPos)
            orginal_pos = _CurrentNode.Attributes["pos"].InnerText.Split(',');

        int index = System.Convert.ToInt32(_CurrentNode.Attributes["index"].InnerText);
        int ZLayout = System.Convert.ToInt32(_CurrentNode.Attributes["zlayout"].InnerText);
        Vector2 orginal = Vector2.zero;
        Sprite Cover = Resources.Load<Sprite>("Dialog/Texture/" + src);
        DialogSystemTextMessage result;
        if (!noPos)
            orginal = String2Vector2(orginal_pos);
        if (DialosSystemTextManager.TextCollection.TryGetValue(index, out result))
        {
            if (!noPos)
                result.TargetPos = orginal;
            //result.Movement.position = orginal;
            result.MessageContent.text = "";
            result.StartType(text);
            if (!noFilpX)
            {
                Vector2 dir = new Vector2(filp == "true" ? -1 : 1, 1);
                Vector2 Textdir = new Vector2(filp == "true" ? 1 : -1, -1);
                result.BalloonImage.gameObject.transform.localScale = dir;

                result.MessageContent.gameObject.transform.localScale = Textdir;
            }
            if (!noSrc)
                result.BalloonImage.sprite = Cover;
            return;
        }
        GameObject Temp = Instantiate(dialogSystemInstance.TextMessage.gameObject);
        DialogSystemTextMessage Temp_Text = Temp.GetComponent<DialogSystemTextMessage>();
        Temp.transform.SetParent(dialogSystemInstance.TextMessage.gameObject.transform.parent.transform);
        if (!noPos)
        {
            Temp_Text.Movement.anchoredPosition = orginal;
            Temp_Text.TargetPos = orginal;
        }
        Temp_Text.index = index;
        if (!noFilpX)
        {
            Vector2 dir = new Vector2(filp == "true" ? -1 : 1, 1);
            Vector2 Textdir = new Vector2(filp == "true" ? 1 : -1, -1);
            Temp_Text.BalloonImage.gameObject.transform.localScale = dir;
            Temp_Text.MessageContent.gameObject.transform.localScale = Textdir;

        }
        if (!noSrc)
            Temp_Text.BalloonImage.sprite = Cover;
        Temp.transform.SetSiblingIndex(ZLayout);
        Temp_Text.StartType(text);
        Temp_Text.gameObject.transform.localScale = Vector2.one;
        DialosSystemTextManager.TextCollection.Add(index, Temp_Text);
        Temp.SetActive(true);
    }
    public CubismParameter FindParams(CubismParameter[] Params, string Condition1 = "", string Condition2 = "", string Condition3 = "")
    {

        int index = 0;
        foreach (CubismParameter _param in Params)
        {
            bool Switch1, Switch2, Switch3;


            Switch1 = _param.Id.ToLower().Contains(Condition1);

            Switch2 = _param.Id.ToLower().Contains(Condition2);

            Switch3 = _param.Id.ToLower().Contains(Condition3);
            if ((Switch1 && Condition1 != "") || (Switch2 && Condition2 != "") || (Switch3 && Condition3 != ""))
                return _param;
            index++;

        }
        Debug.Log("Not Found");
        return Params[0];
    }
    void DestroyCharacterImage()
    {
        Debug.Log("Image Destroyed");
        int index = System.Convert.ToInt32(_CurrentNode.Attributes["index"].InnerText);
        DialogSystemCharacterImage result;
        if (DialosSystemTextManager.CharImgCollection.TryGetValue(index, out result))
        {
            result.Destroy();
        }
    }
    void DestroyCharacterText()
    {
        int index = System.Convert.ToInt32(_CurrentNode.Attributes["index"].InnerText);
        DialogSystemTextMessage result;
        if (DialosSystemTextManager.TextCollection.TryGetValue(index, out result))
        {
            result.Destroy();
        }
    }
}


