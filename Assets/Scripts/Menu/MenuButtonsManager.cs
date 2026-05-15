using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuButtonsManager : MonoBehaviour
{
    [Header("Animation")]
    public float duration = .2f;
    public float delay = .05f;
    public Ease ease = Ease.OutBack;
    public RectTransform title;
    public float titleDuration = 0.7f;

    public List<GameObject> buttons;

    private void OnEnable()
    {
        HideAllButtons();
        AnimateTitleEntrance();
        ShoWButtons();
    }

    private void HideAllButtons()
    {
        foreach (var b in buttons)
        {
            b.transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
            b.SetActive(false);
        }
    }

    private void AnimateTitleEntrance()
    {
        title.localScale = Vector3.zero;
        title.DOScale(1f, titleDuration).SetEase(Ease.OutBack);
    }

    private void ShoWButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            var b = buttons[i];
            b.SetActive(true);
            b.transform.DOScale(1, duration).SetDelay(i*delay).SetEase(ease);
        }
    }

}
