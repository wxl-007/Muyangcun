using UnityEngine;
using System.Collections;

public class CarTrigger : MonoBehaviour {
	public LayerMask ground = (1 << 11);
	private bool hasBroken = false;
	void OnTriggerEnter (Collider other) {
		//Debug.Log(other.gameObject.ToString() + other.gameObject.layer);
		if (((1 << other.gameObject.layer) & ground) != 0 && GameController.Instance.isInputAllowed) {
			GameController.Instance.Replay();
			GameController.Instance.LockInput();
		//	GameController.Instance.doReplay = true;
		}
		if (((1 << other.gameObject.layer) & ground) != 0 && !hasBroken) {
			hasBroken = true;
			//SoundController.Instance.Play("tractor_drop_broken");
		}
	}
	
	void Update () {
		if (transform.parent.localEulerAngles.z > 135 && transform.parent.localEulerAngles.z < 225 && GameController.Instance.isInputAllowed){
			GameController.Instance.Replay();
			GameController.Instance.LockInput();
			
		}
	}
}