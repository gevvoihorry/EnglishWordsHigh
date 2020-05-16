using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SelectKindManager : MonoBehaviour {

    public GameObject buttonBack;
    //public GameObject buttonTop;

    public GameObject[] imageClear = new GameObject[5];

    private int[] lessonCount = { 29, 43, 31 };
    private int[] clearLessonCount = new int[5];

    void Awake() {

    }

    // Start is called before the first frame update
    void Start() {
        JudgeClear();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void JudgeClear() {
        for (int i = 0; i < lessonCount.Length; i++) {
            for (int n = 0; n < lessonCount[i]; n++) {
                int readTestScore = PlayerPrefs.GetInt("READ_TEST_SCORE" + i + "_" + n, 0);
                int writeTestScore = PlayerPrefs.GetInt("WRITE_TEST_SCORE" + i + "_" + n, 0);
                if (readTestScore >= 80 && writeTestScore >= 80) {
                    clearLessonCount[i]++;
                } else {
                    break;
                }
            }
        }
        for (int i = 0; i < lessonCount.Length; i++) {
            if (clearLessonCount[i] == lessonCount[i]) {
                imageClear[i].SetActive(true);
            }
        }
    }

    public void PushButtonSelect(int num) {
        PlayerPrefs.SetInt("SELECT", num);
        PlayerPrefs.SetInt("LESSON", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("SelectScene");
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

    //public void PushButtonTop() {
    //    var sequence = DOTween.Sequence();
    //    sequence.Append(
    //        buttonTop.transform.DOScale(0.8f, 0.1f)
    //    );
    //    sequence.Append(
    //        buttonTop.transform.DOScale(1.0f, 0.1f)
    //        .OnComplete(() => {
    //            SceneManager.LoadScene("TitleScene");
    //        })
    //    );
    //}
}
