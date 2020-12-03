using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuickInventory : MonoBehaviour
{
    //---------------------------Get player attack input------------------------------------------
    private InventoryManager inventoryManager;
    public Transform fortunePouchItem;
    public Transform crimsonAshesItem;
    public Transform homingAshesItem;
    private int chosenItem;
    // Update is called once per frame
    void Start()
    {
        chosenItem = 0;
        InitializeQuickInventory();

        inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
    }
    void Update()
    {
        if (fortunePouchItem.Find("Text-QuickAmount").GetComponent<TextMeshProUGUI>().text != inventoryManager.playerConsumable.GetItemList()[0].amount.ToString())
        {
            fortunePouchItem.Find("Text-QuickAmount").GetComponent<TextMeshProUGUI>().text = inventoryManager.playerConsumable.GetItemList()[0].amount.ToString();
        }
        if (crimsonAshesItem.Find("Text-QuickAmount").GetComponent<TextMeshProUGUI>().text != inventoryManager.playerConsumable.GetItemList()[1].amount.ToString())
        {
            crimsonAshesItem.Find("Text-QuickAmount").GetComponent<TextMeshProUGUI>().text = inventoryManager.playerConsumable.GetItemList()[1].amount.ToString();
        }
        if (homingAshesItem.Find("Text-QuickAmount").GetComponent<TextMeshProUGUI>().text != inventoryManager.playerConsumable.GetItemList()[2].amount.ToString())
        {
            homingAshesItem.Find("Text-QuickAmount").GetComponent<TextMeshProUGUI>().text = inventoryManager.playerConsumable.GetItemList()[2].amount.ToString();
        }
        updateQuickInventory();

    }
    void updateQuickInventory()
    {

        if (inventoryManager.selectedConsumable != chosenItem)
        {
            chosenItem = inventoryManager.selectedConsumable;
            int index = 0;
            foreach (Transform item in transform)
            {
                if (index == chosenItem)
                {
                    item.Find("Border").transform.gameObject.SetActive(true);
                }
                else
                {
                    item.Find("Border").transform.gameObject.SetActive(false);
                }
                index++;
            }
        }
        else
        {

        }

    }
    void InitializeQuickInventory()
    {
        int index = 0;
        foreach (Transform item in transform)
        {
            if (index == chosenItem)
            {
                item.Find("Border").transform.gameObject.SetActive(true);
            }
            else
            {
                item.Find("Border").transform.gameObject.SetActive(false);
            }
            index++;
        }

    }
}
