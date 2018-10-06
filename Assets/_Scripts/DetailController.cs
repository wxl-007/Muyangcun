using UnityEngine;
using System.Collections;

public class DetailController : MonoBehaviour {
	
	public SpriteText Detail;
    /// <summary>
    /// active or disactive Achievement
    /// </summary>
	public UIButton[] graphAch;
    /// <summary>
    ///  show current Achievement 
    /// </summary>
	public int curAch;

	public UIPanelManager dialogmanager;
	
	// Use this for initialization
	void Start () {
		GetLevelParam();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private int ach_complete_num, ach_collect_num, ach_score, big_level, small_level;
	private float ach_fast_second;
	
	public static DetailController instance;			// instance of the class;
	
	public static DetailController Instance {
		get 
		{
			if (instance == null)
			{
				instance = (DetailController)FindObjectOfType(typeof(DetailController));
			}
			if (!instance)
			{
				Debug.LogError("DetailController could not find himself!");
			}
			return instance;
		}
	}
	
	public void GetLevelParam()
	{
		foreach (UIButton btn in graphAch) {
			btn.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			btn.Hide(true);
		}
		
		LevelParam.init();
		Levels level = LevelParam.level[GlobalManager.currentBigLevel, GlobalManager.currentSmallLevel];
		if (level != null) {
			ach_complete_num = level.ach_complete_num;
			ach_collect_num = level.ach_collect_num;
			ach_fast_second = level.ach_fast_second;
			ach_score = level.ach_score;
		}
		Detail.Text = "";
	}   
	// aches = {"is_ach_complete", "is_ach_collect", "is_ach_fast", "is_ach_score", "is_ach_special"};
	/// <summary>
	/// achievements button state
	/// </summary>
	/// <param name="id"></param>
	void ChangeAchSelect(int id)
	{
		if (dialogmanager != null) {
			dialogmanager.BringIn("DetailPanel");
		}
		graphAch[curAch].SetControlState(UIButton.CONTROL_STATE.DISABLED);
		graphAch[curAch].Hide(true);
		curAch = id;
		graphAch[curAch].Hide(false);
		graphAch[curAch].SetControlState(UIButton.CONTROL_STATE.NORMAL);
	}
	/// <summary>
	/// control the dialog
	/// </summary>
	public void Dismiss()
	{
		if (dialogmanager != null) {
			dialogmanager.Dismiss();
		}
	}
	/// <summary>
	/// show the anch
	/// </summary>
	/// <param name="id"></param>
	public void ShowAch(int id)
	{
		if (id == 0) ShowJZDetail();
		if (id == 1) ShowFlowerDetail();
		if (id == 2) ShowBagDetail();
		if (id == 3) ShowStarDetail();
		if (id == 4) ShowBookDetail();
	}
	
	void ShowJZDetail()
	{
		Detail.Text = string.Format("至少获得 {0:D} 蔬果.", ach_complete_num);
		ChangeAchSelect(0);
	}	
	void ShowFlowerDetail()
	{
		Detail.Text = string.Format("获得 {0:D} 以上的蔬果.", ach_collect_num);
		ChangeAchSelect(1);
	}
	void ShowBagDetail()
	{
		Detail.Text = string.Format("收集种子袋.");
		ChangeAchSelect(2);
	}	
	void ShowStarDetail()
	{
		Detail.Text = string.Format("在 {0:f1} 秒内通关.", ach_fast_second);
		ChangeAchSelect(3);
	}
	void ShowBookDetail()
	{
		Detail.Text = string.Format("得分超过 {0:D}.", ach_score);
		ChangeAchSelect(4);
	}	
}
