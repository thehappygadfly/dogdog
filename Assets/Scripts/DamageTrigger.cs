using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public float damage = 20.0f;
    public PlayerStatus status;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // status = other.GetComponent<PlayerStatus>();
        status = GameObject.FindWithTag("Player").GetComponent<PlayerStatus>();
        status.ApplyDamage(damage);
    }
}
