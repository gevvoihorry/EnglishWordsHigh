using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnglishManager : MonoBehaviour {

    static public EnglishManager instance;

    private string[] m_scenarios;
    private List<string> englishList = new List<string>();
    private List<string> meanList = new List<string>();

    void Awake() {
        if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
        SetList();
    }

    // Start is called before the first frame update
    void Start() {

        //単語の被りをチェック
        //for (int i = 0; i < meanList.Count; i++) {
        //    string str = meanList[i];
        //    for (int n = 0; n < meanList.Count; n++) {
        //        string str2 = meanList[n];
        //        if (i != n && str == str2) {
        //            Debug.Log(i + ":" + n);
        //            Debug.Log(str + ":" + str2);
        //        }
        //    }
        //}
    }

    // Update is called once per frame
    void Update() {
        
    }

    void SetList() {
        ReadText("english");
        for (int i = 0; i < m_scenarios.Length; i++) {
            string str = m_scenarios[i];
            if (str.Contains("//") == false) {
                englishList.Add(str);
            }
        }

        ReadText("mean");
        for (int i = 0; i < m_scenarios.Length; i++) {
            string str = m_scenarios[i];
            if (str.Contains("//") == false) {
                meanList.Add(str);
            }
        }
    }

    void ReadText(string fileName) {
        var scenarioText = Resources.Load<TextAsset>("Texts/" + fileName);
        if (scenarioText == null) {
            return;
        }

        m_scenarios = scenarioText.text.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        Resources.UnloadAsset(scenarioText);
    }

    public string ReturnEnglish(int num) {
        string str = englishList[num];
        return str;
    }

    public string ReturnMean(int num) {
        string str = meanList[num];
        return str;
    }
}
