using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenGroup : MonoBehaviour
{
    [SerializeField]
    TweenUtilities[] tweeners;
    [SerializeField]
    bool useChildren;

    private void Awake()
    {
        if (useChildren)
            tweeners = GetComponentsInChildren<TweenUtilities>();
    }
    [ContextMenu("Tween")]
    public void Tween()
    {
        for(int i = 0; i < tweeners.Length; i++)
        {
            if(tweeners[i] != null)
                tweeners[i].TweenAll();
        }
    }
    [ContextMenu("Tween Reversed")]
    public void TweenReversed()
    {
        for (int i = 0; i < tweeners.Length; i++)
        {
            tweeners[i].TweenAllReverse();
        }
    }
    public void TweenImmediately()
    {
        for (int i = 0; i < tweeners.Length; i++)
        {
            tweeners[i].TweenImmediately();
        }
    }
    public void TweenReveresImmediately()
    {
        for (int i = 0; i < tweeners.Length; i++)
        {
            tweeners[i].TweenImmediatelyReversed();
        }
    }
}
