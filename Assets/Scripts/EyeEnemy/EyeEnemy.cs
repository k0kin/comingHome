using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class EyeEnemy : MonoBehaviour
{
    public AnimationCurve myCurve;
    [SerializeField] private float offsetY;
    [SerializeField] private GameObject laser;
    [SerializeField] private float distanceToFollowPlayer;
    [SerializeField] private float speed;
    [SerializeField] private float health = 2;
    [SerializeField] private ParticleSystem deadParticleSystem;
    
    [SerializeField] private Transform player;
    private bool canFollowPlayer;

    [SerializeField] private Claws[] claws;

    private Coroutine laserCoroutine;
    private Coroutine trowClawsCoroutine;
    private Coroutine clawsCoroutine;

    void Start()
    {
        laserCoroutine = StartCoroutine(ActivateLaser());
        
        trowClawsCoroutine = StartCoroutine(TrowClaws());
    }

    void Update()
    {
        Vector2 newPos;
        float distance = Vector2.Distance(transform.position, player.position);
        
        if (distance > distanceToFollowPlayer && distance < distanceToFollowPlayer + 3f)
        {
            newPos = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            newPos.y = myCurve.Evaluate((Time.time % myCurve.length)) + offsetY;
        }
        else
        {
            newPos = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)) + offsetY);
        }

        transform.position = newPos;
        
        /*
        Vector3 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 165f, Vector3.forward);
        */
        
        transform.rotation = (transform.position.x - player.position.x) < 0
            ? Quaternion.Euler(0f, 180f, 0f)
            : Quaternion.identity;
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FireBullet"))
        {
            other.gameObject.SetActive(false);
            
            Camera.main.transform.DOComplete();
            Camera.main.transform.DOShakePosition(.2f, 0.3f, 14, 90, false, true);
            health -= 0.1f;
            
            if(health <= 0){
                Die();
            }
        }
    }

    public void Hit(int launchDirection){
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, 1f, 14, 90, false, true);
        health -= 1;
        
        /*
        //animator.SetTrigger ("hurt");
        velocity.y = launchPower;

        launch = launchDirection*(launchPower/5);
        recoveryCounter = 0;
        recovering = true;
        
        */
        if(health <= 0){
            Die();
        }
    }

    private void Die()
    {
        deadParticleSystem.transform.parent = null;
        deadParticleSystem.gameObject.SetActive(true);
        deadParticleSystem.Play();
        Destroy(gameObject);
    }

    IEnumerator ActivateLaser()
    {
        laser.SetActive(false);
        yield return new WaitForSeconds(4f);
        laser.SetActive(true);
        yield return new WaitForSeconds(2f);

        laserCoroutine = StartCoroutine(ActivateLaser());
    }

    IEnumerator TrowClaws()
    {
        int index = Random.Range(0, 3);

        clawsCoroutine = StartCoroutine(claws[index].StartTween());
        
        yield return new WaitForSeconds(4f);

        trowClawsCoroutine = StartCoroutine(TrowClaws());
    }
    
}
