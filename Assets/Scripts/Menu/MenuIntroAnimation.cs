using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuIntroAnimation : MonoBehaviour
{
    [Header("UI Elements")]
    public RectTransform title;
    public RectTransform playButton;
    public RectTransform aboutButton;
    public RectTransform exitButton;

    [Header("Animation Settings")]
    public float titleDuration = 0.7f;
    public float buttonDuration = 0.45f;
    public float delayBetweenButtons = 0.15f;

    private Sequence sequence;

    private void Start()
    {
        AnimateMenuEntrance();
    }


    private void AnimateMenuEntrance()
    {
        title.localScale = Vector3.zero;


        playButton.localScale = new Vector3(0.85f, 0.85f, 0.85f);
        aboutButton.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        exitButton.localScale = new Vector3(0.65f, 0.65f, 0.65f);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(title.DOScale(1f, titleDuration).SetEase(Ease.OutBack));

        sequence.AppendInterval(0.2f);

        sequence.Append(playButton.DOScale(1, buttonDuration).SetEase(Ease.OutElastic));
        sequence.AppendInterval(delayBetweenButtons);

        sequence.Append(aboutButton.DOScale(1, buttonDuration).SetEase(Ease.OutElastic));
        sequence.AppendInterval(delayBetweenButtons);

        sequence.Append(exitButton.DOScale(1, buttonDuration).SetEase(Ease.OutElastic));

        sequence.SetLink(gameObject);

        //sequence?.Kill();

        /*sequence.Append(playButton.DOAnchorPosX(0, buttonDuration).SetEase(Ease.OutBack));
        sequence.AppendInterval(delayBetweenButtons);

        sequence.Append(aboutButton.DOAnchorPosX(0, buttonDuration).SetEase(Ease.OutBack));
        sequence.AppendInterval(delayBetweenButtons);

        sequence.Append(exitButton.DOAnchorPosX(0, buttonDuration).SetEase(Ease.OutBack));*/
    }
    private void OnDestroy()
    {
        sequence?.Kill();
    }
}
