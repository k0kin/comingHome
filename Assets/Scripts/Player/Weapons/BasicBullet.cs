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
    
}
