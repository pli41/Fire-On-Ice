using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RollingRock_Object : MonoBehaviour {

	public Ability ability;
	public int damage;
	public float forceMagnitude;
	public float explosionRadius;
	public int rollingDamage;
	public float rollingMeltFactor;

	public float maxSpeed; 
	public float rollingTime;
	public float acc;

	public float explosionForceMag;

	public SetExplosion boomAudio;

	private Rigidbody rigid;
	private float timer;
	private MeshRenderer rend;
	private Transform explosion;
	private Transform fire;
	private bool exploded;
	private AudioSource audioS;

	//public GameObject[] players;
	//private List<GameObject> playerList = new List<GameObject>();

	// Use this for initialization
	void Start () {
		audioS = GetComponent<AudioSource> ();
		//players = GameObject.FindGameObjectsWithTag ("Player");
		//SetupPlayerList ();
		fire = transform.Find ("Fire");
		explosion = transform.Find ("Explosion");
		rend = GetComponent<MeshRenderer> ();
		rigid = GetComponent<Rigidbody> ();
		boomAudio.damage = damage;
	}

//	void SetupPlayerList(){
//		foreach(GameObject p in players){
//			playerList.Add(p);
//		}
//	}

	// Update is called once per frame
	void Update () {
		if(!exploded){
			Vector3 deltaSpeed = transform.forward * acc * Time.deltaTime;
			
			if(rigid.velocity.magnitude < maxSpeed){
				rigid.velocity += deltaSpeed;
			}
			
			//Watch for explosion
			if(timer < rollingTime){
				timer += Time.deltaTime;
			}
			else{
				Explode();
				exploded = true;
			}
		}
	}

	void Explode(){
		Debug.Log ("EXPLODE");
		audioS.Stop ();
		rigid.velocity = Vector3.zero;
		rend.enabled = false;
		rigid.useGravity = false;
		explosion.parent = null;
		explosion.gameObject.SetActive (true);
		fire.gameObject.SetActive (false);

		Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach(Collider col in hitColliders){
			float distance = Vector3.Distance(col.transform.position, transform.position);
			if(col.tag == "Player"){
				col.gameObject.GetComponent<PlayerHealth>().TakeDamage(
					(int)(damage * (explosionRadius - distance) / explosionRadius), false, ability.owner.GetComponent<PlayerAttack>().playerNum);
				col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(forceMagnitude, transform.position, explosionRadius);
			}
			else if(col.tag == "Island"){
				if(distance < explosionRadius){
					col.gameObject.GetComponent<MeltingIsland>().meltByExplode((damage * (explosionRadius - distance) / explosionRadius));
				}
			}
			else if(col.tag == "Island_New"){
				distance = Vector3.Distance(col.bounds.center, transform.position);
				if(distance < explosionRadius){
					col.gameObject.GetComponent<MeltingIsland_New>().meltByExplode((damage * (explosionRadius - distance) / explosionRadius));
				}
			}
			else if(col.tag == "Obstacle"){
				col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(forceMagnitude, transform.position, explosionRadius);
			}
		}

		Invoke ("DestroyRock", 4f);
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Player"){
			col.gameObject.GetComponent<PlayerHealth>().TakeDamage(rollingDamage, false, ability.owner.GetComponent<PlayerAttack>().playerNum);
		}
		else if(col.gameObject.tag == "Island"){
			col.gameObject.GetComponent<MeltingIsland>().meltByExplode(rollingDamage*rollingMeltFactor);
		}
		else if(col.gameObject.tag == "Island_New"){
			col.gameObject.GetComponent<MeltingIsland_New>().meltByExplode(rollingDamage*rollingMeltFactor);
		}
    }

//	void CheckPlayers(){
//		foreach(GameObject p in players){
//			if(Vector3.Distance(p.transform.position, transform.position) < explosionRadius){
//				Debug.Log("Explosion in range");
//				p.GetComponent<Rigidbody>().AddExplosionForce(forceMagnitude, transform.position, explosionRadius);
//				p.GetComponent<PlayerHealth>().TakeDamage(damage, true, ability.owner.GetComponent<PlayerAttack>().playerNum);
//			}
//		}
//	}

	void DestroyRock(){
		Destroy (explosion.gameObject);
		Destroy (gameObject);
	}
}
