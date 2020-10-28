using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceTransition : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform destination;
    GameObject[] toDisable;
    GameObject[] toEnable;
    GameObject[] Player;
    public string fromCam;
    public string toCam;
    public float timeToWait;
    void Start()
    {
        timeToWait = 2.0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            Player = GameObject.FindGameObjectsWithTag("player");
<<<<<<< HEAD
            toDisable = GameObject.FindGameObjectsWithTag(fromCam);
            toEnable = GameObject.FindGameObjectsWithTag(fromCam);
            other.transform.position = destination.position;
            for (int i = 0; i < toDisable.Length; i++)
            {
                toDisable[i].SetActive(false);
            }
            for (int i = 0; i < toEnable.Length; i++)
            {
                toEnable[i].SetActive(true);
            }
            StartCoroutine(stopPlayer());
=======
            // Debug.Log(toDisable.Length);
            // Debug.Log(toEnable.Length);
            other.transform.position = destination.position;
            Debug.Log("--------------------------------------------------------------------------");
            StartCoroutine(stopPlayer());
            Debug.Log(Cams);
            Cams[fromCam - 1].SetActive(false);
            Cams[toCam - 1].SetActive(true);
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
>>>>>>> parent of 4445036... Camera now works flawlessly

        }
    }
    IEnumerator stopPlayer()
    {
        Player[0].GetComponent<PlayerController>().SendMessage("setMoving", false);
        Player[0].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(timeToWait);
        Player[0].GetComponent<PlayerController>().SendMessage("setMoving", true);

    }
<<<<<<< HEAD
=======
    public void addCam(GameObject cam)
    {
        Cams.Add(cam);
    }
>>>>>>> parent of 4445036... Camera now works flawlessly
}
