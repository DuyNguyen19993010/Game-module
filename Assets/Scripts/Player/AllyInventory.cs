using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyInventory
{
    List<Item> ally = new List<Item>();
    public AllyInventory()
    {
        ally = new List<Item>();
        AddAlly(new shiroinu());
        AddAlly(new kuroinu());
    }
    public void AddAlly(Item item)
    {
        ally.Add(item);
    }
    public List<Item> GetItemList()
    {
        return ally;
    }
}
