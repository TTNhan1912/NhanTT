using System.Collections;
using System.Collections.Generic;
// using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RateUsPopup : PopupBase
{
    [SerializeField] private Button[] _btnArray = new Button[0];
    [SerializeField] private Image[] _imgArray = new Image[0];
    [SerializeField] private Sprite _goldStar;
    [SerializeField] private Sprite _silverStar;
    [SerializeField] private string _androidId, _iosId;
    public Button _btnRate;

    private int _rateCount;

    private void Start()
    {
        // UIManager.onRate += OnShow;
        for (int i = 0; i < _btnArray.Length; i++)
        {
            int starIndex = i;
            var btn = _btnArray[starIndex];

            btn.onClick.AddListener(() => { OnChooseStar(starIndex); });
        }
        _btnRate.onClick.AddListener(() => { RateForUs(_rateCount); });

#if UNITY_ANDROID
        androidId = Application.identifier;
#endif
    }

    public override void OnShow()
    {
        base.OnShow();
        _rateCount = 0;

        for (int i = 0; i < _imgArray.Length; i++)
        {
            _imgArray[i].sprite = _silverStar;
        }
        _btnRate.interactable = false;
    }


    private void OnChooseStar(int star)
    {
        this._rateCount = star;
        StartCoroutine(I_Choose());
    }

    private IEnumerator I_Choose()
    {
        for (int i = 0; i < _imgArray.Length; i++)
        {
            if (i <= _rateCount)
                _imgArray[i].sprite = _goldStar;
            else
                _imgArray[i].sprite = _silverStar;
            yield return new WaitForSeconds(0.1f);
        }

        _btnRate.interactable = true;
    }

    public void RateForUs(int rateCount)
    {
        this._rateCount = rateCount;
        StartCoroutine(I_Rate(_rateCount));
    }

    private IEnumerator I_Rate(int rateCount)
    {
        float delay = rateCount * 0.1f + 0.5f;

        if (rateCount >= 4)
        {
#if UNITY_ANDROID
            Application.OpenURL("market://details?id=" + _androidId);
#elif UNITY_IPHONE
            Application.OpenURL("itms-apps://itunes.apple.com/app/id"+_iosId);
#endif
        }
        yield return new WaitForSeconds(delay);
        yield return new WaitForSeconds(0.15f);
        OnHide();
    }
}