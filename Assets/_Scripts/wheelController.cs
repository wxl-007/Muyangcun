using UnityEngine;
using System.Collections;

public class wheelController : MonoBehaviour {
	
	private LayerMask groundlayer = 1 + (1 << 11) + (1 << 12);
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnCollisionStay(Collision collisionInfo) {
		float move = InputController.Instance.move;
        foreach (ContactPoint contact in collisionInfo.contacts) {
            if ( ((1 << contact.otherCollider.gameObject.layer) & groundlayer) > 0){
				transform.parent.rigidbody.AddForce(transform.parent.right * move * 15000);
				rigidbody.AddForce(transform.parent.right * move * 3000);
				GameController.Instance.curGroundWheelCount ++;
			}
        }
    }
}
