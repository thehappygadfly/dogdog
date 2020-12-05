using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public PlayerController controller;
    public Animator animator;

    public float attackTime = 1.0f;
    public Vector3 attackPosition = new Vector3(0, 1, 0);
    public float attackRadius = 3.0f;
    public float damage = 1.0f;

    private float timer = 0.0f;
    private bool isBusy = false;
    private Vector3 ourLocation;
    private GameObject[] enemies;
    private GameObject closestEnemy;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(!isBusy && Input.GetKeyDown(KeyCode.J) && timer > attackTime)
        {
            StartCoroutine(DidAttack());
            isBusy = true;
            timer = 0;
        }
    }

    IEnumerator DidAttack() // attack animation
    {
        animator.SetBool("Attack01", true);
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("Attack01", false);

        ourLocation = transform.TransformPoint(attackPosition);
        enemies = GameObject.FindGameObjectsWithTag("enemy");

        foreach(GameObject enemy in enemies)
        {
            Status_EBunny enemyStatus = enemy.GetComponent<Status_EBunny>();
            if (enemyStatus == null)
            {
                continue;
            }

            if (Vector3.Distance(enemy.transform.position ,ourLocation) < attackRadius)
            {
                enemyStatus.ApplyDamage(damage);
            }
        }

        isBusy = false;
    }

    public GameObject GetClosestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        float distanceToEnemy = Mathf.Infinity;

        foreach(GameObject enemy in enemies)
        {
            float newDistanceToEnemy = Vector3.Distance(enemy.transform.position, this.transform.position);
            if (newDistanceToEnemy < distanceToEnemy)
            {
                distanceToEnemy = newDistanceToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
