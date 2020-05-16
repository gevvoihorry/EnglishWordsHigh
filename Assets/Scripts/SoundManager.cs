using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    static public SoundManager instance;

    private AudioSource audioSource;

    public AudioClip buttonSE;
    public AudioClip yesSE;
	public AudioClip noSE;
    public AudioClip resultSE;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        audioSource = gameObject.GetComponent<AudioSource> ();
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void PlayEnglishVoice(int number , string spell) {
        int fileNum = number / 10;
        string fileName = "lesson" + fileNum;
        AudioClip testSE = Resources.Load("Sounds/" + fileName + "/" + spell) as AudioClip;
        if (testSE == null) {
			return;
		}
        audioSource.PlayOneShot (testSE);
    }

    public void SoundButton() {
        audioSource.PlayOneShot(buttonSE);
    }

    public void SoundYes(){
		audioSource.PlayOneShot (yesSE);
	}

    public void SoundNo(){
		audioSource.PlayOneShot (noSE);
	}

    public void SoundResult(){
		audioSource.PlayOneShot (resultSE);
	}
}
