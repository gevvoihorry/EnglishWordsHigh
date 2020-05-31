using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class LessonSelectManager : MonoBehaviour {

    public GameObject[] textTitle = new GameObject[2];
    public GameObject[] imageScoreWindow = new GameObject[2];
    public GameObject textReadTestScore;
    public GameObject textWriteTestScore;
    public GameObject buttonStudy;
    public GameObject[] buttonTest = new GameObject[2];
    public GameObject buttonBack;

    private int select = 0;
    private int lesson = 0;
    private int readTestScore = 0;
    private int writeTestScore = 0;

    void Awake() {
        select = PlayerPrefs.GetInt("SELECT", 0);
        lesson = PlayerPrefs.GetInt("LESSON", 0);
        readTestScore = PlayerPrefs.GetInt("READ_TEST_SCORE" + select + "_" + lesson, 0);
        writeTestScore = PlayerPrefs.GetInt("WRITE_TEST_SCORE" + select + "_" + lesson, 0);
    }

    // Start is called before the first frame update
    void Start() {
        DisplayTextTitle();
        DisplayTestScore();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void DisplayTextTitle() {
        textTitle[0].GetComponent<Text>().text = "LEVEL" + (select + 1);
        textTitle[1].GetComponent<Text>().text = "LESSON" + (lesson + 1);
    }

    void DisplayTestScore() {
        textReadTestScore.GetComponent<Text>().text = readTestScore + " / 100点";
        if (readTestScore >= 80) {
            imageScoreWindow[0].GetComponent<Image>().color = new Color(0.0f / 255.0f, 64.0f / 255.0f, 48.0f / 255.0f, 255.0f / 255.0f);
            textReadTestScore.GetComponent<Text>().color = Color.white;
        } else {
            imageScoreWindow[0].GetComponent<Image>().color = Color.white;
            textReadTestScore.GetComponent<Text>().color = Color.black;
        }

        textWriteTestScore.GetComponent<Text>().text = writeTestScore + " / 100点";
        if (writeTestScore >= 80) {
            imageScoreWindow[1].GetComponent<Image>().color = new Color(0.0f / 255.0f, 64.0f / 255.0f, 48.0f / 255.0f, 255.0f / 255.0f);
            textWriteTestScore.GetComponent<Text>().color = Color.white;
        } else {
            imageScoreWindow[1].GetComponent<Image>().color = Color.white;
            textWriteTestScore.GetComponent<Text>().color = Color.black;
        }
    }

    public void PushButtonStudy() {
        int backStudyFlg = PlayerPrefs.GetInt("BACK_STUDY_FLG", 0);
        if (backStudyFlg == 1) {
            PlayerPrefs.SetInt("BACK_STUDY_FLG", 0);
            PlayerPrefs.Save();
        }
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonStudy.transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonStudy.transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                SceneManager.LoadScene("StudyScene");
            })
        );
    }

    public void PushButtonTest(int num) {
        PlayerPrefs.SetInt("TEST_FLG", num);
        PlayerPrefs.Save();
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonTest[num].transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonTest[num].transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                SceneManager.LoadScene("TestReadyScene");
            })
        );
    }

    public void PushButtonBack() {
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonBack.transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonBack.transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                SceneManager.LoadScene("SelectScene");
            })
        );
    }
}
