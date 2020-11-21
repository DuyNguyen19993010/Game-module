using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shiroinu : Item
{
    // Start is called before the first frame update
    public float spawnTime;
    public shiroinu()
    {
        this.spawnTime = 10;
        this.type = ItemType.shiroinu;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public override Sprite GetSprite()
    {
        return ItemAssests.Instance.shiroinu;
    }
}
