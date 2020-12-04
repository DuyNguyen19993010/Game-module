using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuroinuExplode : MonoBehaviour
{
    [Header("Particle effects")]
    public GameObject explosionEffectLayer1;
    public GameObject explosionEffectLayer2;
    public GameObject explosionEffectLayer3;
    public GameObject explosionEffectLayer4;
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


    public void explosionEffectLayer1Play()
    {
        Instantiate(explosionEffectLayer1, transform.position, transform.rotation);
    }
    public void explosionEffectLayer2Play()
    {
        Instantiate(explosionEffectLayer2, transform.position, transform.rotation);
    }
    public void explosionEffectLayer3Play()
    {
        Instantiate(explosionEffectLayer3, transform.position, transform.rotation);
    }
    public void explosionEffectLayer4Play()
    {
        Instantiate(explosionEffectLayer4, transform.position, transform.rotation);
    }

}
