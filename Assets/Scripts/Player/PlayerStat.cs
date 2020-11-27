using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    //---------------------------Player instance ------------------------------
    public Player player;
    //animator
    private Animator animator;
    //---------------------Stat
    [Header("Max Stat")]
    public float maxHP;
    public float maxRage;
    public float damage;
    public float maxHP_temp;
    public float maxRage_temp;
    [Header("Current stat")]
    public float currentHP;
    public float Rage;
    //Check if rage mode is activated or not
    public bool isRaging;
    //Get playermovement
    private PlayerController playermovement;
    //Check if player can be hurt
    [Header("Can be hurt or not")]
    public bool canBeHurt;
    //Check if player is in parry state
    private bool isParrying;
    private bool canParry;

    // Start is called before the first frame update
    void Start()
    {
        player = new Player(10, 10, 5);
        canBeHurt = true;
        maxHP = player.maxHP;
        maxRage = player.maxRage;
        damage = player.damage;
        currentHP = maxRage;
        Rage = maxRage;
        isRaging = false;
        animator = gameObject.GetComponent<Animator>();
        playermovement = gameObject.GetComponent<PlayerController>();
        isParrying = false;
        canParry = true;

    }
    void Update()
    {
        //--------------If rage mode is activate ,decrease it over time---------
        if (isRaging)
        {
            Rage -= Time.deltaTime;
            if (Rage <= 0)
            {
                //Deactivate Rage mode
                Rage = 0;
                isRaging = false;
                damage -= 3;
                Debug.Log("Ragemode deactivated");
            }
        }
        //--------------Activate Ragemode---------
        if (Input.GetKeyDown(KeyCode.R) && Rage == maxRage)
        {
            //Activate Rage mode
            isRaging = true;
            damage += 3;
        }
        if (canParry)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                StartCoroutine(Parry());

            }
        }
    }
    // Update is called once per frame


    // Modification for current real time stat
    void increaseHP(float amount)
    {
        Debug.Log("Increase by" + amount);
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    void decreaseHP(float damage)
    {
        if (canBeHurt)
        {
            if (isParrying)
            {
                Debug.Log("-------------------No damage taken----------------");

            }
            else
            {
                animator.SetTrigger("hurt");
                // Play hurt animation
                currentHP -= damage;
                if (currentHP <= 0)
                {
                    currentHP = 0;
                    //PLay death animation
                }
            }
        }

    }
    void increaseDamage(float amount)
    {
        damage += amount;
    }
    void decreaseDamage(float amount)
    {
        damage -= amount;
    }
    void increaseRage(float amount)
    {
        if (!isRaging)
        {
            damage += 3;
        }

    }
    void decreaseRage(float amount)
    {
        Rage -= amount;
    }
    void resetRage()
    {
        Rage = 0;
    }


    // Modification for current permanent stat

    void increaseMaxHP(float amount)
    {
        maxHP_temp = maxHP;
        maxHP += amount;
    }

    void decreaseMaxHP(float amount)
    {
        maxHP_temp = maxHP;
        maxHP += amount;
    }

    void increaseMaxRage(float amount)
    {
        maxRage_temp += amount;
        maxRage += amount;
    }

    void decreaseMaxRage(float amount)
    {
        maxRage_temp += amount;
        maxHP += amount;

    }
    //--------------Set the player state to parrying,meaning wont take damage and will deflect enemy attack
    IEnumerator Parry()
    {
        //-----------------Disable parry ability because player is using it------
        canParry = false;
        playermovement.SendMessage("FreezePlayer");
        yield return new WaitForSeconds(0.05f);
        //-----------------Start parrying duration------
        StartParry();
        yield return new WaitForSeconds(1f);
        //-----------------End parrying duration and let player parry again------
        StopParry();
        playermovement.SendMessage("UnFreezePlayer");
        canParry = true;
    }
    void StartParry()
    {
        isParrying = true;
    }
    void StopParry()
    {
        isParrying = false;

    }

    void ShakeCamera()
    {
        CameraShake.Instance.ShakeCamera(0.2f, .1f);
    }


}
