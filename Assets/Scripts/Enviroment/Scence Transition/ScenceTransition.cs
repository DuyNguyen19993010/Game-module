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
