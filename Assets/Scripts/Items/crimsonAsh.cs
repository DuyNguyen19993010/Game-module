using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crimsonAsh : Item
{
    public float attackBuff;
    public crimsonAsh()
    {
        this.attackBuff = 30;
        this.type = ItemType.crimsonAsh;
        this.max = 2;
        this.amount = this.max;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override Sprite GetSprite()
    {
        return ItemAssests.Instance.crimsonAsh;
    }
}
