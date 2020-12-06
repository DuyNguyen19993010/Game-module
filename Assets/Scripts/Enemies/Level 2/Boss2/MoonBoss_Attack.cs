using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBoss_Attack : MonoBehaviour
{
    //------------------used to flip boss----------
    private bool faceRight;     // check boss is facing right or not
    //------------------detect the player inside battale area--------
    public bool Fight;
    //------------------Spin Attack--------------
    private bool spinMove;      //check if allow to spin
    private float originalPos;  //initial position x of the boss
    private int counter;    // track the spinning times
    private bool hurtBySpin;    //player can only hit by spin one time per loop
    //------------------Jump Attack-----------------
    private bool jumpMove;  // if boss is in air
    private Vector3 jumpAttackTarget;   //the target position which the boss is jumping to
    //------------------Wall Check------------------
    public LayerMask walllayer;
    private RaycastHit2D wallCheck;
    private float wallDetectRadius;
    //----------------------------------------------
    private PlayerStat stats;   // Playerstat
    private Rigidbody2D rb;
    private Animator animator;
    private bool Attacking;     //if boss is attacking
    private bool collideDmg;    //Damage player when it collide with boss

    // Start is called before the first frame update
    void Start()
    {
        faceRight = false;
        Fight = false;
        stats = GameObject.Find("Player").GetComponent<PlayerStat>();
        collideDmg = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Attacking = false;
        spinMove = false;
        wallDetectRadius = 0.19f;
        counter = 0;
        jumpMove = false;
        originalPos = transform.position.x;
        hurtBySpin = true;
        flip(); //As player enters the room on the left hand side, so flip the boss first to face to player
    }

    // Update is called once per frame
    void Update()
    {
        //-----------------if player collides with boss, Damage player 1 hp--------------
        try
        {
            if (Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position) < 0.05 && collideDmg)
            {
                stats.SendMessage("decreaseHP", 1);     //damage active
                                                        //---------------disable collide damage with 2s, so player won't get hurt multiple times----------------
                collideDmg = false;
                Invoke("canbeCollide", 2);
                //------------------------------------------------------------------------------------------------------
            }
        }
        catch { }
        if (Fight)
        {  //if player enter the room
           //-----------------------start the battle only once, so won't call attacks every frame-----------------
            if (!Attacking)
            {
                startSpinAttack();  //start spin attack
                Attacking = true;

            }
            //-------------------------------------------------------------------------------------------------------
            if (spinMove)
            {   //if boss is spinning
                collideDmg = false;     //disable collide damage to avoid damage player two times(once spin, once collide)
                spinDmg();      //check if can deal spin damage
                checkWall();    //wall checking
                //----------if boss is on a set position which is left hand side of the room--------------------
                if (Mathf.Abs(transform.position.x - 59.30f) < 0.1 && faceRight)
                {
                    counter += 1;   //make sure boss is still in the spinning attack
                    flip();     //change the direction of the boss is facing to right
                    rb.velocity = transform.right * 1;      //spin with speed 1
                }
                //----------------------------------------------------------------------------------------------
                //----------if boss is back to the original position and it is in the spinning loop, not just start spinning--------------------
                else if (Mathf.Abs(transform.position.x - originalPos) < 0.1 && counter != 0)
                {
                    spinMove = false;   //finish spining
                    collideDmg = true;  //enable collide damage
                    rb.velocity = new Vector2(0, 0);    //disable move of the boss
                    flip();     //flip to left
                    stopSpinAttack();   //do the stop spin animation and quit spin attack       
                }
                //-------------------------------------------------------------------------------------------------------------------------------
                //-----------if boss just start spin, so the first spin------------------------------------------
                else if (counter == 0)
                {
                    rb.velocity = transform.right * 1;  //spin to the left because flip changed the direction of the boss facing
                }
                //------------------------------------------------------------------------------------------------
            }
            //---------if boss is in the air----------
            if (jumpMove)
            {
                transform.position = Vector2.MoveTowards(transform.position, jumpAttackTarget, 1.5f * Time.deltaTime);    // move to the jumAttackTarget position
                if (Mathf.Abs(transform.position.x - jumpAttackTarget.x) < 0.1)
                {     // when boss is close enough to the target
                    jumpMove = false;   //set to false
                    animator.SetBool("InAir", false);    //start the jumpAttack anmiation
                }
            }

        }
    }
    //---------------------fliping boss-------------------------
    void flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }
    //-----------------------------------------------------------
    //---------------------start the battle----------------------
    void startFight()
    {
        Fight = true;
    }
    //----------------------------------------------------------
    //----------------------start spin animation----------------------
    void startSpinAttack()
    {
        animator.SetTrigger("StartSpin");
    }
    //----------------------------------------------------------------
    //----------------------Animation event triggered when starSpin animation finished-----------
    void spinAttack()
    {
        animator.SetBool("SpinAttack", true);
        spinMove = true;    //boss is spinning
    }
    //-------------------------------------------------------------------------------------------
    //----------------------Spin damage-----------------------------------
    void spinDmg()
    {
        if (Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) < 0.2 && hurtBySpin) //if player is in range and have not been hurt by the spin yet
        {
            stats.SendMessage("decreaseHP", stats.damage);  //hurt player
            hurtBySpin = false; //disable spin damge until next spin loop
        }
    }
    //-------------------------------------------------------------------
    //------------------------Stop spin attack and start jump attack-------------------------
    void stopSpinAttack()
    {
        animator.SetBool("SpinAttack", false);   //play the finish spin animation
        hurtBySpin = true;  // can be hurt by spin again as the loop finished
        counter = 0;    //set loop counter to 0
        jumpAttack();   //start jump attack
    }

    //------------------------Start jump attack------------------------------------------
    void jumpAttack()
    {
        jumpAttackTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;   // locate where the player is
        if (Vector2.Distance(transform.position, jumpAttackTarget) < 0.5)
        {  //if player is too close to the boss, do the spin attack instead of jump attack
            startSpinAttack();
        }
        else
        {
            animator.SetTrigger("StartJump");   //do the start jump animation
            //---------------------------make sure boss is facing to the player--------------------------------------------------------
            if (transform.position.x - GameObject.Find("Player").transform.position.x > 0 && !faceRight)
            {
                flip();
            }
            else if (transform.position.x - GameObject.Find("Player").transform.position.x < 0 && faceRight)
            {
                flip();
            }
        }

    }
    //--------------------Animation event that be called when startJump animation finished--------------------
    void jump()
    {
        animator.SetBool("InAir", true); //play jump animation
        rb.velocity = Vector2.up * 3f;  //jump
        jumpMove = true;    //boss is jumping
    }
    //---------------------------------------------------------------------------------------------------
    //--------------------------------Animation event that be called when boss slam the hammer-----------------------------
    void jumpAttackDmg()
    {
        collideDmg = false; //disable collide damge
        if (Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) < 0.15)  //if player is in the attack range damge player
        {
            stats.SendMessage("decreaseHP", stats.damage);  //hurt player
        }
        startSpinAttack();  //start spin attack
    }
    //-----------------------------------------------------------------------------------------------------------------------
    //--------------------------------Check if there is a wall in front of boss using raycast----------------------------------
    void checkWall()
    {
        wallCheck = Physics2D.Raycast(transform.position, transform.right, wallDetectRadius, walllayer);
        if (wallCheck.collider != null)
        {
            flip(); //if there is wall, flip the boss
        }
    }
    //----------------------------------------------------------------------------------------------------------------------
    //------------------Collide damge controller---------------------
    void canbeCollide()
    {
        collideDmg = true;
    }
}
