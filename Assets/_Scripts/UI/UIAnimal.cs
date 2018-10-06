using UnityEngine;
using System.Collections;

public class UIAnimal : MonoBehaviour {
	public int animalID;
	public int price;
	public bool isLocked;
	
	// Use this for initialization
	void Start () {
		UIBtnChangePanel btn = gameObject.GetComponent<UIBtnChangePanel>();
		if (isLocked) {
			btn.AddValueChangedDelegate(Unlock);
		} else {
			btn.AddValueChangedDelegate(SetCurrentAnimal);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void Unlock(IUIObject obj) {
		Debug.Log(" UIAnimal unlock : "+ animalID);
		MenuController.Instance.UnlockAnimal(animalID, price);
	}
	
	public void SetCurrentAnimal (IUIObject obj) {
		Debug.Log(" Current Animal: "+ animalID);
		MenuController.Instance.SetCurrentAnimal(animalID);
		MenuController.Instance.ShowDrivers();
		MenuController.Instance.GoToPanel("Panel2.2_SelectDriver");
	}
}
