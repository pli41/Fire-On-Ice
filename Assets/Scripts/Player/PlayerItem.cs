using UnityEngine;
using System.Collections;

public class PlayerItem : MonoBehaviour {

	public int joystickNum;

	public GameObject chestAround;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			if(chestAround){
				Debug.Log("Open chest");
				chestAround.GetComponent<Chest>().Open();
				gameObject.GetComponent<PlayerHealth>().TakeDamage(-chestAround.GetComponent<Chest>().healthRG, false);
			}

		}
	}
}
