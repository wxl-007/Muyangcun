using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
	public float move;
	public float angle;
	public float borderline;
	
	public float sliderwidth;
	public Camera guiCamera;
	
	private int curFingerID;
	private Ray ray;
	private RaycastHit hit;
	private Vector3 sliderPos, fingerPos;
	private float minRayY,maxRayY;
	
	private bool mIsInStarAera;
	
	/// <summary>
	/// check is the car in the starAera
	/// </summary>
	/// <returns>
	/// inner or outer
	/// </returns>
	public bool isInStarAera(){
		return mIsInStarAera;	
	}
	
	/// <summary>
	/// set the car inner or outer
	/// </summary>
	/// <param name="inner">
	/// inner or outer
	/// </param>
	public void setInStarAera(bool inner){
		this.mIsInStarAera = inner;
	}
	
	// it aims to determine which side of the screen the user touched
	private static InputController instance;
	public static InputController Instance {
		get 
		{
			if (instance == null)
			{
				instance = (InputController)FindObjectOfType(typeof(InputController));
			}
			if (!instance)
			{
				Debug.LogError("InputController could not find himself!");
			}
			return instance;
		}
	}
	
	
	// Use this for initialization
	void Start () {
		move = 0;
		
		borderline = Screen.width*0.5f;
		
		curFingerID = -1;
		ray = guiCamera.ScreenPointToRay(new Vector2(Screen.width*0.5f, Screen.height*0.01f));
		if (Physics.Raycast(ray, out hit, 2000) && hit.collider.tag == "_ControllPanel")
		{
			minRayY = hit.point.y;
			maxRayY = -hit.point.y;
		}
	}
	
	// Update is called once per frame
	void Update () {
      
		move = angle = 0f;
		if (GameController.Instance.isInputAllowed) {


            if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
            {
				bool tmpleft = false, tmpright = false;
				foreach (Touch touch in Input.touches) {
					if (touch.position.x < borderline) {
						tmpleft = true;
					} else if (touch.position.x > Screen.width - borderline) {
						tmpright = true;
					}
				}
				if (tmpleft) {
					if (tmpright) move = 0f;
					else move = -1f;
				} else if (tmpright) move = 1f;
				
			}else{
					move = Input.GetAxisRaw("Horizontal");
					angle = Input.GetAxisRaw("Vertical");
			}
			
			if(mIsInStarAera){
					//car in the staraera that means it can not move left 	
					move = move < 0 ? 0 : move;
			}
			
			#if (UNITY_EDITOR)
			if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
			if (Input.mousePosition.x > borderline)
			{
				ray = guiCamera.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit, 2000) && hit.collider.tag == "_ControllPanel")
				{
					fingerPos = hit.point;
					if (Input.GetMouseButtonDown(0))
					{
						sliderPos = fingerPos;
						if (sliderPos.y < minRayY)
							sliderPos = new Vector3(sliderPos.x, minRayY, 0f);
						else if (sliderPos.y > maxRayY)
							sliderPos = new Vector3(sliderPos.x, maxRayY, 0f);
						else
							sliderPos = new Vector3(sliderPos.x, sliderPos.y, 0f);
						
					}
					if (Input.GetMouseButtonUp(0))
					{
						
					}
					else
					{
						float delta = fingerPos.y - sliderPos.y;
						delta = Mathf.Clamp(delta, -sliderwidth/2f, sliderwidth/2f);
						
						angle = delta / (sliderwidth/2f);
						angle = Mathf.Clamp(angle, -1f, 1f);
					}
				}
				else
				{
					
				}
			}
			else
			{
				
			}
			#endif
			
			foreach (Touch touch in Input.touches)
			if (touch.position.x > borderline)
			{
				ray = guiCamera.ScreenPointToRay(touch.position);
				if (Physics.Raycast(ray, out hit, 2000) && hit.collider.tag == "_ControllPanel")
				{
					fingerPos = hit.point;
					switch(touch.phase)
					{
					case TouchPhase.Began:
						if (curFingerID == -1)
						{
							curFingerID = touch.fingerId;
							sliderPos = fingerPos;
							if (sliderPos.y < minRayY)
								sliderPos = new Vector3(sliderPos.x, minRayY, 0f);
							else if (sliderPos.y > maxRayY)
								sliderPos = new Vector3(sliderPos.x, maxRayY, 0f);
							else
								sliderPos = new Vector3(sliderPos.x, sliderPos.y, 0f);
							
							
							
						}
						goto default;
					case TouchPhase.Ended:
						if (touch.fingerId == curFingerID)
						{
							curFingerID = -1;
							
						}
						break;
					default :
						if (touch.fingerId == curFingerID)
						{
							float delta = fingerPos.y - sliderPos.y;
							delta = Mathf.Clamp(delta, -sliderwidth/2f, sliderwidth/2f);
							angle = delta / (sliderwidth/2f);
							angle = Mathf.Clamp(angle, -1f, 1f);
						}
						break;
					}
				}
				else
				{
					if (touch.fingerId == curFingerID)
					{
						curFingerID = -1;
					}
				}
			}
			if (Mathf.Abs(angle) < 0.1f) angle = 0;
		}
	}
}
