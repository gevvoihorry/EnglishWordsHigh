using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class JudgeManager : MonoBehaviour {

	public GameObject textSerif;
	public GameObject imageGage;
	public GameObject buttonOK;

	void Awake(){
		buttonOK.GetComponent<Button> ().interactable = false;
	}

	// Use this for initialization
	void Start () {
		MoveGage ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void MoveGage(){
		imageGage.transform.DOScaleX (1.0f, 5.0f).SetEase (Ease.Linear).OnComplete (() => {
			buttonOK.GetComponent<Button> ().interactable = true;
			textSerif.GetComponent<Text>().text = "集計が終わりました。";
		});
	}

	public void PushButtonOK(){
        SceneManager.LoadScene("ResultScene");
    }
}
