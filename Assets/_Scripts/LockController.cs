using UnityEngine;
using System.Collections;

public class LockController : MonoBehaviour {
	
	/// <summary>
	/// 场景的按钮集
	/// </summary>
	public UIButton[] mBtnSences = null;
	
	/// <summary>
	/// Level的滚动list对象
	/// </summary>
	public UIScrollList mLevelList = null;
	
	/// <summary>
	/// 没有过关的level的渲染图集
	/// </summary>
	public Material mLevelUnPass = null;
	
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private static LockController instance;
	
	public static LockController Instance {
		get 
		{
			if (instance == null)
			{
				instance = (LockController)FindObjectOfType(typeof(LockController));
			}
			if (!instance)
			{
				Debug.LogError("LockController could not find himself!");
			}
			return instance;
		}
	}
	
	
	/// <summary>
	/// 检测场景的解锁数量
	/// </summary>
	/// <param name="passNumber">
	/// 已经解锁的场景数量，
	/// </param>
	public void checkForSencesLock(int passNumber){
		//TODO:这里最好加一个数量上的判断,以防万一
		
		//for(int i = 0; i < passNumber; i ++){
		//	mBtnSences[i].SetControlState(UIButton.CONTROL_STATE.NORMAL);
		//}
		
		int btnLengh = mBtnSences.Length;
		// Debug.Log("scens length" + btnLengh);
		for(int g = passNumber; g < btnLengh; g ++){
			mBtnSences[g].SetControlState(UIButton.CONTROL_STATE.DISABLED);
		}
	}
	
	/// <summary>
	/// 检测对应场景的关卡解锁数量
	/// </summary>
	/// <param name="passNumber">
	/// 已经解锁的关卡数量
	/// </param>
	public void checkForLevelLock(int passNumber){
		int listSize = mLevelList.sceneItems.Length;
		Debug.Log("listSize length" + listSize);
		for(int g = passNumber; g < listSize; g ++){
			mLevelList.sceneItems[g].GetComponent<UIListButton>().spriteText.renderer.material = mLevelUnPass;
			if (g < listSize - 1) {
				((UIListButton) mLevelList.GetItem(2*g+1)).spriteText.renderer.material = mLevelUnPass;
			}
			mLevelList.sceneItems[g].GetComponent<UIListButton>().spriteText.GetComponent<UISmallLevel>().Forbid();
		}
	}
}
