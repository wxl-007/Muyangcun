using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	public bool isPause;

	// Use this for initialization
	void Start () {
		Debug.Log("Pause Start!");
		UIBtnChangePanel btn = gameObject.GetComponent<UIBtnChangePanel>();
		if (isPause) {
			btn.AddValueChangedDelegate(DoPause);
		} else {
			btn.AddValueChangedDelegate(DoResume);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	public void DoPause (IUIObject obj) {
		GameController.Instance.Pause ();
	}
	
	public void DoResume (IUIObject obj) {
		GameController.Instance.Resume();
	}
}
