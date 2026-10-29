using UnityEngine;
using UnityEngine.Advertisements;

public class Advertising : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{
#if UNITY_IOS
    const string GAMEID = "5403000";
    const string INTER_ADS_ID = "Interstitial_iOS";
#else
    // Android ids are also used in the Editor and on other platforms so the project always compiles
    const string GAMEID = "5403001";
    const string INTER_ADS_ID = "Interstitial_Android";
#endif

    private bool _interAdLoaded;

    void Start()
    {
        if (!Advertisement.isSupported) return;

        // Ads can only be loaded once the SDK is initialized
        if (Advertisement.isInitialized) LoadUnityAds();
        else Advertisement.Initialize(GAMEID, false, this);
    }

    void LoadUnityAds(){
        _interAdLoaded = false;
        Advertisement.Load(INTER_ADS_ID, this);
    }

    public void ShowInterAd(){
        // Only show an ad about half of the time
        if (!_interAdLoaded || Random.Range(0, 2) != 0) return;

        Debug.Log("The selected ad source is Unity Ads");
        _interAdLoaded = false;
        Advertisement.Show(INTER_ADS_ID, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("The unity ads are initialized....");
        LoadUnityAds();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogWarning($"There's an issue with the unity ads {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("The ads are loaded for the user...");
        _interAdLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogWarning($"There's an issue with loading the unity ads {message}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("The ad was clicked...");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("The ads are completed...");
        // Each ad can only be shown once, so get the next one ready
        LoadUnityAds();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogWarning($"There's an issue with showing the unity ads {message}");
        LoadUnityAds();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("The ads are starting....");
    }
}
