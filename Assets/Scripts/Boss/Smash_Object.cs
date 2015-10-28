using UnityEngine;
using System.Collections;

public class Smash_Object : MonoBehaviour {

	public Ability ability;
	public float damage;
	public float forceMagnitude;


	void OnTriggerEnter(Collider other){
		//Debug.Log ("Boss hit");
		if (other.tag == "Player") {
			PlayerHealth healthP = other.GetComponent<PlayerHealth> ();
			//healthP.TakeDamage ((int)damage, true);
			Rigidbody rigidP = other.GetComponent<Rigidbody> ();
			Vector3 forceDir = rigidP.transform.position - ability.owner.transform.position;
			rigidP.AddForce(forceDir*forceMagnitude);
		}
	}
}
