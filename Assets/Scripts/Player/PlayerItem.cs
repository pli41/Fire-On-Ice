using UnityEngine;
using System.Collections;

public class PlayerItem : MonoBehaviour {

	private int joystickNum;

	public GameObject chestAround;
	public GameObject healingEffect;

	private GameManager gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
		joystickNum = GetComponent<PlayerAttack>().joystickNum;
	}
	
	// Update is called once per frame
	void Update () {
		if (gm.GameInProgress) {
			if(ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.A, joystickNum)){
				if(chestAround){
					//Debug.Log("Open chest");
					chestAround.GetComponent<Chest>().Open();
				}
				
			}
		}
	}
}
