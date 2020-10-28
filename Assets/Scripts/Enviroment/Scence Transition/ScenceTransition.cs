using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class ScenceTransition : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform destination;
    GameObject[] toDisable;
    GameObject[] toEnable;
    GameObject[] Player;
    public int fromCam;
    public int toCam;
    public float timeToWait;
    public List<GameObject> Cams;


    void Start()
    {
        Cams = GameObject.FindGameObjectsWithTag("SceneTransitionSumarize")[0].GetComponent<CameraSumarize>().Cams;

        timeToWait = 2.0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            Player = GameObject.FindGameObjectsWithTag("player");
            // Debug.Log(toDisable.Length);
            // Debug.Log(toEnable.Length);

            other.transform.position = destination.position;
            Debug.Log("--------------------------------------------------------------------------");
            StartCoroutine(stopPlayer());
            Debug.Log("This is level " + gameObject.tag);
            for (int i = 0; i < Cams.Count; i++)
            {
                if (Cams[i].tag != ("Camera " + toCam))
                {
                    Debug.Log("Disabled " + Cams[i].tag);
                    Cams[i].SetActive(false);
                }
                if (Cams[i].tag == ("Camera " + toCam))
                {
                    Debug.Log("Changed to" + Cams[i].tag);
                    Cams[i].SetActive(true);
                }

            }
            // for (int i = 0; i < toDisable.Length; i++)
            // {
            //     Debug.Log("fromCam set");
            //     toDisable[i].GetComponent<CamInitialization>().SendMessage("setDisplay", false);
            // }
            // for (int i = 0; i < toEnable.Length; i++)
            // {
            //     Debug.Log("toCam set");
            //     toEnable[i].GetComponent<CamInitialization>().SendMessage("setDisplay", true);
            // }

        }
    }

    IEnumerator stopPlayer()
    {
        Player[0].GetComponent<PlayerController>().SendMessage("setMoving", false);
        Player[0].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(timeToWait);
        Player[0].GetComponent<PlayerController>().SendMessage("setMoving", true);

    }

}
