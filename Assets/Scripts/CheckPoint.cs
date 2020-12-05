using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static CheckPoint isActivePt;
    public CheckPoint firstPt;
    public PlayerStatus status;

    // Start is called before the first frame update
    void Start()
    {
        status = GameObject.FindWithTag("Player").GetComponent<PlayerStatus>();
        isActivePt = firstPt;
    }

    private void OnTriggerEnter()
    {
        if (isActivePt != this)
        {
            isActivePt = this;
        }
        
        status.AddHealth(status.maxHealth);
        status.addEnergy(status.maxEnergy);
    }
}
