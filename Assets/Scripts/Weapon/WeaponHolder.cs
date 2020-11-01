using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public int selectedWeapon;
    public GameObject player;
    public bool RageMode;
    public Transform activeWeapon;
    void Start()
    {
        selectedWeapon = 0;
        RageMode = false;
        SelectWeapon();
        player = GameObject.FindGameObjectsWithTag("player")[0];
    }

    void Update()
    {
        if (RageMode)
        {
            selectedWeapon = 1;
            SelectWeapon();
        }
        else
        {
            selectedWeapon = 0;
            SelectWeapon();
        }

    }
    public void setRage(bool val)
    {
        RageMode = val;
    }
    public void DebugMyStuff()
    {
        Debug.Log("Working");
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {

            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);

            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;

        }

    }
}
