using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
[System.Serializable]
public class RandomDescription{
    [Multiline]
    public string Title,Description;
    public Sprite image;
}
public class AsyncSceneManagement : MonoBehaviour {
    AsyncOperation async;
    float progressValue;
    public Slider slider;
    public Text title,content;
    public Image bgImage,ImageCharacter;
    public Animator DialogAnim;
    public List<RandomDescription> descriptionList = new List<RandomDescription>();
    public bool Playing = false;
    public void StartLoading()
    {
        //在这里开启一个异步任务，
        //进入loadScene方法。
        StartCoroutine(loadScene());
 
    }
    private void Awake()
    {
        RandomDisplay();
    }
    public void RandomDisplay(){
        RandomDescription _t = descriptionList [Random.Range (0, descriptionList.Count)];
        title.text = _t.Title;
        ImageCharacter.sprite = _t.image;
        content.text = _t.Description;
    }
    //注意这里返回值一定是 IEnumerator
    IEnumerator loadScene()
    {
        //异步读取场景。
        //Globe.loadName 就是A场景中需要读取的C场景名称。
        async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(ScenesChooser.BeyondSceneCount);
        async.allowSceneActivation = false;
        while (!async.isDone) {
            if (async.progress < 0.9f)
                progressValue = Mathf.Lerp(progressValue,async.progress,0.2f);
            else
                progressValue = Mathf.Lerp(progressValue, 1.0f, 0.2f);

            slider.value = progressValue;

            if (progressValue >= 0.9f && !Playing) {
                DialogAnim.PlayInFixedTime ("LoadingFinishedDialog", 0, 0);
                Playing = true;
            }
            yield return null;
        }

    }
    public void EnterScene(){
        async.allowSceneActivation = true;
    }
}
