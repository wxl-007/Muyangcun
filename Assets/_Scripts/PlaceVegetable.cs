using UnityEngine;
using System.Collections;

public class PlaceVegetable : MonoBehaviour {
	
	private static GameController instance;	
		
	public static GameController Instance {
		get 
		{
			if (instance == null)
			{
				instance = (GameController)FindObjectOfType(typeof(GameController));
			}
			if (!instance)
			{
				Debug.LogError("GameController could not find himself!");
			}
			return instance;
		}
	}
		
	public GameObject[] prefabVegetables;
	
	// Use this for initialization
	void Start () {
		int pn = prefabVegetables.Length;
		int lim = transform.childCount;
		for(int i = 0; i < lim; i++){
			Transform t = transform.GetChild(i);
			GameObject tmpObj = Instantiate(prefabVegetables[Random.Range(0, pn)], t.position, t.rotation) as GameObject;
			tmpObj.name = tmpObj.name.Replace("(Clone)", "");
		}
	}
	
	void OnDrawGizmos ()
	{
		Gizmos.color = Color.black;
		int lim = transform.childCount;
		for(int i = 0; i < lim; i++){
			Transform t = transform.GetChild(i);
			Gizmos.DrawWireSphere(t.position, 20f);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
