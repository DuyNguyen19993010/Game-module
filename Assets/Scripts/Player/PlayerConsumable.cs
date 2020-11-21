using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConsumable
{
    public bool canUse;
    List<Item> consumables = new List<Item>();
    public PlayerConsumable()
    {
        consumables = new List<Item>();
        AddItem(new fortunePouch());
        AddItem(new crimsonAsh());
        AddItem(new homingAsh());
    }
    public void AddItem(Item item)
    {
        consumables.Add(item);
    }
    public List<Item> GetItemList()
    {
        return consumables;
    }

}
