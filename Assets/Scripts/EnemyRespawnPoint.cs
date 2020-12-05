using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnPoint : MonoBehaviour
{
    public float spawnRange = 40.0f;
    public GameObject enemy;

    private Transform target;
    private GameObject currentEnemy;
    private bool isOutsideRange = true;
    private Vector3 distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = transform.position - target.position;
        if (distanceToPlayer.magnitude < spawnRange)
        {
            if (!currentEnemy)
            {
                currentEnemy = Instantiate(enemy, transform.position, transform.rotation) as GameObject;
            }
            isOutsideRange = false;
        }
        else
        {
            if (currentEnemy)
            {
                Destroy(currentEnemy);
            }
        }
        isOutsideRange = true;
    }
}
