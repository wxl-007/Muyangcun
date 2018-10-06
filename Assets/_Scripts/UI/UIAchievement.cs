using UnityEngine;
using System.Collections;

public class UIAchievement : MonoBehaviour {
	public UIButton[] achievements;
	// aches = {"is_ach_complete", "is_ach_collect", "is_ach_fast", "is_ach_score", "is_ach_special"};
	public static UIAchievement instance;			// instance of the class;
	
	public static UIAchievement Instance {
		get 
		{
			if (instance == null)
			{
				instance = (UIAchievement)FindObjectOfType(typeof(UIAchievement));
			}
			if (!instance)
			{
				Debug.LogError("UIAchievement could not find himself!");
			}
			return instance;
		}
	}
	
	public void UpdateAch()
	{
		int big_level = GlobalManager.currentBigLevel;
		int small_level = GlobalManager.currentSmallLevel;
		string param;
		if (0 < big_level && big_level <= 4 && 0 < small_level && small_level <= 10)
		{
			param = LevelAchievement.GetAchievement(big_level, small_level);
			int appear = -1;
			for (int i = 0; i < param.Length; i++)
			{
				achievements[i].Hide(param[i] != '1');
				if (appear == -1 && param[i] != '1') {
					appear = i;
				}
			}
			DetailController.Instance.ShowAch(appear);
		}
		else Debug.Log("Can't find level_" + big_level + "_" + small_level);
	}
	
	public void ClearAch()
	{
		foreach(UIButton ach in achievements) ach.Hide(true);
	}
}
