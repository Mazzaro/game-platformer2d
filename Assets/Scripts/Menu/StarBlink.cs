using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StarBlink : MonoBehaviour
{
    public Image starImage;
    public float minAlpha = 0.2f;
    public float maxAlpha = 1f;
    public float duration = 1f;

    private Tween blinkTween;

    private void Awake()
    {
        if (starImage == null)
            starImage = GetComponent<Image>();
    }

    private void Start()
    {
        if (starImage == null) return;

        blinkTween = starImage
            .DOFade(minAlpha, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetLink(gameObject);
    }

    private void OnDisable()
    {
        blinkTween?.Kill();

        if (starImage != null)
            starImage.DOKill();
    }

    private void OnDestroy()
    {
        blinkTween?.Kill();

        if (starImage != null)
            starImage.DOKill();
    }
}
