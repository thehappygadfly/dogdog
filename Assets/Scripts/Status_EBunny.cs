using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_EBunny : MonoBehaviour
{
    public float health = 10.0f;
    public float maxHealth = 10.0f;
    public int numHeldItemMin = 1; // number of enemy held items
    public int numheldItemMax = 3;
    public GameObject pickup1;
    public GameObject pickup2;

    private bool isDead = false;
    private Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float damage)
    {
        if (health <= 0)
            return;

        health -= damage;

        anim.Play("EBunny_Hit");

        if (health <= 0 && !isDead)
        {
            health = 0;
            isDead = true;
            StartCoroutine(Die());

        }

    }

    IEnumerator Die()
    {
        anim.Stop();
        anim.Play("EBunny_Death");

        Destroy(this.GetComponent<EnemyAI_Ebunny>());
        yield return new WaitForSeconds(2.0f);
        
        Vector3 itemLocation = this.transform.position;

        int rewardItems = Random.Range(numHeldItemMin, numheldItemMax);
        for(int i = 0; i < rewardItems; i++)
        {
            Vector3 randomItemLocation = itemLocation;
            randomItemLocation += new Vector3(Random.Range(-3, 3), 0.2f, Random.Range(-3, 3));
            if (Random.value > 0.5f)
                Instantiate(pickup1, randomItemLocation, pickup1.transform.rotation);
            else
                Instantiate(pickup2, randomItemLocation, pickup2.transform.rotation);
        }

        Destroy(this.gameObject);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
