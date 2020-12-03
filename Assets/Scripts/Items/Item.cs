using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public enum ItemType
    {
        fortunePouch,
        crimsonAsh,
        homingAsh,
        shiroinu,
        kuroinu
    };
    public ItemType type;
    public int max;
    public int amount;
    void increaseMax(int _amouunt)
    {
        max += _amouunt;
    }
    void increaseAmount()
    {
        amount += 1;
        if (amount > max)
        {
            amount = max;

        }
    }
    public void resetAmount()
    {
        amount = max;
    }
    public void decreaseAmount()
    {
        if (amount - 1 >= 0)
        {
            amount -= 1;
        }
    }

    public abstract Sprite GetSprite();
}
