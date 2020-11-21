using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fortunePouch : Item
{
    public int heal;
    public fortunePouch()
    {
        this.type = ItemType.fortunePouch;
        this.heal = 1;
        this.max = 2;
        this.amount = this.max;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public int Heal()
    {
        return this.heal;
    }
    public override Sprite GetSprite()
    {
        return ItemAssests.Instance.fortunePouch;
    }
}
