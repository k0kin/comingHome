using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    public Transform canon;
    public GameObject bulletPrefab;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
        //LookMouse();
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, canon.position, canon.rotation);
    }

    private void LookMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 lookAtDirection = mousePosition - transform.position;

        transform.right = lookAtDirection;

    }
    
}
