using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class TestSelectManager : MonoBehaviour {

    public GameObject[] buttonProTest = new GameObject[2];
    public GameObject buttonBack;

    private void Awake() {

    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void PushButtonProTest(int num) {
        int testNumber = num + 2;
        PlayerPrefs.SetInt("TEST_FLG", testNumber);
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonProTest[num].transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonProTest[num].transform.DOScale(1.0f, 0.1f)
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
                SceneManager.LoadScene("TitleScene");
            })
        );
    }
}
