/* using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommentDonate : PoolElement
{
    [SerializeField, TabGroup("Attribute")]
    private float timeAnim = 0.3f;

    [SerializeField, TabGroup("UI")]
    private Image icon;

    [SerializeField, TabGroup("UI")]
    private Image img_avatar;
    [SerializeField, TabGroup("UI")]
    private Image img_ads;

    [SerializeField, TabGroup("UI")]
    private RectTransform rect;
    [SerializeField, TabGroup("UI")]
    private Text text;

    [SerializeField, TabGroup("UI")]
    private List<Sprite> sprite_avatar;
    private int gold;

    public void Init(int gold)
    {
        this.gold = gold;
        text.text = "+" + gold.ToString();
        img_avatar.overrideSprite = RandomManager.Instance.RandomValueInList<Sprite>(sprite_avatar);

        if (gold <= 50) img_ads.gameObject.SetActive(false);
        else img_ads.gameObject.SetActive(true);
    }

    public void OnGetDonate()
    {
        //AudioManager.Instance.PlayOnceShot(AudioType.GetGold);

        //UIManager.Instance.GetScreen<ScreenSkewerEat>().PlayAnimMoney();

        int gold_temp = DataAPIController.Instance.ReadGold;
        gold_temp += gold;

        if (gold <= 50)
        {
            GetGold(gold_temp);
        }
        else
        {
            WatchAds(() =>
            {
                AdsManager.instance.ShowRewardVideo(() =>
                {
                    GetGold(gold_temp);
                });
            });
        }
        //DataAPIController.Instance.UpdateGold(gold_temp, () => {
        //    //Destroy(this.gameObject);
        //    PoolManager.Instance.DespawnObject(transform);
        //    GamePlayControl.Instance.rect_commentDonate = null;
        //});

        //int goldLevel_temp = DataAPIController.Instance.ReadGoldLevel;
        //if (GamePlayControl.LevelCaculator(goldLevel_temp) >= 10) return;
        //goldLevel_temp += gold;
        //DataAPIController.Instance.UpdateGoldLevel(goldLevel_temp, null);
    }

    public void GetGold(int gold_temp)
    {
        PoolManager.Instance.DespawnObject(transform);
        GamePlayControl.Instance.rect_commentDonate = null;
        ClaimCoinController.Instance.ShowEffect(text.transform, () =>
        {
            DataAPIController.Instance.UpdateGold(gold_temp, null);
        },
        () =>
        {
            int goldLevel_temp = DataAPIController.Instance.ReadGoldLevel;
            if (DataAPIController.Instance.ReadLevelUnlock >= 10) return;
            goldLevel_temp += gold;
            DataAPIController.Instance.UpdateGoldLevel(goldLevel_temp, null);
        });
    }

    public void WatchAds(Action callback)
    {
        Debug.Log("Watch Ads");
        callback?.Invoke();
    }
}
*/