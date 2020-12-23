using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float force;
    [SerializeField] private LayerMask layerToHit;
    
    [SerializeField] private GameObject explosionPrefab;

    private Rigidbody2D rb2D;
    private bool isActivated = true;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerSword"))
        {
            rb2D.gravityScale = 1f;
            isActivated = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isActivated)
        {
            Camera.main.transform.DOComplete();
            Camera.main.transform.DOShakePosition(.5f, 2f, 14, 90, false, true);
            
            Explode();
            
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Destroy(explosion, 2f);
        }
    }

    private void Explode()
    {
        Collider2D[] objs = Physics2D.OverlapCircleAll(transform.position, radius, layerToHit);

        foreach (Collider2D obj in objs)
        {
            Vector2 dir = obj.transform.position - transform.position;
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.AddForce(dir * force);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
