using UnityEngine;
using System.Collections;

public class Flame_Object : MonoBehaviour {

	public Ability ability;
	public float damage;
	public float forceMagnitude;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnParticleCollision(GameObject other){
		if (other.tag == "Player") {
			PlayerHealth healthP = other.GetComponent<PlayerHealth> ();
			healthP.TakeDamage ((int)damage);
			Rigidbody rigidP = other.GetComponent<Rigidbody> ();
			Vector3 forceDir = rigidP.transform.position - ability.owner.transform.position;
			rigidP.AddForce(forceDir*forceMagnitude);
		}
	}
}
