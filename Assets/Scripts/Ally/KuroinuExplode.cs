using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuroinuExplode : MonoBehaviour
{
    public float Explode_radius;
    public LayerMask enemyLayer;
    void Start()
    {
        Explode_radius = 0.53f;
    }
    void Update()
    {

    }
    void explode()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, Explode_radius, enemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<EnemyStat>().decreaseHP(25);
        }
        Destroy(gameObject);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Explode_radius);

    }

}
