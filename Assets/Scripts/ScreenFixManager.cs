using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenFixManager : MonoBehaviour {

	private void Awake()
	{
		if(1.0f*Screen.width/Screen.height<9/16.0f){
			GetComponent<CanvasScaler>().matchWidthOrHeight=0.0f;
		}else{
			GetComponent<CanvasScaler>().matchWidthOrHeight=1.0f;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
