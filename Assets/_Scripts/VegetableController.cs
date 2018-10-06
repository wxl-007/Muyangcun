using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VegetableController : MonoBehaviour {
	public GameObject tail;
	private List<GameObject> curVege = new List<GameObject>();
	public GameObject popScore;
	
	private static VegetableController instance;	
		
	public static VegetableController Instance {
		get 
		{
			if (instance == null)
			{
				instance = (VegetableController)FindObjectOfType(typeof(VegetableController));
			}
			if (!instance)
			{
				Debug.LogError("VegetableController could not find himself!");
			}
			return instance;
		}
	}

	public void CollectVegetable(GameObject vegetable) {
		Vector3 delta = new Vector3(0f, 70f, 0.1f);
		delta = GameController.Instance.vegetableContainer.rotation * delta;
		Vector3 pos = GameController.Instance.vegetableContainer.position + delta;
		vegetable.rigidbody.useGravity = true;
		vegetable.collider.isTrigger = false;
		vegetable.transform.position = pos;
		vegetable.rigidbody.velocity = GameController.Instance.vegetableContainer.rigidbody.velocity;
		GameController.Instance.vegetableQuantity++;
		curVege.Add(vegetable);
	}
	
	public void DropVegetable(GameObject vegetable) {
		curVege.Remove(vegetable);
		GameController.Instance.vegetableQuantity--;
		if (GameController.Instance.vegetableQuantity < 0) {
			Debug.LogError("vegetable error!");
		}
	}
	
	bool AreSame(string a, string b) {
		return (a == b || (a == "watermelon" && b == "cabbage") || (a == "watermelon" && b == "cabbage"));
	}
	
	public bool DropVegetable(List<string> vegeNames, Vector3 collectpos, int score) {
		foreach(GameObject t in curVege){
			Debug.Log(t.name);
		}
		List<GameObject> tmp = new List<GameObject>();
		foreach(string vegeName in vegeNames){
			foreach(GameObject vege in curVege) {
				if (AreSame(vegeName, vege.name)) {
					bool alreadyHas = false;
					foreach(GameObject t in tmp) {
						if (vege == t) {
							alreadyHas = true;
						}
					}
					if (!alreadyHas) {
						tmp.Add(vege);
						break;
					}
				}
			}
		}
		Debug.Log(tmp.Count);
		if (tmp.Count <= vegeNames.Count && tmp.Count > 0) {
			StartCoroutine(CollectThese(tmp, collectpos, score));
			return true;
		} else {
			return false;
		}
	}
	
	IEnumerator CollectThese(List<GameObject> veges, Vector3 collectpos, int score)
	{
		foreach (GameObject vege in veges){
			VegeCollect(vege, collectpos, score);
			ScoreController.Instance.GetScore(score);
			if (vege.name != "package") GameController.Instance.vegeBouns += score;
			yield return new WaitForSeconds(0.2f);	
		}
	}
	
	public IEnumerator CollectAll(Vector3 collectpos, int score)
	{
		GameController.Instance.LockInput();
		GameObject[] veges = new GameObject[curVege.Count];
		curVege.CopyTo(veges);
		for (int i=0; i<veges.Length; i++) {
			GameObject vege = veges[i];
			if (vege.name == "package") continue;
			VegeCollect(vege, collectpos, score);
			ScoreController.Instance.GetScore(score);
			if (vege.name != "package") GameController.Instance.vegeBouns += score;
			yield return new WaitForSeconds(0.2f);
		}
		yield return new WaitForSeconds(1.3f);
		GameController.Instance.FinishGame();
	}
	
	// move vegetable by a parabola to the collectpos.
	void VegeCollect(GameObject vege, Vector3 collectpos, int score)
	{
		curVege.Remove(vege);
		if (vege.name != "package") {
			GameController.Instance.vegetableQuantity--;
			GameController.Instance.vegetableCollected++;
		}
		
		vege.SendMessage("DropToBasket");
		
		vege.GetComponent<Vegetable>().holdZ = 4f;
		vege.transform.position = new Vector3(vege.transform.position.x, vege.transform.position.y, 4f);
		vege.collider.isTrigger = true;
		vege.rigidbody.drag = 0f;
		
		float t = 1.5f; // The time run in parabola
		Vector2 S = vege.transform.position; //The Start position (X1, Y1)
		Vector2 T = collectpos; // The end position (X2, Y2)
		
		// X2 = X1 + t * Vx;
		// Y2 = Y1 + t * Vy + 0.5 * gt^2;
		vege.rigidbody.velocity = new Vector3( (T.x - S.x)/t, (T.y - S.y)/t - 0.5f * Physics.gravity.y * t, 0f);
		
		
		GameObject tmp = Instantiate(tail,vege.transform.position + new Vector3(0f, 0f, 1f), Quaternion.identity) as GameObject;
		ParticleEmitter p = tmp.GetComponent<ParticleEmitter>();
		p.maxSize = Mathf.Min(vege.GetComponent<PackedSprite>().width, vege.GetComponent<PackedSprite>().height);
		p.minSize = p.maxSize * 0.4f;
		tmp.transform.parent = vege.transform;
		StartCoroutine(DestoryVegetable(t, vege, tmp, score));
	}
	
	IEnumerator DestoryVegetable(float t, GameObject vege,GameObject tail, int score)
	{
		yield return new WaitForSeconds(t);
		Debug.Log(vege.transform.position);
		popScore.GetComponent<SpriteText>().characterSize = 50 + Random.Range(-10f, 5f);
		GameObject tmp = Instantiate(popScore, vege.transform.position, Quaternion.identity) as GameObject;
		tmp.GetComponent<PopScore>().setScore(score);
		tmp.GetComponent<PopScore>().mUp = true;
		
		tail.transform.parent = null;
		Destroy(vege);
		yield return new WaitForSeconds(2f);
		Destroy(tail);
	}
	
	public void ShowBagDetail()
	{
		Debug.Log("!!");
	}

}
