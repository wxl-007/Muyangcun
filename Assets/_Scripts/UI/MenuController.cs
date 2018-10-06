using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {		
	public UIPanelManager PanelManagerScript;		// this is useful to bring in an other panel (redirection);
	public GameObject PreSmallLevel;
	public GameObject PreSmallArrow;
	public Material defaultMaterail;
	public Material newMaterial;

	public float SelectTractorBG;					// the position of z of the SelectTractor Panel's Background;
	public float SelectDriverBG;					// the position of z of the SelectDriver Panel's Background;
	public int UnlockWhat; 							// 0 stands for tractor, 1 stands for driver;
	public bool ShowLevelState;

	public UIBtnChangePanel[] UnlockedTractorsBtn;	// the active tractor buttons;
	public UIBtnChangePanel[] UnlockedAnimalBtn;
	public UIBtnChangePanel[] LockedDriversBtn; 		// the unactive driver buttons;
	public UIBtnChangePanel[] UnlockedDriversBtn;   // the active driver buttons;
	public SpriteText PriceText;					// It shows price of a tractor or a driver in the UnlockConfirm panel
	public SpriteText NeedCoinsText;
	public SpriteText[] TotalCoinsTexts;				
	public UIScrollList LevelList;

	
	public int tobeUnlockedDriver;					// driver which may be unlocked later;
	public int tobeUnlockedTractor;					// tractor which may be unlocked later; 
	public int tobeUnlockedAnimal;
	public int currentPrice;						// the price of a tractor or a driver, depending on which one
													// the user want to unlock.
	
	public static MenuController instance;			// instance of the class;

    public GameObject UnlockBtn;
    public GameObject Sheld;

	public static MenuController Instance {
		get 
		{
			if (instance == null)
			{
				instance = (MenuController)FindObjectOfType(typeof(MenuController));
			}
			if (!instance)
			{
				Debug.LogError("MenuController could not find himself!");
			}
			return instance;
		}
	}
	
	void Awake () {
		GlobalManager.LoadAllFromplayerPrefs();
        if (GlobalManager.BiglevelLock == true) {
            UnlockBtn.SetActiveRecursively(false);
            Sheld.SetActiveRecursively(false);
        }
        //更改
        GlobalManager.BiglevelLock = true;
		AudioListener.volume = GlobalManager.isSoundOn ? 1f : 0f;
	}
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 1f;
		
		//PlayerPrefs.SetInt("unlockedTractors", 1); 			// For debug sake;
		//PlayerPrefs.SetInt("unlockedDrivers", 1);  			// For debug sake;
		//PlayerPrefs.SetInt("totalCoins", 200);  			// For debug sake;
		SelectDriverBG = -10f;
		SelectTractorBG = -10f;
		UnlockWhat = -1;
		currentPrice = -1;
		tobeUnlockedDriver = -1;
		tobeUnlockedTractor = -1;
		ShowLevelState = true;
		
		LockController.Instance.checkForSencesLock(GlobalManager.maxBigLevel);
		if (GlobalManager.showCredit) {
			GoToPanel("Panel5_AboutUs");
			GlobalManager.showCredit = false;
		}
		if (GlobalManager.isBackToMenu) {
			ShowLevel();
			GoToPanel("Panel4_SmallLevel");
			GlobalManager.isBackToMenu = false;
			MenuController.Instance.SetCurrentSmallLevel(GlobalManager.currentSmallLevel);
		}
        ///激活大关卡
        if (GlobalManager.isBackToBigLevel) {
            GoToPanel("Panel3_BigLevel");
            GlobalManager.isBackToBigLevel = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                NativeDialogs.Instance.ShowMessageBox("提示", "真的要退出吗?", new string[] { "退出", "取消" }, (string button) => { QuitHint(button); });
            }

        }
		ShowTotalCoins();
       
	}


    void QuitHint(string code)
    {
        if (code.Equals("退出"))
        {
            Application.Quit();
        }
    }
	
	// When cancelling the dialog of buying tractors , drivers and coins, we use the unlockWhat to determine
	// to which panel the user will go.
	public void CancelUnlock () {
		Debug.Log("CancelUnlock is called");	
		if (UnlockWhat == 0) {
			UnlockWhat = -1;
			GoToPanel("Panel2.1_SelectTractor");
			ShowTractors();
			return;
		} else if (UnlockWhat == 1) {
			UnlockWhat = -1;
			GoToPanel("Panel2.2_SelectDriver");
			ShowDrivers();
			return;
		} else {
			UnlockWhat = -1;
			GoToPanel("Panel2.1_SelectAnimal");
			ShowAnimals();
			return;
		}
	}
	
	public void SetUnlockTractor() {
		UnlockWhat = 0;
	}
	
	public void SetUnlockDriver() {
		UnlockWhat = 1;
	}
	
	public void SetUnlockAnimal() {
		UnlockWhat = 2;
	}
	
	// Go to certain panel using panelNames;
	public void GoToPanel (string panelName) {
		PanelManagerScript.BringIn(panelName);
	}
	
	// This function is used to deal with unlocking drivers and tractors;
	// And after that, it will redirect the user to previous panel (selectDriver or selectTractor Panel);
	public void DoUnlock () {		
		if (tobeUnlockedDriver > 0) {
			DoUnlockDriver(tobeUnlockedDriver);
			ShowDrivers();
			GoToPanel("Panel2.2_SelectDriver");
			return;
		}
		
		if (tobeUnlockedTractor	> 0) {
			DoUnlockTractor(tobeUnlockedTractor);
			ShowTractors();
			GoToPanel("Panel2.1_SelectTractor");
			return;
		}
		
		if (tobeUnlockedAnimal > 0) {
			DoUnlockAnimal(tobeUnlockedAnimal);
			ShowAnimals();
			GoToPanel("Panel2.1_SelectAnimal");
			return;
		}
		Debug.Log("Hope you can't see this!");
	}
	
	// Unlock a certain driver using driverIDs. It will update the UnlockedDriver in the PlayerPrefs.
	// And reset the tobeUnlockedDriver and tobeUnlockedTractor, for safty sake;
	public void DoUnlockDriver (int driverID) {	
		if (currentPrice < 0) {
			return;
		}
		GlobalManager.unlockedDrivers |= (1 << (driverID - 1));
		GlobalManager.totalCoins -= currentPrice;
		UnlockReset();
		GlobalManager.SaveAllToPlayerPrefs();
		SetCurrentDriver(driverID);
	}
	
	// Acts like the DoUnlockDriver;
	public void DoUnlockTractor (int tractorID) {
		if (currentPrice < 0) {
			return;
		}
		GlobalManager.unlockedTractors |= (1 << (tractorID - 1));
		GlobalManager.totalCoins -= currentPrice;
		UnlockReset();
		GlobalManager.SaveAllToPlayerPrefs();
		SetCurrentTractor(tractorID);
	}
	
	public void DoUnlockAnimal (int animalID) {
		if (currentPrice < 0) {
			return;
		}
		GlobalManager.unlockedAnimals |= (1 << (animalID - 1));
		GlobalManager.totalCoins -= currentPrice;
		UnlockReset();
		GlobalManager.SaveAllToPlayerPrefs();
		SetCurrentAnimal(animalID);
	}
	
	// Reset the unlock-associate attributes;
	public void UnlockReset() {
		tobeUnlockedTractor = -1;
		tobeUnlockedDriver = -1;
		currentPrice = -1;
	}
	
	// Set the tobeUnlockedTractor and compare the tractor price with the user's current coins.
	// And then it will determine which panel the user will go to.
	public void UnlockTractor (int tractorID, int price) {
		tobeUnlockedTractor = tractorID;
		UnlockWhat = 0;
		if (price > GlobalManager.totalCoins) {
			NeedCoinsText.Text = "你还需要 " + (price - GlobalManager.totalCoins) + " 金币.";
			GoToPanel("Panel2.1.1.2_NeedMoreCoins");
		} else {
			currentPrice = price;
			ShowPrice();
			GoToPanel("Panel2.1.1.1_UnlockConfirm");
		}
	}
    
	// Acts like the UnlockTractor method.
    /// <summary>
    /// unlock the dirver who is on the carriage
    /// </summary>
    /// <param name="driverID"></param>
    /// <param name="price"></param>
	public void UnlockDriver (int driverID, int price) {
		tobeUnlockedDriver = driverID;
		UnlockWhat = 1;
		if (price > GlobalManager.totalCoins) {
			NeedCoinsText.Text = "你还需要 " + (price - GlobalManager.totalCoins) + " 金币.";
			GoToPanel("Panel2.1.1.2_NeedMoreCoins");
		} else {
			currentPrice = price;
			ShowPrice();
			GoToPanel("Panel2.1.1.1_UnlockConfirm");
		}
	}
	/// <summary>
	/// unlock animal
	/// </summary>
	/// <param name="animalID"></param>
	/// <param name="price"></param>
	public void UnlockAnimal (int animalID, int price) {
		tobeUnlockedAnimal = animalID;
		UnlockWhat = 2;
		if (price > GlobalManager.totalCoins) {
            NeedCoinsText.Text = "你还需要 " + (price - GlobalManager.totalCoins) + " 金币.";
			GoToPanel("Panel2.1.1.2_NeedMoreCoins");
		} else {
			currentPrice = price;
			ShowPrice();
			GoToPanel("Panel2.1.1.1_UnlockConfirm");
		}
	}
		
	
	public void SetCurrentDriver (int driverID) {

		GlobalManager.currentDriver = driverID;
		GlobalManager.SaveAllToPlayerPrefs();
	}
	
	public void SetCurrentTractor (int tractorID) {
		GlobalManager.currentTractor = tractorID;
		GlobalManager.SaveAllToPlayerPrefs();
	}
	
	public void SetCurrentAnimal (int animalID) {
		GlobalManager.currentAnimal = animalID;
		GlobalManager.SaveAllToPlayerPrefs();
	}
		
	// Set current biglevel;
	public void SetCurrentBigLevel (int bigLevel) {
		GlobalManager.currentBigLevel = bigLevel;
		GlobalManager.currentSmallLevel = 0;
		GlobalManager.SaveAllToPlayerPrefs();
	}
	
	// Set current smallLevel;
	public void SetCurrentSmallLevel (int smallLevel) {
		if (GlobalManager.currentSmallLevel > 0) {
			LevelList.sceneItems[GlobalManager.currentSmallLevel-1].GetComponent<UIButton>().spriteText.renderer.material = defaultMaterail;
			//((UIListButton) LevelList.GetItem(GlobalManager.currentSmallLevel * 2 - 1)).spriteText.renderer.material = defaultMaterail;
		}
		LevelList.sceneItems[smallLevel-1].GetComponent<UIButton>().spriteText.renderer.material = newMaterial;
		//((UIListButton) LevelList.GetItem(smallLevel * 2 - 1)).spriteText.renderer.material = newMaterial;
		GlobalManager.currentSmallLevel = smallLevel;
		DetailController.Instance.GetLevelParam();
		UIAchievement.Instance.UpdateAch();
		GlobalManager.SaveAllToPlayerPrefs();
	}
	
	// Display the available Tractors and locked ones. unlockedTractors stored in the PlayerPrefs is used here.
	public void ShowTractors () {
		for (int i = 0; i < UnlockedTractorsBtn.Length; i++) {
			Vector3 pos = new Vector3(UnlockedTractorsBtn[i].transform.position.x, UnlockedTractorsBtn[i].transform.position.y, SelectTractorBG);
			int result = (1 << i) & GlobalManager.unlockedTractors;
			if (0 == result) {
				UnlockedTractorsBtn[i].transform.position = pos - new Vector3(0, 0, 3f);
			} else {
				UnlockedTractorsBtn[i].transform.parent.FindChild("LockedTractor").position += new Vector3(0, 0, 300f);
				UnlockedTractorsBtn[i].transform.position = pos - new Vector3(0, 0, 30f);
			}	
		}
		GlobalManager.SaveAllToPlayerPrefs();
		ShowTotalCoins();
	}
	
	public void ShowAnimals () {
		for (int i = 0; i < UnlockedAnimalBtn.Length; i++) {
			Vector3 pos = new Vector3(UnlockedAnimalBtn[i].transform.position.x, UnlockedAnimalBtn[i].transform.position.y, SelectTractorBG);
			int result = (1 << i) & GlobalManager.unlockedAnimals;
			if (0 == result) {
				UnlockedAnimalBtn[i].transform.position = pos - new Vector3(0, 0, 3f);
			} else {
				UnlockedAnimalBtn[i].transform.parent.FindChild("LockedTractor").position += new Vector3(0, 0, 300f);
				UnlockedAnimalBtn[i].transform.position = pos - new Vector3(0, 0, 30f);
			}	
		}
		GlobalManager.SaveAllToPlayerPrefs();
		ShowTotalCoins();
	}
	
	// Acts like ShowTractors;
	public void ShowDrivers () {
		for (int i = 0; i < UnlockedDriversBtn.Length; i++) {
			Vector3 pos = new Vector3(UnlockedDriversBtn[i].transform.position.x,UnlockedDriversBtn[i].transform.position.y, SelectDriverBG);
			int result = (1 << i) & GlobalManager.unlockedDrivers;
			if (0 == result) {
				UnlockedDriversBtn[i].transform.position = pos + new Vector3(0, 0, 20f);
				LockedDriversBtn[i].transform.position = pos - new Vector3(0, 0, 40f);
			} else {
				UnlockedDriversBtn[i].transform.position = pos - new Vector3(0, 0, 20f);
				LockedDriversBtn[i].transform.position = pos + new Vector3(0, 0, 40f);		
			}
		}
		SetCurrentDriver(GlobalManager.currentDriver);
		GlobalManager.SaveAllToPlayerPrefs();
	}
		
	public void ShowPrice () {
		Debug.Log("Showing Price Now!!!!");
		PriceText.Text = "" + currentPrice;
	}
	
	public void ShowTotalCoins () {
		foreach (SpriteText t in TotalCoinsTexts) {
			t.Text = "" + GlobalManager.totalCoins;
		}
        if (GlobalManager.BiglevelLock == true)
        {
            UnlockBtn.SetActiveRecursively(false);
            Sheld.SetActiveRecursively(false);
        }
	}
	
	
	public void ShowLevel () {
		LevelList.ClearList(true);
		GameObject[] levels = new GameObject[GlobalManager.SmallLevelTable[GlobalManager.currentBigLevel]];
		for (int i = 0; i < GlobalManager.SmallLevelTable[GlobalManager.currentBigLevel]; i++) {
			Debug.Log("Show Level " + (i+1));
			GameObject level;
			level = (GameObject)Instantiate(PreSmallLevel, Vector3.zero, Quaternion.identity);
			foreach (Transform t in level.transform) {
				t.GetComponent<SpriteText>().Text = " 关卡" + GlobalManager.currentBigLevel+"-"+(i+1)+" ";
				t.GetComponent<UISmallLevel>().smallLevelID = i+1;
			}
			if (i != 0) LevelList.AddItem(Instantiate(PreSmallArrow, Vector3.zero, Quaternion.identity) as GameObject);
			LevelList.AddItem(level);
			levels[i] = level;
		}
		LevelList.sceneItems = levels;
		ShowLevelState = false;
		if (GlobalManager.currentBigLevel == GlobalManager.maxBigLevel) {
			LockController.Instance.checkForLevelLock(GlobalManager.maxSmallLevel);
		} else LockController.Instance.checkForLevelLock(10);
		GlobalManager.SaveAllToPlayerPrefs();
	}
		
	
	// Use this to update user's coins;
	public void AddCoins (int coin) {
		GlobalManager.totalCoins += coin;
		GlobalManager.SaveAllToPlayerPrefs();
	}
	
	// Start the game scene; 
	public void GameStart(){
		string levelName = GlobalManager.GetCurrentLevelName();
		GlobalManager.SaveAllToPlayerPrefs();
		GlobalManager.LoadScene(levelName);
	}
	
	public void BtnMoreGames(){
		//Application.OpenURL ("http://s.mogupai.com/");	
        Application.OpenURL("http://wapgame.189.cn/c/index.html?CAF=20110041");
	}



    ///<summary>
    /// 跳转到解锁界面
    /// <summary>
    public void ToUnlockPanel() {
        GoToPanel("Panel2.1.1.3_UnlockLevels");
    }

    ///<summary>
    /// 取消解锁
    /// <summary>
    public void NoThanks() {
        GoToPanel("Panel3_BigLevel");
    }


    public void Unlock() {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("UnlockBigLevels");
    }

    //void OnGUI() {
    //    if (GUI.Button(new Rect(0,0,100,80),"Billing succeed")) {
    //        GlobalManager.FeedBackUnlockLevels();
    //    }
    //}

    public void OnPause() {
        Time.timeScale = 0;
        Debug.Log("Pause===========");
    }
    public void OnResume() {
        Time.timeScale = 1;
        Debug.Log("Resume===========");
    }
}