using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillar : MonoBehaviour
{
    [Header("Appear time")]
    public float lastingTime;
    private float disapearTime;
    void Start()
    {
        lastingTime = 10;
        disapearTime = Time.time + lastingTime;
    }

    void Update()
    {
        if (Time.time > disapearTime)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyStat>().SendMessage("decreaseHP", 10);
        }
    }
}
