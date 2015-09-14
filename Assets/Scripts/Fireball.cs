using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

	public ParticleSystem ps1;
	public GameObject ps2G;
	public ParticleSystem ps2;
	public GameObject ps3G;
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
		ps1 = GetComponent<ParticleSystem> ();
		ps2 = ps2G.GetComponent<ParticleSystem> ();
		ps3 = ps3G.GetComponent<ParticleSystem> ();
		ps1.startSize = 2f;
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
			Invoke("DestroyFire", 0.5f);
			destroyed = true;
		}

		//change parameters based on size
		force = size * 50f + 200f;
		damage = size * 8f;
		//ps1.startSize = size / 2f;
		ps2.startSize = size;
		ps3.startSize = size;

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
			PlayerHealth_1 healthP = other.GetComponent<PlayerHealth_1> ();
			healthP.TakeDamage ((int)damage);
			Debug.Log ("Hit");
			Rigidbody rigidP = other.GetComponent<Rigidbody> ();
			rigidP.AddExplosionForce (force, transform.position, forceR);
			disabled = true;
		}
		else if(other.tag == "Player2"){
			PlayerHealth_2 healthP = other.GetComponent<PlayerHealth_2> ();
			healthP.TakeDamage ((int)damage);
			Debug.Log ("Hit");
			Rigidbody rigidP = other.GetComponent<Rigidbody> ();
			rigidP.AddExplosionForce (force, transform.position, forceR);
			disabled = true;
		}
		else {
			Debug.Log("out of world");
			Destroy(gameObject);
		}
	}
}
