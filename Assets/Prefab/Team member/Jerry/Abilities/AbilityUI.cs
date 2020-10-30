using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{

    public Image dashImg;
    public float dashCooldown = 5;
    bool isCooldown = false;
    public KeyCode dash;
    // Start is called before the first frame update
    void Start()
    {
        dashImg.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        dashAbility();
    }

    void dashAbility(){
        if(Input.GetKey(dash) && isCooldown == false){
            isCooldown = true;
            dashImg.fillAmount = 1;
        }

        if(isCooldown){
            dashImg.fillAmount -=1 / dashCooldown * Time.deltaTime;
            if(dashImg.fillAmount <= 0){
                dashImg.fillAmount = 0;
                isCooldown=false;
            }
        }
    }
}
