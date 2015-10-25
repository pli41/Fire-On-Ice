using UnityEngine;
using System.Collections;

public class CdReduction : MonoBehaviour {
	public float duration;
	Ability ability;
	float cdOld;
	public GameObject CdEffect;


	//public GameObject speedingEffect;
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			GetComponent<Renderer>().enabled = false;
			GetComponent<SphereCollider>().enabled = false;
			GetComponent<BoxCollider>().enabled = false;
			//ability = other.transform.GetComponent<PlayerkeySkill>().currentMagic;
			cdOld = ability.cooldown;
			ability.cooldown = ability.cooldown/4f;
			ability.abilityReady = true;
			ability.cdTimer = 0f;

			Vector3 SpeedPos = new Vector3(other.transform.position.x,other.transform.position.y-0.3f,other.transform.position.z);
			GameObject effect = (Instantiate(CdEffect,SpeedPos,Quaternion.identity) as GameObject);
			effect.transform.parent = other.transform;
			StartCoroutine(DisableEffect(other.transform,duration,effect));
		}
		//Destroy (gameObject);
	}

	IEnumerator DisableEffect(Transform player, float delay,GameObject effect){
		yield return new WaitForSeconds (delay);
		ability.cooldown = cdOld;
		Destroy (gameObject);
		Destroy (effect);
	}
}
