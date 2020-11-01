using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSumarize : MonoBehaviour
{
    public List<GameObject> Cams;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void addCam(GameObject cam)
    {
        Cams.Add(cam);
    }
}
