using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystemLive2DCameraController : MonoBehaviour
{
    public Camera Live2DCamera;
    public float tarSize = 0.6f;
    public Vector2 tarPos = Vector2.zero;
    public Vector3 oriPos;
    Transform cameraTran;
    // Start is called before the first frame update
    void Start()
    {
        cameraTran = Live2DCamera.transform;
        oriPos = cameraTran.position;
    }

    // Update is called once per frame
    void Update()
    {
        Live2DCamera.orthographicSize = Mathf.Lerp(Live2DCamera.orthographicSize, tarSize, 0.2f);
        cameraTran.position = Vector3.Lerp(cameraTran.position, oriPos+ new Vector3(tarPos.x, tarPos.y, -10), 0.2f);
    }
}
