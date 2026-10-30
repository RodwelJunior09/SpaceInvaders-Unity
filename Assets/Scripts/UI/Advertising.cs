using UnityEngine;
using UnityEngine.Advertisements;

namespace UI
{
    public class Advertising : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
    {
#if UNITY_IOS
    const string GAMEID = "5403000";
    const string INTER_ADS_ID = "Interstitial_iOS";
#else
        // Android ids are also used in the Editor and on other platforms, so the project always compiles
        private const string GameID = "5403001";
        private const string InterAdsID = "Interstitial_Android";
#endif

        // Minimum time in seconds between two interstitial ads
        [SerializeField] private float adCooldownSeconds = 120f;

        private bool _interAdLoaded;

        // Static, because every scene has its own Advertising object and a per-instance timer
        // would be reset on each scene change
        private static float _nextAdTime;

        // True when an ad was played and the next player that spawns should get a few seconds of immunity
        private static bool _pendingImmunity;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetCooldown()
        {
            _nextAdTime = 0f;
            _pendingImmunity = false;
        }

        // Returns true once after an ad was played, so only one player gets the immunity
        public static bool ConsumePendingImmunity()
        {
            var pending = _pendingImmunity;
            _pendingImmunity = false;
            return pending;
        }

        private void Start()
        {
            if (!Advertisement.isSupported) return;

            // Ads can only be loaded once the SDK is initialized
            if (Advertisement.isInitialized) LoadUnityAds();
            else Advertisement.Initialize(GameID, false, this);
        }

        void LoadUnityAds(){
            _interAdLoaded = false;
            Advertisement.Load(InterAdsID, this);
        }

        public void ShowInterAd(){
            // Only show an ad if the cooldown since the last shown ad has passed
            if (!_interAdLoaded || Time.unscaledTime < _nextAdTime) return;

            Debug.Log("The selected ad source is Unity Ads");
            _interAdLoaded = false;
            _nextAdTime = Time.unscaledTime + adCooldownSeconds;
            _pendingImmunity = true;
            Advertisement.Show(InterAdsID, this);
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
            // If a player is already in the game, give the immunity now that the ad is over
            var player = FindFirstObjectByType<Player.Player>();
            if (player)
            {
                _pendingImmunity = false;
                player.GrantAdImmunity();
            }
            // Each ad can only be shown once, so get the next one ready
            LoadUnityAds();
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.LogWarning($"There's an issue with showing the unity ads {message}");
            // No ad was actually shown, so no immunity is needed
            _pendingImmunity = false;
            LoadUnityAds();
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.Log("The ads are starting....");
        }
    }
}
