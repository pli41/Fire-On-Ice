using UnityEngine;
using System.Collections;

public class PlayerItem : MonoBehaviour {

	private int joystickNum;

	public GameObject chestAround;
	public GameObject healingEffect;

	// Use this for initialization
	void Start () {
		joystickNum = GetComponent<PlayerAttack>().joystickNum;
	}
	
	// Update is called once per frame
	void Update () {
		if(ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.A, joystickNum)){
			if(chestAround){
				//Debug.Log("Open chest");
				chestAround.GetComponent<Chest>().Open();
			}

		}
	}
}
