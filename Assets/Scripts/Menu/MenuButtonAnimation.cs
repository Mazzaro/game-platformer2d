using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MenuButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public RectTransform buttonTransform;
    public Image buttonImage;

    [Header("Scale")]
    public float normalScale = 1f;
    public float hoverScale = 1.05f;
    public float clickScale = 0.97f;

    [Header("Durations")]
    public float hoverDuration = 0.15f;
    public float clickDuration = 0.08f;

    [Header("Pulse")]
    public bool pulseContinuously = false;
    public float pulseAmount = 0.025f;
    public float pulseDuration = 1.2f;

    [Header("Alpha")]
    public float normalAlpha = 1f;
    public float hoverAlpha = 0.9f;

    private bool isHovering;
    private Tween pulseTween;
    private Tween alphaTween;
    private Sequence clickSequence;

    private void Awake()
    {
        if (buttonTransform == null)
            buttonTransform = GetComponent<RectTransform>();

        if (buttonImage == null)
            buttonImage = GetComponent<Image>();
    }

    private void Start()
    {
        SetBaseScale(normalScale);

        if (pulseContinuously)
            StartPulse();
    }

    private void StartPulse()
    {
        pulseTween?.Kill();

        float baseScale = isHovering ? hoverScale : normalScale;
        float targetScale = baseScale + pulseAmount;

        pulseTween = buttonTransform
            .DOScale(targetScale, pulseDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetLink(gameObject);
    }

    private void SetBaseScale(float scale)
    {
        buttonTransform.DOKill();

        buttonTransform
            .DOScale(scale, hoverDuration)
            .SetEase(Ease.OutSine)
            .SetLink(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;

        SetBaseScale(hoverScale);

        if (pulseContinuously)
            StartPulse();

        FadeTo(hoverAlpha);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

        SetBaseScale(normalScale);

        if (pulseContinuously)
            StartPulse();

        FadeTo(normalAlpha);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clickSequence?.Kill();

        float returnScale = isHovering ? hoverScale : normalScale;

        clickSequence = DOTween.Sequence();

        clickSequence.Append(buttonTransform.DOScale(clickScale, clickDuration));
        clickSequence.Append(buttonTransform.DOScale(returnScale, clickDuration).SetEase(Ease.OutBack));

        clickSequence.SetLink(gameObject);
    }

    private void FadeTo(float alpha)
    {
        if (buttonImage == null) return;

        alphaTween?.Kill();
        buttonImage.DOKill();

        alphaTween = buttonImage
            .DOFade(alpha, hoverDuration)
            .SetLink(gameObject);
    }

    private void OnDisable()
    {
        pulseTween?.Kill();
        alphaTween?.Kill();
        clickSequence?.Kill();

        if (buttonTransform != null)
            buttonTransform.DOKill();

        if (buttonImage != null)
            buttonImage.DOKill();
    }

    private void OnDestroy()
    {
        pulseTween?.Kill();
        alphaTween?.Kill();
        clickSequence?.Kill();

        if (buttonTransform != null)
            buttonTransform.DOKill();

        if (buttonImage != null)
            buttonImage.DOKill();
    }

}
