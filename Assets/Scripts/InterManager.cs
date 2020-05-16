using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;

public class InterManager : MonoBehaviour {

//	public GameObject unityAdsManager;

	private InterstitialAd interstitial;

	static public InterManager instance;

	void Awake (){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
        MobileAds.Initialize(initStatus => { });
		// 起動時にインタースティシャル広告をロードしておく
		RequestInterstitial ();

//		unityAdsManager = GameObject.Find ("UnityAdsManager");

	}

	void Update () {

	}

	public void RequestInterstitial(){
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-3940256099942544/1033173712"; //test
		//string adUnitId = "ca-app-pub-2998875168668037/8112516167";
		#elif UNITY_IOS
		string adUnitId = "ca-app-pub-3940256099942544/4411468910"; //test
		//string adUnitId = "ca-app-pub-2998875168668037/9234026147";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd (adUnitId);

		// Called when an ad is shown.
		this.interstitial.OnAdOpening += HandleOnAdOpened;

		// Called when the ad is closed.
		this.interstitial.OnAdClosed += HandleOnAdClosed;

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder ().AddTestDevice("").Build ();
		// Load the interstitial with the request.
		interstitial.LoadAd (request);
	}

	public void LoadInterstitial(){
		if (interstitial.IsLoaded ()) {
			return;
		} else {
			RequestInterstitial ();
		}
	}

	public void HandleOnAdOpened(object sender, EventArgs args){
		MonoBehaviour.print("HandleAdOpened event received");
	}

	public void HandleOnAdClosed(object sender, EventArgs args){
		MonoBehaviour.print("HandleAdClosed event received");
		interstitial.Destroy ();
		RequestInterstitial ();
	}

	public void showInterstitial(){
		if (interstitial.IsLoaded ()) {
			interstitial.Show ();
            Debug.Log("showInter");
		} else {

		}
	}
}
