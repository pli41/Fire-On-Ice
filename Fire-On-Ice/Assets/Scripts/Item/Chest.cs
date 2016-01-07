using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour {
    public TextMesh floatingText;
	Animator anim;	// Use this for initialization
	public Item[] items;
	int selection;
	bool active;
	GameObject player;
	public GameObject indicator;
	int playersAround;
	void Awake () {

		anim = transform.Find("Chest_cover").gameObject.GetComponent<Animator> ();
		//items = GameObject.FindGameObjectsWithTag ("Item");
		Debug.Log (items.Length);
		selection = Random.Range (0, items.Length);
		active = true;
	}
	public void Open(){
		if (active == true) {
            GameObject obj = (GameObject)Instantiate(floatingText.gameObject, Vector3.zero, new Quaternion());
            obj.GetComponent<TextMesh>().text = items[selection].itemName;
            obj.GetComponent<TextMesh>().color = items[selection].textColor;
			anim.SetBool ("Open", true);
			Destroy (gameObject, 2f);
			items[selection].takeEffect(player);
            obj.transform.parent = player.transform;
            obj.transform.localPosition = Vector3.zero;

			Debug.Log (selection);
			active = false;
		}
	}

    void Update(){
		if(playersAround > 0){
			indicator.SetActive(true);
		}
		else{
			indicator.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			if(active){
				other.GetComponent<PlayerItem>().chestAround = gameObject;
				player = other.transform.gameObject;

				playersAround ++;

			}
		} 
	}
	
	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			GameObject playerChest = other.GetComponent<PlayerItem>().chestAround;
			if(gameObject.Equals(playerChest)){
				other.GetComponent<PlayerItem>().chestAround = null;
				player = null;
			}
			playersAround--;
		}
	}
	
}
