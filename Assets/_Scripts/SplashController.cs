using UnityEngine;
using System.Collections;

public class SplashController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(Splash());
	}
	
	IEnumerator Splash () {
		yield return new WaitForSeconds(1.5f);
		Application.LoadLevel("Menus");
	}
}
