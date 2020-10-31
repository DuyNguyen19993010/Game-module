using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class katana_demo : WeaponStat
{
    public float skillCoolDown;
    void Awake()
    {
        weapon_name = "katana";
        damage = 10;
        skillCoolDown = 4;
        playerstat = player.GetComponent<PlayerStat>();
    }
    public void Buff()
    {
        playerstat.SendMessage("increaseMaxHP", 20);
    }
    public void DebugFix()
    {
        Debug.Log(weapon_name);
    }
    public void SpecialAttack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackBox.position, 4, enemies);
        Collider2D[] boss = Physics2D.OverlapCircleAll(attackBox.position, 4, enemies);
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 20));
        foreach (Collider2D en in enemy)
        {
            en.transform.GetComponent<EnemyCombat>().SendMessage("Damage");
            en.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 20));
        }

    }

}
