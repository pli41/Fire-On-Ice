﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject[] players;
	//public GameObject boss;
	public UI_Manager UI_manager;
	public List<GameObject> playerList = new List<GameObject>();

	public Transform[] spawnPoints;

	public bool GameInProgress;
	
	private GameObject[] activePlayers;

	public int winnerNum;
	public CameraMove cameraMove;


	public AudioClip startClip;
	public AudioSource audioS;

	bool gameStarted;

	// Use this for initialization
	void Awake () {
		audioS = GetComponents<AudioSource> () [1];

		//ControllerInputWrapper.setPlatform ();
		//ControllerInputWrapper.setControlTypes ();

		//players = GameObject.FindGameObjectsWithTag ("Player");
		//boss = GameObject.FindGameObjectWithTag ("Boss");
		gameStarted = false;
		SetupPlayers ();
		SetupPlayerAbilities ();
		Invoke ("CameraStart", 2f);

	}



	//Setup Players and playerlist
	void SetupPlayers(){
		for(int i = 0; i < GameSettings.numPlayers; i++){
			//Debug.Log(i);
			GameObject p = Instantiate(players[i]);
			p.transform.position = spawnPoints[i].position;
			p.GetComponent<PlayerAttack>().playerNum = i+1;
			playerList.Add(p);

		}
	}

	void SetupPlayerAbilities(){
		for(int i = 0; i < playerList.Count; i++ ){
			playerList[i].GetComponent<PlayerAttack>().joystickNum = GameSettings.playerController[i];
			
			//Setup Abilities
			Ability[] newAbilities = new Ability[4];
			for(int j = 0; j < 4; j++){
				newAbilities[j] = GameSettings.playerAbilitites[i, j];
			}
			playerList[i].GetComponent<PlayerAttack>().abilities = newAbilities;
		}
	}

	void CameraStart(){
		cameraMove.startMovement = true;
		UI_manager.HideTutorial ();
	}

	public void StartGame(){
		//Debug.Log ("Start audio");
		audioS.Stop ();
		audioS.clip = startClip;
		audioS.Play ();
		GameInProgress = true;
		gameStarted = true;
	}

	GameObject CheckForWinnner(){
		activePlayers = GameObject.FindGameObjectsWithTag ("Player");
		if(activePlayers.Length <= 1){
			return activePlayers[0];
		}
		else{
			return null;
		}
	}

	// Update is called once per frame
	void Update () {
		if(GameInProgress){
			GameObject winner = CheckForWinnner ();

			if(winner){
				winnerNum = winner.GetComponent<PlayerAttack>().playerNum;
				GameInProgress = false;
				UI_manager.ShowWinScreen(winnerNum, FindPlayerWithMostDamage(), FindPlayerOpenedMostChests());
			}
		}
		else{
			if(cameraMove.endMovement && !gameStarted){
				UI_manager.ShowArrows();
				cameraMove.endMovement = false;
			}
		}
	}

	int FindPlayerOpenedMostChests(){
		int playerNum = 0;
		float highestChestNum = 0;
		
		for(int i = 0; i < playerList.Count; i ++){
			float playerChestNum = playerList[i].GetComponent<PlayerItem>().chestOpenedNum;
			if(playerChestNum > highestChestNum){
				highestChestNum = playerChestNum;
				playerNum = playerList[i].GetComponent<PlayerAttack>().playerNum;
			}
		}
		return playerNum;
	}

	int FindPlayerWithMostDamage(){
		int playerNum = 0;
		float highestDamage = 0;

		for(int i = 0; i < playerList.Count; i ++){
			float playerDamage = playerList[i].GetComponent<PlayerAttack>().damageDealt;
			if(playerDamage > highestDamage){
				highestDamage = playerDamage;
				playerNum = playerList[i].GetComponent<PlayerAttack>().playerNum;
			}
		}
		return playerNum;
	}




}
