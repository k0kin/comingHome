using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBullet : MonoBehaviour
{
    [SerializeField] private PointEffector2D effector;

    private void Start()
    {
        effector = GetComponent<PointEffector2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            effector.enabled = true;
        }
        else
        {
            effector.enabled = false;
        }
    }
    
}

