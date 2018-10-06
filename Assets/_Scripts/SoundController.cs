using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(AudioListener))]
public class SoundController : MonoBehaviour {
	
	public GameObject playerPrefab;
	private List<GameObject> players = new List<GameObject>();
	public string path = "_NewSounds/SoundEffects/";
	private GameObject tractorPlayer;
	
	public string pathBGM = "_NewSounds/BGM/";
	private string[] BGM = {
		"op_bg", 
		"spring1",
		"summer1",
		"autumn1",
		"winter1"
	};
	
	private static SoundController instance;	
		
	public static SoundController Instance {
		get 
		{
			if (instance == null)
			{
				instance = (SoundController)FindObjectOfType(typeof(SoundController));
			}
			if (!instance)
			{
				Debug.LogError("SoundController could not find himself!");
			}
			return instance;
		}
	}
	
	// Use this for initialization
	void Start () {
		BGMSelect();
	}
	
	public void Play (string sound) {
		bool played = false;
		foreach (GameObject player in players) {
			if (!player.audio.isPlaying) {
				player.audio.clip = Resources.Load(path + sound) as AudioClip;
				player.audio.Play();
				played = true;
				break;
			}
		}
		Debug.Log(played);
		if (!played) {
			GameObject player = Instantiate(playerPrefab) as GameObject;
			player.transform.parent = transform;
			player.audio.clip = Resources.Load(path + sound) as AudioClip;
			player.audio.Play();
			players.Add(player);
		}
	}
	
	public void PlayTractorRun () {
		tractorPlayer = Instantiate(playerPrefab) as GameObject;
		tractorPlayer.transform.parent = transform;
		tractorPlayer.transform.localPosition = new Vector3(0f, 0f, 0f);
		tractorPlayer.audio.clip = Resources.Load(path + "tractor_run") as AudioClip;
		// tractorPlayer.audio.volume = 0.5f;
		tractorPlayer.audio.loop = true;
		tractorPlayer.audio.Play();
		
	}
	
	public void PlayBGM (int id) {
		GameObject BGMPlayer = Instantiate(playerPrefab) as GameObject;
		BGMPlayer.transform.parent = transform;
		BGMPlayer.transform.localPosition = new Vector3(0f, 0f, 0f);
		BGMPlayer.audio.clip = Resources.Load(pathBGM + BGM[id]) as AudioClip;
		BGMPlayer.audio.volume = 0.5f;
		BGMPlayer.audio.loop = true;
		BGMPlayer.audio.Play();
	}
	
	int SelectRand (params int[] values) {
		return values[Random.Range(0,values.Length)];
	}
	
	public void BGMSelect () {
		if (Application.loadedLevelName == "Menus") {
			PlayBGM(0);
		} else {
			int selSongId = GlobalManager.currentBigLevel;
			PlayBGM(selSongId);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (tractorPlayer != null) {
			float v = Camera.main.gameObject.GetComponent<CameraFocus>().FocusTarget.rigidbody.velocity.x;
			float t = v * v / 100000f;
			tractorPlayer.audio.volume = t;
		}
	}
}
