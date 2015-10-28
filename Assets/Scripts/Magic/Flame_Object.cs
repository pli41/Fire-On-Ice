using UnityEngine;
using System.Collections;

public class Flame_Object : MonoBehaviour {

	public Ability ability;
	public float damage;
	public float forceMagnitude;

	void OnParticleCollision(GameObject other){
		if (other.tag == "Player") {
			PlayerHealth healthP = other.GetComponent<PlayerHealth> ();
			healthP.TakeDamage ((int)damage, true, ability.owner.GetComponent<PlayerAttack>().playerNum);
			Rigidbody rigidP = other.GetComponent<Rigidbody> ();
			Vector3 forceDir = rigidP.transform.position - ability.owner.transform.position;
			rigidP.AddForce(forceDir*forceMagnitude);
		}
		else if(other.tag == "Boss"){
			BossHealth health = other.GetComponent<BossHealth> ();
			health.TakeDamage ((int)damage);

		}
	}
}
