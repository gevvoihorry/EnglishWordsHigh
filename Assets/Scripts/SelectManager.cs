using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour {

    public GameObject[] buttonLesson = new GameObject[16];
    public GameObject[] imageStar = new GameObject[16];
    public GameObject[] buttonTrans = new GameObject[2];
    public GameObject textPage;
    public GameObject buttonBack;

    private int[] lessonCount = { 33, 26, 27, 35, 39 };

    private int select = 0;
    private int lesson = 0;
    private int page = 0;
    private int maxPage = 0;
    private int clearCount = 0;

    void Awake() {
        select = PlayerPrefs.GetInt("SELECT", 0);
        lesson = PlayerPrefs.GetInt("LESSON", 0);
        for (int i = 0; i < lessonCount[select]; i++) {
            int readTestScore = PlayerPrefs.GetInt("READ_TEST_SCORE" + select + "_" + i, 0);
            int writeTestScore = PlayerPrefs.GetInt("WRITE_TEST_SCORE" + select + "_" + i, 0);
            if (readTestScore >= 80 && writeTestScore >= 80) {
                clearCount++;
            } else {
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start() {
        ChangePage();
        SetMaxPage();
        RefreshScene();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void RefreshScene() {
        DisplayTextPage();
        DisplayTextLesson();
        DisplayButtonLesson();
        DisplayButtonTrans();
        JudgeClear();
    }

    void ChangePage() {
        page = lesson / 16;
    }

    void DisplayTextPage() {
        textPage.GetComponent<Text>().text = (page + 1) + " / " + (maxPage + 1);
    }

    void SetMaxPage() {
        maxPage = lessonCount[select] / buttonLesson.Length;
        if (lessonCount[select] % buttonLesson.Length == 0) {
            maxPage--;
        }
    }

    void DisplayButtonLesson() {
        for (int i = 0; i < buttonLesson.Length; i++) {
            int number = (page * 16) + i;
            if (number < lessonCount[select]) {
                buttonLesson[i].SetActive(true);
            } else {
                buttonLesson[i].SetActive(false);
            }
        }
    }

    void DisplayTextLesson() {
        for (int i = 0; i < buttonLesson.Length; i++) {
            int number = (page * 16) + i;
            buttonLesson[i].GetComponentInChildren<Text>().text = "LESSON\n" + (number + 1);
        }
    }

    void JudgeClear() {
        for (int i = 0; i < buttonLesson.Length; i++) {
            int num = (page * 16) + i;
            if (num <= clearCount) {
                buttonLesson[i].GetComponent<Button>().interactable = true;
            } else {
                buttonLesson[i].GetComponent<Button>().interactable = false;
            }
            if (num < clearCount) {
                imageStar[i].SetActive(true);
            } else {
                imageStar[i].SetActive(false);
            }
        }
    }

    void DisplayButtonTrans() {
        if (page == 0) {
            buttonTrans[0].SetActive(false);
        } else {
            buttonTrans[0].SetActive(true);
        }

        if (page == maxPage) {
            buttonTrans[1].SetActive(false);
        } else {
            buttonTrans[1].SetActive(true);
        }
    }

    public void PushButtonLesson(int num) {
        //var sequence = DOTween.Sequence();
        //sequence.Append(
        //    buttonLesson[num].transform.DOScale(0.8f, 0.1f)
        //);
        //sequence.Append(
        //    buttonLesson[num].transform.DOScale(1.0f, 0.1f)
        //    .OnComplete(() => {
        //        int number = (page * 16) + num;
        //        PlayerPrefs.SetInt("LESSON", number);
        //        PlayerPrefs.Save();
        //        SceneManager.LoadScene("LessonSelectScene");
        //    })
        //);
        int number = (page * 16) + num;
        PlayerPrefs.SetInt("LESSON", number);
        PlayerPrefs.Save();
        SceneManager.LoadScene("LessonSelectScene");
    }

    public void PushButtonTrans(int num) {
        for (int i = 0; i < buttonTrans.Length; i++) {
            buttonTrans[i].GetComponent<Button>().interactable = false;
        }
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonTrans[num].transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonTrans[num].transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                if (num == 0) {
                    page--;
                } else {
                    page++;
                }
                RefreshScene();
                for (int i = 0; i < buttonTrans.Length; i++) {
                    buttonTrans[i].GetComponent<Button>().interactable = true;
                }
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
                SceneManager.LoadScene("SelectKindScene");
            })
        );
    }
}
