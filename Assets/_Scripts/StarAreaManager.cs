using UnityEngine;
using System.Collections;

public class StarAreaManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// triggerd when the tractor inner.
	void OnTriggerEnter (Collider other) {
		if(other.gameObject.CompareTag("Tractor")){
			InputController.Instance.setInStarAera(true);
		}
	}
	
	//
	void OnTriggerExit (Collider other) {
		if(other.gameObject.CompareTag("Tractor")){
			InputController.Instance.setInStarAera(false);
		}
	}
}
