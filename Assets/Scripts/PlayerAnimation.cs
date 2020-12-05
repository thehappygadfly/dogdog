using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerController controller;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        if (animator.layerCount == 2)
        {
            animator.SetLayerWeight(1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.IsGrounded())
        {
            animator.SetBool("isFallDown", true);

            if (controller.IsBoosting())
            {
                animator.SetBool("isBoost", true);
            }
            else
            {
                animator.SetBool("isBoost", false);
            }

            if (controller.IsMoving())
            {
                animator.SetFloat("Speed", Input.GetAxis("Vertical"));
            }

        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetBool("isJump", true);
            }
            if (Input.GetButtonUp("Jump"))
            {
                animator.SetBool("isJump", false);
            }
            if (!controller.IsGrounded())
            {
                animator.SetBool("isFallDown", true);
            }
        }
    }

    public void GotHit()
    {
        animator.SetBool("isGotHit", true);
    }

    public void PlayDie()
    {
        animator.SetBool("isDead", true);
    }

    public void ReBorn()
    {
        animator.SetBool("isDead", false);
    }
}
