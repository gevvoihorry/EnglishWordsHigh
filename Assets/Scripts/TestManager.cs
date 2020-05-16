using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class TestManager : MonoBehaviour {

    public GameObject interManager;
    public GameObject englishManager;
    public GameObject soundManager;

    public GameObject textCount;
    public GameObject textTimeLimit;
    public GameObject textQuestion;
    public GameObject buttonSoundQuestion;
    public GameObject[] buttonAnswer = new GameObject[4];
    public GameObject[] buttonSoundAnswer = new GameObject[4];
    public GameObject imageYes;
    public GameObject imageMiniYes;
    public GameObject imageNo;
    public GameObject buttonBack;

    private int[] lessonCount = { 29, 43, 31 };

    private int timeLimit = 20;
    private float span = 1.0f;
    private float delta = 0;

    private int[] ary = { 0, 1, 2, 3 };
    private string[] answerArray = new string[4]; //回答を４択で入れるための配列
    private int[] problemArray = new int[20]; //本試験の問題をランダムで格納するための配列

    private int count = 0;
    private int baseCount = 0;
    private int maxCount = 0;
    private int number = 0;

    private int testFlg = 0;
    private int select = 0;
    private int lesson = 0;

    private int score = 0;
    private int missCount = 0; //間違えた問題を紐付けるための数値

    private int[] answerSoundNumber = new int[4];

    private int vibFlg = 0; //バイブレーションをONにしてるかどうか。

    private int wordCount; //全部の単語数 1030

    void Awake() {
        interManager = GameObject.Find("InterManager");
        englishManager = GameObject.Find("EnglishManager");
        soundManager = GameObject.Find("SoundManager");

        select = PlayerPrefs.GetInt("SELECT", 0);
        lesson = PlayerPrefs.GetInt("LESSON", 0);
        testFlg = PlayerPrefs.GetInt("TEST_FLG", 0);
        vibFlg = PlayerPrefs.GetInt("VIB_FLG", 0);

        for (int i = 0; i < lessonCount.Length; i++) {
            wordCount += (lessonCount[i] * 10);
        }
    }

    // Start is called before the first frame update
    void Start() {
        DisplaySoundButtons();
        if (testFlg == 2 || testFlg == 3) {
            CreateProblemArray();
        }
        SetBaseCount();
        SetMaxCount();
        RefreshScene();
    }

    // Update is called once per frame
    void Update() {
        if (timeLimit > 0) {
            this.delta += Time.deltaTime;
            if (delta > span) {
                delta = 0;
                timeLimit--;
                DisplayTextTimeLimit();
            }
            if (timeLimit == 0) {
                PushButtonAnswer(99);
            }
        }
    }

    void DisplaySoundButtons() {
        if (testFlg == 0 || testFlg == 2) {
            for (int i = 0; i < buttonSoundAnswer.Length; i++) {
                buttonSoundAnswer[i].SetActive(false);
            }
        } else {
            buttonSoundQuestion.SetActive(false);
        }
    }

    void SetMaxCount() {
        if (testFlg == 0 || testFlg == 1) {
            maxCount = 9;
        } else {
            maxCount = 19;
        }
    }

    void RefreshScene() {
        if (count > maxCount) {
            PlayerPrefs.SetInt("SCORE", score);
            PlayerPrefs.Save();
            SceneManager.LoadScene("JudgeScene");
            return;
        }
        for (int i = 0; i < 4; i++) {
            buttonAnswer [i].GetComponent<Button> ().interactable = true;
            buttonAnswer[i].GetComponent<Image>().color = Color.white;
        }
        timeLimit = 20;
		delta = 0;
		DisplayTextTimeLimit ();
        imageYes.SetActive(false);
        imageMiniYes.SetActive(false);
        imageNo.SetActive(false);
        SetNumber();
        DisplayTextCount();
        DisplayTextQuestion();
        CreateAnswerArray();
        ShuffleAnswer();
    }

    void SetBaseCount() {
        for (int i = 0; i < lessonCount.Length; i++) {
            if (i < select) {
                baseCount += (lessonCount[i] * 10);
            }
        }
    }

    void SetNumber() {
        if (testFlg == 0 || testFlg == 1) {
            number = baseCount + (lesson * 10) + count;
        } else {
            number = problemArray[count];
        }
    }

    void DisplayTextCount() {
        textCount.GetComponent<Text>().text = (count + 1) + " / " + (maxCount + 1);
    }

    void DisplayTextTimeLimit() {
        textTimeLimit.GetComponent<Text> ().text = timeLimit.ToString ();
    }

    void DisplayTextQuestion() {
        string str = "";
        if (testFlg == 0 || testFlg == 2) {
            str = englishManager.GetComponent<EnglishManager>().ReturnEnglish(number);
        } else {
            str = englishManager.GetComponent<EnglishManager>().ReturnMean(number);
        }
        textQuestion.GetComponent<Text>().text = str;
        ChangeTextSize(textQuestion, 600, 100);
    }

    void CreateProblemArray() {
        int problemCount = 0;
        while (problemCount < 20) {
            int ram = UnityEngine.Random.Range(0, wordCount);
            if (0 <= Array.IndexOf(problemArray, ram)) {
                //in
                continue;
            } else {
                problemArray[problemCount] = ram;
                problemCount++;
            }
        }
    }

    void CreateAnswerArray() {
        int createCount = 1;
        if (testFlg == 0 || testFlg == 2) {
            answerArray[0] = englishManager.GetComponent<EnglishManager>().ReturnMean(number);
        } else {
            answerArray[0] = englishManager.GetComponent<EnglishManager>().ReturnEnglish(number);
        }
        answerSoundNumber[0] = number;
        while (createCount < 4) {
            int ram = 0;
            if (testFlg < 2) {
                int num0 = baseCount + (lesson * 10);
                int num1 = baseCount + (lesson * 10) + 10;
                ram = UnityEngine.Random.Range(num0, num1);
            } else {
                ram = UnityEngine.Random.Range(0, wordCount);
            }

            string str = "";
            if (testFlg == 0 || testFlg == 2) {
                str = englishManager.GetComponent<EnglishManager> ().ReturnMean (ram);
            } else {
                str = englishManager.GetComponent<EnglishManager>().ReturnEnglish(ram);
            }
            if (0 <= Array.IndexOf(answerArray, str)) {
                //in
                continue;
            }

            string str2 = "";
            if (testFlg == 0 || testFlg == 2) {
                str2 = englishManager.GetComponent<EnglishManager>().ReturnEnglish(ram);
            } else {
                str2 = englishManager.GetComponent<EnglishManager> ().ReturnMean (ram);
            }
            string question = textQuestion.GetComponent<Text>().text;
            if (str2 == question) {
                continue;
            }

            answerArray [createCount] = str;
            answerSoundNumber[createCount] = ram;
            createCount++;
        }
    }

    void ShuffleAnswer(){
        //Fisher-Yatesアルゴリズムでシャッフルする
        System.Random rng = new System.Random();
        int n = ary.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int tmp = ary[k];
            ary[k] = ary[n];
            ary[n] = tmp;
        }

        for (int i = 0; i < 4; i++) {
            buttonAnswer [i].GetComponentInChildren<Text> ().text = answerArray [ary[i]];
            GameObject textAnswer = buttonAnswer[i].transform.Find("Text").gameObject;
            if (testFlg == 0 || testFlg == 2) {
                ChangeTextSize(textAnswer, 430, 40);
            } else {
                ChangeTextSize(textAnswer, 430, 45);
            }
        }
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

    public void PushButtonAnswer(int num) {
        if (num != 99 && ary[num] == 0) {
            if (vibFlg == 1 && Application.platform == RuntimePlatform.IPhonePlayer) {
                #if UNITY_IOS
                int soundId = 1520;
                UniIosAudioService.PlaySystemSound(soundId);
                #endif
            }
            imageYes.SetActive(true);
            score++;
        } else {
            imageNo.SetActive (true);

            //間違えた問題の番号を格納する。
            PlayerPrefs.SetInt ("MISS_NUMBER" + missCount, number);
            PlayerPrefs.Save ();

            missCount++;
        }

        DisplayImageMiniYes();

        //ボタンを効かないようにする
        for (int i = 0; i < 4; i++) {
            buttonAnswer [i].GetComponent<Button> ().interactable = false;
        }
        if (num != 99) {
            buttonAnswer[num].GetComponent<Image>().color = new Color(200.0f/255.0f, 200.0f/255.0f, 200.0f/255.0f, 255.0f/255.0f);
        }

        count++;

        Invoke ("RefreshScene", 0.5f);
    }

    void DisplayImageMiniYes() {
        for (int i = 0; i < ary.Length; i++) {
            if (ary[i] == 0) {
                float positionY = buttonAnswer[i].transform.localPosition.y;
                imageMiniYes.transform.localPosition = new Vector3(imageMiniYes.transform.localPosition.x, positionY, 0);
                imageMiniYes.SetActive(true);
            }
        }
    }

    public void PushButtonSoundQuestion() {
        string spell = textQuestion.GetComponent<Text>().text;
        var sequence = DOTween.Sequence();
		sequence.Append(
			buttonSoundQuestion.transform.DOScale (0.8f, 0.1f)
			.OnComplete(() => {
                if (soundManager != null) {
                    soundManager.GetComponent<SoundManager>().PlayEnglishVoice(number, spell);
                }
			})
		);
		sequence.Append(
			buttonSoundQuestion.transform.DOScale (1.0f, 0.1f)
        );
    }

    public void PushButtonSoundAnswer(int num) {
        string spell = buttonAnswer[num].GetComponentInChildren<Text>().text;
        int soundNumber = answerSoundNumber[ary[num]];
        var sequence = DOTween.Sequence();
		sequence.Append(
			buttonSoundAnswer[num].transform.DOScale (0.8f, 0.1f)
			.OnComplete(() => {
                if (soundManager != null) {
                    soundManager.GetComponent<SoundManager>().PlayEnglishVoice(soundNumber, spell);
                }
			})
		);
		sequence.Append(
			buttonSoundAnswer[num].transform.DOScale (1.0f, 0.1f)
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
                int ram = UnityEngine.Random.Range(0, 3);
                if (ram == 0 && interManager != null) {
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
}
