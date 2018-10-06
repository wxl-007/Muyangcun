using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {
	
	public UIPanelManager panelManager;
	public SpriteText targetText, levelText, clockText;
	public GameObject smallCar;
    public UIActionBtn nextLevel;
	
	// Use this for initialization
	void Start () {
		if (GlobalManager.currentBigLevel == 1 && GlobalManager.currentSmallLevel <= 3) {
			Time.timeScale = 0f;
			panelManager.BringIn("PanelHelp");
		}
        
        if (GlobalManager.currentBigLevel == 1 && GlobalManager.currentSmallLevel ==10 && GlobalManager.BiglevelLock == false) {
            //Time.timeScale = 0f;
            nextLevel.Hide(true);
          //  panelManager.BringIn("PanelUnlockLevels");
        }
        else if (GlobalManager.currentBigLevel == 1 && GlobalManager.currentSmallLevel == 10 && GlobalManager.BiglevelLock == true) {
            nextLevel.Hide(false);
        }
        /*
		if (GlobalManager.currentBigLevel >= 2 && !GlobalManager.isRated) {
			if (Random.Range(0,10) < 3) { //30%
				Time.timeScale = 0f;
				panelManager.BringIn("PanelRate");
			}
		}
         * */
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
               
                    //Application.Quit();
                
                NativeDialogs.Instance.ShowMessageBox("提示", "真的要退出吗?", new string[] { "退出", "取消" }, (string button) => { QuitHint(button); });
                //NativeDialogs.Instance.ShowMessageBox("tips", "do u want to quite game?", new string[] { "quite", "cancel" }, (string button) => { QuitHint(button); });
            }

        }
		targetText.Text = "" + GameController.Instance.vegetableCollected + " / " + GameController.Instance.ach_complete_num;
		levelText.Text = "" + GlobalManager.currentBigLevel + "-" + GlobalManager.currentSmallLevel;
		clockText.Text = "" + GameController.Instance.currentTime.ToString("F1");
		Vector3 t = smallCar.transform.localPosition;
		float curX = GameController.Instance.completedPercent * 180 - 99;
		smallCar.transform.localPosition = new Vector3(curX, t.y, t.z);
	}



    void QuitHint(string code)
    {
        if (code.Equals("退出"))
        {
            Application.Quit();
        }
    }


	
	void DoPause () {
		GameController.Instance.Pause();
	}
	
	public void DoResume () {
		GameController.Instance.Resume();
		panelManager.BringIn("PanelPauseBtn");
	}
	
	public void DoBackToMenu () {
		GlobalManager.BackToMenu();
		Debug.Log("Do back to menu!");
	}
	
	public void DoRestart () {
		GameController.Instance.Resume();
		GameController.Instance.LoadCurrentScene();
	}
	
	public void DoNextScene(){
        Debug.Log("currentBigLevel   " + GlobalManager.currentBigLevel + "currentSmallLevel   " + GlobalManager.currentSmallLevel);
        if (GlobalManager.currentBigLevel == 1 && GlobalManager.currentSmallLevel == 10 && GlobalManager.BiglevelLock == false && GlobalManager.GetNextLevelName() == "Level_2_1")
        {
            
            Time.timeScale = 0;
            panelManager.BringIn("PanelUnlockLevels");
        }
        else {
            GameController.Instance.NextLevel(); 
        }
		
	}
		
	public void ShowFailMenu(){
		Debug.Log("Do fail Menu!");
		panelManager.BringIn("PanelFail");
	}
	
	public void ShowWinMenu(){
		panelManager.BringIn("PanelWined");
	}
	
	public void DoToggleSound() {
		Debug.Log("toggle sound");
		GlobalManager.ToggleSound();
	}
	
	public void DoExit() {
		GlobalManager.Exit();
		Debug.Log("exit now!");
	}
	
	public void RateUs() {
		GlobalManager.isRated = true;
		GlobalManager.SaveAllToPlayerPrefs();
		if (Application.platform != RuntimePlatform.IPhonePlayer  &&  Application.platform != RuntimePlatform.Android  ){
			Application.OpenURL ("http://itunes.apple.com/us/app/tractor-hero/id508265916?ls=1&mt=8"); 
		}else{ 
			Application.OpenURL ("itms-apps://ax.itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id=508265916"); 
		}
	}


    ///<summary>
    ///移动付费 解锁关卡
    /// <summary>
    public void UnlockLevel() {

        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("UnlockBigLevels");
    }


    /// <summary>
    /// 返回到关卡选关
    /// </summary>
    public void DontUnlock() {
        GameController.Instance.Resume();
        panelManager.BringIn("PanelWined");
    }



}
