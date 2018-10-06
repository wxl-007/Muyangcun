using UnityEngine;
using System.Collections;

public class LevelAchievement : MonoBehaviour {
	
	static string GetAchName( int bigLevel, int smallLevel) {
		return "Level_" + bigLevel + "_" + smallLevel + "_ach";
	}
	
	public static string GetAchievement(int bigLevel, int smallLevel) {
		string achName = GetAchName(bigLevel, smallLevel);
		return PlayerPrefs.GetString(achName, "00000");
	}
	
	public static void SetAchievement(int bigLevel, int smallLevel, string val) {
		string achName = GetAchName(bigLevel, smallLevel);
		PlayerPrefs.SetString(achName, val);
		PlayerPrefs.Save();
	}
}
