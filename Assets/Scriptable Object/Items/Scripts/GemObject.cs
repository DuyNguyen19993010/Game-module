using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Consumable Object", menuName = "Inventory System/Items/Gem")]
public class GemObject : ItemObject
{
    public float increaseAttack;
    public float increaseHealth;
    public void Awake()
    {
        type = ItemType.gem;
    }

}
