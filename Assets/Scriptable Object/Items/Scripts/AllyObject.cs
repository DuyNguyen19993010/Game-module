using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Ally Object", menuName = "Inventory System/Items/Ally")]
public class AllyObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.ally;
    }
}
