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
            Debug.Log("����� ADS SDK �� �ʱ�ȭ��");
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
                Debug.Log("���� ������ �߻�");
                Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // ��������� �߻��ϸ� ȣ���
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("������� �߻� {0}, {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // �������� ������ ��ϵǸ� �߻���
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("���� ���� �߻�");
        };
        // ���� Ŭ���� ��ϵǸ� �߻���
        ad.OnAdClicked += () =>
        {
            Debug.Log("���� Ŭ�� ��ϵ�");
        };
        // ���� ��ü ȭ�� �������� ���� �߻���
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("���� ��üȭ�� ������ ��");
        };
        // ���� ��üȭ�� �������� ������ �߻���
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("���� ��üȭ�� ������ ���� ");
      
        };
        // ���� ��üȭ�� �������� ���� �������� �߻���
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("���� ��üȭ�� �������� ��������" +
                           "with error : " + error);
        
        };
    }
 
}
