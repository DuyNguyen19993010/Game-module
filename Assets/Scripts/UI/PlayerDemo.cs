using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerDemo : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void PlayAttack_test()
    {
        animator.Play("attack_test");
    }
}
