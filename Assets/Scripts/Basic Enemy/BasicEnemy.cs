using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private ObjectsToActivateSides scriptableList;
    
    [Space]
    [Header("Health")] 
    [SerializeField] private float health = 3;

    [SerializeField] private ParticleSystem dieExplotion;
    
    [Space]
    
    [SerializeField] private float speed;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool facingRight;
    [SerializeField] private bool isGrounded;

    [SerializeField] private Vector2 impulseDirection;
    [SerializeField] private float forceImpulse = 10f;
    [SerializeField] private float recoveryTimer = 0.5f;

    private Rigidbody2D rb2D;

    private bool takeDamage = false;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);

        
        if (!isGrounded && facingRight)
        {
            Flip();
        }
        else if (!isGrounded && !facingRight)
        {
            Flip();
        }
        

        if (recoveryTimer > 0)
        {
            recoveryTimer -= Time.deltaTime;
        }
        else
        {
            takeDamage = false;
        }

    }

    private void FixedUpdate()
    {
        if(!takeDamage)
            rb2D.velocity = Vector2.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && takeDamage == false)
        {
            takeDamage = true;
            rb2D.velocity = Vector2.zero;
            recoveryTimer = 1f;

            if(facingRight)
                rb2D.AddForce(impulseDirection * (forceImpulse + 100), ForceMode2D.Force);
            else
                rb2D.AddForce(-impulseDirection * (forceImpulse + 100), ForceMode2D.Force);
            
            //Flip();
        }
        
        if (other.CompareTag("PlayerSword"))
        {
            print("Collide with Sword");
            health--;
            takeDamage = true;
            rb2D.velocity = Vector2.zero;
            recoveryTimer = 1f;

            if(facingRight)
                rb2D.AddForce(impulseDirection * forceImpulse, ForceMode2D.Force);
            else
                rb2D.AddForce(new Vector2(-impulseDirection.x, impulseDirection.y) * forceImpulse, ForceMode2D.Force);
            
            if(health < 0)
            {
                dieExplotion.Play();
                dieExplotion.transform.parent = null;

                gameObject.SetActive(false);
                Camera.main.transform.DOComplete();
                Camera.main.transform.DOShakePosition(.2f, 1f, 14, 90, false, true);
                
                RemoveFromScriptableList();
            }
            
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        speed = -speed;
    }

    private void RemoveFromScriptableList()
    {
        if (scriptableList.downSide.Contains(gameObject))
            scriptableList.downSide.Remove(gameObject);
        else if (scriptableList.rightSide.Contains(gameObject))
            scriptableList.rightSide.Remove(gameObject);
        else if (scriptableList.upSide.Contains(gameObject))
            scriptableList.upSide.Remove(gameObject);
        else if (scriptableList.leftSide.Contains(gameObject))
            scriptableList.leftSide.Remove(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
    }
}
