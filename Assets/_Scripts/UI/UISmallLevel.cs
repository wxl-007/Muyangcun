using UnityEngine;
using System.Collections;

public class UISmallLevel : MonoBehaviour {
	public int smallLevelID;

	// Use this for initialization
	void Start () {
		UIListItem btn = transform.parent.GetComponent<UIListButton>();
		btn.AddValueChangedDelegate(SetCurrentSmallLevel);
	}
	
	public void Forbid () {
		Debug.Log("forbid" + smallLevelID);
		Destroy(transform.parent.GetComponent<BoxCollider>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetCurrentSmallLevel (IUIObject obj) {
		MenuController.Instance.SetCurrentSmallLevel(smallLevelID);
	}
}
