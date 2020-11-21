using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private EnemyMovement movement;
    private EnemyCombat combat;
    private Animator animator;
    void Start()
    {
        movement = gameObject.GetComponent<EnemyMovement>();
        combat = gameObject.GetComponent<EnemyCombat>();
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        animator.SetBool("isRunning", movement.isMoving);
    }
    void attack_animation()
    {
        animator.SetTrigger("attack");
    }
}
