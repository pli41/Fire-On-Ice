using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

	public ParticleSystem ps1;
	public ParticleSystem ps2;
	public ParticleSystem ps3;

	public float damage = 20f;
	public float coolDown = 0.5f;
	public float speed = 5f;
	public float size;
	public float force = 300f;
	public float forceR = 50f;
		
	Rigidbody rigid;
	bool disabled;
	bool destroyed;

	// Use this for initialization
	void Start () {


		force = size * 100f + 200f;
		damage = size * 30f;

		ps1.startSize = 0.01f;
		ps2.startSize = size * 2.5f + 2f;
		ps3.startSize = size * 2.5f + 2f;

		destroyed = false;
		disabled = false;
		rigid = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(!disabled && !destroyed){
			//Move ();
		}
		else if(!destroyed && disabled){
			Invoke("DestroyFire", 1.5f);
			destroyed = true;
		}

		//change parameters based on size

		//ps1.startSize = size / 2f;

	}
	

	void DestroyFire(){
		Destroy (gameObject);
	}

	void Move(){
		Vector3 movement = transform.forward.normalized * speed *Time.deltaTime;
		rigid.MovePosition (transform.position + movement);
	}

	void OnParticleCollision(GameObject other){
	
		if (other.tag == "Player1") {
			PlayerHealth healthP = other.GetComponent<PlayerHealth> ();
			healthP.TakeDamage ((int)damage);
			Rigidbody rigidP = other.GetComponent<Rigidbody> ();
			rigidP.AddExplosionForce (force, transform.position, forceR);
			disabled = true;
		}
		else if(other.tag == "Player2"){
			PlayerHealth healthP = other.GetComponent<PlayerHealth> ();
			healthP.TakeDamage ((int)damage);
			Rigidbody rigidP = other.GetComponent<Rigidbody> ();
			rigidP.AddExplosionForce (force, transform.position, forceR);
			disabled = true;
		}
		else {
			Invoke("DestroyFire", 2.0f);
		}
	}
}
