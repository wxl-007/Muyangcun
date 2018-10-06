using UnityEngine;
using System.Collections;

public class PopScore : MonoBehaviour {
	
	public bool mUp = false;
	private Color orgColor;
	private Vector3 orgPosition;
	private float mTime = 2.0f;
	private float riseSpeed = 200f;
	
	// Use this for initialization
	void Start () {
		this.orgColor = renderer.material.color;
		this.orgPosition = transform.position;
		mTime += Random.Range(-0.8f, 0.8f);
		riseSpeed += Random.Range(-50f, 50f);
		this.orgPosition += new Vector3( Random.Range(-50f, 50f), Random.Range(-10f, 10f), 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(mUp){
			if(renderer.material.color.a > 0){
				orgPosition.y += Time.deltaTime * riseSpeed;
				//位置提升
				transform.position = 
					new Vector3(
			          orgPosition.x, 
				      orgPosition.y,
				      orgPosition.z);
				
				orgColor.a -= Time.deltaTime / mTime;
				
				renderer.material.color = 
					new Color(
					      orgColor.r, 
					      orgColor.g, 
					      orgColor.b, 
					      orgColor.a);  
			}else{
				mUp = false;
				Destroy(gameObject);
			}
			
		}
	}
	
	public void setScore(int score){
		transform.GetComponent<SpriteText>().Text = score.ToString();
	}
}
