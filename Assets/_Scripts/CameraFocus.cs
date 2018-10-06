using UnityEngine;
using System.Collections;

public class CameraFocus : MonoBehaviour 
{
	public Transform FocusTarget;
	public float minY;
	
	private int camOrtSize = 384;
	
	void Start ()
	{
		float ration = (float) Screen.width / (float) Screen.height;
		if (Mathf.Abs(ration - 1.5f) < 0.01f)
			camOrtSize = 320;
		else
			camOrtSize = 384;
	}
	
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(-100000f, minY, 0f), new Vector3(100000f, minY, 0f));
	}
	
	void Update () 
	{
		// focus to the target position
		Vector3 newPos = FocusTarget.position;
		newPos.z = transform.position.z;
		newPos.y = Mathf.Max(minY, newPos.y);
		transform.position = newPos;
		camera.orthographicSize = camOrtSize - 10 + Mathf.Abs(FocusTarget.rigidbody.velocity.x) * 0.2f;
	}
}