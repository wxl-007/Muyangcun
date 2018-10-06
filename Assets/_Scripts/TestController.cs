/*
Maintaince Logs:
2011-12-19    XuMingzhao    Add Debug Controller.
*/

using UnityEngine;
using System.Collections;

public class TestController : MonoBehaviour {
	
	void OnGUI () {
		
		if (Application.loadedLevelName == "TestControll"){	
			if (GUI.Button(new Rect(100f, 100f, 100f, 50f), "GetMoney")){
				GlobalManager.totalCoins += 10000;
				GlobalManager.SaveAllToPlayerPrefs();
			}
			if (GUI.Button(new Rect(400f, 100f, 100f, 50f), "UnlockLevel")){
				GlobalManager.maxBigLevel = 4;
				GlobalManager.maxSmallLevel = 10;
				GlobalManager.SaveAllToPlayerPrefs();
			}
			if (GUI.Button(new Rect(100f, 250f, 100f, 50f), "ClearData")){
				PlayerPrefs.DeleteAll();
			}
			
			if (GUI.Button(new Rect(150f, 175f, 150f, 50f), "StoreKitTestScene")){
				GlobalManager.LoadScene("StoreKitTestScene");
			}
			
			if (GUI.Button(new Rect(250f, 250f, 100f, 50f), "Quit")){
				GlobalManager.LoadScene("Menus");
				GlobalManager.SaveAllToPlayerPrefs();
			}
			
		}
		else{
			GUIStyle style = new GUIStyle();
			style.border = new RectOffset(0,0,0,0);
			if (GUI.Button(new Rect(0f, 0f, 60f, 60f),Resources.Load("alpha") as Texture, style)){
				GlobalManager.LoadScene("TestControll");
			}
		}
	}
}
