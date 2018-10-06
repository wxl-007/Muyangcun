using UnityEngine;
using System.Collections;

public class BackGroundRoll : MonoBehaviour {
	private Transform tracTrans;
	public Vector2 rollSpeed = new Vector2(0.0001f, 0.00001f);
	public float autoRollSpeedX = 0f;
	private float autoRollDis;
	// Use this for initialization
	void Start () {
		tracTrans = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		autoRollDis += autoRollSpeedX * Time.deltaTime;
		gameObject.renderer.material.mainTextureOffset = new Vector2(tracTrans.position.x * rollSpeed.x + autoRollDis, tracTrans.position.y * rollSpeed.y);
	}
}
