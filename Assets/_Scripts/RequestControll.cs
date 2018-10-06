using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RequestControll : MonoBehaviour {
	
	private List<string> needs = new List<string>();
	private bool hasCollected = false;
	private bool shouldDisappear = false;
	private float curAlpha = 1f;
	public float disappearTime = 1f;
	public int score = 200;
	private float yInit;
	
	// Use this for initialization
	void Start () {
		yInit = transform.position.y;
		Transform t = null;
		foreach (Transform vegeTrans in transform){
			if (vegeTrans.name == "kuang") t = vegeTrans;
		}
		if (t != null) t.parent = transform.parent;
		foreach (Transform vegeTrans in transform){
			needs.Add(vegeTrans.name);
		}
		if (needs.Count == 0) GameController.Instance.goalPosX = transform.position.x;
	}
	
	void PlayColletedAnimation()
	{
		shouldDisappear = true;
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag("Tractor") && !hasCollected) {
			if (needs.Count > 0) {
				hasCollected = VegetableController.Instance.DropVegetable(needs, transform.position, score);
				if (hasCollected && needs.Count == 1 && needs[0] == "package") {
					GameController.Instance.isPackageCollected = true;
				}
				if (hasCollected) {
					Invoke("PlayColletedAnimation", needs.Count * 0.2f + 1.5f);
				}
			} else {
				StartCoroutine(VegetableController.Instance.CollectAll(transform.position, score));
				hasCollected = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.name != "Goal") {
			float yDelta = -5;
			float xDelta = 0.1f;
			float t = 0.5f;
			
			transform.position = new Vector3(transform.position.x, yInit + Mathf.Cos(Time.time / t) * yDelta, transform.position.z);
	 		float scaleDelta = -Mathf.Cos(Time.time / t * 2) * xDelta + 1.15f;
			transform.localScale = new Vector3(scaleDelta, scaleDelta, scaleDelta);
		}
		if (shouldDisappear)
		{
			if (curAlpha > 0) {
				curAlpha -= Time.deltaTime / disappearTime;
				foreach (Transform vegeTrans in transform){
						vegeTrans.renderer.material.color = new Color(0.5f, 0.5f, 0.5f, curAlpha);
				}
				renderer.material.color = new Color(0.5f, 0.5f, 0.5f, curAlpha);
			}
		}
	}
}
