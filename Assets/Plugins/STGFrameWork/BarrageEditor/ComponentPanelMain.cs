using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class ComponentPanelMain : MonoBehaviour {
    public GameObject targetChecking;
    [Header ("BASE OBJECT")]
    public ComponentPanelController component_Displayer;
    public GroupPanelController     component_ParmGroup;
    public ParmPanelController      component_ParmInfo;
    public GameObject               component_MainContent;
    public GameObject               panel_WaitingFor;
    public RectTransform            ScrollPanel;
    public Text                     commandText;

    protected List<ComponentPanelController> ComponentPanelList = new List<ComponentPanelController> ();
    protected List<string>                   parmName           = new List<string> ();
    protected FieldInfo[]                    fInfo              = typeof(Shooting).GetFields ();  

    protected FieldInfo fInfoSelected;

    [ButtonGroup("Update")]
    void UpdatePramNow(){
        UpdateParm ();
    }

	void Update () {
        commandText.gameObject.SetActive (targetChecking == null);
        component_MainContent.SetActive (targetChecking != null);
	}

    void GetAllParms(){
        parmName.Clear ();
        for (int i = 0; i != fInfo.Length; ++i) {
                parmName.Add (fInfo [i].Name);
        }
    }
    void UpdateParm(){
        StartCoroutine (UpdateParmLaunch ());
    }
    IEnumerator UpdateParmLaunch(){
        panel_WaitingFor.SetActive (true);
        Component[] _components = targetChecking.GetComponents<Component> ();
        int _index = 0;
        float totalHi = 0;
        float Chg = -145;
        foreach (Component _component in _components) {
 
            if (_component.GetType () == typeof(Transform))
                continue;
            _index++;

            // 做显示组件组的初始化
            int   GroupAmount                  = 0,
                  ParmAmount                   = 0;
            float _scale                       = Screen.width / 1366.0f;

            GameObject               _tempDisplayer       = Instantiate (component_Displayer.gameObject);
            RectTransform            _tempDisplayer_rT    = _tempDisplayer.GetComponent<RectTransform> ();
            ComponentPanelController _compTemp            = _tempDisplayer.GetComponent<ComponentPanelController> ();

            // 显示组件到窗口里头
            _tempDisplayer_rT.SetParent (component_Displayer.transform.parent.transform);
            _tempDisplayer_rT.localScale = Vector3.one;
            ComponentPanelList.Add (_compTemp);
            _compTemp.Title.text = _component.GetType ().Name;
            if (ComponentPanelList.Count >= 1)
                _tempDisplayer_rT.anchoredPosition = new Vector2 (0, -140 * _index - 30);
            else
                _tempDisplayer_rT.anchoredPosition = new Vector2 (0, -140 * _index);
            
            _compTemp.GroupIndex     = _index;
       

            _tempDisplayer.SetActive (true);

            fInfo = _component.GetType().GetFields ();  

            List<string> GroupName = new List<string> ();
            yield return new WaitForSeconds (0.2f);
            foreach (FieldInfo _point in fInfo) {
                
                //object[] CustomAttributes = a.GetCustomAttributes (typeof(LabelTextAttribute), true);
                object[] GroupAttributes = _point.GetCustomAttributes (typeof(FoldoutGroupAttribute), true);

                for (int r = 0; r != GroupAttributes.Length; ++r) {
                    
                    FoldoutGroupAttribute _object = (FoldoutGroupAttribute)GroupAttributes [r];

                    if (!GroupName.Contains (_object.GroupName))
                        GroupName.Add (_object.GroupName);
                    else
                        continue;
                    
                    if (_compTemp != null) {
                        // 显示组到组件集合的初始化
                        GameObject           _tempGroupDisplayer     = Instantiate (component_ParmGroup.gameObject);
                        RectTransform        _tempDisplayer_rG       = _tempGroupDisplayer.GetComponent<RectTransform>();
                        GroupPanelController _GroupTemp              = _tempGroupDisplayer.GetComponent<GroupPanelController> ();
                        
                        // 显示组到组件集合
                        _compTemp.GroupList.Add (_GroupTemp);

                        _tempDisplayer_rG.SetParent (_compTemp.GroupParent.transform);
                        _tempDisplayer_rG.localScale = Vector3.one;
        

                        _GroupTemp.Title.text = _object.GroupName;
                        _tempGroupDisplayer.SetActive (true);
                        List<string> ParmName = new List<string> ();
                        // 显示参数
                        foreach (FieldInfo _Parm in fInfo) {
                            
                            bool isCurrentGroup         = false;
                            List<string> PramName       = new List<string> ();
                            object[] GroupAttribute     = _Parm.GetCustomAttributes (typeof(FoldoutGroupAttribute), true);

                            for (int g = 0; g != GroupAttribute.Length; ++g) {
                                FoldoutGroupAttribute _objectParm = (FoldoutGroupAttribute)GroupAttribute [g];
                                if (_objectParm.GroupName == _object.GroupName) {
                                    isCurrentGroup = true;
                                    Debug.Log ("Enter");
                                    break;
                                }
                            }
    
                         
                            if (isCurrentGroup == false)
                                continue;
               
                            ParmName.Add (_Parm.Name);
                            GameObject           _tempParmDisplayer      = Instantiate (component_ParmInfo.gameObject);
                            RectTransform        _tempParmDisplayer_rT   = _tempParmDisplayer.GetComponent<RectTransform>();
                            ParmPanelController  _ParmTemp               = _tempParmDisplayer.GetComponent<ParmPanelController> ();
                            _GroupTemp.PramList.Add (_ParmTemp);
                            _tempParmDisplayer_rT.SetParent (_tempGroupDisplayer.transform);
                            _tempParmDisplayer_rT.localScale = Vector3.one;
                            _tempParmDisplayer_rT.anchoredPosition = new Vector2 (0, -30 * ( _GroupTemp.PramList.Count)) ;
                            _tempParmDisplayer.gameObject.SetActive (true);

                            object[] Translate = _Parm.GetCustomAttributes (typeof(LabelTextAttribute), true);

                            for (int g = 0; g != Translate.Length; ++g) {
                                LabelTextAttribute _Lable = (LabelTextAttribute)Translate [g];
                                _ParmTemp.Title.text = _Lable.Text;
                            }
                            if (Translate.Length == 0)
                                _ParmTemp.Title.text = _Parm.Name;
                            _ParmTemp.fInfoSelected = _Parm;
                            _ParmTemp.a = _component;
                            _ParmTemp.DisplayParm ();
                      
                            _tempDisplayer_rG.sizeDelta =  new Vector2 (361.85f, 30.0f *  (_GroupTemp.PramList.Count + 1));
                            _GroupTemp.Height = 30.0f *( _GroupTemp.PramList.Count + 1);

                        }
                        ParmAmount += ParmName.Count;
                    }
                }

            }
            GroupAmount = GroupName.Count;
            _compTemp.Height = 30;
            float hg = 0;

            for (int i = 0; i != _compTemp.GroupList.Count; ++i) {
                _compTemp.Height += _compTemp.GroupList [i].Height;
                if (i >= 1) {
         
                    _compTemp.GroupList [i]._rect.anchoredPosition = new Vector2 (0, hg);
                    hg -= _compTemp.GroupList [i].Height;
                } else {
                    _compTemp.GroupList [i]._rect.anchoredPosition = new Vector2 (0, 0);
                    hg -= _compTemp.GroupList [i].Height ;
                }
            }
            totalHi += _compTemp.Height;
            _tempDisplayer_rT.sizeDelta =  new Vector2 (361.85f, _compTemp.Height);
            _tempDisplayer_rT.anchoredPosition =  new Vector2 (0, Chg);
            Chg -= _compTemp.Height;
            ScrollPanel.sizeDelta =  new Vector2 (361.85f, totalHi + 100);
            panel_WaitingFor.SetActive (false);
        }
     

    }
}
