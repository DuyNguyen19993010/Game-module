using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    //animator
    private Animator animator;
    public float HP;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();


    }
    // Update is called once per frame
    IEnumerator decreaseHP(float damage)
    {
        Debug.Log("Player damaged with " + damage);
        // Play hurt animation
        // animator.SetTrigger("Damage");
        //Disable movement for 1s 
        gameObject.GetComponent<PlayerController>().SendMessage("setMoving", false);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        animator.SetFloat("Speed", 0);
        yield return new WaitForSeconds(1 / 2);
        //Enable movement
        gameObject.GetComponent<PlayerController>().SendMessage("setMoving", true);
        // HP -= damage;

    }
    void increaseHP(float buff)
    {
        HP += buff;
    }

}
