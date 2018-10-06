using UnityEngine;
using System.Collections;

public class LoadingControll : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("LoadScene", 1.5f);
	}
	
	void LoadScene () {
		Debug.Log("load:" + GlobalManager.levelToLoad);
		Application.LoadLevel(GlobalManager.levelToLoad);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
