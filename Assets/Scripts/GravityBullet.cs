using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBullet : MonoBehaviour
{
    [SerializeField] private PointEffector2D effector;

    [SerializeField] private List<Rigidbody2D> objsToInteract;

    private void Start()
    {
        effector = GetComponent<PointEffector2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q) || Input.GetAxis("GravityGun") >= 0.5)
        {
            foreach (Rigidbody2D rb in objsToInteract)
                rb.isKinematic = false;

            effector.forceMagnitude = -20f;
            effector.enabled = true;
        }
        else if (Input.GetKey(KeyCode.E) || Input.GetAxis("GravityGun") <= -0.5)
        {
            foreach (Rigidbody2D rb in objsToInteract)
                rb.isKinematic = false;
            
            effector.forceMagnitude = 20f;
            effector.enabled = true;
        }
        else
        {
            effector.enabled = false;
            
            foreach (Rigidbody2D rb in objsToInteract)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactuable"))
        {
            Rigidbody2D rbToAdd = other.GetComponent<Rigidbody2D>();
            if(rbToAdd != null)
                objsToInteract.Add(rbToAdd);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (objsToInteract.Contains(other.GetComponent<Rigidbody2D>()))
            objsToInteract.Remove(other.GetComponent<Rigidbody2D>());
    }
}

