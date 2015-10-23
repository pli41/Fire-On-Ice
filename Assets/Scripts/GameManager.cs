using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject[] players;
	public GameObject boss;

	public List<GameObject> playerList = new List<GameObject>();
	public bool enablePlayers;
	public bool enableBoss;



	// Use this for initialization
	void Awake () {
		ControllerInputWrapper.setPlatform ();
		ControllerInputWrapper.setControlTypes ();

		players = GameObject.FindGameObjectsWithTag ("Player");
		boss = GameObject.FindGameObjectWithTag ("Boss");
		SetupPlayerList ();
		DisableAll ();
	}

	void DisableAll(){
		//Debug.Log ("Disabled all");
		for(int i = 0; i < playerList.Count; i++ ){
			playerList[i].SetActive(false);
		}

			boss.SetActive(false);	
	}

	void SetupPlayerList(){
		foreach(GameObject p in players){
			playerList.Add(p);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Q)){
			for(int i = 0; i < playerList.Count; i++ ){
				playerList[i].SetActive(true);
			}
			enablePlayers = true;
		}

		if(Input.GetKeyDown(KeyCode.W)){
			boss.SetActive(true);
			enableBoss = true;
		}
	}
}
