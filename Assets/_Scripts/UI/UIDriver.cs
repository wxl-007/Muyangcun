using UnityEngine;
using System.Collections;

public class UIDriver : MonoBehaviour {
	public int driverID;
	public int price;
	public bool isLocked;
	
	// Use this for initialization
	void Start () {
		UIBtnChangePanel btn = gameObject.GetComponent<UIBtnChangePanel>();
		if (isLocked) {
			//Debug.Log("unlock:" + btn);
			btn.AddValueChangedDelegate(Unlock);
		} else {
			//Debug.Log("set:" + btn);
			btn.AddValueChangedDelegate(SetCurrentDriver);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Unlock (IUIObject obj) {
		Debug.Log(" UIDriver unlock : "+ driverID);
		MenuController.Instance.UnlockDriver(driverID, price);
	}
	
	public void SetCurrentDriver (IUIObject obj) {
		Debug.Log(" Current Driver: "+ driverID);
		MenuController.Instance.SetCurrentDriver(driverID);
		MenuController.Instance.GoToPanel("Panel3_BigLevel");
	}
}
