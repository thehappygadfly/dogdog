// Weijian Hu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // follow target
    public float distance = 10.0f;
    public float height = 5.0f;
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;
    public float distanceDampingX = 0.5f;
    public float distanceDampingZ = 0.2f;
    public float camSpeed = 3.0f;
    public bool smoothed = true; //soothed moving

    private float wantedRotationAngle; // wanted ratation angle
    private float wantedHeight; // wanted height
    private float wantedDistanceZ;
    private float wantedDistanceX;

    private float currentRotationAngle;
    private float currentHeight;
    private float currentDistanceZ;
    private float currentDistanceX;

    private Quaternion currentRotation;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (!target)
        {
            return;
        }

        wantedRotationAngle = target.eulerAngles.y;
        wantedHeight = target.position.y + height;
        wantedDistanceZ = target.position.z - distance;
        wantedDistanceX = target.position.x - distance;
        

        currentRotationAngle = transform.eulerAngles.y;
        currentHeight = transform.position.y;
        currentDistanceZ = transform.position.z;
        currentDistanceX = transform.position.x;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle,rotationDamping * Time.deltaTime);
        currentHeight = Mathf.LerpAngle(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        currentDistanceZ = Mathf.LerpAngle(currentDistanceZ, wantedDistanceZ, distanceDampingZ * Time.deltaTime);
        currentDistanceX = Mathf.LerpAngle(currentDistanceX, wantedDistanceX, distanceDampingX * Time.deltaTime);

        currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        transform.position -= currentRotation * Vector3.forward * distance; // update transform
        transform.position = new Vector3(currentDistanceX, currentHeight, currentDistanceZ);

        LookAtTarget();
    }

    void LookAtTarget()
    {
        if (smoothed)
        {
            Quaternion camRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, camRotation, camSpeed * Time.deltaTime);
        }
        else
        {
            transform.LookAt(target);

        }
    }

}
