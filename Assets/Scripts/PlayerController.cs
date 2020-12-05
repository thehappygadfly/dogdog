// Weijian Hu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rollSpeed = 6.0f; // move speed
    public float fastSpeed = 1.5f; // speed up
    public float rotateSpeed = 1.0f; // rotate speed
    public float duckSpeed = 0.5f; // duck speed
    public float gravity = 20.0f; // gravity
    public float jumpSpeed = 8.0f; // jump speed
    public bool isControllable = true;


    public CharacterController controller;
    public PlayerStatus status;

    private float moveHorz = 0.0f;
    private float normalHeight = 2.0f;
    private float duckHeight = 2.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 rotateDirection = Vector3.zero;
    private bool isGrounded = false; // is grounded
    private bool isBoosting = false; // is boosting
    private bool isDucking = false; // is ducking


    void Start()
    {
        controller = GetComponent<CharacterController>();
        status = GetComponent<PlayerStatus>();
    }

    void FixedUpdate()
    {
        if (!isControllable)
        {
            Input.ResetInputAxes();
        }
        else
        {
            if (isGrounded)
            {
                float h = Input.GetAxis("Horizontal"); // A, D
                float v = Input.GetAxis("Vertical"); // W, S
                moveDirection = new Vector3(h, 0, v);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= rollSpeed;

                moveHorz = Input.GetAxis("Horizontal");
                if (moveHorz > 0)
                    rotateDirection = new Vector3(0, 1, 0);
                else if (moveHorz < 0)
                    rotateDirection = new Vector3(0, -1, 0);
                else
                    rotateDirection = new Vector3(0, 0, 0);

                if (Input.GetButton("Jump")) // jump
                {
                    moveDirection.y = jumpSpeed;
                }

                if (Input.GetButton("Boost")) // press left shift, jump
                {
                    if (status)
                    {
                        if (status.energy > 0) // need energy when speeding up
                        {
                            moveDirection *= fastSpeed;
                            status.energy -= status.boostUsage * Time.deltaTime;
                            isBoosting = true;
                        }
                    }


                }
                if (Input.GetButtonUp("Boost"))
                {
                    isBoosting = false;
                }

                if (Input.GetButton("Duck")) // press caps lock, duck
                {
                    controller.height = duckHeight;
                    controller.center = new Vector3(controller.center.x, controller.height / 2 + 0.25f, controller.center.z);
                    moveDirection *= duckSpeed;
                    isDucking = true;
                }
                if (Input.GetButtonUp("Duck"))
                {
                    controller.height = normalHeight;
                    controller.center = new Vector3(controller.center.x, controller.height / 2, controller.center.z);
                    isDucking = false;
                }

                if (Input.GetKeyUp(KeyCode.P))
                {
                    status.ApplyDamage(3);
                }
                if (Input.GetKeyUp(KeyCode.O))
                {
                    status.AddHealth(2);
                }
            }

            moveDirection.y -= gravity * Time.deltaTime;

            CollisionFlags flags = controller.Move(moveDirection * Time.deltaTime);
            controller.transform.Rotate(rotateDirection * Time.deltaTime, rotateSpeed);
            isGrounded = ((flags & CollisionFlags.CollidedBelow) != 0);
        }
    }

    public bool IsMoving()
    {
        return moveDirection.magnitude > 0.5;
    }

    public bool IsBoosting()
    {
        return isBoosting;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    public bool IsDead()
    {
        return status.health <= 0;
    }

}