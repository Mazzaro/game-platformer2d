using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatingUI : MonoBehaviour
{public RectTransform target;
    public float moveAmount = 15f;
    public float duration = 2f;

    private Tween floatingTween;

    private void Awake()
    {
        if (target == null)
            target = GetComponent<RectTransform>();
    }

    private void Start()
    {
        if (target == null) return;

        floatingTween = target
            .DOAnchorPosY(target.anchoredPosition.y + moveAmount, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetLink(gameObject);
    }

    private void OnDisable()
    {
        floatingTween?.Kill();

        if (target != null)
            target.DOKill();
    }

    private void OnDestroy()
    {
        floatingTween?.Kill();

        if (target != null)
            target.DOKill();
    }
}
