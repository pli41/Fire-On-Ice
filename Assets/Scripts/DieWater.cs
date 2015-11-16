using UnityEngine;
using System.Collections;

public class DieWater : MonoBehaviour {

	public AudioClip[] oceanClips;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			col.gameObject.GetComponent<PlayerHealth>().TakeDamage(100f, false, 0);
			PlayRandomAudio(oceanClips, GetComponent<AudioSource>());
		}
		else{
			Destroy(col.gameObject);
		}
	}

	void PlayRandomAudio(AudioClip[] clips, AudioSource audioS){
		audioS.Stop ();
		audioS.clip = clips [(int)(Random.Range (0, clips.Length) - 0.01f)];
		audioS.Play ();
	}
}
