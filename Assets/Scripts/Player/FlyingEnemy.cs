using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pathfinding;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float forceImpulse;
    [SerializeField] private ParticleSystem dieExplotion;
    
    private bool takeDamage = false;
    private Rigidbody2D rb2D;
    private float recoveryTimer = 1f;

    private Animator animPlayer;

    private AIPath path;
    
    void Start()
    {
        path = GetComponent<AIPath>();
        
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (recoveryTimer > 0)
        {
            recoveryTimer -= Time.deltaTime;
        }
        else
        {
            path.enabled = true;
            takeDamage = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        recoveryTimer = 1f;
            
        path.enabled = false;
           
        rb2D.AddForce(Vector3.up * (forceImpulse + 300), ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        if (other.CompareTag("PlayerSword") && takeDamage == false)
        {
            print("Collide with Sword");
            health--;
            takeDamage = true;
            rb2D.velocity = Vector2.zero;
            recoveryTimer = 1f;

            path.enabled = false;

            rb2D.AddForce(Vector3.up * (forceImpulse + 300), ForceMode2D.Force);
            
            if(health <= 0)
            {
                dieExplotion.Play();
                dieExplotion.transform.parent = null;

                gameObject.SetActive(false);
                Camera.main.transform.DOComplete();
                Camera.main.transform.DOShakePosition(.2f, 1f, 14, 90, false, true);
            }
        }

        if (other.CompareTag("FireBullet") && takeDamage == false)
        {
            health -= 0.5f;
            
            takeDamage = true;
            recoveryTimer = 1f;
            
            path.enabled = false;

            rb2D.AddForce(Vector3.up * (forceImpulse + 300), ForceMode2D.Force);
            
            if(health < 0)
            {
                dieExplotion.Play();
                dieExplotion.transform.parent = null;

                gameObject.SetActive(false);
                Camera.main.transform.DOComplete();
                Camera.main.transform.DOShakePosition(.2f, 1f, 14, 90, false, true);
            }
        }
    }
}
