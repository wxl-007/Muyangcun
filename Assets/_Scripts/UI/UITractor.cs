using UnityEngine;
using System.Collections;

public class UITractor: MonoBehaviour {
	public int tractorID;
	public int price;
	public bool isLocked;
	
	// Use this for initialization
	void Start () {
		UIBtnChangePanel btn = gameObject.GetComponent<UIBtnChangePanel>();
		if (isLocked) {
			btn.AddValueChangedDelegate(Unlock);
		} else {
			btn.AddValueChangedDelegate(SetCurrentTractor);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void Unlock(IUIObject obj) {
		Debug.Log(" UITractor unlock : "+ tractorID);
		MenuController.Instance.UnlockTractor(tractorID, price);
	}
	
	public void SetCurrentTractor (IUIObject obj) {
		Debug.Log(" Current Tractor: "+ tractorID);
		MenuController.Instance.SetCurrentTractor(tractorID);
		MenuController.Instance.ShowAnimals();
		MenuController.Instance.GoToPanel("Panel2.1_SelectAnimal");
	}
}