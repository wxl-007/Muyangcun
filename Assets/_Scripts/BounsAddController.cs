using UnityEngine;
using System.Collections;

public class BounsAddController : MonoBehaviour {
	
	public SpriteText[] bounses;
	public SpriteText[] digitals;
	public PackedSprite coin;
	public SpriteText coinText;
	public int coinNum;
	public int[] scores = new int[4];
	public float idle1 = 1f;
	public float idle2 = 1f;
	public int scoreRiseSpeed = 1000;
	private bool shouldShow = false;
	
	// Use this for initialization
	void Start () {
		foreach (SpriteText bouns in bounses){
			bouns.Hide(true);
		}
		foreach (SpriteText digit in digitals){
			digit.Hide(true);
		}
		coin.Hide(true);coinText.Hide(true);
	}
	
	public IEnumerator showBouns () {
		shouldShow = true;
		scores[0] = GameController.Instance.vegeBouns;
		scores[1] = GameController.Instance.timeBouns;
		scores[2] = GameController.Instance.isPackageCollected ? 500 : 0;
		scores[3] = scores[0] + scores[1] + scores[2];
		string t1 = GameController.Instance.completedAch();
		string t2 = GameController.Instance.curCompleteAch();
		coinNum = 0;
		for (int i = 0; i<5; i++) {
			if (t1[i] == '0' && t2[i] == '1') coinNum += 10;
		}
		coinNum += scores[3] / 200;
		GlobalManager.totalCoins += coinNum;
		GlobalManager.SaveAllToPlayerPrefs();
		for (int i=0; i<bounses.Length; i++){
			yield return new WaitForSeconds(idle1);
			bounses[i].Hide(false);
			yield return new WaitForSeconds(idle2);
			digitals[i].Text = "0";
			digitals[i].Hide(false);
		}
		yield return new WaitForSeconds(idle1);
		coin.Hide(false);
		yield return new WaitForSeconds(idle2);
		coinText.Text = "x 00";
		coinText.Hide(false);
		yield return new WaitForSeconds(1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldShow) {
			for (int i=0; i< digitals.Length; i++){
				int curScore = int.Parse(digitals[i].Text);
				if (curScore < scores[i]) {
					int tmp = scores[i] - curScore;
					if (tmp < scoreRiseSpeed) tmp = scoreRiseSpeed;
					curScore += (int)(tmp * Time.deltaTime);
				} else {
					curScore = scores[i];
				}
				digitals[i].Text = curScore.ToString();
			}
			int curCoin = int.Parse(coinText.Text.Substring(1));
			if (curCoin < coinNum) {
					int tmp = coinNum - curCoin;
					if (tmp < 20) tmp = 20;
					curCoin += (int)(tmp * Time.deltaTime) < 1 ? 1 : (int)(tmp * Time.deltaTime);
				} else {
					curCoin = coinNum;
				}
				coinText.Text ="x " + curCoin.ToString("d2");
		}
	}
}
