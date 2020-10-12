using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamableObj : MonoBehaviour
{
    public ParticleSystem fireParticle;
    [SerializeField] private bool canDetectBullets = true;

    private bool isBurning = false;

    private void Update()
    {
	    if (isBurning && !fireParticle.isPlaying)
	    {
		    gameObject.SetActive(false);
	    }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
	    if (canDetectBullets)
	    {
		    if(other.CompareTag("FireBullet") && !fireParticle.isPlaying)
				fireParticle.Play();
	    }

	    else
	    {
		    FlamableObj flamable = other.GetComponent<FlamableObj>();
		    
		    if(flamable != null && flamable.fireParticle.isPlaying)
		    {
			    fireParticle.Play();
			    isBurning = true;
		    }
		    
	    }
    }
}
