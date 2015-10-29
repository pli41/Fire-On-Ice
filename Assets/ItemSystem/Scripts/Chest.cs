using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour {
	Animator anim;	// Use this for initialization
	public Item[] items;
	int selection;
	bool active;
	GameObject player;
	void Awake () {

		anim = transform.Find("Chest_cover").gameObject.GetComponent<Animator> ();
		//items = GameObject.FindGameObjectsWithTag ("Item");
		Debug.Log (items.Length);
		selection = Random.Range (0, items.Length);
		active = true;
	}
	public void Open(){
		if (active == true) {
			anim.SetBool ("Open", true);
			Destroy (gameObject, 2f);
			items[selection].takeEffect(player);
			Debug.Log (selection);
			active = false;
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			other.GetComponent<PlayerItem>().chestAround = gameObject;
			player = other.transform.gameObject;
		} 
	}
	
	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			GameObject playerChest = other.GetComponent<PlayerItem>().chestAround;
			if(gameObject.Equals(playerChest)){
				other.GetComponent<PlayerItem>().chestAround = null;
				player = null;
			}
		}
	}
}
