using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnowStorm_Object : MonoBehaviour {

	public Ability ability;
	public int damage;
	public float damageIntervalT;
	
	public GameObject[] players;
	private List<GameObject> playerList = new List<GameObject>();

	private List<float> timers = new List<float>();

	void Start(){
		players = GameObject.FindGameObjectsWithTag ("Player");
		SetupPlayerList ();
		SetupTimers ();
	}

	void SetupTimers(){
		timers.Add(new float ());
		timers.Add(new float ());
	}

	            

	void SetupPlayerList(){
		foreach(GameObject p in players){
			playerList.Add(p);
		}
	}

	void CheckAllPlayers(){
		for(int i = 0; i < playerList.Count; i++){
			if(!playerList[i].activeInHierarchy){
				playerList.RemoveAt(i);
			}
		}
	}
	void Update(){
		CheckAllPlayers ();
		for(int i = 0; i < playerList.Count; i++ ){
			if(!playerList[i].GetComponent<PlayerAttack>().onFire){
				if(timers[i] < damageIntervalT){
					timers[i] += Time.deltaTime;
				}
				else{
					playerList[i].GetComponent<PlayerHealth>().TakeDamage(damage, false);
					timers[i] = 0;
				}
			}
		}
	}
}
