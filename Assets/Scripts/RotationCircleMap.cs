using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCircleMap : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private float rotationSpeed = 1f;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        
        transform.Rotate(Vector3.forward, x * (Time.deltaTime * rotationSpeed));
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            rotationSpeed -= 15f;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            rotationSpeed += 15f;
        }
    }
}
