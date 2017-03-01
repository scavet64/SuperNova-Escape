using UnityEngine;
using System;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdvertManager : MonoBehaviour {

    public static AdvertManager adManager;

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private static string outputMessage = "";
    private bool testingApp = false;
    private string iosBannerAdID = "ca-app-pub-6333353846841342/6145890514";
    private string iosIntersitialAdID = "ca-app-pub-6333353846841342/7622623717";

    void Awake() {
        if (adManager != null) {
            Destroy(gameObject);
        } else {
            adManager = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    public void RequestBanner() {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
			string adUnitId = iosBannerAdID;
#elif UNITY_IPHONE
			string adUnitId = "ca-app-pub-6333353846841342/5385634111";
#else
			string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the bottom of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
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

    public void destoryBanner() {
        bannerView.Destroy();
    }

    public void RequestInterstitial() {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
		string adUnitId = iosIntersitialAdID;
#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-6333353846841342/8339100514";
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
        //interstitial.Show ();
    }

    public void showInterstitial() {
        interstitial.Show();
    }

    private AdRequest createAdRequest() {
        //		if (testingApp) {
        //			Debug.Log ("TESTAD");
        //			return new AdRequest.Builder()
        //			.AddTestDevice(AdRequest.TestDeviceSimulator)
        //			.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
        //			.AddKeyword("game")
        //			.SetGender(Gender.Male)
        //			.SetBirthday(new DateTime(1999, 1, 1))
        //			.TagForChildDirectedTreatment(false)
        //			.AddExtra("color_bg", "9B30FF")
        //			.Build();
        //		} else {
        return new AdRequest.Builder().Build();
        //		}
    }

    //	private AdRequest createTestAdRequest(){
    //		return new AdRequest.Builder()
    //		.AddTestDevice(AdRequest.TestDeviceSimulator)
    //		.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
    //		.AddKeyword("game")
    //		.SetGender(Gender.Male)
    //		.SetBirthday(new DateTime(1999, 1, 1))
    //		.TagForChildDirectedTreatment(false)
    //		.AddExtra("color_bg", "9B30FF")
    //		.Build();
    //	}

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