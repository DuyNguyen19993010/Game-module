using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFireBalls : MonoBehaviour
{

    public Transform firePoint0;

    public GameObject fireBallPrefab0;


    void Shoot ()
    {
        Instantiate(fireBallPrefab0, firePoint0.position, firePoint0.rotation);

    }
    

}
