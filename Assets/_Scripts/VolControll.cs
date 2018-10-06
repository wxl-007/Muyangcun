using UnityEngine;
using System.Collections;

public class VolControll : MonoBehaviour {
	public UIButton btnOn, btnOff;
	// Use this for initialization
	void Start () {
		Refresh ();
	}
	
	void Refresh () {
		btnOn.Hide(!GlobalManager.isSoundOn);
		btnOff.Hide(GlobalManager.isSoundOn);
	}
	
	void Toggle () {
		GlobalManager.ToggleSound();
		AudioListener.volume = GlobalManager.isSoundOn ? 1f : 0f;
		Refresh();
	}
}
