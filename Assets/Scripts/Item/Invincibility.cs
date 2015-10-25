using UnityEngine;
using System.Collections;

public class Invincibility : MonoBehaviour {

	public float duration;
	public GameObject invincibilityEffect;
	//public GameObject speedingEffect;
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			GetComponent<Renderer>().enabled = false;
			GetComponent<SphereCollider>().enabled = false;
			GetComponent<BoxCollider>().enabled = false;
			Vector3 SpeedPos = new Vector3(other.transform.position.x,other.transform.position.y-0.3f,other.transform.position.z);
			GameObject effect = (Instantiate(invincibilityEffect,SpeedPos,Quaternion.identity) as GameObject);
			effect.transform.parent = other.transform;
			StartCoroutine(DisableEffect(other.transform,duration,effect));
		}
		//Destroy (gameObject);
	}
	IEnumerator DisableEffect(Transform player, float delay,GameObject effect){
		yield return new WaitForSeconds (delay);
		Destroy (gameObject);
		Destroy (effect);
	}
}
