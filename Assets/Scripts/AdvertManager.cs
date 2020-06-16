using UnityEngine;
using System;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdvertManager : MonoBehaviour {

	/// <summary>
	/// Singlton instance.
	/// </summary>
    public static AdvertManager adManager;

    private BannerView bannerView;
    private InterstitialAd interstitial;

	/// <summary>
	/// The ios ad identifiers. Set these using the ids from AdMob
	/// </summary>
    private string iosBannerAdID = "ca-app-pub-6333353846841342/6145890514";
    private string iosIntersitialAdID = "ca-app-pub-6333353846841342/7622623717";

	/// <summary>
	/// The position you want the ad to appear.
	/// </summary>
	private AdPosition adPosition = AdPosition.Bottom;

	/// <summary>
	/// Method runs when this game object is awoken.
	/// </summary>
    void Awake() {
        if (adManager != null) {
            Destroy(gameObject);
        } else {
            adManager = this;
            MobileAds.Initialize(initStatus => { });
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

	/// <summary>
	/// Requests the banner. This method will show the ad when ready.
	/// </summary>
    public void RequestBanner() {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_IPHONE
			string adUnitId = iosBannerAdID;
#else
			string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the bottom of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, adPosition);
        // Register for ad events.
        bannerView.OnAdLoaded += HandleAdLoaded;
        bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
        bannerView.OnAdOpening += HandleAdOpened;
        bannerView.OnAdClosed += HandleAdClosed;
        bannerView.OnAdLeavingApplication += HandleAdLeftApplication;
        // Load a banner ad.
        bannerView.LoadAd(createAdRequest());
        bannerView.Show();
    }

	/// <summary>
	/// Destories the banner.
	/// </summary>
    public void DestoryBanner() {
        bannerView.Destroy();
    }

	/// <summary>
	/// Requests the interstitial. Does not show the ad.
	/// </summary>
    public void RequestInterstitial() {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_IPHONE
		string adUnitId = iosIntersitialAdID;
#else
		string adUnitId = "unexpected_platform";
#endif

        // Create an interstitial.
        interstitial = new InterstitialAd(adUnitId);
        // Register for ad events.
        interstitial.OnAdLoaded += HandleInterstitialLoaded;
        interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
        interstitial.OnAdOpening += HandleInterstitialOpened;
        interstitial.OnAdClosed += HandleInterstitialClosed;
        interstitial.OnAdLeavingApplication += HandleInterstitialLeftApplication;
        // Load an interstitial ad.
        interstitial.LoadAd(createAdRequest());
    }

	/// <summary>
	/// Shows the interstitial.
	/// </summary>
    public void ShowInterstitial() {
        interstitial.Show();
    }

	private AdRequest createAdRequest()
	{
		return new AdRequest.Builder().Build();
	}

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args) {
        print("HandleAdLoaded event received.");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args) {
        print("HandleAdOpened event received");
    }

    void HandleAdClosing(object sender, EventArgs args) {
        print("HandleAdClosing event received");
    }

    public void HandleAdClosed(object sender, EventArgs args) {
        print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args) {
        print("HandleAdLeftApplication event received");
    }

    #endregion

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args) {
        print("HandleInterstitialLoaded event received.");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args) {
        print("HandleInterstitialOpened event received");
    }

    void HandleInterstitialClosing(object sender, EventArgs args) {
        print("HandleInterstitialClosing event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args) {
        print("HandleInterstitialClosed event received");
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args) {
        print("HandleInterstitialLeftApplication event received");
    }

    #endregion

}