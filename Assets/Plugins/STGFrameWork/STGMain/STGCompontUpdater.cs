using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STGCompontUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        List<LocalComponentUpdater> r = new List<LocalComponentUpdater>();
        for (int i = 0; i != ComponentOrderEditor.updateComponents.Count; ++i)
        {
            if (ComponentOrderEditor.updateComponents[i] == null)
                r.Add(ComponentOrderEditor.updateComponents[i]);

        }
        foreach (var a in r)
        {
            ComponentOrderEditor.updateComponents.Remove(a);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var updateInfo in ComponentOrderEditor.updateComponents)
        {
           
                updateInfo.DoUpdate();
        }
    }
}
