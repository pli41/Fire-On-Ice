using UnityEngine;
using System.Collections;

public class SpeedPotion : MonoBehaviour {
	public float duration;
	public float increasement;
	public GameObject speedingEffect;
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			GetComponent<Renderer>().enabled = false;
			GetComponent<SphereCollider>().enabled = false;
			GetComponent<BoxCollider>().enabled = false;
			other.transform.GetComponent<PlayerMovement_Key> ().speed *= increasement;
			Vector3 SpeedPos = new Vector3(other.transform.position.x,other.transform.position.y-0.3f,other.transform.position.z);
			GameObject effect = (Instantiate(speedingEffect,SpeedPos,Quaternion.identity) as GameObject);
			effect.transform.parent = other.transform;
			StartCoroutine(DisableEffect(other.transform,duration,effect));


		}
	}
	IEnumerator DisableEffect(Transform player, float delay,GameObject effect){
		yield return new WaitForSeconds (delay);
		player.GetComponent<PlayerMovement_Key> ().speed /= increasement;
		Destroy (gameObject);
		Destroy (effect);
	}
}
