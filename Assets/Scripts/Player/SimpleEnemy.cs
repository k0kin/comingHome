using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField] private bool recovering = false;
    private float recoveryCounter = 0f;
    private float recoveryTime = 0.2f;
    
    private float launch = 0.5f;
    [SerializeField] private float launchPower = 2;
    
    private Vector2 targetVelocity;
    private Vector2 velocity;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity (); 
    }
    
    private void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        if (recovering)
        {
            recoveryCounter += Time.deltaTime;
            move.x = launch;
            launch += (0 - launch) * Time.deltaTime;
            if (recoveryCounter >= recoveryTime)
            {
                recoveryCounter = 0;
                recovering = false;
            }
        }
        
        targetVelocity = move * 7;
        rb2D.position += move.normalized * 1;
    }

    private void Movement(Vector2 move)
    {
        rb2D.position = rb2D.position + move.normalized * 1;
    }
    
    void FixedUpdate()
    {
        velocity += 1 * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;
        
        //Movement();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerSword"))
        {
            Hurt(1);
            print("Hurt enemy");
        }
    }

    private void Hurt(int launchDirection)
    {
        //Camera Shake
        //Decrease Health
        //Set Hurt Animation
        velocity.y = launchPower;

        launch = launchDirection*(launchPower/5);
        recoveryCounter = 0;
        recovering = true;
    }
}
