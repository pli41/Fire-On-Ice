using UnityEngine;
using System.Collections;

public class FireBomb_Object : MonoBehaviour {

	public float forwardForce;
	public float upForce;
	public float explosionTime;
	public float explosionRadius;
	public float explosionDamage;
	public float explosionForce;

	private ParticleSystem fire;
	private GameObject explosionEffect;
	private bool initialized;
	private bool attached;
	private bool pickedByPlayer;
	private GameObject[] players;
	private GameObject[] tiles;
	private Collider[] colliders;

	// Use this for initialization
	void Start () {
		colliders = GetComponents<Collider> ();
		//Debug.Break ();
		players = GameObject.FindGameObjectsWithTag ("Player");
		fire = GetComponent<ParticleSystem>();
		explosionEffect = transform.Find ("Boom").gameObject;
		explosionEffect.SetActive (false);
		//Initialize ();
	}


	void Initialize(){

		Vector3 objVelocity = transform.forward * forwardForce + Vector3.up * upForce;
		GetComponent<Rigidbody> ().AddForce (objVelocity);
		Debug.Log (objVelocity);
		initialized = true;
		Invoke ("Explode", explosionTime);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(!initialized){
			Initialize();
		}
	}

	void Explode(){
		Debug.Log ("Explode");
		//Debug.Break();
		fire.Stop ();
		explosionEffect.SetActive (true);
		Invoke ("DestroyObj", explosionEffect.GetComponent<ParticleSystem>().duration);

		Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach(Collider col in hitColliders){
			float distance = Vector3.Distance(col.transform.position, transform.position);
			if(col.tag == "Player"){
				col.gameObject.GetComponent<PlayerHealth>().TakeDamage((int)(explosionDamage * (explosionRadius - distance) / explosionRadius), true);
				col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
			}
			else if(col.tag == "Island" && !pickedByPlayer){
				if(distance < explosionRadius){
					col.gameObject.GetComponent<MeltingIsland>().meltByExplode((int)(explosionDamage * (explosionRadius - distance) / explosionRadius));
				}
			}
			else if(col.tag == "Obstacle"){
				col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
			}

		}

		foreach(GameObject p in players){
			float distance = Vector3.Distance(p.transform.position, transform.position);
			if(distance < explosionRadius){

			}
		}
	}

	void DestroyObj(){
		Destroy (gameObject);
	}

	void DisableColliders(){
		foreach(Collider col in colliders){
			col.enabled = false;
		}
	}
	                 

	void OnTriggerEnter(Collider col){
		//GetComponent<Rigidbody>().isKinematic = true;
		DisableColliders ();
		GetComponent<Rigidbody> ().useGravity = false;
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		transform.parent = col.transform;
		if(col.tag == "Player"){
			pickedByPlayer = true;
		}

	}


}
