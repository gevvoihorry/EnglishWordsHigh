using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MessageManager : MonoBehaviour {

    public GameObject messageView;
    public GameObject imageWindow;
    public GameObject textMessage;
    public GameObject buttonMessageOK;
    public GameObject buttonMessageYes;
    public GameObject buttonMessageNo;
    public GameObject imageLineV;

    private int firstPlay;

    void Awake() {
        DisplayTutorial();
        firstPlay = PlayerPrefs.GetInt("FIRST_PLAY", 0);
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    void DisplayTutorial() {
        List<string> messageList = new List<string>();
        string saveKey = "";
        if (SceneManager.GetActiveScene().name == "TitleScene") {
            saveKey = "TITLE_TUTORIAL";
            messageList.Add("「勉強」ボタンを押して、レッスンを開始しましょう。");
        } else if (SceneManager.GetActiveScene().name == "SelectScene") {
            saveKey = "SELECT_TUTORIAL";
            messageList.Add("各レッスン内にある小テストに合格（80点以上）すると、次のレッスンへ進めるようになります。");
        } else if (SceneManager.GetActiveScene().name == "LessonSelectScene") {
            saveKey = "LESSON_SELECT_TUTORIAL";
            messageList.Add("「勉強スタート」ボタンを押してください。");
            messageList.Add("小テストは「読みテスト」と「書きテスト」に分かれています。それぞれに合格（80点以上）して、次のレッスンへ進みましょう。");
        } else if (SceneManager.GetActiveScene().name == "StudyScene") {
            saveKey = "STUDY_TUTORIAL";
            messageList.Add("「音声」ボタンを押すと、実際の発音を聞くことが出来ます。（マナーモードOFF）");
            messageList.Add("「例文」ボタンを押すと、単語の使い方を検索することが出来ます。");
            messageList.Add("何か気になることがあれば、「メモ」ボタンを押して、書き残してください。");
        }
        int tutorialCount = PlayerPrefs.GetInt(saveKey, 0);
        if (tutorialCount < messageList.Count) {
            DisplayMessage(messageList[tutorialCount]);
            tutorialCount++;
            PlayerPrefs.SetInt(saveKey, tutorialCount);
            PlayerPrefs.Save();
        }

    }

    public void DisplayMessage(string str) {
        textMessage.GetComponent<Text>().text = str;
        ChangeWindowSize();
        buttonMessageYes.SetActive(false);
        buttonMessageNo.SetActive(false);
        imageLineV.SetActive(false);
        buttonMessageOK.SetActive(true);
        messageView.SetActive(true);
        ChangeWindowSize();
    }

    public void DisplayQuestion(string str) {
        textMessage.GetComponent<Text>().text = str;
        ChangeWindowSize();
        buttonMessageYes.SetActive(true);
        buttonMessageNo.SetActive(true);
        imageLineV.SetActive(true);
        buttonMessageOK.SetActive(false);
        messageView.SetActive(true);
        ChangeWindowSize();
    }

    void ChangeWindowSize() {
        Text text = textMessage.GetComponent<Text>();
        float width = imageWindow.GetComponent<RectTransform> ().sizeDelta.x;
		float height = text.preferredHeight;
        height += 220.0f;
        imageWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }

    public void PushButtonMessageOK() {
        if (firstPlay == 0) {
            PlayerPrefs.SetInt("FIRST_PLAY", 1);
            PlayerPrefs.Save();
        }
        messageView.SetActive(false);
    }
}
