using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using TMPro;

public class CommentController : MonoBehaviour
{
    [SerializeField] private float _timeAnim = 0.3f;
    [SerializeField] private float _widthMaxScaleText = 324f;
    [SerializeField] private float _widthScaleText = 40f;
    [SerializeField] private RectTransform _rect;
    [SerializeField] private RectTransform _layoutTextRectTransform;
    [SerializeField] private TextMeshProUGUI _contentText;


    [SerializeField] private Image _avatarImage;
    [SerializeField] private Transform _localScale;


    private Tween _tween;

    private void OnDisable()
    {
        if (_tween != null) _tween.Kill();
    }


    public void Init(string content , Sprite avatar)
    {
        _contentText.text = content;
        WidthTextCalculator(_layoutTextRectTransform, content, _widthScaleText, _widthMaxScaleText);

        _avatarImage.overrideSprite = avatar;
        _localScale.localScale = Vector3.zero;
        StartCoroutine(IEScale());
    }


    private IEnumerator IEScale()
    {
        yield return new WaitForEndOfFrame();
        _localScale.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
    }


    public void WidthTextCalculator(RectTransform rectWidth, string text, float widthWith1character, float maxWidth)
    {
        float width = text.Length * widthWith1character;
        if (width > maxWidth) width = maxWidth;
        rectWidth.sizeDelta = new Vector2(width, rectWidth.sizeDelta.y);
    }


    public void OnMoveUp(Action callback)
    {
        _tween =  _rect.DOAnchorPosY(_rect.localPosition.y + 140f, _timeAnim).OnComplete(() => {
        callback?.Invoke();
        });
    }


    public void OnDespawn()
    {
        _rect.GetComponent<CanvasGroup>().DOFade(0, _timeAnim).OnComplete(() => {
            
        });
    }
}

