using UnityEngine;
using System.Collections;

public class MagicChannel_Sound : MonoBehaviour {


	private float playTimer;
	private AudioSource audioS;

	// Use this for initialization
	void Awake () {
		audioS = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(audioS.isPlaying){
			playTimer += Time.deltaTime;
			if(playTimer < 3f){
				audioS.pitch = playTimer;
			}
		}
	}

	void OnDisable() {
		ResetPitch ();
	}

	void ResetPitch(){
		playTimer = 0f;
		audioS.pitch = 1f;
	}
}
