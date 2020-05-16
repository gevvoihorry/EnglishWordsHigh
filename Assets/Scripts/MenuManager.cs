using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using SocialConnector;

public class MenuManager : MonoBehaviour {

    public GameObject socialManager;

    public GameObject canvasMenuAbout;
    public GameObject buttonMenu;
    public GameObject buttonShare;
    public GameObject buttonReview;
    public GameObject buttonVibWindow;
    public GameObject buttonVibOn;
    public GameObject buttonVibOff;
    public GameObject buttonNotificationWindow;
    public GameObject buttonNotificationOn;
    public GameObject buttonNotificationOff;
    public GameObject buttonMenuShare;
    public GameObject buttonMenuReview;
    public GameObject buttonReturnTop;

    private int vibFlg = 0;
    private int pushFlg = 0;

    void Awake() {
        socialManager = GameObject.Find("SocialManager");
        vibFlg = PlayerPrefs.GetInt("VIB_FLG", 0);
        pushFlg = PlayerPrefs.GetInt("PUSH_FLG", 0);
    }

    // Start is called before the first frame update
    void Start() {
        ChangeButtonVib();
        ChangeButtonNotification();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void ChangeButtonVib() {
        if (vibFlg == 0) {
            buttonVibWindow.transform.localPosition = buttonVibOff.transform.localPosition;
        } else {
            buttonVibWindow.transform.localPosition = buttonVibOn.transform.localPosition;
        }
    }

    void ChangeButtonNotification() {
        if (pushFlg == 0) {
            buttonNotificationWindow.transform.localPosition = buttonNotificationOff.transform.localPosition;
        } else {
            buttonNotificationWindow.transform.localPosition = buttonNotificationOn.transform.localPosition;
        }
    }

    public void PushButtonVib(int num) {
        vibFlg = num;
        PlayerPrefs.SetInt("VIB_FLG", vibFlg);
        PlayerPrefs.Save();
        ChangeButtonVib();
    }

    public void PushButtonNotification(int num) {
        pushFlg = num;
        PlayerPrefs.SetInt("PUSH_FLG", pushFlg);
        PlayerPrefs.Save();
        ChangeButtonNotification();
        GameObject pushManager = GameObject.Find("PushManager");
        if (pushManager != null) {
            pushManager.GetComponent<PushManager>().ChangePushFlg(pushFlg);
        }
    }

	public void PushButtonShere() {
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonShare.transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonShare.transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                if (socialManager != null) {
                    socialManager.GetComponent<SocialManager>().PushSocial();
                }
            })
        );
    }

    public void PushButtonMenuShere() {
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonMenuShare.transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonMenuShare.transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                if (socialManager != null) {
                    socialManager.GetComponent<SocialManager>().PushSocial();
                }
            })
        );
    }

    public void PushButtonReview() {
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonReview.transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonReview.transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                if (Application.platform == RuntimePlatform.Android) {
			        Application.OpenURL ("https://play.google.com/store/apps/details?id=com.gevvoihorry.EnglishTestLevelSemiTwo");
		        } else if (Application.platform == RuntimePlatform.IPhonePlayer) {
			        Application.OpenURL ("https://apps.apple.com/jp/app/id1513455662?mt=8&action=write-review");
		        } else {
			        Application.OpenURL ("https://apps.apple.com/jp/app/id1513455662?mt=8&action=write-review");
		        }
            })
        );
    }

    public void PushButtonMenuReview() {
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonMenuReview.transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonMenuReview.transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                if (Application.platform == RuntimePlatform.Android) {
			        Application.OpenURL ("https://play.google.com/store/apps/details?id=com.gevvoihorry.EnglishTestLevelSemiTwo");
		        } else if (Application.platform == RuntimePlatform.IPhonePlayer) {
			        Application.OpenURL ("https://apps.apple.com/jp/app/id1513455662?mt=8&action=write-review");
		        } else {
			        Application.OpenURL ("https://apps.apple.com/jp/app/id1513455662?mt=8&action=write-review");
		        }
            })
        );
    }

    public void PushButtonMenu() {
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonMenu.transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonMenu.transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                canvasMenuAbout.SetActive(true);
            })
        );
    }

    public void PushButtonMenuDelete() {
        canvasMenuAbout.SetActive(false);
    }

    public void PushButtonReturnTop() {
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonReturnTop.transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonReturnTop.transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                SceneManager.LoadScene("TitleScene");
            })
        );
    }

}
