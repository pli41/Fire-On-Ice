using UnityEngine;
using System.Collections;

public class SetExplosion : MonoBehaviour {

	public float damage;


	private float calculatedPitch;
	private AudioSource audioS;

	// Use this for initialization
	void Start () {
		audioS = GetComponent<AudioSource> ();
		CalculatePitch ();
		audioS.pitch = calculatedPitch;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CalculatePitch(){
		calculatedPitch = 3f - damage / 50f * 3f;
		if(calculatedPitch < 0.5f){
			calculatedPitch = 0.5f;
		}
	}
}
