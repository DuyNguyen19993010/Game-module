using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kuroinu : Item
{
    public float spawnTime;
    public kuroinu()
    {
        this.spawnTime = 10;
        this.type = ItemType.kuroinu;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public override Sprite GetSprite()
    {
        return ItemAssests.Instance.kuroinu;
    }
}
