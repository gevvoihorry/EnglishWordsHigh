using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreManager : MonoBehaviour {

    public GameObject textContinuedDays;
    public GameObject textTotalStudyDays;
    public GameObject textClearLessonCount;
    public GameObject textReadTestScore;
    public GameObject textWriteTestScore;

    public GameObject buttonBack;

    private int continuedDays = 0;
    private int totalStudyDays = 0;
    private int clearCount = 0;
    private int proReadTestScore = 0;
    private int proWriteTestScore = 0;

    private int[] lessonCount = { 29, 43, 31 };

    private int totalLessonCount;

    void Awake() {
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
        proReadTestScore = PlayerPrefs.GetInt("PRO_READ_TEST_SCORE", 0);
        proWriteTestScore = PlayerPrefs.GetInt("PRO_WRITE_TEST_SCORE", 0);

        for (int i = 0; i < lessonCount.Length; i++) {
            totalLessonCount += lessonCount[i];
        }
    }

    // Start is called before the first frame update
    void Start() {
        DisplayScene();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void DisplayScene() {
        textContinuedDays.GetComponent<Text>().text = continuedDays + "日";
        textTotalStudyDays.GetComponent<Text>().text = totalStudyDays + "日";
        textClearLessonCount.GetComponent<Text>().text = clearCount + " / " + totalLessonCount;
        textReadTestScore.GetComponent<Text>().text = proReadTestScore + "点";
        textWriteTestScore.GetComponent<Text>().text = proWriteTestScore + "点";
    }

    public void PushButtonBack() {
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonBack.transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonBack.transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                SceneManager.LoadScene("TitleScene");
            })
        );
    }
}
