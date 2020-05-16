using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TestReadyManager : MonoBehaviour {

	public GameObject text0;
	public GameObject text1;

	void Awake(){

		Vector3 pos = text0.transform.localPosition;
		pos.x = -650f;
		text0.transform.localPosition = pos;

		Vector3 pos2 = text1.transform.localPosition;
		pos2.x = 650f;
		text1.transform.localPosition = pos2;

		int testFlg = PlayerPrefs.GetInt ("TEST_FLG", 0);
		if (testFlg == 0 || testFlg == 1) {
			text0.GetComponent<Text> ().text = "小テスト";
		} else {
			text0.GetComponent<Text> ().text = "実力テスト";
		}

		PlayerPrefs.SetInt ("SCORE", 0);
		PlayerPrefs.Save ();
	}

	// Use this for initialization
	void Start () {
		var sequence = DOTween.Sequence();
		sequence.Append(
			text0.transform.DOLocalMoveX (0, 1.0f)
		);
		// Join()で追加する
		sequence.Join(
			text1.transform.DOLocalMoveX (0, 1.0f)
		);
		sequence.Append(
			text0.transform.DOScale(
				new Vector3(1.0f, 1.0f),     // 終了時点のScale
				0.5f                        // アニメーション時間
			).OnComplete(() => {
				SceneManager.LoadScene("TestScene");
			})
		);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
