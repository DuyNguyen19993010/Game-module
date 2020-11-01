using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat : MonoBehaviour
{
    public PlayerStat playerstat;
    public string weapon_name;
    protected float damage;
    public GameObject player;
    public Transform attackBox;
    protected Animator animator;
    protected LayerMask enemies;
    protected LayerMask bosses;
    public float basic_attack_Range;
    public float basic_attack_wait_time;

    void Start()
    {
        basic_attack_Range = 1;
        enemies = LayerMask.GetMask("Enemy");
        bosses = LayerMask.GetMask("Boss");
        playerstat = player.GetComponent<PlayerStat>();

    }

    public void Buff() { }
    public void SpecialAttack() { }

    public void Enhancement() { }
}
