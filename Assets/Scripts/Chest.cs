using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Chest : MonoBehaviour {
	Animator anim;	// Use this for initialization
	public int healthRG;

	public 

	void Awake () {
		anim = transform.Find("Chest_cover").gameObject.GetComponent<Animator> ();
	}
	
	public void Open(){
		anim.SetBool("Open",true);
		GetComponent<AudioSource> ().Play ();
		Destroy(gameObject, 3f);
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			other.GetComponent<PlayerItem>().chestAround = gameObject;
		} 
	}

	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			GameObject playerChest = other.GetComponent<PlayerItem>().chestAround;
			if(gameObject.Equals(playerChest)){
				other.GetComponent<PlayerItem>().chestAround = null;
			}
		}
	}
}
