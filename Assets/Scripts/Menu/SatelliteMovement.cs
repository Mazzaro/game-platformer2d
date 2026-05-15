using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SatelliteMovement : MonoBehaviour
{
     [Header("Movement Settings")]
    public float moveX = 1.5f;
    public float moveY = 0.5f;
    public float duration = 4f;

    [Header("Rotation Settings")]
    public float rotationZ = 5f;

    private Vector3 startPosition;

    private Tween moveTween;
    private Tween rotateTween;

    private void Start()
    {
        startPosition = transform.position;

        moveTween = transform
            .DOMove(startPosition + new Vector3(moveX, moveY, 0), duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetLink(gameObject);

        rotateTween = transform
            .DORotate(new Vector3(0, 0, rotationZ), duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetLink(gameObject);
    }

    private void OnDisable()
    {
        moveTween?.Kill();
        rotateTween?.Kill();

        transform.DOKill();
    }

    private void OnDestroy()
    {
        moveTween?.Kill();
        rotateTween?.Kill();

        transform.DOKill();
    }
}
