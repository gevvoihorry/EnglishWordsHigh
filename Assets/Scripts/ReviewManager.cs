using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReviewManager : MonoBehaviour {

	public GameObject[] imageStar = new GameObject[5];
	public GameObject textMessage;
	public GameObject textCloseMessage;
	public GameObject textYes;
	public GameObject textNo;
	public GameObject buttonMessageClose;
	public GameObject imageChoiceWindow;

	public Sprite[] starPicture = new Sprite[2];

	private string appName = "英検5級の英単語";

	private int nextFlg = 0;

	// Use this for initialization
	void Start () {
		RefreshText ();
		ChengeYesNo ();
	}

	// Update is called once per frame
	void Update () {

	}

	void ChengeYesNo(){
		textYes.GetComponent<Text>().text = "書いてあげる";
		textNo.GetComponent<Text>().text = "あとで";
	}

	public void Refresh(){ //初期化。もしかしたらこの処理いらんかもしれん。どのみち、パスした人にしか再度表示しないから。
		nextFlg = 0;
		RefreshText ();
		RefreshStar ();
		imageChoiceWindow.SetActive (false);
	}

	void RefreshText(){
		if (nextFlg == 0) {
			textMessage.GetComponent<Text> ().text = "\"" + appName + "\"はいかがですか？星をタップして評価してください。";
			textCloseMessage.GetComponent<Text>().text = "今はしない";
		} else {
            //textMessage.GetComponent<Text> ().text = "ありがとうございます！！もしよろしければ、レビューを書いていただけないでしょうか？";
            //textMessage.GetComponent<Text>().text = "ありがとうございます！！「いいね」の一言でも結構ですので、レビューを書いていただけないでしょうか？";
            textMessage.GetComponent<Text>().text = "ありがとうございます！！よろしければ「いいね」の一言でも結構ですので、応援レビューいただけると嬉しいです！！";
        }
	}

	void RefreshStar(){
		for (int i = 0; i < 5; i++) {
			imageStar [i].GetComponent<Image> ().sprite = starPicture[0];
		}
	}

	public void PushButtonMessageClose(){
        //画面遷移の条件は各アプリごとに異なるので、あえてAwakeではなく、ここでデータを呼び出している。
		int testFlg = PlayerPrefs.GetInt ("TEST_FLG", 0);
		if (testFlg < 2) {
            SceneManager.LoadScene("LessonSelectScene");
        } else {
            SceneManager.LoadScene("TitleScene");
        }
	}

	public void PushButtonStar(int num){
		for (int i = 0; i < 5; i++) {
			if (i <= num) {
				imageStar [i].GetComponent<Image> ().sprite = starPicture[1];
			}
		}
		if (num > 2) { //星４以上になってる
			nextFlg++;
			Invoke ("RefreshText", 0.5f);
			Invoke ("ChangeWindow", 0.5f);
		} else {
			//Invoke ("ReviewNo", 0.5f);
            Invoke ("PushButtonMessageClose", 0.5f);
		}
	}

	void ChangeWindow(){
		imageChoiceWindow.SetActive (true);
	}

	public void ReviewYes(){
		if (Application.platform == RuntimePlatform.Android) {
			Application.OpenURL ("https://play.google.com/store/apps/details?id=com.gevvoihorry.EnglishTestLevelSemiTwo");
		} else if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Application.OpenURL ("https://apps.apple.com/jp/app/id1513455662?mt=8&action=write-review");
		} else {
			Application.OpenURL ("https://apps.apple.com/jp/app/id1513455662?mt=8&action=write-review");
		}
		PlayerPrefs.SetInt ("REVIEW", 999);
		PlayerPrefs.Save ();
		PushButtonMessageClose ();
	}

	public void ReviewNo(){
		PlayerPrefs.SetInt ("REVIEW", 999);
		PlayerPrefs.Save ();
		PushButtonMessageClose ();
	}
}
