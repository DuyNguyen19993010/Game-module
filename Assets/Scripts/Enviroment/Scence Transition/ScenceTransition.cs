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
            Debug.Log(Cams.Count);
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

        }
    }

    IEnumerator stopPlayer()
    {
        Player[0].GetComponent<PlayerController>().SendMessage("setMoving", false);
        Player[0].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(timeToWait);
        Player[0].GetComponent<PlayerController>().SendMessage("setMoving", true);

    }
    public void addCam(GameObject cam)
    {
        Cams.Add(cam);
    }
}
