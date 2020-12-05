using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHub : MonoBehaviour
{
    public Slider HealthBar;
    public Status_EBunny enemyStatus;
    public PlayerAttackController playerAttackController;

    private GameObject closestEnemy;
    private GameObject player;
    private Image HealthBar_Enemy_Back;
    private Image HealthBar_Enemy_Fill;
    private float distanceToClosestEnemy;

    void Start()
    {
        HealthBar = GameObject.Find("HealthBar_Enemy").GetComponent<Slider>();
        player = GameObject.FindWithTag("Player");
        playerAttackController = player.GetComponent<PlayerAttackController>();
        HealthBar_Enemy_Back = GameObject.Find("HealthBar_Enemy_Back").GetComponent<Image>();
        HealthBar_Enemy_Fill = GameObject.Find("HealthBar_Enemy_Fill").GetComponent<Image>();

        HideEnemyHub();
    }

    // Update is called once per frame
    void Update()
    {
        HideEnemyHub();
        closestEnemy = playerAttackController.GetClosestEnemy();
        if (closestEnemy != null)
        {
            distanceToClosestEnemy = Vector3.Distance(closestEnemy.transform.position, player.transform.position);
            if (distanceToClosestEnemy < 20)
            {
                ShowEnemyHub();
                enemyStatus = closestEnemy.GetComponent<Status_EBunny>();
                HealthBar.value = enemyStatus.health / enemyStatus.maxHealth;
                Debug.Log(HealthBar.value);
            }
        }
        else
        {
            HideEnemyHub();
        }
    }

    void HideEnemyHub()
    {
        HealthBar_Enemy_Back.enabled = false;
        HealthBar_Enemy_Fill.enabled = false;
    }

    void ShowEnemyHub()
    {
        HealthBar_Enemy_Back.enabled = true;
        HealthBar_Enemy_Fill.enabled = true;
    }
}