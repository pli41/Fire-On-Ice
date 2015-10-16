using UnityEngine;
using System.Collections;

public class PlayerItem : MonoBehaviour {

	public int joystickNum;

	public GameObject chestAround;
	public GameObject healingEffect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.A, joystickNum)){
			if(chestAround){
				Debug.Log("Open chest");
				chestAround.GetComponent<Chest>().Open();
				gameObject.GetComponent<PlayerHealth>().TakeDamage(-chestAround.GetComponent<Chest>().healthRG, false);
				Vector3 healingPos = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y-0.5f,gameObject.transform.position.z);
				GameObject effect = (Instantiate(healingEffect,healingPos,Quaternion.identity) as GameObject);
				effect.transform.parent = gameObject.transform;
				Destroy(effect, 6);
			}

		}
	}
}
