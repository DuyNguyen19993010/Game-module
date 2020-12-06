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
    //checkpoint 
    public Vector2 checkpointPosition;
    //Player Sprite
    SpriteRenderer sprite;
    [Header("Save manager")]
    //used to save the last state of the player to respawn
    public SaveManager saveManager;
    [Header("Particle")]
    public GameObject spirit;
    public ParticleSystem spiritParticle;
    public GameObject hurtEffect;
    public GameObject parryEffect;
    void Awake()
    {
        player = new Player(20, 10, 5, 0.4f);
        canBeHurt = true;
        maxHP = player.maxHP;
        maxRage = player.maxRage;
        damage = player.damage;
        currentHP = player.maxHP;
        Rage = maxRage;
        isRaging = false;
        animator = gameObject.GetComponent<Animator>();
        playermovement = gameObject.GetComponent<PlayerController>();
        isParrying = false;
        canParry = true;
        sprite = gameObject.GetComponent<SpriteRenderer>();

    }
    void OnEnable()
    {
        checkpointPosition = transform.position;
        Debug.Log(checkpointPosition);
    }
    void Update()
    {
        //--------------If rage mode is activate ,decrease it over time---------
        if (isRaging)
        {
            Rage -= Time.deltaTime;
            increaseHP(Time.deltaTime);
            if (Rage <= 0)
            {
                spirit.SetActive(false);
                spiritParticle.Stop();
                //Deactivate Rage mode
                Rage = 0;
                isRaging = false;
                damage -= 3;
                sprite.color = Color.white;
                Debug.Log("Ragemode deactivated");
            }
        }
        //--------------Activate Ragemode---------
        if (Input.GetKeyDown(KeyCode.R) && Rage == maxRage)
        {
            spirit.SetActive(true);
            spiritParticle.Play();
            //Activate Rage mode
            isRaging = true;
            damage += 3;
        }
        if (canParry)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Instantiate(parryEffect, transform.position, transform.rotation);
                StartCoroutine(Parry());

            }
        }
    }
    // Update is called once per frame


    // Modification for current real time stat
    void increaseHP(float amount)
    {
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
                //trigger successparry animation
                animator.SetTrigger("SucessParry");
            }
            else
            {
                animator.SetTrigger("hurt");
                // Play hurt animation
                if (!isRaging)
                {
                    currentHP -= damage;
                    //Player hurt effects
                    Instantiate(hurtEffect, transform.position, transform.rotation);
                }
                else
                {
                    currentHP = currentHP - damage + 3;
                    //Player hurt effects
                    Instantiate(hurtEffect, transform.position, transform.rotation);
                }
                if (currentHP <= 0)
                {
                    saveManager.RespawnSave();
                    currentHP = 0;
                    //PLay death animation
                    Destroy(gameObject);
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
    public void increaseRage(float amount)
    {
        if (!isRaging && Rage <= player.maxRage)
        {
            Debug.Log("Rage is increased ");
            Rage += 3;
            if (Rage > player.maxRage)
            {
                Rage = player.maxRage;
            }
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
        sprite.color = Color.yellow;
        StartParry();
        yield return new WaitForSeconds(1.5f);
        //-----------------End parrying duration and let player parry again------
        StopParry();
        ResetPlayerSpriteColor();
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
    public void ResetPlayerSpriteColor()
    {
        sprite.color = Color.yellow;
    }
    public void CanNotTakeDamage()
    {
        canBeHurt = false;
    }
    public void CanTakeDamage()
    {
        canBeHurt = true;
    }

    void ShakeCamera()
    {
        CameraShake.Instance.ShakeCamera(0.2f, .1f);
    }
    public void RestoreStat()
    {
        currentHP = maxHP;

    }



}
