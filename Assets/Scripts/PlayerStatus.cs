// Weijian Hu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float health = 10.0f;
    public float maxHealth = 10.0f;
    public float energy = 10.0f;
    public float maxEnergy = 10.0f;
    public float boostUsage = 5.0f;
    public AudioClip hitSound;
    public AudioClip deathSound;

    public PlayerController controller;
    public PlayerAnimation animationState;
    public CharacterController characterController;
    public AudioSource audios;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        animationState = GetComponent<PlayerAnimation>();
        characterController = GetComponent<CharacterController>();
        audios = GetComponent<AudioSource>();
    }

    public void AddHealth(float boost)
    {
        health += boost;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        if (hitSound)
        {
            audios.clip = hitSound;
            audios.Play();
        }
        if (health <= 0)
        {
            health = 0;
            StartCoroutine(Die());
        }

    }

    public void addEnergy(float boost)
    {
        energy += boost;
        if (energy >= maxHealth)
        {
            energy = maxEnergy;
        }
    }

    IEnumerator Die()
    {
        // audio
        if (deathSound)
        {
            audios.clip = deathSound;
            audios.Play();
        }

        // release player's control
        controller.isControllable = false;

        // die animation
        animationState.PlayDie();

        // wait for die
        yield return StartCoroutine(WaitForDie());

        Debug.Log(characterController.transform.position);
        // hide charcter
        HideCharacter();

        // wait
        yield return StartCoroutine(WaitForOneSeconds());
        Debug.Log("weapon");
        Debug.Log(GameObject.FindGameObjectWithTag("weapon").transform.position);
        // show character
        if (CheckPoint.isActivePt)
        {
            
            Debug.Log(CheckPoint.isActivePt.transform.position);
            characterController.transform.position = CheckPoint.isActivePt.transform.position;
            characterController.transform.position = new Vector3(characterController.transform.position.x,
                characterController.transform.position.y + 0.5f, characterController.transform.position.z);
            Debug.Log(characterController.transform.position);
        }
        ShowCharacter();
        Debug.Log("weapon");
        Debug.Log(GameObject.FindGameObjectWithTag("weapon").transform.position);
        Debug.Log(characterController.transform.position);

        animationState.ReBorn();

        // reset status
        health = maxHealth;
    }

    IEnumerator WaitForDie()
    {
        yield return new WaitForSeconds(3.0f);
    }

    IEnumerator WaitForOneSeconds()
    {
        yield return new WaitForSeconds(1.0f);
    }

    void HideCharacter()
    {
        GameObject.FindGameObjectWithTag("body").GetComponent<SkinnedMeshRenderer>().enabled = false;
        GameObject.FindGameObjectWithTag("weapon").GetComponent<MeshRenderer>().enabled = false;
        GameObject.FindGameObjectWithTag("shield").GetComponent<MeshRenderer>().enabled = false;
        controller.isControllable = false;
    }

    void ShowCharacter()
    {
        GameObject.FindGameObjectWithTag("body").GetComponent<SkinnedMeshRenderer>().enabled = true;
        GameObject.FindGameObjectWithTag("weapon").GetComponent<MeshRenderer>().enabled = true;
        GameObject.FindGameObjectWithTag("shield").GetComponent<MeshRenderer>().enabled = true;
        controller.isControllable = true;
    }
}
