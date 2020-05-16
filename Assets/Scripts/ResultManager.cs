using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class ResultManager : MonoBehaviour {

    public GameObject interManager;

    public GameObject textSerif;
    public GameObject textScore;
    public GameObject imageResult;
    public GameObject buttonBackStudy;
    public GameObject buttonOK;

    public Sprite[] resultPicture = new Sprite[2];

    private int score = 0;
    private int testFlg = 0;
    private int select = 0;
    private int lesson = 0;

    private bool clearFlg = false;

    private float span = 1.0f;
	private float delta = 0;
	private int resultFlg = 0;

    private int review = 0;

    private int vibFlg = 0; //バイブレーションをONにしてるかどうか。

    void Awake() {
        interManager = GameObject.Find("InterManager");

        score = PlayerPrefs.GetInt("SCORE", 0);
        select = PlayerPrefs.GetInt("SELECT", 0);
        lesson = PlayerPrefs.GetInt("LESSON", 0);
        testFlg = PlayerPrefs.GetInt("TEST_FLG", 0);
        vibFlg = PlayerPrefs.GetInt("VIB_FLG", 0);
        if (testFlg < 2) {
            score *= 10;
        } else {
            score *= 5;
        }
        review = PlayerPrefs.GetInt("REVIEW", 0);
    }

    // Start is called before the first frame update
    void Start() {
        JudgeSave();
    }

    // Update is called once per frame
    void Update() {
        if (resultFlg > 3) {
			return;
		}
        this.delta += Time.deltaTime;
		if (delta > span) {
			resultFlg++;
			delta = 0;
		}
        if (resultFlg < 2) {
			Shuffle ();
		}
        if (resultFlg == 2 && delta == 0) {
			DisplayTextScore ();
		} else if (resultFlg == 3 && delta == 0) {
            if (vibFlg == 1 && Application.platform == RuntimePlatform.IPhonePlayer) {
                #if UNITY_IOS
                int soundId = 1520;
                UniIosAudioService.PlaySystemSound(soundId);
                #endif
            }
			DisplayTextSerif ();
			DisplayImageResult ();
            DisplayButtons();
        }
    }

    void JudgeSave() {
        if (score >= 80) {
            clearFlg = true;
        }
        string saveKey = "";
		if (testFlg == 0) {
            saveKey = "READ_TEST_SCORE" + select + "_" + lesson;
		} else if (testFlg == 1) {
			saveKey = "WRITE_TEST_SCORE" + select + "_" + lesson;
		} else if (testFlg == 2) {
			saveKey = "PRO_READ_TEST_SCORE";
		} else if (testFlg == 3) {
			saveKey = "PRO_WRITE_TEST_SCORE";
		}
        int beforeScore = PlayerPrefs.GetInt (saveKey, 0);
        if (score > beforeScore) {
			PlayerPrefs.SetInt (saveKey, score);
		}

        if (review != 999 && clearFlg == true) {
            review++;
            if (review > 4) {
                review = 0;
            }
            PlayerPrefs.SetInt("REVIEW", review);
        }

        PlayerPrefs.Save ();
    }

    void Shuffle(){
        int num = Random.Range(1, 21);
        int num2 = 5 * num;
        textScore.GetComponent<Text>().text = num2 + " / 100 点";
	}

    void DisplayTextScore(){
        textScore.GetComponent<Text>().text = score + " / 100 点";
	}

    void DisplayTextSerif(){
		string str = "";
		if (clearFlg) {
            if (score == 100) {
                str = "すごい！！\n満点です！！";
            } else {
                str = "やった！！\n合格ですね！！";
            }
		} else {
			str = "頑張りましょう！！";
		}
		textSerif.GetComponent<Text> ().text = str;
	}

    void DisplayImageResult(){
        int kind = 0;
        if (clearFlg) {
            kind = 1;
        }
		imageResult.GetComponent<Image> ().sprite = resultPicture [kind];
		imageResult.SetActive (true);
	}

    void DisplayButtons(){
        //if (!clearFlg) {
        //    buttonBackStudy.SetActive(true);
        //}
        if (score != 100) {
            buttonBackStudy.SetActive(true);
        }
		buttonOK.SetActive (true);
	}

    public void PushButtonOK() {
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonOK.transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonOK.transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                if (clearFlg == true && review == 4) {
                    SceneManager.LoadScene("ReviewScene");
                    return;
                }
                if (interManager != null) {
                    interManager.GetComponent<InterManager>().showInterstitial();
                }
                if (testFlg < 2) {
                    SceneManager.LoadScene("LessonSelectScene");
                } else {
                    SceneManager.LoadScene("TitleScene");
                }
            })
        );
    }

    public void PushButtonBackStudy() {
        PlayerPrefs.SetInt("BACK_STUDY_FLG", 1);
        PlayerPrefs.Save();
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonBackStudy.transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonBackStudy.transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                SceneManager.LoadScene("StudyScene");
            })
        );
    }
}
