using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class Vegetable : MonoBehaviour {
	/** vegetableState: 0 -- in the air;
	 *  				1 -- on the back car;
	 * 					2 -- on the ground;
	 **/
	
	public float holdZ = 0.1f;
	public int vegetableState;
	
	private float yDelta = -5;
	private float xDelta = 0.05f;
	private float t = 0.3f;
	private float yInit;
	private bool isFalling;
	
	void Start () {
		vegetableState = 0;
		yInit = transform.position.y;
	}
	
	void Update () {
    	if (vegetableState == 0) {
			transform.position = new Vector3(transform.position.x, yInit + Mathf.Cos(Time.time / t) * yDelta, transform.position.z);
 			float scaleDelta = -Mathf.Cos(Time.time / t * 2) * xDelta + 1.15f;
			transform.localScale = new Vector3(scaleDelta, scaleDelta, scaleDelta);
		}
	}
	
	void FixedUpdate () {
		transform.position = new Vector3(transform.position.x, transform.position.y, holdZ);
	}
	
	// triggerd when hit by the tractor.
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag("Tractor") && vegetableState == 0) {
			SoundController.Instance.Play("vegetable_collect");
			vegetableState = 1;
			gameObject.layer = 15;
			transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
			VegetableController.Instance.CollectVegetable(gameObject);
		}
	}
	
	// triggered when vegetable hit the ground.
	void OnCollisionEnter (Collision theCollision) {
		
		if (theCollision.gameObject.layer == 11 && vegetableState != 2) {
			SoundController.Instance.Play("vegetable_drop");
			vegetableState = 2;
			VegetableController.Instance.DropVegetable(gameObject);
			gameObject.layer = 14;
		}
	}
	
	void DropToBasket () {
		SoundController.Instance.Play("vegetable_to_basket");
	}
}