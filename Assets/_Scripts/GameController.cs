using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public GameObject gui;
	public GameObject[] tractors;
	public GameObject[] drivers;
	public GameObject[] animals;
	public GameObject tractor;
	public GameObject driver;
	public Transform vegetableContainer;	// Actually it's the CarBack Game Object. Use this to locate vegetable.
	public Transform startPointTractor;
	public Transform startPointDriver;
	public Transform startPointAnimal;
	public bool isInputAllowed;				// used to enable and disable user input.
	public bool doReplay;					// if true, load current scene and let the the use play it again.
	public bool doNextLevel;				// if true, load the next scene
	
	public bool isPackageCollected;
	public int vegetableCollected;
	public int vegetableQuantity;			// the total of the vegetable the user collected
	public float currentTime;				// passed time
	public float completedPercent;
	public float goalPosX;
	public int score;
	public int timeBouns;
	public int vegeBouns;
	public string user_name = "Test";
	public int curGroundWheelCount;
	public float airTime;
	
	public int ach_complete_num, ach_collect_num, ach_score, big_level, small_level;
	public float ach_fast_second;

	private static GameController instance;	
		
	public static GameController Instance {
		get 
		{
			if (instance == null)
			{
				instance = (GameController)FindObjectOfType(typeof(GameController));
			}
			if (!instance)
			{
				Debug.LogError("GameController could not find himself!");
			}
			return instance;
		}
	}
	
	void Awake () {
		gui = Instantiate(gui) as GameObject;
		InputController.Instance.guiCamera = gui.GetComponentInChildren<Camera>();
		GlobalManager.LoadAllFromplayerPrefs();
		AudioListener.volume = GlobalManager.isSoundOn ? 1f : 0f;
	}
	// Use this for initialization 
	void Start () {
		//SoundContoller.Instance.PlayTractorRun();
		Time.timeScale = 1f;
		big_level = GlobalManager.currentBigLevel;
		small_level =GlobalManager.currentSmallLevel;
		vegetableQuantity = 0;
		currentTime = 0;
		isInputAllowed = true;
		doReplay = false;
		doNextLevel = false;
		GetLevelParam();
		Debug.Log("Loading level.");
		
		tractor = Instantiate(tractors[GlobalManager.currentTractor-1], startPointTractor.position, Quaternion.identity) as GameObject;
		startPointDriver = tractor.transform.FindChild("StartPointDriver");
		startPointAnimal = tractor.transform.FindChild("StartPointAnimal");
		
		TractorControl tc = tractor.GetComponent<TractorControl>();
		
		tc.FrontCar = Instantiate(animals[GlobalManager.currentAnimal-1], startPointAnimal.position, Quaternion.identity) as GameObject;
		tc.LeftWheel = tc.FrontCar.transform.FindChild("wheel2").gameObject;
		tc.RightWheel = tc.FrontCar.transform.FindChild("wheel1").gameObject;
		tc.Animal = tc.FrontCar.GetComponent<PackedSprite>();
		tc.FrontCar.transform.parent = tractor.transform;
		tc.FrontCar.GetComponent<HingeJoint>().connectedBody = tc.BackCar.rigidbody;
		
		Camera.main.gameObject.GetComponent<CameraFocus>().FocusTarget = tc.FrontCar.transform;
		
		driver = Instantiate(drivers[GlobalManager.currentDriver-1], startPointDriver.position, Quaternion.identity) as GameObject;
		driver.transform.parent = tractor.transform;
		driver.hingeJoint.connectedBody = tractor.transform.FindChild("TractorBack").rigidbody;
		
		vegetableContainer = tractor.transform.FindChild("TractorBack").transform;
	}
	
	void FixedUpdate () {
		if (curGroundWheelCount == 0) {
			airTime += Time.fixedDeltaTime;
		} else {
			if (airTime > 0.6f) {
				//SoundController.Instance.Play("tractor_hit_ground");
			}
			airTime = 0f;
		}
		curGroundWheelCount = 0;
	}

	// Update is called once per frame
	void Update () {
		if (isInputAllowed) {	// if the user is still in the play mode.
			currentTime = currentTime + Time.deltaTime;
		}
		completedPercent = (vegetableContainer.position.x - startPointTractor.position.x) / (goalPosX - startPointTractor.position.x);
	}
		
	void OnGUI() {
		//Debug.Log("vegetables: "+vegetableQuantity);
	//	Statistics(); 	//show the time and vegetable quantity.
		//if (doReplay) {	//let the user replay this level again
	//		Replay();
	//		return;
	//	}
	//	if (doNextLevel) { // let him play next level.
	//		NextLevel();
	//		return;
	//	}
	}
//	*/
	
	public void Statistics () {
		GUILayout.BeginArea(new Rect(Screen.width - 100, 20, 100, 50));
		GUILayout.Box("Time: " + currentTime.ToString("F1") + 
		              "\nVegetables: " + vegetableCollected + 
		              "\nScore: " + ScoreController.Instance.displayScore);
		
		GUILayout.EndArea();
	}
	
/*	
	public void Replay () {
		if (GUI.Button(new Rect(150, 200, 300, 100), "Please retry!")) {
			LoadCurrentScene();
			return;
		}	
	}
*/
	public void Replay () {
		SoundController.Instance.Play("level_lose");
		gui.SendMessage("ShowFailMenu", SendMessageOptions.DontRequireReceiver);
	}
		
	
	public void NextLevel () {
		Debug.Log("nextScene: "+GlobalManager.GetNextLevelName());
		GlobalManager.UpdateCurrentLevel();
		LoadCurrentScene();
	}
	
	public void LoadCurrentScene (){
		GlobalManager.LoadScene(GlobalManager.GetCurrentLevelName());
	}
		
	public void LoadNextScene() {
		GlobalManager.LoadScene(GlobalManager.GetNextLevelName());
	}
	
	// set isInputAllowed to false, which disables the user input.
	public void LockInput () {
		isInputAllowed = false;
	}
	
	public void Pause () {
		Time.timeScale = 0;
		(GameObject.FindObjectOfType(typeof(TractorControl)) as TractorControl).Animal.SetFramerate(0f);
	}
	
	public void Resume () {
		Time.timeScale = 1;
	}
	
	public void GetLevelParam()
	{
		LevelParam.init();
		Levels level = LevelParam.level[GlobalManager.currentBigLevel, GlobalManager.currentSmallLevel];
		if (level != null) {
			ach_complete_num = level.ach_complete_num;
			ach_collect_num = level.ach_collect_num;
			ach_fast_second = level.ach_fast_second;
			ach_score = level.ach_score;
		}
	}
	
	public void CompleteAch () {
		// aches = {"is_ach_complete", "is_ach_collect", "is_ach_special", "is_ach_fast", "is_ach_score"};
		string param1 = curCompleteAch();
		string param2 = completedAch();
		string param = "";
		for (int i=0; i<param1.Length; i++) {
			param += (param1[i] == '1' || param2[i] == '1') ? "1" : "0";
		}
		LevelAchievement.SetAchievement(big_level, small_level, param);
	}
	
	public void AddTimeScoreBouns()
	{
		int s = 0;
		if (currentTime < ach_fast_second) s = 500;
		else if (currentTime < ach_fast_second * 1.15) s = 400;
		else if (currentTime < ach_fast_second * 1.3) s = 300;
		else if (currentTime < ach_fast_second * 1.45) s = 200;
		else if (currentTime < ach_fast_second * 1.6) s = 100;
		timeBouns = s;
		ScoreController.Instance.GetScore(s);
	}
	
	public void FinishGame () {
		Debug.Log("" + vegetableCollected + "," + ach_complete_num);
		if (vegetableCollected >= ach_complete_num) // means it's win!
		{
			SoundController.Instance.Play("level_win");
			//doNextLevel = true;
			AddTimeScoreBouns();
			gui.SendMessage("ShowWinMenu");
			StartCoroutine(AchievementsFuction.Instance.showWinPanel(curCompleteAch(), completedAch()));
			LockInput();
			GlobalManager.UpdateMaxLevel();
			CompleteAch();
		}
		else // lose.
		{
			Replay();
			LockInput();
		}
	}
	
	public string completedAch()
	{
		return LevelAchievement.GetAchievement(big_level, small_level);
	}
	
	public string curCompleteAch()
	{
		string param = "1";
		param += vegetableCollected >= ach_collect_num ? "1" : "0";
		param += isPackageCollected ? "1" : "0";
		param += currentTime <= ach_fast_second ? "1" : "0";
		param += score >= ach_score ? "1" : "0";
		return param;
	}
}
