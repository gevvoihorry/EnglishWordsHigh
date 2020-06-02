using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class BannerManager : MonoBehaviour {

	static public BannerManager instance;
    BannerView bannerView;

	void Awake (){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
    }

    void Start () {
        MobileAds.Initialize(initStatus => { });
        RequestBanner();
        BannerHide();
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
	}

    void OnActiveSceneChanged( Scene prevScene, Scene nextScene ) {
        //Debug.Log ( prevScene.name + "->"  + nextScene.name );
    }

    void OnSceneLoaded( Scene scene, LoadSceneMode mode ) {
        //Debug.Log ( scene.name + " scene loaded");
        if (scene.name == "StudyScene" || scene.name == "SelectKindScene") {
            BannerShow();
        }
    }

    void OnSceneUnloaded( Scene scene ) {
        BannerHide();
        //Debug.Log ( scene.name + " scene unloaded");
    }

	private void RequestBanner()
	{
		// 広告ユニット ID を記述します

		#if UNITY_EDITOR
		string adUnitId = "unused";
        #elif UNITY_ANDROID
		//string adUnitId = "ca-app-pub-3940256099942544/6300978111"; //test
		string adUnitId = "ca-app-pub-2998875168668037/4364842847";
        #elif UNITY_IOS
		//string adUnitId = "ca-app-pub-3940256099942544/2934735716"; //test
		string adUnitId = "ca-app-pub-2998875168668037/4173271159";
        #else
		string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        //bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        //AdRequest request = new AdRequest.Builder().Build();
        AdRequest request = new AdRequest.Builder ().AddTestDevice("").Build ();
		// Load the banner with the request.
		bannerView.LoadAd(request);

	}

    public void BannerHide(){
		bannerView.Hide ();
	}

	public void BannerShow(){
		bannerView.Show ();
	}
}
