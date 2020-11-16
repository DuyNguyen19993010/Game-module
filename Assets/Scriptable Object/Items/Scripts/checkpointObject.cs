using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Consumable Object", menuName = "Inventory System/Items/Checkpoint")]
public class checkpointObject : ItemObject
{
    public Transform checkPoint;
    public void Awake()
    {
        type = ItemType.checkpoint;
    }
}
