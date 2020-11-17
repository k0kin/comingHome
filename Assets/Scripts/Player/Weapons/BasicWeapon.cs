using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    public Transform canon;
    public GameObject bulletPrefab;

    public float fireRate = 15f;
    private float nextTimetoFire = 0f;
    
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0.5f && Time.time >= nextTimetoFire)
        {
            nextTimetoFire = Time.time + 1f / fireRate;
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
