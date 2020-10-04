using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamableObj : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireParticle;

    private void OnTriggerEnter2D(Collider2D other)
    {
	    if (other.CompareTag("FireBullet") && !fireParticle.isPlaying)
	    {
		    fireParticle.Play();
	    }
    }
}
