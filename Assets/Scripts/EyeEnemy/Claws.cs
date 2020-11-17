using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claws : MonoBehaviour
{
    private TweenSettings tweenSettings;
    public float delay = 1f;

    void Start()
    {
        tweenSettings = GetComponent<TweenUtilities>().tweens[0];

        //StartCoroutine(StartTween());
    }

    void Update()
    {
        transform.Rotate(new Vector3(0,0,1), 5f);
    }

    public IEnumerator StartTween()
    {
        yield return new WaitForSeconds(delay);
        
        tweenSettings.PerformTween(transform);
        yield return new WaitForSeconds(tweenSettings.tweenDuration * 2);
        tweenSettings.StopTween();
    }
}
