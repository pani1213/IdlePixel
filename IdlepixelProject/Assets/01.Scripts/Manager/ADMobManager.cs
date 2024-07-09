using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class ADMobManager : Singleton<ADMobManager>
{

    private string blessingAD_UnitId = "ca-app-pub-3940256099942544/5224354917";//test

    private RewardedAd _rewardedAd;

    private void Start()
    {
        MobileAds.Initialize((InitializationStatus initializationStatus)=>
        {
            Debug.Log("모바일 ADS SDK 가 초기화됨");
        });
    }
    private RewardedAd rewardedAd;
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(blessingAD_UnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;

                RegisterEventHandlers(rewardedAd);

                ShowRewardedAd();
            });
    }
    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log("광고 리워드 발생");
                Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // 광고수익이 발생하면 호출됨
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("광고수익 발생 {0}, {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // 광고에대한 노출이 기록되면 발생됨
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("광고 노출 발생");
        };
        // 광고 클릭이 기록되면 발생됨
        ad.OnAdClicked += () =>
        {
            Debug.Log("광고 클릭 기록됨");
        };
        // 광고가 전체 화면 콘텐츠를 열때 발생됨
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("광고 전체화면 콘텐츠 엶");
        };
        // 광고가 전체화면 콘텐츠를 닫을때 발생됨
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("광고 전체화면 콘텐츠 닫음 ");
      
        };
        // 광고가 전체화면 콘텐츠를 열지 못했을때 발생됨
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("광고 전체화면 콘텐츠를 열지못함" +
                           "with error : " + error);
        
        };
    }
 
}
