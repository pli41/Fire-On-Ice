using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

	public float damage = 20f;
	public float knockDist = 3f;
	public float coolDown = 0.5f;
	public float speed = 5f;

		
	Rigidbody rigid;
	bool disabled;
	bool destroyed;

	// Use this for initialization
	void Start () {


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

	}
	

	void DestroyFire(){
		Destroy (gameObject);
	}

	void Move(){
		Vector3 movement = transform.forward.normalized * speed *Time.deltaTime;
		rigid.MovePosition (transform.position + movement);
	}

	void OnParticleCollision(GameObject other){


		if (other.tag == "Player") {

			PlayerHealth healthP = other.GetComponent<PlayerHealth> ();

			healthP.TakeDamage ((int)damage);
			Debug.Log ("Hit");
			Rigidbody rigidP = other.GetComponent<Rigidbody> ();
			rigidP.AddExplosionForce (300f, transform.position, 50f);
			disabled = true;
		} else if (other.tag == "Obstacle") {
			EnemyHealth healthO = other.GetComponent<EnemyHealth> ();
			
			healthO.TakeDamage ((int)damage);
			Debug.Log ("Hit");
			Rigidbody rigidP = other.GetComponent<Rigidbody> ();
			rigidP.AddExplosionForce (300f, transform.position, 50f);
			disabled = true;
		} else {
			Debug.Log("out of world");
			Destroy(gameObject);
		}


	}
}
