using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class LoginManager : MonoBehaviour　{

    public GameObject canvasLogin;

    public GameObject textContinuedDays;
	public GameObject textTotalStudyDays;
    public GameObject textClearLessonCount;

    public GameObject imageUpdating;

    public GameObject buttonLoginOK;

    private string loginDate = "";

    private int continuedDays = 0;
    private int totalStudyDays = 0;
    private int clearCount = 0;

    private bool nextFlg = false; //日付が経過したかどうかを判定するフラグ。
    private bool updatingFlg = false; //更新中かどうかを判定するフラグ

    private int[] lessonCount = { 33, 26, 27, 35, 39 };

    private int totalLessonCount;

    void Awake() {
        loginDate = PlayerPrefs.GetString("LOGIN_DATE", "");
        continuedDays = PlayerPrefs.GetInt("CONTINUED_DAYS", 0);
        totalStudyDays = PlayerPrefs.GetInt("TOTAL_STUDY_DAYS", 0);
        for (int i = 0; i < lessonCount.Length; i++) {
            for (int n = 0; n < lessonCount[i]; n++) {
                int readTestScore = PlayerPrefs.GetInt("READ_TEST_SCORE" + i + "_" + n, 0);
                int writeTestScore = PlayerPrefs.GetInt("WRITE_TEST_SCORE" + i + "_" + n, 0);
                if (readTestScore >= 80 && writeTestScore >= 80) {
                    clearCount++;
                } else {
                    break;
                }
            }
        }
        LoginCheck();

        for (int i = 0; i < lessonCount.Length; i++) {
            totalLessonCount += lessonCount[i];
        }
    }

    // Start is called before the first frame update
    void Start()　{
        int firstPlay = PlayerPrefs.GetInt("FIRST_PLAY", 0);
        if (nextFlg == true && firstPlay == 1) {
            DisplayCanvasLogin();
        }
    }

    // Update is called once per frame
    void Update()　{

    }

    void LoginCheck() {
        DateTime dtNow = DateTime.Now;
        //dtNow = dtNow.AddDays(1); //テスト用
        string todayStr = dtNow.ToString("yyyyMMdd");

        //日付が変わったかどうかの判定
        if (loginDate != todayStr) {
            nextFlg = true;
        }

        if (nextFlg) {
            //LoginDateの上書き
            PlayerPrefs.SetString("LOGIN_DATE", todayStr);
            PlayerPrefs.Save();

            //継続中かどうかの判定
            DateTime beforeDate = dtNow.AddDays(-1);
            string beforeDateStr = beforeDate.ToString("yyyyMMdd");
            if (loginDate == beforeDateStr) {
                updatingFlg = true;
            }

            //更新日数の上書き
            if (updatingFlg) {
                continuedDays++;
            } else {
                continuedDays = 1;
            }

            PlayerPrefs.SetInt("CONTINUED_DAYS", continuedDays);

            //総学習日数の上書き
            totalStudyDays++;
            PlayerPrefs.SetInt("TOTAL_STUDY_DAYS", totalStudyDays);

            PlayerPrefs.Save();
        }
    }

    void DisplayCanvasLogin() {
        canvasLogin.SetActive(true);
        if (updatingFlg) {
            imageUpdating.SetActive(true);
        }
        textContinuedDays.GetComponent<Text>().text = continuedDays + "日";
        textTotalStudyDays.GetComponent<Text>().text = totalStudyDays + "日";
        textClearLessonCount.GetComponent<Text>().text = clearCount + " / " + totalLessonCount;
    }

    public void PushButtonLoginOK() {
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonLoginOK.transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonLoginOK.transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                canvasLogin.SetActive(false);
            })
        );
    }
}
