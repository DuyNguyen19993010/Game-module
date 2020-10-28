using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamInitialization : MonoBehaviour
{
    public bool isDisplayed;
    public GameObject[] st;
    // Start is called before the first frame update
    void Awake()
    {

        st = GameObject.FindGameObjectsWithTag("SceneTransitionSumarize");
        Debug.Log("st is:" + st.Length);
        for (int i = 0; i < st.Length; i++)
        {
            Debug.Log(gameObject.tag + " added");
            st[i].GetComponent<CameraSumarize>().SendMessage("addCam", gameObject);
        }
        gameObject.SetActive(isDisplayed);

    }
    // void Update()
    // {
    //     gameObject.SetActive(isDisplayed);
    // }
    // void setDisplay(bool value)
    // {
    //     isDisplayed = value;
    // }
}
