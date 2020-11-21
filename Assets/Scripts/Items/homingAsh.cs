using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homingAsh : Item
{
    public homingAsh()
    {
        this.type = ItemType.homingAsh;
        this.max = 1;
        this.amount = this.max;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override Sprite GetSprite()
    {
        return ItemAssests.Instance.homingAsh;
    }
}
