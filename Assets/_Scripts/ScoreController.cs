using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {
	
	private static ScoreController instance;	
		
	public static ScoreController Instance {
		get 
		{
			if (instance == null)
			{
				instance = (ScoreController)FindObjectOfType(typeof(ScoreController));
			}
			if (!instance)
			{
				Debug.LogError("ScoreController could not find himself!");
			}
			return instance;
		}
	}
	
	public int displayScore;
	public int curScore;
	public int scoreRiseSpeed = 200;
	
	public void GetScore(int delta)
	{
		curScore += delta;
		GameController.Instance.score = curScore;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (displayScore < curScore) {
			int tmp = curScore - displayScore;
			if (tmp < scoreRiseSpeed) tmp = scoreRiseSpeed;
			displayScore += (int)(tmp * Time.deltaTime);
		} else {
			displayScore = curScore;
		}
	}
}
