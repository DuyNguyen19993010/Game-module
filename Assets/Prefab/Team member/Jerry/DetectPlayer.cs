using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DetectPlayer : MonoBehaviour
{
    public event EventHandler DetectTrigger;

    private void OnDetectTrigger(Collider2D collider){
        PlayerController player = collider.GetComponent<PlayerController>();
        if (player != null){
            
            DetectTrigger?.Invoke(this, EventArgs.Empty);
        }
        else{
            Debug.Log("error");
        }
    }
}