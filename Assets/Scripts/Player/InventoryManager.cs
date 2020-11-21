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
    private int selectedConsumable;
    private int selectedSkill;
    private int selectedAlly;
    private bool closed;
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

        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !closed)
        {
            Inventory_UI.gameObject.SetActive(false);
            closed = true;
        }
        //-------------------------Use Ally----------------------------
        if (Input.GetKeyDown(KeyCode.Q))
        {
            nextAlly();
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
    // ----------------------------Ally---------------------------
    void nextAlly()
    {
        selectedAlly += 1;
        if (selectedAlly > allyInventory.GetItemList().Count - 1)
        {
            selectedAlly = 0;
        }
        Debug.Log("Selecting :" + allyInventory.GetItemList()[selectedAlly].type);


    }


    // ----------------------------Consumable---------------------------
    void UseConsumbale()
    {
        if (playerConsumable.GetItemList()[selectedConsumable].amount > 0)
        {
            if (playerConsumable.GetItemList()[selectedConsumable].type.ToString() == "fortunePouch")
            {
                gameObject.GetComponent<PlayerStat>().SendMessage("increaseHP", (playerConsumable.GetItemList()[selectedConsumable] as fortunePouch).Heal());
            }
            else if (playerConsumable.GetItemList()[selectedConsumable].type.ToString() == ("crimsonAsh"))
            {
                StartCoroutine(useCrimsonAsh());
            }
            else if (playerConsumable.GetItemList()[selectedConsumable].type.ToString() == ("homingAsh"))
            {
                Debug.Log("Using homing ash");
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
        yield return new WaitForSeconds(3.0f);
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
}
