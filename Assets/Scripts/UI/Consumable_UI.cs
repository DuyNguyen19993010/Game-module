using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Consumable_UI : MonoBehaviour
{
    private PlayerConsumable playerConsumable;
    private Transform ConsumableContainer;
    private Transform ConsumableSlotTemplate;
    public GameObject player;
    private bool closed;

    void Awake()
    {
        closed = false;
        // player = GameObject.Find("Player");
        ConsumableSlotTemplate = transform.Find("ConsumableSlotTemplate");
        SetConsumableInventory(player.GetComponent<InventoryManager>().playerConsumable);
    }
    void Update()
    {

    }

    public void SetConsumableInventory(PlayerConsumable consumable)
    {
        Debug.Log(player.name);
        this.playerConsumable = consumable;
        RefreshConsumableInventory();
    }
    public void RefreshConsumableInventory()
    {
        int i = 0;
        foreach (Transform item in transform)
        {
            if (i == 0)
            {
            }
            else
            {
                Destroy(item.gameObject);
            }
            i++;

        }
        foreach (Item item in playerConsumable.GetItemList())
        {
            RectTransform consumableSlot = Instantiate(ConsumableSlotTemplate, transform).GetComponent<RectTransform>();
            consumableSlot.gameObject.SetActive(true);
            Image image = consumableSlot.Find("Item").GetComponent<Image>();
            image.sprite = item.GetSprite();
            try
            {
                if (consumableSlot.Find("TextPro").GetComponent<TextMeshProUGUI>() != null)
                {

                }
                consumableSlot.Find("TextPro").GetComponent<TextMeshProUGUI>().text = item.max.ToString();

            }
            catch
            {
                Debug.Log("Text not found");
            }

        }

    }
}
