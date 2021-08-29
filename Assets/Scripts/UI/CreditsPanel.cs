using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CreditsPanel : MonoBehaviour
{
    [SerializeField] private Transform _credits;
    [SerializeField] private float _creditsAnimationTime;
    [SerializeField] private float _fadeValue;
    [SerializeField] private float _fadeTime;

    private IEnumerator _fadeCoroutine;
    private Image _panelImage;

    private void Awake()
    {
        _panelImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _fadeCoroutine = DoFade(_fadeValue, MoveCredits);
        StartCoroutine(_fadeCoroutine);
    }

    private void OnValidate()
    {
        if (_fadeValue > 1f || _fadeValue < 0f)
        {
            _fadeValue = 1f;
        }        
    }

    private IEnumerator MoveCredits()
    {
        var startPosition = _credits.position;
        var endPosition = _credits.position;
        endPosition.y = 750f;

        _credits.DOMove(endPosition, _creditsAnimationTime);

        yield return new WaitForSeconds(_creditsAnimationTime);

        _credits.position = startPosition;

        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        StartCoroutine(DoFade(0f, EndCoroutine));
    }

    private IEnumerator DoFade(float fadeValue, Func<IEnumerator> nextMethod)
    {
        _panelImage.DOFade(fadeValue, _fadeTime);

        yield return new WaitForSeconds(_fadeTime);

        StartCoroutine(nextMethod());
    }

    private IEnumerator EndCoroutine()
    {
        yield return null;

        gameObject.SetActive(false);
    }
}
