using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    private float speed;
    private bool restoreTime = false;

    private PlayerMovement movement;
    
    
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        
        restoreTime = false;
    }

    
    void Update()
    {
        if (restoreTime)
        {
            if (Time.timeScale < 1f)
                Time.timeScale += Time.deltaTime * speed;
            else
            {
                Time.timeScale = 1f;
                restoreTime = false;

                movement.canMove = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            StopTime(0.05f, 3, 0.1f);
            movement.canMove = false;
            
            Camera.main.transform.DOComplete();
            Camera.main.transform.DOShakePosition(.2f, 1.5f, 14, 90, false, true);
        }
    }

    private void StopTime(float changeTime, int restoreSpeed, float delay)
    {
        speed = restoreSpeed;

        if (delay > 0)
        {
            StopCoroutine(StartTimeAgain(delay));
            StartCoroutine(StartTimeAgain(delay));
        }
        else
        {
            restoreTime = true;
        }

        Time.timeScale = changeTime;
    }

    private IEnumerator StartTimeAgain(float amount)
    {
        restoreTime = true;
        yield return new WaitForSeconds(amount);
    }
}
