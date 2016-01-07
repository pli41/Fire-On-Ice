using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public float duration;
	public GameObject ItemEffect;
    public string itemName;
    public Color textColor = Color.white;
	GameObject effect;
	GameObject objectAppear;
	//public GameObject speedingEffect;

	public virtual void takeEffect(GameObject player){
		Vector3 effectPos = new Vector3(player.transform.position.x,player.transform.position.y-0.1f,player.transform.position.z);
		effect = (Instantiate(ItemEffect,effectPos,Quaternion.identity) as GameObject);
		effect.transform.parent = player.transform;
		Vector3 objectPos = new Vector3(player.transform.position.x,player.transform.position.y+6.5f,player.transform.position.z);
		objectAppear = (Instantiate(gameObject,objectPos,Quaternion.identity) as GameObject);
		objectAppear.transform.parent = player.transform;
		Invoke ("deleteEffect", duration);
	}

	public virtual void deleteEffect (){
		Destroy(effect);
		Destroy(objectAppear);
	}
}
