using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //----------------------------Main Inventory UI--------------------
    public GameObject Inventory_UI;
    // -------------------Consumable Inventory----------------------
    [Header("Consumable inventory")]
    public PlayerConsumable playerConsumable;
    public Consumable_UI consumable_UI;

    //--------------------Ally Inventory-----------------------
    [Header("Ally inventory")]
    public AllyInventory allyInventory;
    public Ally_UI ally_UI;



    // -------------------Skill Inventory----------------------
    public int selectedConsumable;
    private int selectedSkill;
    private int selectedAlly;
    private bool closed;
    [Header("Particle Effects")]
    public GameObject healParticle;
    public GameObject crimsonAshesEffect;
    void Awake()
    {
        selectedConsumable = 0;
        selectedAlly = 0;
        playerConsumable = new PlayerConsumable();
        allyInventory = new AllyInventory();
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab) && closed)
        {
            closed = false;
            Inventory_UI.gameObject.SetActive(true);
            consumable_UI.GetComponent<Consumable_UI>().RefreshConsumableInventory();
            ally_UI.GetComponent<Ally_UI>().RefreshAllyInventory();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !closed)
        {
            Inventory_UI.gameObject.SetActive(false);
            closed = true;
        }
        // ------------------------UseConsumbale-----------------------
        if (Input.GetKeyDown(KeyCode.F))
        {
            UseConsumbale();

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            nextConsumable();
        }
    }



    // ----------------------------Consumable---------------------------
    void UseConsumbale()
    {
        if (playerConsumable.GetItemList()[selectedConsumable].amount > 0)
        {
            if (playerConsumable.GetItemList()[selectedConsumable].type.ToString() == "fortunePouch")
            {

                Instantiate(healParticle, transform.position, transform.rotation);
                gameObject.GetComponent<PlayerStat>().SendMessage("increaseHP", (playerConsumable.GetItemList()[selectedConsumable] as fortunePouch).Heal());
            }
            else if (playerConsumable.GetItemList()[selectedConsumable].type.ToString() == ("crimsonAsh"))
            {
                //Make player can not be hurt by anything
                StartCoroutine(useCrimsonAsh());
            }
            else if (playerConsumable.GetItemList()[selectedConsumable].type.ToString() == ("homingAsh"))
            {
                //Teleport player
                transform.position = gameObject.GetComponent<PlayerStat>().checkpointPosition;
            }
            playerConsumable.GetItemList()[selectedConsumable].decreaseAmount();
        }
        else
        {
            Debug.Log("No more " + playerConsumable.GetItemList()[selectedConsumable].type + " to use");
        }

    }
    IEnumerator useCrimsonAsh()
    {
        gameObject.GetComponent<PlayerStat>().canBeHurt = false;
        Instantiate(crimsonAshesEffect, transform.position, transform.rotation);
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
        yield return new WaitForSeconds(5.0f);
        sprite.color = Color.white;
        gameObject.GetComponent<PlayerStat>().canBeHurt = true;
    }

    void nextConsumable()
    {
        selectedConsumable += 1;
        if (selectedConsumable > playerConsumable.GetItemList().Count - 1)
        {
            selectedConsumable = 0;
        }
        Debug.Log("Selecting :" + playerConsumable.GetItemList()[selectedConsumable].type);
    }
    // Reset consumable amount to max
    public void ResetConsumable()
    {
        foreach (Item item in playerConsumable.GetItemList())
        {
            item.resetAmount();
        }
    }
}
