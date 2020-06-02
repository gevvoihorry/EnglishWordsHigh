using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class RectangleManager : MonoBehaviour {

	static public RectangleManager instance;
    public BannerView bannerView;

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
		RequestRectangle();
        bannerView.Hide ();
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
	}

    void OnActiveSceneChanged( Scene prevScene, Scene nextScene ) {
        //Debug.Log ( prevScene.name + "->"  + nextScene.name );
    }

    void OnSceneLoaded( Scene scene, LoadSceneMode mode ) {
        //Debug.Log ( scene.name + " scene loaded");
        if (scene.name == "JudgeScene") {
            RectangleShow();
        }
    }

    void OnSceneUnloaded( Scene scene ) {
        RectangleHide();
        //Debug.Log ( scene.name + " scene unloaded");
    }

	private void RequestRectangle()
	{
		// 広告ユニット ID を記述します

		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		//string adUnitId = "ca-app-pub-3940256099942544/6300978111"; //test
		string adUnitId = "ca-app-pub-2998875168668037/3051761172";
		#elif UNITY_IOS
		//string adUnitId = "ca-app-pub-3940256099942544/2934735716"; //test
		string adUnitId = "ca-app-pub-2998875168668037/1547107811";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Create a 320x50 banner at the top of the screen.
		//BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
		//bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        //bannerView = new BannerView(adUnitId, AdSize.MediumRectangle, AdPosition.Top);
        bannerView = new BannerView(adUnitId, AdSize.MediumRectangle, AdPosition.Center);
		// Create an empty ad request.
		//AdRequest request = new AdRequest.Builder().Build();
		AdRequest request = new AdRequest.Builder ().AddTestDevice("").Build ();
		// Load the banner with the request.
		bannerView.LoadAd(request);
	}

    public void RectangleHide(){
		bannerView.Hide ();
	}

	public void RectangleShow(){
		bannerView.Show ();
	}
}
