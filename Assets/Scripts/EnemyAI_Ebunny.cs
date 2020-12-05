// Weijian Hu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Ebunny : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed = 30.0f;
    public float directionTraveltime = 2.0f; //Enemy change direction(time)
    public float idleTime = 1.5f;
    public float walkSpeed = 3.0f;
    public float attackMoveSpeed = 5.0f; // speed when attacking
    public float attackDistance = 15.0f; // chase character
    public bool isAttacking = false;
    public Vector3 attackPostion = new Vector3(0, 1, 0);
    public float attackRadius = 5.0f;
    public float damage = 1.0f;

    private CharacterController characterController;
    private Animation anim;
    private float timeToNewDirection = 0.0f; //time
    private float lastAttackTime = 0.0f;
    private Vector3 distanceToPlayer;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animation>();

        if (!target)
            target = GameObject.FindWithTag("Player").transform;

        anim.wrapMode = WrapMode.Loop;   
        anim["EBunny_Death"].wrapMode = WrapMode.Once;

        anim["EBunny_Death"].layer = 5;
        anim["EBunny_Hit"].layer = 3;
        anim["EBunny_Attack"].layer = 1;

        StartCoroutine(InitEnemy());
        
    }

    IEnumerator InitEnemy()
    {
        while (true)
        {
            yield return StartCoroutine(Idle());
            yield return StartCoroutine(Attack());
        }
    }

    IEnumerator Idle()
    {
        while (true)
        {
            if (Time.time > timeToNewDirection) // change direction
            {
                yield return new WaitForSeconds(idleTime);

                if (Random.value > 0.5)
                    transform.Rotate(new Vector3(0, 5, 0), rotateSpeed);
                else
                    transform.Rotate(new Vector3(0, -5, 0), rotateSpeed);
                timeToNewDirection = Time.time + directionTraveltime;
            }

            Vector3 walkForward = transform.TransformDirection(Vector3.forward);
            characterController.SimpleMove(walkForward * walkSpeed);

            distanceToPlayer = transform.position - target.position;
            if (distanceToPlayer.magnitude < attackDistance) // chase character
            {
                yield break;
            }

            yield return null;
        }

        
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        anim.Play("EBunny_Attack");

        transform.LookAt(target);
        Vector3 direction = transform.TransformDirection(Vector3.forward * attackMoveSpeed);
        characterController.SimpleMove(direction);

        bool lostSight = false; // see character 
        while (!lostSight)
        {
            Vector3 location = transform.TransformPoint(attackPostion) - target.position;
            if (Time.time > lastAttackTime + 2.0f && location.magnitude < attackRadius)
            {
                target.SendMessage("ApplyDamage", damage);
                lastAttackTime = Time.time;
            }

            if (location.magnitude > attackRadius)
            {
                lostSight = true;
                yield break;
            }

            yield return null;
        }

        isAttacking = false;
    }
}
