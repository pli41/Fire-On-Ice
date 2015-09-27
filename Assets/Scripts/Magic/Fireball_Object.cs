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

	private bool disabled;
	private Chargable chargeTimer;
	
	// Use this for initialization
	void Start () {
		force = size * 100f + 200f;
		damage = size * 20f;
		
		ps1.startSize = 0.01f;
		ps2.startSize = size * 2.5f + 2f;
		ps3.startSize = size * 2.5f + 2f;
		
		disabled = false;
	}

	
	void DestroyFire(){
		Destroy (gameObject);
	}

	void OnParticleCollision(GameObject other){
		if (other.tag == "Player") {
			PlayerHealth healthP = other.GetComponent<PlayerHealth> ();
			healthP.TakeDamage ((int)damage);
			Rigidbody rigidP = other.GetComponent<Rigidbody> ();
			rigidP.AddExplosionForce (force, transform.position, forceR);
		}
		else if(other.tag == "Boss"){
			BossHealth health = other.GetComponent<BossHealth> ();
			health.TakeDamage ((int)damage);
		}
		Invoke("DestroyFire", 1.5f);
	}
}
