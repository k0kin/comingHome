using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;

[CanEditMultipleObjects]
#endif
public class TweenUtilities : MonoBehaviour
{
    [SerializeField]bool startTweenOnEnable;
    [SerializeField]
    public List<TweenSettings> tweens = new List<TweenSettings>();
    void OnEnable(){
        if(startTweenOnEnable)
        TweenAll();
    }
    void OnDisable(){
        KillAll();
    }
    [ContextMenu("TweenAll")]
    public void TweenAll(){
        foreach (TweenSettings tween in tweens) {
            tween.PerformTween(transform);
        }
    }
    [ContextMenu("Tween Reversed All")]
    public void TweenAllReverse()
    {
        foreach(TweenSettings tween in tweens)
        {
            tween.PerformReversedTween(transform);
        }
    }

    [ContextMenu("TweenSingle")]
    public void TweenSingle(int value)
    {
        tweens[value].PerformTween(transform);
    }
    [ContextMenu("TweenSingleReversed")]
    public void TweenSingleReverse(int value)
    {
        tweens[value].PerformReversedTween(transform);
    }

    public void TweenImmediately()
    {
        foreach(TweenSettings tween in tweens)
        {
            tween.PerformImmediateTween(transform);
        }
    }
    public void TweenImmediatelyReversed()
    {
        foreach (TweenSettings tween in tweens)
        {
            tween.PerformImmediateTweenReversed(transform);
        }
    }
    public void KillAll(){
        foreach(TweenSettings tween in tweens){
            tween.StopTween();
        }
    }
}
[System.Serializable]
public class TweenSettings
{
    Tween tween;
    public TweenType tweenType;
    public bool useRectTransform;
    public bool useTransformValuesAsInitial;
    public Vector3 initialValue;
    public Vector3 endValue;
    public float tweenDuration=.1f;
    public int loops;
    public float delay;
    public LoopType loopType;
    public Ease ease;
    [Space(10)]
    [Header("Alpha Values")]
    public float aplphaEndValue;
    public float aplphaTimer;
    public bool FateIn;
    public bool FateOut;
    private Image UIImage;
    private Text UIText;
    private SpriteRenderer SpriteImage;
    RectTransform rectTransform;
    public Tween PerformTween(Transform target){
        if (useRectTransform)
        {
            rectTransform  = target.GetComponent<RectTransform>();
        }
        if(tween!=null)
            tween.Kill();
        switch (tweenType)
        {
            case TweenType.Scale:
                if (useRectTransform)
                {
                    if(!useTransformValuesAsInitial)
                    rectTransform.localScale = initialValue;
                    if (Application.isPlaying)
                        tween = rectTransform.DOScale(endValue, tweenDuration);
                    else
                        rectTransform.localScale = endValue;
                }
                else
                {
                    if (!useTransformValuesAsInitial)
                        target.localScale = initialValue;
                    if (Application.isPlaying)
                        tween = target.DOScale(endValue, tweenDuration);
                    else
                        target.localScale = endValue;
                }
                break;
            case TweenType.Rotation:
                if (useRectTransform)
                {
                    if (!useTransformValuesAsInitial)
                        rectTransform.localRotation = Quaternion.Euler(initialValue);
                    if (Application.isPlaying)
                        tween = rectTransform.DOLocalRotate(endValue, tweenDuration);
                    else
                        rectTransform.localEulerAngles = endValue;
                }
                else
                {
                    if (!useTransformValuesAsInitial)
                        target.localRotation = Quaternion.Euler(initialValue);
                    if (Application.isPlaying)
                        tween = target.DOLocalRotate(endValue, tweenDuration);
                    else
                        target.localEulerAngles = endValue;
                }
                break;
            case TweenType.Position:
                if (useRectTransform)
                {
                    if (!useTransformValuesAsInitial)
                        rectTransform.anchoredPosition = initialValue;
                    if (Application.isPlaying)
                        tween = rectTransform.DOAnchorPos(endValue, tweenDuration);
                    else
                        rectTransform.anchoredPosition = endValue;
                }
                else
                {
                    if (!useTransformValuesAsInitial)
                        target.localPosition = initialValue;
                    if (Application.isPlaying)
                        tween = target.DOLocalMove(endValue, tweenDuration);
                    else
                        target.localPosition = endValue;
                }
                break;
            case TweenType.Alpha:
                UIImage = target.gameObject.GetComponent<Image>();
                SpriteImage = target.gameObject.GetComponent<SpriteRenderer>();
                UIText = target.gameObject.GetComponent<Text>();
                if (UIImage != null)
                {
                    if (FateIn)
                    {
                        Color tmpcolo = UIImage.color;
                        tmpcolo.a = 0;
                        UIImage.color = tmpcolo;
                    }
                    else if (FateOut)
                    {
                        Color tmpcolo = UIImage.color;
                        tmpcolo.a = 1;
                        UIImage.color = tmpcolo;
                    }
                    if (Application.isPlaying)
                        tween = UIImage.DOFade(aplphaEndValue, aplphaTimer);
                }
                else if (SpriteImage != null)
                {
                    if (FateIn)
                    {
                        Color tmpcolo = SpriteImage.color;
                        tmpcolo.a = 0;
                        SpriteImage.color = tmpcolo;
                    }
                    else if (FateOut)
                    {
                        Color tmpcolo = SpriteImage.color;
                        tmpcolo.a = 1;
                        SpriteImage.color = tmpcolo;
                    }
                    if (Application.isPlaying)
                        tween = SpriteImage.DOFade(aplphaEndValue, aplphaTimer);
                }
                else if (UIText != null)
                {
                    if (FateIn)
                    {
                        Color tmpcolo = UIText.color;
                        tmpcolo.a = 0;
                        UIText.color = tmpcolo;
                    }
                    else if (FateOut)
                    {
                        Color tmpcolo = UIText.color;
                        tmpcolo.a = 1;
                        UIText.color = tmpcolo;
                    }
                    if (Application.isPlaying)
                        tween = UIText.DOFade(aplphaEndValue, aplphaTimer);
                }
                break;
        }
        SetTweenValues(tween);
        return tween;
    }

    public Tween PerformReversedTween(Transform target)
    {
        Vector3 initialValue = this.endValue;
        Vector3 endValue = this.initialValue;
        if (useRectTransform)
        {
            rectTransform = target.GetComponent<RectTransform>();
        }
        if (tween != null)
            tween.Kill();
        switch (tweenType)
        {
            case TweenType.Scale:
                if (useRectTransform)
                {
                    if (!useTransformValuesAsInitial)
                        rectTransform.localScale = initialValue;
                    if (Application.isPlaying)
                        tween = rectTransform.DOScale(endValue, tweenDuration);
                    else
                        rectTransform.localScale = endValue;
                }
                else
                {
                    if (!useTransformValuesAsInitial)
                        target.localScale = initialValue;
                    if (Application.isPlaying)
                        tween = target.DOScale(endValue, tweenDuration);
                    else
                        target.localScale = endValue;
                }
                break;
            case TweenType.Rotation:
                if (useRectTransform)
                {
                    if (!useTransformValuesAsInitial)
                        rectTransform.localRotation = Quaternion.Euler(initialValue);
                    if (Application.isPlaying)
                        tween = rectTransform.DOLocalRotate(endValue, tweenDuration);
                    else
                        rectTransform.localEulerAngles = endValue;
                }
                else
                {
                    if (!useTransformValuesAsInitial)
                        target.localRotation = Quaternion.Euler(initialValue);
                    if (Application.isPlaying)
                        tween = target.DOLocalRotate(endValue, tweenDuration);
                    else
                        target.localEulerAngles = endValue;
                }
                break;
            case TweenType.Position:
                if (useRectTransform)
                {
                    if (!useTransformValuesAsInitial)
                        rectTransform.anchoredPosition = initialValue;
                    if (Application.isPlaying)
                        tween = rectTransform.DOAnchorPos(endValue, tweenDuration);
                    else
                        rectTransform.anchoredPosition = endValue;
                }
                else
                {
                    if (!useTransformValuesAsInitial)
                        target.localPosition = initialValue;
                    if (Application.isPlaying)
                        tween = target.DOLocalMove(endValue, tweenDuration);
                    else
                        target.localPosition = endValue;
                }
                break;
            case TweenType.Alpha:
                break;
        }
        SetTweenValues(tween);
        return tween;
    }

    public void PerformImmediateTween(Transform target)
    {
        if (useRectTransform)
        {
            rectTransform = target.GetComponent<RectTransform>();
        }
        if (tween != null)
            tween.Kill();
        switch (tweenType)
        {
            case TweenType.Scale:
                if (useRectTransform)
                {
                        rectTransform.localScale = endValue;
                }
                else
                {
                        target.localScale = endValue;
                }
                break;
            case TweenType.Rotation:
                if (useRectTransform)
                {
                        rectTransform.localEulerAngles = endValue;
                }
                else
                {
                        target.localEulerAngles = endValue;
                }
                break;
            case TweenType.Position:
                if (useRectTransform)
                {
                        rectTransform.anchoredPosition = endValue;
                }
                else
                {
                        target.localPosition = endValue;
                }
                break;
            case TweenType.Alpha:
                break;
        }
    }
    public void PerformImmediateTweenReversed(Transform target)
    {
        Vector3 endValue = this.initialValue;
        if (useRectTransform)
        {
            rectTransform = target.GetComponent<RectTransform>();
        }
        if (tween != null)
            tween.Kill();
        switch (tweenType)
        {
            case TweenType.Scale:
                if (useRectTransform)
                {
                        rectTransform.localScale = endValue;
                }
                else
                {
                        target.localScale = endValue;
                }
                break;
            case TweenType.Rotation:
                if (useRectTransform)
                {
                        rectTransform.localEulerAngles = endValue;
                }
                else
                {
                        target.localEulerAngles = endValue;
                }
                break;
            case TweenType.Position:
                if (useRectTransform)
                {
                        rectTransform.anchoredPosition = endValue;
                }
                else
                {
                        target.localPosition = endValue;
                }
                break;
            case TweenType.Alpha:
                break;
        }
    }

    public void StopTween(){
        if(tween!=null)
            tween.Kill();
    }
    void SetTweenValues(Tween tween){
            if(loops!=0)
                tween.SetLoops(loops,loopType);
            tween.SetEase(ease);
        if (delay > 0)
            tween.SetDelay(delay);
    }
}

public enum TweenType
{
    Scale,
    Rotation,
    Position,
    Alpha
}

#if UNITY_EDITOR
[CustomEditor(typeof(TweenUtilities),true),CanEditMultipleObjects]
public class TweenUtilitiesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(Application.isPlaying && GUILayout.Button("Test Tween"))
        {
            foreach (Object target in targets)
            {
                ((TweenUtilities)target).KillAll();
                ((TweenUtilities)target).TweenAll();
            }
        }
        if(Application.isPlayer && GUILayout.Button("Test Reverse Tween"))
        {
            foreach (Object target in targets)
            {
                ((TweenUtilities)target).KillAll();
                ((TweenUtilities)target).TweenAllReverse();
            }
        }
    }
}
#endif