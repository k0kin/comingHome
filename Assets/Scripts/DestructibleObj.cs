using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DestructibleObj : MonoBehaviour
{
    [SerializeField] private ParticleSystem explotion;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerSword"))
        {
            explotion.transform.parent = null;
            explotion.Play();
            gameObject.SetActive(false);
            
            Camera.main.transform.DOComplete();
            Camera.main.transform.DOShakePosition(.2f, 2f, 14, 90, false, true);
        }
    }
}
