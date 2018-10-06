using UnityEngine;
using System.Collections;

public class AchievementsFuction : MonoBehaviour {
	
	/// <summary>
	/// 星星粒子
	/// </summary>
	public GameObject mPreStartParticle = null;
	public GameObject mSecondParticle = null;
	
	public BounsAddController bac;
	
	/// <summary>
	/// 代表奖章的按钮数组集
	/// </summary>
	public UIButton[] mBtnMedal = null;
	
	/// <summary>
	/// 游戏中对成就是否获取的成功的定义
	/// </summary>
	private const string TRUE = "1";
	
	/// <summary>
	/// 游戏中对成就是否获取的失败的定义
	/// </summary>
	private const string FALSE = "0";
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	/// <summary>
	/// AchievementsFuction静态对象
	/// </summary>
	private static AchievementsFuction instance;
	
	/// <summary>
	/// AchievementsFuction构造器
	/// </summary>
	private AchievementsFuction(){
		
	}
	
	/// <summary>
	/// 获取AchievementsFuction的单例
	/// </summary>
	public static AchievementsFuction Instance {
		get 
		{
			if (instance == null)
			{
				instance = (AchievementsFuction)FindObjectOfType(typeof(AchievementsFuction));
			}
			if (!instance)
			{
				Debug.LogError("AchievementsFuction could not find himself!");
			}
			return instance;
		}
	}
	
	/*
	void actionButton(UIButton.CONTROL_STATE state){
		this.mBtnAciton.SetControlState(state);
	}
	
	
	void actionButton(UIButton btnForAciton, bool particle){
		
	}
	*/
	
	/// <summary>
	/// 烟火爆炸吧～
	/// </summary>
	/// <param name="position">
	/// 参照物的三维坐标
	/// </param>
	/// <param name="delytime">
	/// 延迟时间
	/// </param>
	/// <returns>
	/// 
	/// </returns>
	IEnumerator bomb(Vector3 position, float delytime, bool isSecond){
		yield return new WaitForSeconds(delytime);
		if (isSecond) {
			Instantiate(mSecondParticle, position, Quaternion.identity);
		} else {
			Instantiate(mPreStartParticle, position, Quaternion.identity);
		}
	}
	
	/// <summary>
	/// 徽章亮起来啊～
	/// </summary>
	/// <param name="medal">
	/// 需要被闪亮起来的徽章
	/// </param>
	/// <param name="delytime">
	/// 延迟时间
	/// </param>
	/// <returns>
	/// 
	/// </returns>
	IEnumerator shiny(UIButton medal, float delytime){
		
		yield return new WaitForSeconds(delytime);
		Debug.Log("timedely:" + delytime);
		SoundController.Instance.Play("get_badge");
		medal.Hide(false);
		medal.SetControlState(UIButton.CONTROL_STATE.NORMAL);
	}
	
	///   <summary>   
  	///   展示胜利时的提示框
  	///   </summary>   
  	///   <param name="result">结果字符串比如00000</param>
	public IEnumerator showWinPanel(string result, string history){
		bool[] successed = {false, false, false, false, false};
		for (int i=0; i<5; i++){
			UIButton u = mBtnMedal[i];
			if (history[i] == '0') {
				u.SetControlState(UIButton.CONTROL_STATE.DISABLED);
				u.Hide(true);
			} else {
				successed[i] = true;
			}
		}
		
		StartCoroutine(bac.showBouns());
		yield return new WaitForSeconds(4f);
		//需要被新点亮的数量
		int shinyNumber = 0;
		
		//这里还要判断当初是否记录过数据,把数据放到success里面;预先要点亮对应的按钮,当这是没有动画效果的,或者说在这个panel出现之前应该先加载好
		//下面做一个假设,用于记录历史上的成功记录个数
		
		int reward = result.Length;
		for(int i = 0; i < reward; i ++){
			//if(calReal(result.Substring(i, 1)) && !successed[i]){
			if(calReal(result.Substring(i, 1))){
				StartCoroutine(shiny(mBtnMedal[i], shinyNumber * 1f));
				StartCoroutine(bomb(mBtnMedal[i].transform.position, shinyNumber * 1f - 0.25f, successed[i]));
				shinyNumber ++;
			}
		}
	}	
	
	/// <summary>
	/// 用来计算是真是假的逻辑,大家开看～正大综艺
	/// </summary>
	/// <param name="code">
	/// 传进来的真假字符串
	/// </param>
	/// <returns>
	/// 是真还是假,丫的,我是不是应该叫解码器
	/// </returns>
	bool calReal(string code){
		if(code.Equals(TRUE)){
			return true;
		}else{
			return false;
		}
	}
}
