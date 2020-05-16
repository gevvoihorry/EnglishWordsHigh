using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleManager : MonoBehaviour {

    public GameObject buttonStudy;
    public GameObject buttonTest;
    public GameObject buttonScore;

    void Awake() {
        int firstPlay = PlayerPrefs.GetInt("FIRST_PLAY", 0);
        if (firstPlay == 0) {
            PlayerPrefs.SetInt("VIB_FLG", 1);
            PlayerPrefs.SetInt("PUSH_FLG", 1);
            PlayerPrefs.SetInt("REVIEW", 3);
            PlayerPrefs.Save();
        }
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void PushButtonStudy() {
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonStudy.transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonStudy.transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                SceneManager.LoadScene("SelectKindScene");
            })
        );
    }

    public void PushButtonTest() {
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonTest.transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonTest.transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                SceneManager.LoadScene("TestSelectScene");
            })
        );
    }

    public void PushButtonScore() {
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonScore.transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonScore.transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                SceneManager.LoadScene("ScoreScene");
            })
        );
    }

    public void PushButtonDebug() {
        SceneManager.LoadScene("DebugScene");
    }
}
