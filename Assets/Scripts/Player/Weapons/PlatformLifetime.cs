using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLifetime : MonoBehaviour
{
    private SpriteRenderer renderer;
    private float lifeTimeSeconds = 5f;
    
    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public float spriteBlinkingTotalDuration = 1.0f;
    public bool startBlinking = false;
    
    private float countDown;
    
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        countDown = lifeTimeSeconds;
    }

    void Update()
    {
        countDown -= Time.deltaTime;
        
        if (countDown <= 1)
        {
            SpriteBlinkingEffect();
        }
        
        if (countDown < 0)
        {
            Destroy(gameObject);
        }
    }
    
    private void SpriteBlinkingEffect()
    {
        spriteBlinkingTotalTimer += Time.deltaTime;
        if (spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
        {
            startBlinking = false;
            spriteBlinkingTotalTimer = 0.0f;
            renderer.enabled = true;
            return;
        }

        spriteBlinkingTimer += Time.deltaTime;
        if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
            if (renderer.enabled)
            {
                renderer.enabled = false;
            }
            else
            {
                renderer.enabled = true;
            }
        }
    }
}
