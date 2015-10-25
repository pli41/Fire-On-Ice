using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject[] players;
	//public GameObject boss;
	public UI_Manager UI_manager;
	public List<GameObject> playerList = new List<GameObject>();
	public bool enablePlayers;
	public bool enableBoss;



	// Use this for initialization
	void Awake () {
		ControllerInputWrapper.setPlatform ();
		ControllerInputWrapper.setControlTypes ();

		//players = GameObject.FindGameObjectsWithTag ("Player");
		//boss = GameObject.FindGameObjectWithTag ("Boss");

		SetupPlayers ();
		SetupPlayerAbilities ();
	}


	//Setup Players and playerlist
	void SetupPlayers(){
		for(int i = 0; i < GameSettings.numPlayers; i++){
			GameObject p = Instantiate(players[i]);
			playerList.Add(p);
		}
	}

	void SetupPlayerAbilities(){
		for(int i = 0; i < playerList.Count; i++ ){
			playerList[i].GetComponent<PlayerAttack>().joystickNum = GameSettings.playerController[i];
			
			//Setup Abilities
			Ability[] newAbilities = new Ability[3];
			for(int j = 0; j < 3; j++){
				newAbilities[j] = GameSettings.playerAbilitites[i, j];
			}
			playerList[i].GetComponent<PlayerAttack>().abilities = newAbilities;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
