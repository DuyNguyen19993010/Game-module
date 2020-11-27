using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//----------------Enemy type-----------------
public enum EnemyType
{
    Skeleton,
    FlyingEyeBall,
    Witch,
    Samurai,
    ExplodingDemon,
    BomberMan,
    SpiritBall,
    HammerMan,
    FireBoss,
    MoonBoss


}
public class EnemyStat : MonoBehaviour
{
    public EnemyType enemyType;

    //animator
    private Animator animator;
    //---------------------Stat
    public EnemyMovement enemymovement;
    public float currentHP;
    public float maxHP;
    public float maxHP_temp;
    public float damage;
    //Self reference to be able to respawn
    public Object enemyRef;
    //----------------Enemt spawn position to respawn---------------------
    public Vector2 enemyPosition;
    void Start()
    {
        enemyPosition = transform.position;
        if (enemyType == EnemyType.Samurai || enemyType == EnemyType.Skeleton)
        {
            maxHP = 30;
            currentHP = maxHP;
            damage = 3;
        }
        else if (enemyType == EnemyType.Witch || enemyType == EnemyType.BomberMan || enemyType == EnemyType.ExplodingDemon)
        {
            maxHP = 20;
            damage = 1;
        }
        else if (enemyType == EnemyType.FlyingEyeBall || enemyType == EnemyType.SpiritBall)
        {
            maxHP = 10;
            damage = 1;
        }
        else if (enemyType == EnemyType.HammerMan)
        {
            maxHP = 50;
            damage = 4;
        }
        else if (enemyType == EnemyType.FireBoss || enemyType == EnemyType.MoonBoss)
        {
            maxHP = 150;
            damage = 3;
        }
        currentHP = maxHP;
        //animations
        animator = gameObject.GetComponent<Animator>();
        enemymovement = gameObject.GetComponent<EnemyMovement>();
    }
    void Update()
    {

    }

    // Modification for current real time stat
    void increaseHP(float amount)
    {
        currentHP += amount;
    }
    //Used for damaging the enemy
    public void decreaseHP(float damage)
    {
        //--------------------------------Play hurt animation
        // animator.SetTrigger("hurt");
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        currentHP -= damage;
        if (currentHP <= 0)
        {
            //Play death animation
            // animator.SetTrigger("Die");
            gameObject.SetActive(false);
            Invoke("Respawn", 10);
        }
    }
    //used as animation event to destroy enemy object
    void DeleteEnemy()
    {
        Destroy(gameObject);
    }
    void Respawn()
    {
        GameObject enemyClone = (GameObject)Instantiate(enemyRef);
        enemyClone.SetActive(true);
        enemyClone.transform.position = enemyPosition;
        DeleteEnemy();
    }

}
