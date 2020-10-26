using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        transform.position = new Vector2(player.transform.position.x+5, player.transform.position.y+2);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
