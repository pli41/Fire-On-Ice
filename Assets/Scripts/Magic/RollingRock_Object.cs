using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RollingRock_Object : MonoBehaviour {

	public Ability ability;
	public int damage;
	public float forceMagnitude;
	public float explosionRadius;

	public float maxSpeed; 
	public float rollingTime;
	public float acc;

	public float explosionForceMag;

	private Rigidbody rigid;
	private float timer;
	private MeshRenderer rend;
	private Transform explosion;
	private Transform fire;
	private bool exploded;

	public GameObject[] players;
	private List<GameObject> playerList = new List<GameObject>();

	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");
		SetupPlayerList ();
		fire = transform.Find ("Fire");
		explosion = transform.Find ("Explosion");
		rend = GetComponent<MeshRenderer> ();
		rigid = GetComponent<Rigidbody> ();
	}

	void SetupPlayerList(){
		foreach(GameObject p in players){
			playerList.Add(p);
		}
	}

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
		rigid.velocity = Vector3.zero;
		rend.enabled = false;
		explosion.gameObject.SetActive (true);
		fire.gameObject.SetActive (false);
		CheckPlayers ();
		Invoke ("DestroyRock", 4f);
	}

	void CheckPlayers(){
		foreach(GameObject p in players){
			if(Vector3.Distance(p.transform.position, transform.position) < explosionRadius){
				Debug.Log("Explosion in range");
				p.GetComponent<Rigidbody>().AddExplosionForce(forceMagnitude, transform.position, explosionRadius);
				p.GetComponent<PlayerHealth>().TakeDamage(damage, true);
			}
		}
	}

	void DestroyRock(){
		Destroy (gameObject);
	}
}
