using UnityEngine;
using System.Collections;

public class UIBigLevel : MonoBehaviour {
	public int bigLevelID;
	
	// Use this for initialization
	void Start () {
		UIBtnChangePanel btn = gameObject.GetComponent<UIBtnChangePanel>();
		btn.AddValueChangedDelegate(SetCurrentBigLevel);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetCurrentBigLevel (IUIObject obj) {
		MenuController.Instance.SetCurrentBigLevel(bigLevelID);
		MenuController.Instance.ShowLevel();
		MenuController.Instance.GoToPanel("Panel4_SmallLevel");
		MenuController.Instance.SetCurrentSmallLevel(1);
	}
		
}
