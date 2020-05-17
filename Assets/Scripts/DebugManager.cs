using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour {

        private int[] lessonCount = { 33, 26, 27, 35, 39 };

    // Start is called before the first frame update
    void Start() {
        PlayerPrefs.DeleteAll();

        //PlayerPrefs.SetInt("FIRST_PLAY", 1);

        //for (int i = 0; i < lessonCount.Length; i++) {
        //    for (int n = 0; n < lessonCount[i]; n++) {
        //        PlayerPrefs.SetInt("READ_TEST_SCORE" + i + "_" + n, 80);
        //        PlayerPrefs.SetInt("WRITE_TEST_SCORE" + i + "_" + n, 90); 
        //    }
        //}

        //for (int i = 0; i < lessonCount; i++) {
        //    if (i < 20) {
        //        PlayerPrefs.SetInt("READ_TEST_SCORE" + i, 80);
        //        PlayerPrefs.SetInt("WRITE_TEST_SCORE" + i, 80);
        //    }
        //}

        //PlayerPrefs.SetInt("TEST_FLG", 1);

        PlayerPrefs.Save();

        SceneManager.LoadScene("TitleScene");
        //SceneManager.LoadScene("SelectScene");
        //SceneManager.LoadScene("TestScene");
        //SceneManager.LoadScene("StudyScene");
    }

    // Update is called once per frame
    void Update() {
        
    }
}
