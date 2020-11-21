using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ally_UI : MonoBehaviour
{
    private AllyInventory allyInventory;
    private Transform AllyTemplate;
    public GameObject player;

    void Awake()
    {
        // player = GameObject.Find("Player");
        AllyTemplate = transform.Find("AllyTemplate");
        SetAllyInventory(player.GetComponent<InventoryManager>().allyInventory);
    }
    void Update()
    {

    }

    public void SetAllyInventory(AllyInventory allyInventory)
    {
        this.allyInventory = allyInventory;
        RefreshAllyInventory();
    }
    public void RefreshAllyInventory()
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
        foreach (Item ally in allyInventory.GetItemList())
        {
            RectTransform allySlot = Instantiate(AllyTemplate, transform).GetComponent<RectTransform>();
            allySlot.gameObject.SetActive(true);
            Debug.Log("Ally spawned");
            Image image = allySlot.Find("allyIcon").GetComponent<Image>();
            image.sprite = ally.GetSprite();

        }

    }

}
