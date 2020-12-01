using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFireGround : MonoBehaviour
{
    public Transform GroundPoint0;
    public Transform GroundPoint1;
    public Transform GroundPoint2;
    public Transform GroundPoint3;
    public Transform GroundPoint4;

    public GameObject fireGroundPrefab0;
    public GameObject fireGroundPrefab1;
    public GameObject fireGroundPrefab2;
    public GameObject fireGroundPrefab3;
    public GameObject fireGroundPrefab4;

    public void Spawn()
    {
        Instantiate(fireGroundPrefab0, GroundPoint0.position, GroundPoint0.rotation);
        Instantiate(fireGroundPrefab1, GroundPoint1.position, GroundPoint1.rotation);
        Instantiate(fireGroundPrefab2, GroundPoint2.position, GroundPoint2.rotation);
        Instantiate(fireGroundPrefab3, GroundPoint3.position, GroundPoint3.rotation);
        Instantiate(fireGroundPrefab4, GroundPoint4.position, GroundPoint4.rotation);

        
    }
}
