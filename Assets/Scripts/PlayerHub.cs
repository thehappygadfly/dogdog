using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHub : MonoBehaviour
{
    public Slider Health;
    public Slider Energy;
    public PlayerStatus status;

    // Start is called before the first frame update
    void Start()
    {
        Health = GameObject.Find("HealthBar_player").GetComponent<Slider>();
        Energy = GameObject.Find("EnergyBar_Player").GetComponent<Slider>();
        status = GameObject.FindWithTag("Player").GetComponent<PlayerStatus>();
        Debug.Log("find");
    }

    // Update is called once per frame
    void Update()
    {
        Health.value = status.health / status.maxHealth;
        Energy.value = status.energy / status.maxEnergy;
    }
}
