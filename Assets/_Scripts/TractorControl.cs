using UnityEngine;
using System.Collections;

public class TractorControl : MonoBehaviour 
{
	public GameObject RightWheel;
	public GameObject LeftWheel;
	public GameObject BackWheel;
	public GameObject FrontCar;
	public GameObject BackCar;
	public GameObject BackCar2;
	public PackedSprite Animal;
	public Vector3 shiftCentreFront;
	public Vector3 shiftCentreBack;
	
	public AnimationCurve power = AnimationCurve.Linear(0f, 5000f, 8000f, 0f);
	
	public float maxRPMIncrease = 500f;
	public float maxRPMDecrease = 1000f;
	public float idleRPM = 500f;
	public float torque = 150f;
	
	float motorRPM;
	
	float CalcPower(float rpm)
	{
		float val = power.Evaluate(rpm) * torque / 5000f;
		return val;
	}
	
	public class WheelData
	{
		public Transform transform;
		public GameObject go;
		public WheelCollider col;
		public float rotation = 0f;
		public bool moto;
	}
	
	void Start()
	{
		FrontCar.rigidbody.centerOfMass += shiftCentreFront;
		BackCar.rigidbody.centerOfMass -= shiftCentreBack;
		
	}

	float lastt = 1;
	void FixedUpdate ()
	{
		FrontCar.transform.position = new Vector3(FrontCar.transform.position.x, FrontCar.transform.position.y, 0.1f);
		BackCar.transform.position = new Vector3(BackCar.transform.position.x, BackCar.transform.position.y, 0f);
		if (null != BackCar2) BackCar2.transform.position = new Vector3(BackCar2.transform.position.x, BackCar2.transform.position.y, 0f);
		Move();
		
		float t = FrontCar.rigidbody.velocity.x * 0.015f;
		if (Mathf.Abs(t) < 1f) {
			t = 0;
			StopRun();
		}
		if (t < 0) t *= 0.7f;
		if (t > -1.5f && t < 3) {
			if (t < 0) t = -1.5f;
			if (t > 0) t = 3;
		}
		if (t < 0) {
			if (lastt >= 0) {
				Animal.PlayAnimInReverse(0);
			}
			Animal.SetFramerate(-t);
		}
		if (t > 0) {
			if (lastt <= 0) {
				Animal.PlayAnim(0);
			}
			Animal.SetFramerate(t);
		}
		lastt = t;
		
	}
	private float holdDuring = 1.5f, endDuring = 2f;
	private float lastStartTime = 0f, lastEndTime = 0f;
	private int lastAngle = -1;
	void Move()
	{
		float move = InputController.Instance.move;
		float angle = InputController.Instance.angle;
		
		//if (move == 0f) StopRun();
		//else Run();
		if (move < 0) move *= 0.3f;
		
		// The angle only be affacted for limit holdDuring time.
		if (Time.time > lastStartTime + holdDuring){
			if (lastEndTime <= lastStartTime) lastEndTime = Time.time;
		}else if (lastEndTime <= lastStartTime){
			if (angle > 0) angle *= 1700000f;
			else angle *= 600000f;
			FrontCar.rigidbody.AddTorque(0f, 0f, angle);
		}
		
		if (Mathf.Abs(angle) < 0.3f) lastStartTime = Time.time;
		
		if ((lastStartTime < lastEndTime && Time.time > lastEndTime + endDuring) || angle * lastAngle < 0){
			lastStartTime = Time.time;
			lastAngle = angle > 0 ? 1 : -1;
		}
		
		FrontCar.rigidbody.AddForce(FrontCar.transform.right * move * 1600f);
		BackCar.rigidbody.AddForce(BackCar.transform.right * move * 1600f);
	}
	
	public void Run()
	{
		Animal.UnpauseAnim();
		
	}
	
	public void StopRun()
	{
		Animal.StopAnim();
	}
}