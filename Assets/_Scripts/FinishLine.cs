using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour {
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Tractor")
		{
			GameController.Instance.LockInput();
		}
	}
	
}