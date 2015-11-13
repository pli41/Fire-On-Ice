using UnityEngine;
using System.Collections;

public class Fireball_Object : MonoBehaviour {

	public Ability ability;
	public ParticleSystem ps1;
	public ParticleSystem ps2;
	public ParticleSystem ps3;
	public ParticleSystem ps4;
	public ParticleSystem ps5;
	public GameObject explosionEffect;

	public float explosionDamage;
	public float explosionRadius;
	public float explosionForce;
	public float explosionFactor;

	public float damage;
	public float speed;
	public float size;
	public float force = 300f;
	public float forceR = 50f;

	public AudioClip explosion;

	private bool disabled;
	private Chargable chargeTimer;
	private AudioSource audioS;
	private Rigidbody rigid;

	// Use this for initialization
	void Start () {
		audioS = GetComponent<AudioSource> ();

		explosionRadius = size * 1.5f + 1f;
		explosionForce = size * 500f + 300f;
		explosionDamage = size * 8f + 10f;
		
		ps1.startSize = size;
		ps2.startSize = size * 1.5f + 1f;
		ps3.startSize = size * 1.5f + 1f;
		ps4.startSize = size * 1.5f + 1f;
		ps5.startSize = size * 1.5f + 3f;

		rigid = GetComponent<Rigidbody>();
		rigid.velocity = transform.forward * speed;
		Physics.IgnoreCollision (ability.owner.GetComponent<Collider> (), GetComponent<Collider> ());
		disabled = false;

	}

	void Explode(){
		//Debug.Break();
		rigid.velocity = Vector3.zero;
		ps1.Stop ();
		ps2.Stop ();
		ps3.Stop ();
		ps4.Stop ();
		explosionEffect.SetActive (true);
		
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach(Collider col in hitColliders){
			
			float distance = Vector3.Distance(col.transform.position, transform.position);
			
			if(col.tag == "Player"){
				col.gameObject.GetComponent<PlayerHealth>().TakeDamage(
					explosionDamage, false, ability.owner.GetComponent<PlayerAttack>().playerNum);
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

	void OnTriggerEnter(Collider other){
//		if (other.tag == "Player") {
//			PlayerHealth healthP = other.GetComponent<PlayerHealth> ();
//			healthP.TakeDamage ((int)damage, true, ability.owner.GetComponent<PlayerAttack>().playerNum);
//			Rigidbody rigidP = other.GetComponent<Rigidbody> ();
//			rigidP.AddExplosionForce (force, transform.position, forceR);
//		}
//		else if(other.tag == "Boss"){
//			BossHealth health = other.GetComponent<BossHealth> ();
//			health.TakeDamage ((int)damage);
//		}
		Explode ();
		GetComponent<Collider> ().enabled = false;
		PlayExplosion ();
		Destroy (gameObject, 2f);
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
