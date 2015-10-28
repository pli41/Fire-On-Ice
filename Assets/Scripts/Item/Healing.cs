using UnityEngine;
using System.Collections;

public class Healing : MonoBehaviour {

	public float duration;
	public int healingAmount;
	public GameObject HealingEffect;


	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			GetComponent<Renderer>().enabled = false;
			GetComponent<SphereCollider>().enabled = false;
			GetComponent<BoxCollider>().enabled = false;
			other.transform.GetComponent<PlayerHealth>().TakeDamage(-healingAmount, false, 0);
			Vector3 SpeedPos = new Vector3(other.transform.position.x,other.transform.position.y-0.3f,other.transform.position.z);
			GameObject effect = (Instantiate(HealingEffect,SpeedPos,Quaternion.identity) as GameObject);
			effect.transform.parent = other.transform;
			StartCoroutine(DisableEffect(other.transform,duration,effect));
		}
	}


	IEnumerator DisableEffect(Transform player, float delay,GameObject effect){
		yield return new WaitForSeconds (delay);
		Destroy (gameObject);
		Destroy (effect);
	}
}
