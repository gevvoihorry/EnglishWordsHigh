using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Networking;

public class StudyManager : MonoBehaviour {

    public GameObject interManager;
    public GameObject englishManager;
    public GameObject soundManager;

    public GameObject textCount;
    public GameObject textEnglish;
    public GameObject textMean;
    public GameObject buttonSound;
    public GameObject buttonExample;
    public GameObject buttonBack;
    public GameObject[] buttonTrans = new GameObject[2];

    private int select = 0;
    private int lesson = 0;
    private int number = 0;
    private int page = 0;
    private int maxPage = 0;
    private int backStudyFlg = 0;
    private int score = 0;
    private int testFlg = 0;

    private int[] lessonCount = { 33, 26, 27, 35, 39 };

    private string spell = "";

    //memo
    public GameObject buttonMemo;
    public GameObject canvasMemo;
    public GameObject textMemo;
    public InputField inputField;

    //menu
    public GameObject canvasMenu;

    void Awake() {
        interManager = GameObject.Find("InterManager");
        englishManager = GameObject.Find("EnglishManager");
        soundManager = GameObject.Find("SoundManager");

        select = PlayerPrefs.GetInt("SELECT", 0);
        lesson = PlayerPrefs.GetInt("LESSON", 0);
        backStudyFlg = PlayerPrefs.GetInt("BACK_STUDY_FLG", 0);
        if (backStudyFlg == 1) {
            score = PlayerPrefs.GetInt("SCORE", 0);
            testFlg = PlayerPrefs.GetInt("TEST_FLG", 0);
        }
    }

    // Start is called before the first frame update
    void Start() {
        RefreshScene();
        if (backStudyFlg == 1) {
            canvasMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    void RefreshScene() {
        SetNumber();
        SetMaxPage();
        DisplayTextCount();
        DisplayTextEnglish();
        DisplayTextMean();
        DisplayTranButton();
    }

    void SetNumber() {
        if (backStudyFlg == 0) {
            int baseCount = 0;
            for (int i = 0; i < lessonCount.Length; i++) {
                if (i < select) {
                    baseCount += (lessonCount[i] * 10);
                }
            }
            number = baseCount + (lesson * 10) + page;
        } else {
            number = PlayerPrefs.GetInt("MISS_NUMBER" + page, 0);
        }
    }

    void SetMaxPage() {
        if (backStudyFlg == 0) {
            maxPage = 9;
        } else {
            if (testFlg < 2) {
                maxPage = 9 - score;
            } else {
                maxPage = 19 - score;
            }
        }
    }

    void DisplayTextCount() {
        textCount.GetComponent<Text>().text = (page + 1) + " / " + (maxPage + 1);
    }

    void DisplayTextEnglish() {
        string str = englishManager.GetComponent<EnglishManager>().ReturnEnglish(number);
        textEnglish.GetComponent<Text> ().text = str;
        ChangeTextSize(textEnglish, 650, 120);

        spell = str;
        spell = spell.Replace("?", "");
        spell = spell.Replace("？", "");
        spell = spell.Replace("~", "");
        spell = spell.Replace("〜", "");
    }

    void DisplayTextMean() {
        string str = englishManager.GetComponent<EnglishManager>().ReturnMean(number);
        textMean.GetComponent<Text> ().text = str;
        ChangeTextSize(textMean, 650, 90);
    }

    void ChangeTextSize(GameObject obj, int limit, int initSize) {
        obj.GetComponent<Text>().fontSize = initSize;
        while (true) {
            Text text = obj.GetComponent<Text>();
            float width = text.preferredWidth;
            if (width < limit) {
                break;
            }
            int size = obj.GetComponent<Text>().fontSize;
            obj.GetComponent<Text>().fontSize = (size - 1);
        }
    }

    void DisplayTranButton() {
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
                if (backStudyFlg == 1 && interManager != null) {
                    interManager.GetComponent<InterManager>().showInterstitial();
                }
                if (backStudyFlg == 1 && testFlg > 1) {
                    SceneManager.LoadScene("TitleScene");
                    return;
                }
                SceneManager.LoadScene("LessonSelectScene");
            })
        );
    }

    public void PushButtonSound(){
		var sequence = DOTween.Sequence();
		sequence.Append(
			buttonSound.transform.DOScale (0.8f, 0.1f)
			.OnComplete(() => {
                if (soundManager != null) {
                    soundManager.GetComponent<SoundManager>().PlayEnglishVoice(number, spell);
                }
			})
		);
		sequence.Append(
			buttonSound.transform.DOScale (1.0f, 0.1f)
        );
	}

    public void PushButtonExample() {
        var sequence = DOTween.Sequence();
        sequence.Append(
            buttonExample.transform.DOScale(0.8f, 0.1f)
        );
        sequence.Append(
            buttonExample.transform.DOScale(1.0f, 0.1f)
            .OnComplete(() => {
                string str = spell;
                string escName = UnityWebRequest.EscapeURL(spell);
                Application.OpenURL("https://ejje.weblio.jp/sentence/content/" + escName);
            })
        );
    }

    public void PushButtonMemo() {
        var sequence = DOTween.Sequence();
		sequence.Append(
			buttonMemo.transform.DOScale (0.8f, 0.1f)
		);
		sequence.Append(
			buttonMemo.transform.DOScale (1.0f, 0.1f)
			.OnComplete(() => {
                canvasMemo.SetActive(true);
                string memoData = PlayerPrefs.GetString("MEMO" + number, "");
                string str = "";
                if (memoData == "") {
                    str = "メモ";
                    inputField.text = "";
                } else {
                    str = memoData;
                    inputField.text = memoData;
                }
                textMemo.GetComponent<Text>().text = str;
            })
		);
    }

    public void PushButtonCloseMemo() {
        canvasMemo.SetActive(false);
    }

    public void PushButtonWriteMemo() {
        inputField.ActivateInputField();
    }

    public void WritingMemo() {
        string str = inputField.text;
        textMemo.GetComponent<Text>().text = str;
        ChangeTextMemoSize();
    }

    public void SaveMemo() {
        string str = inputField.text;
        PlayerPrefs.SetString("MEMO" + number, str);
        PlayerPrefs.Save();
    }

    void ChangeTextMemoSize() {
        Text text = textMemo.GetComponent<Text>();
        float width = textMemo.GetComponent<RectTransform> ().sizeDelta.x;
		float height = text.preferredHeight;
        if (height <= 320.0f) {
            height = 320.0f;
        }
        textMemo.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }
}
