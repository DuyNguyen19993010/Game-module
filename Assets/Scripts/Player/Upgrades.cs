using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    //-------------------Reference to other script-----------------------
    private PlayerStat playerstat;
    private Attacks playerAttack;
    private InventoryManager inventoryManager;
    //-------------------List of upgrade-----------------------
    public bool IncreaseDamage;
    public bool IncreaseAttackSpeed;
    public bool IncreaseFortunePouch_1;
    public bool IncreaseFortunePouch_2;

    void OnEnable()
    {
        playerAttack = gameObject.GetComponent<Attacks>();
        playerstat = gameObject.GetComponent<PlayerStat>();
        inventoryManager = gameObject.GetComponent<InventoryManager>();
        IncreaseDamage = false;
        IncreaseAttackSpeed = false;
        IncreaseFortunePouch_1 = false;
        IncreaseFortunePouch_2 = false;
    }
    // Update is called once per frame
    void Update()
    {
    }
    //Chose which upgrade to unlock
    public void ChooseUnlockUpgrade(int upgradeIndex)
    {
        if (upgradeIndex == 1)
        {
            IncreaseDamage = true;
        }
        if (upgradeIndex == 2)
        {
            IncreaseAttackSpeed = true;

        }
        if (upgradeIndex == 3)
        {
            IncreaseFortunePouch_1 = true;
        }

        if (upgradeIndex == 4)
        {
            IncreaseFortunePouch_2 = true;
        }
        UpgradePlayer();
    }
    public void UpgradePlayer()
    {
        if (IncreaseDamage)
        {
            Debug.Log("Attack damage permanently increased");
            playerstat.damage = 15f;
            playerstat.player.damage = 15f;
        }
        if (IncreaseAttackSpeed)
        {
            playerstat.player.attackDelay = 0.2f;
        }
        if (IncreaseFortunePouch_1)
        {
            inventoryManager.playerConsumable.GetItemList()[0].max = 3;
            inventoryManager.playerConsumable.GetItemList()[0].amount = 3;

        }
        if (IncreaseFortunePouch_2)
        {
            inventoryManager.playerConsumable.GetItemList()[0].max = 4;
            inventoryManager.playerConsumable.GetItemList()[0].amount = 4;
        }

    }
}
