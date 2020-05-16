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
			string text = "【高校入試に出る英単語】\n iPhone : https://apple.co/2X5ieUx \n Android : https://bit.ly/2y4A0P0 \n #HorryApps";
			string url = "";
			string path = Application.streamingAssetsPath + "/SNS.png";
			SocialConnector.Share(text, url, path);
		}

	}

}
