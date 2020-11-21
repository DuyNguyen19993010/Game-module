using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVL2_BossCombat : MonoBehaviour
{
    public Transform firepoint;
    public GameObject WindAttackPrefab;
    //[SerializeField] private DetectPlayer colliderTrigger;
    private bool canAttack1;
    private int count;
    private Animator animator;
    private int LoopTimes;
    //-------------------------Tornado-------------------------
    public GameObject TornadoPrefab;
    public GameObject IndicatorPrefab;
    private float spawnX;
    private float spawnY;
    private float minX;
    private bool canAttack2;
    private bool canDestroy;
    private int countT;
    public Vector2 TornadoPos;
    //---------------------------------------------------------
    private bool canAttack3;


    // Start is called before the first frame update
    void Start()
    {
        canAttack1 = true;
        canAttack2 = false;
        canDestroy = false;
        canAttack3 = false;
        LoopTimes = 0;
        animator = gameObject.GetComponent<Animator>();
        count = 0;
        countT = 0;
        minX = GameObject.Find("Player").transform.position.x;


        // colliderTrigger.DetectTrigger += DetectPlayer_DetectTrigger;

    }

    /*   private void DetectPlayer_DetectTrigger(object sender, System.EventArgs e){
          startBattle();
          colliderTrigger.DetectTrigger -= DetectPlayer_DetectTrigger;
      }
   */
    void Update()
    {
        if (LoopTimes == 3)
        {
            destroyTornados();
            canAttack1 = false;
            canAttack2 = false;
            Invoke("setAttack1", 5);
            LoopTimes = 0;
        }
        if (canAttack1)
        {

            animator.SetBool("Attack1", true);
            AttackType1();
            //animator.SetBool("Attack1", false);
            canAttack1 = false;
            StartCoroutine("ResetWindAttack", 1);
        }
        else if (canAttack2)
        {
            if (canDestroy)
            {
                destroyTornados();
            }
            animator.SetBool("Attack2", true);
            AttackType2();
            canAttack2 = false;
            canDestroy = true;
            StartCoroutine("ResetTornado", 2);
            /* if(canDestroy){
                destroyTornados();
            } */
        }



    }
    void AttackType1()
    {
        Instantiate(WindAttackPrefab, firepoint.position, firepoint.rotation);
        count++;
    }

    void AttackType2()
    {
        for (int i = 0; i < 3; i++)
        {

            Vector2 Indicator;
            spawnX = Random.Range(minX, GameObject.Find("Level2Boss").transform.position.x);
            spawnY = GameObject.Find("Level2Boss").transform.position.y;
            TornadoPos = new Vector2(spawnX, spawnY);
            Indicator = new Vector2(spawnX, spawnY);
            Instantiate(IndicatorPrefab, Indicator, IndicatorPrefab.transform.rotation);
            StartCoroutine("IndicatorTimer", 1.5);


        }
        countT++;

    }

    IEnumerator ResetWindAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        //Debug.Log(count);
        if (count < 3)
        {
            canAttack1 = true;
            animator.SetBool("Attack1", true);
        }
        else if (count == 3)
        {
            canAttack1 = false;
            canAttack2 = true;
            count = 0;
            animator.SetBool("Attack1", false);
        }
    }
    IEnumerator ResetTornado(float delay)
    {
        yield return new WaitForSeconds(delay);
        //Debug.Log(count);
        if (countT < 3)
        {
            canAttack2 = true;
        }
        else if (countT == 3)
        {
            destroyTornados();
            animator.SetBool("Attack2", false);
            canAttack2 = false;
            canAttack1 = true;
            LoopTimes++;
            countT = 0;

        }
    }

    IEnumerator IndicatorTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        destroyIndicators();
        Instantiate(TornadoPrefab, TornadoPos, TornadoPrefab.transform.rotation);


    }

    void destroyTornados()
    {
        GameObject[] Tornados = GameObject.FindGameObjectsWithTag("Tornado");
        foreach (GameObject o in Tornados)
        {
            Destroy(o);
        }
    }
    void destroyIndicators()
    {
        GameObject[] Indicators = GameObject.FindGameObjectsWithTag("Indicator");
        foreach (GameObject o in Indicators)
        {
            Destroy(o);
        }
    }

    private void setAttack1()
    {
        canAttack1 = true;
    }
}
