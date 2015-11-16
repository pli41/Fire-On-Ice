using UnityEngine;
using System.Collections;

public class FireBomb_Object : MonoBehaviour {

	public Ability ability;

	public float forwardForce;
	public float upForce;
	public float explosionTime;
	public float explosionRadius;
	public float explosionDamage;
	public float explosionForce;
	public float explosionFactor;

	public SetExplosion boomAudio;

	private ParticleSystem fire;
	private GameObject explosionEffect;
	private bool initialized;
	private bool attached;
	private bool pickedByPlayer;
	//private GameObject[] players;
	private GameObject[] tiles;
	private Collider[] colliders;
	Collider[] hitColliders;


	// Use this for initialization
	void Start () {
		colliders = GetComponents<Collider> ();
		//Debug.Break ();
		//players = GameObject.FindGameObjectsWithTag ("Player");
		fire = GetComponent<ParticleSystem>();
		explosionEffect = transform.Find ("Boom").gameObject;
		explosionEffect.SetActive (false);
		boomAudio.damage = explosionDamage;
		Physics.IgnoreCollision (GetComponent<SphereCollider>(), ability.owner.GetComponent<Collider>());
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
		//Debug.Break();
		transform.parent = null;
		fire.Stop ();
		explosionEffect.SetActive (true);
		Invoke ("DestroyObj", explosionEffect.GetComponent<ParticleSystem>().duration);

		hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach(Collider col in hitColliders){

			float distance = Vector3.Distance(col.transform.position, transform.position);

			if(col.tag == "Player"){
				if(pickedByPlayer){
					col.gameObject.GetComponent<PlayerHealth>().TakeDamage(
						(int)(explosionDamage), false, ability.owner.GetComponent<PlayerAttack>().playerNum);
				}
				else{
					col.gameObject.GetComponent<PlayerHealth>().TakeDamage(
						(int)(explosionDamage * (explosionRadius - distance) / explosionRadius)
						, false, ability.owner.GetComponent<PlayerAttack>().playerNum);
				}
				col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
			}
			else if(col.tag == "Island"){
				if(distance < explosionRadius){
					col.gameObject.GetComponent<MeltingIsland>().meltByExplode((int)(explosionDamage * (explosionRadius - distance) / explosionRadius * explosionFactor));
				}
			}
			else if(col.tag == "Island_New"){
				distance = Vector3.Distance(col.bounds.center, transform.position);
				if(distance < explosionRadius){
					col.gameObject.GetComponent<MeltingIsland_New>().meltByExplode(explosionDamage * (explosionRadius - distance) / explosionRadius * explosionFactor);
				}
			}
			else if(col.tag == "Obstacle"){
				col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
			}
		}
	}

	void DestroyObj(){
		Debug.Log ("obj destroyed");
		Destroy (gameObject);
	}

	void DisableColliders(){
		foreach(Collider col in colliders){
			col.enabled = false;
		}
	}
	                 

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player" || col.tag == "Island" || col.tag == "Island_New" || col.tag == "Obstacle"){
			GetComponent<Rigidbody>().isKinematic = true;
			DisableColliders ();
			//GetComponent<Rigidbody> ().detectCollisions = false;
			//GetComponent<Rigidbody> ().useGravity = false;
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
			transform.parent = col.transform;
			if(col.tag == "Player"){
				pickedByPlayer = true;
				//Debug.Break ();
			}
		}
	}


}
