using UnityEngine;
//using GoogleMobileAds.Api;
using UnityEngine.Advertisements;

public class Advertising : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{
#if UNITY_ANDROID
    const string GAMEID = "5403001";
    const string INTER_ADS_ID = "Interstitial_Android";
    const string AD_MOB_INTER_ID = "ca-app-pub-8743309310393799~1624686902";
#endif
#if UNITY_IOS
        const string GAMEID = "5403000";
        const string INTER_ADS_ID = "Interstitial_iOS";

        const string AD_MOB_INTER_ID = "ca-app-pub-8743309310393799~3951714767";
#endif
    //private InterstitialAd interstitialAd;

    void Start()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
            Advertisement.Initialize(GAMEID, false, this);

        //MobileAds.Initialize((InitializationStatus initStatus) =>
        //{
        //    Debug.Log("Connecting to the admob server");
        //});

        LoadUnityAds();
    }

    //public void LoadAdMobInter()
    //{
    //    if (interstitialAd != null)
    //    {
    //        interstitialAd.Destroy();
    //        interstitialAd = null;
    //    }

    //    Debug.Log("Loading the interstitial ad.");

    //    var adRequest = new AdRequest();

    //    InterstitialAd.Load(AD_MOB_INTER_ID, adRequest, (InterstitialAd ad, LoadAdError error) =>
    //    {
    //        if (error != null || ad == null)
    //        {
    //            Debug.LogError($"Interstitial ad failed to load an ad with error {error}");
    //            return;
    //        }

    //        Debug.Log($"Interstitial ad loaded with response {ad.GetResponseInfo()}");

    //        interstitialAd = ad;
    //    });
    //}

    void LoadUnityAds(){
        Advertisement.Load(INTER_ADS_ID, this);
    }

    public void ShowInterAd(){
        Debug.Log("The selected ad source is Unity Ads");
        Advertisement.Show(INTER_ADS_ID, this);
        // int random = Random.Range(1, 1000);
        // if (random % 2 != 0)
        // {
        //     Debug.Log("The selected ad source is Unity Ads");
        //     Advertisement.Show(INTER_ADS_ID, this);
        // }
        // else {
        //     Debug.Log("The ad is being shown is an AD Mob");
        //     //ShowAdMobInterstitialAds();
        // }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("The unity ads are initialized....");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"There's an issue with the unity ads {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("The ads are loading up for the user...");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"There's an issue with loading the unity ads {message}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("The ads are showing up...");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("The ads are completed...");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"There's an issue with loading the unity ads {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"There's are starting....");
    }

    //public void ShowAdMobInterstitialAds()
    //{
    //    if (interstitialAd != null && interstitialAd.CanShowAd())
    //    {
    //        Debug.Log("Showing AdMob interstitial ad..");
    //        interstitialAd.OnAdPaid += (AdValue adValue) =>
    //        {
    //            Debug.Log($"Interstitial ad paid {adValue.Value} {adValue.CurrencyCode}");
    //        };

    //        interstitialAd.OnAdImpressionRecorded += () =>
    //        {
    //            Debug.Log("Interstitial ad recorded an impression");
    //        };

    //        interstitialAd.OnAdClicked += () =>
    //        {
    //            Debug.Log("Interstitial ad was clicked");
    //        };

    //        interstitialAd.OnAdFullScreenContentOpened += () =>
    //        {
    //            Debug.Log("Interstitial ad full screen content opened");
    //        };

    //        interstitialAd.OnAdFullScreenContentClosed += () =>
    //        {
    //            Debug.Log("Interstitial ad full screen content closed");

    //            LoadAdMobInter();
    //        };

    //        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
    //        {
    //            Debug.LogError($"Interstitial ad failed to open full screen content with error {error}");

    //            LoadAdMobInter();
    //        };

    //        interstitialAd.Show();
    //    }
    //    else
    //        Debug.LogWarning("The ad is not ready yet..");
    //}
}
