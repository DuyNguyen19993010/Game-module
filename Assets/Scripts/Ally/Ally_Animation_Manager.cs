using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally_Animation_Manager : MonoBehaviour
{
    //-----------------------Animator-----------------------
    private Animator animator;

    //----------------------Get the ally movement------------------
    [Header("Ally Movement")]
    Shiroinu shiroinu;
    bool isShiroinu;
    bool isKuroinu;

    void Start()
    {

        animator = gameObject.GetComponent<Animator>();
        try
        {
            //Try to get shiroinu
            shiroinu = gameObject.GetComponent<Shiroinu>();
            isKuroinu = false;
            isShiroinu = true;

        }
        catch
        {
            //Get kuroinu instead;
            isShiroinu = false;
            isKuroinu = true;
        }
    }
    void Update()
    {

        if (isShiroinu)
        {
            if (shiroinu.isRunning)
            {
                animator.SetBool("isRunning", true);

            }
            else
            {
                animator.SetBool("isRunning", false);
            }
            if (shiroinu.isDiving)
            {
                animator.SetTrigger("Dive");
                animator.SetBool("isDiving", true);
            }
            if (shiroinu.explode)
            {
                animator.SetBool("isDiving", false);
            }

        }
    }
    void Explode()
    {
        Destroy(transform.gameObject);
    }



}
