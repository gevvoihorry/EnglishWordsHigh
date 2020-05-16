using UnityEngine;
using System.Collections;

namespace SocialConnector{

	public class SocialManager : MonoBehaviour {

        static public SocialManager instance;

        void Awake() {
            if (instance == null) {
			    instance = this;
			    DontDestroyOnLoad (gameObject);
		    } else {
			    Destroy (gameObject);
		    }
        }

        // Use this for initialization
        void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		public void PushSocial(){
            Debug.Log("social");
			string text = "【英検準2級の英単語】\n iPhone : https://apple.co/3cttNLz \n Android : https://bit.ly/3dJhvin \n #HorryApps";
			string url = "";
			string path = Application.streamingAssetsPath + "/SNS.png";
			SocialConnector.Share(text, url, path);
		}

	}

}
