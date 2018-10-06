using UnityEngine;
using System.Collections;

public class GlobalManager : MonoBehaviour {
	public static bool isBackToMenu = false;
	public static bool showCredit = false;
	public static bool isSoundOn = true;
	public static bool isRated = false;
    /// <summary>
    /// µÚ¶þ¹Ø¿¨Ëø
    /// </summary>
    public static bool BiglevelLock = false;
    public static bool isBackToBigLevel = false;


	public static int currentBigLevel = 1;
	public static int currentSmallLevel = 1;
	public static int currentDriver = 1;
	public static int currentTractor = 1;
	public static int currentAnimal = 1;
	
	public static int previousBigLevel = 1;
	public static int previousSmallLevel = 1;
	public static int previousDriver = 1;
	public static int previousTractor = 1;
	public static int previousAnimal = 1;
	
	public static int totalCoins = 1;
	public static int maxBigLevel = 1;
	public static int maxSmallLevel = 1;
	public static int unlockedTractors = 1;
	public static int unlockedDrivers = 1;
	public static int unlockedAnimals = 1;
	public static string levelToLoad;
	
	public static int[] SmallLevelTable = {0, 10, 10, 10, 10};		// index stands for biglevel and value stands for smallvalue;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static void BackToMenu() {
		isBackToMenu = true;
		LoadScene("Menus");
	}

    public static void BackToBigLevel() {
        isBackToBigLevel = true;
        levelToLoad = "Menus";
        Application.LoadLevel("LoadingScene");
    }
	
	public static string GetCurrentLevelName () {
		return "Level_"+currentBigLevel+"_"+currentSmallLevel;
	}
	
	public static string GetNextLevelName () {
		if (currentSmallLevel == 10) {
			return "Level_"+(currentBigLevel+1)+"_"+1;
		} else {
			return "Level_"+currentBigLevel+"_"+(currentSmallLevel+1);
		}
	}
	
	public static void UpdateCurrentLevel () {
		if (currentSmallLevel == 10) {
			currentSmallLevel = 1;
			currentBigLevel += 1;
		} else {
			currentSmallLevel++;
		}
		
		if (currentBigLevel	 > maxBigLevel) {
			maxBigLevel = currentBigLevel;
			maxSmallLevel = currentSmallLevel;
		} 
		
		if (currentBigLevel == maxBigLevel) {
			if (currentSmallLevel > maxSmallLevel) {
				maxSmallLevel = currentSmallLevel;
			}
		}
	}
	
	public static void UpdateMaxLevel () {
		if (maxBigLevel == currentBigLevel && maxSmallLevel == currentSmallLevel){
			if (maxSmallLevel == 10) {
				maxSmallLevel = 1;
				maxBigLevel += 1;
			} else {
				maxSmallLevel++;
			}
		}
		SaveAllToPlayerPrefs();
	}
	
	public static void ToggleSound() {
		if (isSoundOn) {
			isSoundOn = false;
			/* todo: turn the sound off; */
		} else {
			isSoundOn = true;
			/* todo: turn the sound on; */
		}
		Debug.Log("isSoundOn"+isSoundOn);
	}
	
	public static void Exit() {
		SaveAllToPlayerPrefs();
		/* todo: exit the game; */
	}
		
	
	public static void SaveAllToPlayerPrefs () {
		Debug.Log("Save");
		PlayerPrefs.SetInt("totalCoins", totalCoins);
		PlayerPrefs.SetInt("unlockedTractors", unlockedTractors);
		PlayerPrefs.SetInt("unlockedDrivers", unlockedDrivers);
		PlayerPrefs.SetInt("unlockedAnimals", unlockedAnimals);
		PlayerPrefs.SetInt("maxSmallLevel", maxSmallLevel);
		PlayerPrefs.SetInt("maxBigLevel", maxBigLevel);
		PlayerPrefs.SetInt("currentDirver", currentDriver);
		PlayerPrefs.SetInt("isSoundOn", isSoundOn ? 1 : 0);
		PlayerPrefs.SetInt("isRated", isRated ? 1 : 0);
        PlayerPrefs.SetInt("BiglevelLock", BiglevelLock ? 1 : 0);
		PlayerPrefs.Save();
	}
	
	public static void LoadAllFromplayerPrefs () {
		GlobalManager.totalCoins = PlayerPrefs.GetInt("totalCoins", 0);
		GlobalManager.maxBigLevel = PlayerPrefs.GetInt("maxBigLevel", 1);
		GlobalManager.maxSmallLevel = PlayerPrefs.GetInt("maxSmallLevel", 1);
		GlobalManager.unlockedTractors = PlayerPrefs.GetInt("unlockedTractors", 1);
		GlobalManager.unlockedDrivers = PlayerPrefs.GetInt("unlockedDrivers", 1);
		GlobalManager.unlockedAnimals = PlayerPrefs.GetInt("unlockedAnimals", 1);
		GlobalManager.currentDriver = PlayerPrefs.GetInt("currentDirver", 1);
		GlobalManager.isSoundOn = (PlayerPrefs.GetInt("isSoundOn", 1) == 1);
		GlobalManager.isRated = (PlayerPrefs.GetInt("isRated", 0) == 1);
        GlobalManager.BiglevelLock = (PlayerPrefs.GetInt("BiglevelLock",0) == 1);
	}
	
	public static void LoadScene (string scene) {
		Time.timeScale = 1f;
		levelToLoad = scene;
		if (levelToLoad == "Level_5_1") {
			levelToLoad = "Menus";
			showCredit = true;
		}
		Application.LoadLevel("LoadingScene");
	}


   
}