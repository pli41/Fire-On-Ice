using UnityEngine;
using System.Collections;

public class DieWater : MonoBehaviour {

	public AudioClip[] oceanClips;
	private GameManager gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			col.gameObject.GetComponent<PlayerHealth>().TakeDamage(9999f, false, 0);
			PlayRandomAudio(oceanClips, GetComponent<AudioSource>());
		}
		else{
			Destroy(col.gameObject);
		}
	}

	void PlayRandomAudio(AudioClip[] clips, AudioSource audioS){
		if(!audioS.isPlaying && gm.playerList.Count > 2){
			audioS.Stop ();
			audioS.clip = clips [Random.Range (0, clips.Length-1)];
			audioS.Play ();
		}
	}
}
