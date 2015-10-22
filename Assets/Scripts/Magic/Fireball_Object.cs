using UnityEngine;
using System.Collections;

public class Fireball_Object : MonoBehaviour {

	public Ability ability;
	public ParticleSystem ps1;
	public ParticleSystem ps2;
	public ParticleSystem ps3;

	public float damage;
	public float speed = 5f;
	public float size;
	public float force = 300f;
	public float forceR = 50f;

	public AudioClip explosion;

	private bool disabled;
	private Chargable chargeTimer;
	private AudioSource audioS;

	// Use this for initialization
	void Start () {
		audioS = GetComponent<AudioSource> ();
		force = size * 100f + 200f;
		damage = size * 20f;
		
		ps1.startSize = size;
		ps2.startSize = size * 2.5f + 2f;
		ps3.startSize = size * 2.5f + 2f;
		
		disabled = false;
		Invoke("DestroyFire", 5f);
	}

	
	void DestroyFire(){
		Destroy (gameObject);
	}

	void OnParticleCollision(GameObject other){
		if (other.tag == "Player") {
			PlayerHealth healthP = other.GetComponent<PlayerHealth> ();
			healthP.TakeDamage ((int)damage, true);
			Rigidbody rigidP = other.GetComponent<Rigidbody> ();
			rigidP.AddExplosionForce (force, transform.position, forceR);
		}
		else if(other.tag == "Boss"){
			BossHealth health = other.GetComponent<BossHealth> ();
			health.TakeDamage ((int)damage);
		}
		PlayExplosion ();
		Invoke("DestroyFire", 2f);
	}

	void PlayExplosion(){
		float calculatedPitch = 2f - damage / 50f * 2f;
		if(calculatedPitch < 0.5f){
			calculatedPitch = 0.5f;
		}

		audioS.pitch = calculatedPitch;
		audioS.clip = explosion;
		//Debug.Break ();
		audioS.Play ();
	}
}
