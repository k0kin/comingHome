using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private Rigidbody2D rb2D;
    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        rb2D.velocity = transform.right * speed;
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if(other.CompareTag("Player") || other.CompareTag("GravityGun") || other.CompareTag("Interactuable"))
            //return;
        //if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            //gameObject.SetActive(false);
    }
    

    /*
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            gameObject.SetActive(false);
    }
    
    */
}
