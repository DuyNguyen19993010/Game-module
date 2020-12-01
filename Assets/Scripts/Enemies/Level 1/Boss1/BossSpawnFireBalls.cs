using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnFireBalls : MonoBehaviour
{

    private float nextActionTime = 0.0f;
    private float period = 5.0f;
    public Transform BossfirePoint0;
    public Transform BossfirePoint1;
    public Transform BossfirePoint2;
    public Transform BossfirePoint3;
    public Transform BossfirePoint4;
    public Transform BossfirePoint5;
    public GameObject BossfireBallPrefab0;
    public GameObject BossfireBallPrefab1;
    public GameObject BossfireBallPrefab2;
    public GameObject BossfireBallPrefab3;
    public GameObject BossfireBallPrefab4;
    public GameObject BossfireBallPrefab5;

    void Update()
    {
        if (Time.time > nextActionTime ) {
            nextActionTime += period;
                Shoot();
        }           
    }

    void Shoot ()
    {
        Instantiate(BossfireBallPrefab0, BossfirePoint0.position, BossfirePoint0.rotation);
        Instantiate(BossfireBallPrefab1, BossfirePoint1.position, BossfirePoint1.rotation);
        Instantiate(BossfireBallPrefab2, BossfirePoint2.position, BossfirePoint2.rotation);
        Instantiate(BossfireBallPrefab3, BossfirePoint3.position, BossfirePoint3.rotation);
        Instantiate(BossfireBallPrefab4, BossfirePoint4.position, BossfirePoint4.rotation);
        Instantiate(BossfireBallPrefab5, BossfirePoint5.position, BossfirePoint5.rotation); 
    }
    

}
