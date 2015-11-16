using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rage : Ability, CastDelay {
	
	public float castTime;

	public int numberGenerated;
	public float generateRange;
	public Transform newObsPoint;
	public GameObject obstacleModel;

	private Camera main;
	private bool delaying;
	private bool delayBool = true;
	//private List<GameObject> obstacles;
	// Use this for initialization
	void Start () {
		newObsPoint = owner.transform.Find ("NewObsZone");
		//obstacles = new List<GameObject> ();
	}
	
	public void ResetCooldown(){
		cdTimer = 0f;
		abilityReady = false;
	}
	
	void Update (){
		CooldownUpdate ();
	}

	public override void Cast(){
		//Debug.Log ("Casting");
		if(abilityReady){
			Debug.Log("Cast rage");
			CastDelayStart();
		}
		else{
			Debug.Log("AbilityReady_cast: " + abilityReady);
			Debug.Log("Ability not ready.");
		}
	}
	
	public override void EndCast(){
		Debug.Log ("Endcast");
		if(abilityReady){
			CancelInvoke();
			CastDelayEnd();
			ResetCooldown();
			owner.GetComponent<BossAttack> ().casting = false;
		}
	}
	

	public void CooldownUpdate(){
		if(cdTimer < cooldown){
			cdTimer += Time.deltaTime;
		}
		else{
			abilityReady = true;
		}
	}

	public void CreateObstacles(){
		for(int i = 0; i < numberGenerated; i++){
			GameObject newObs = Instantiate(obstacleModel);
			Transform newObsTran = newObs.transform;
			Random.seed = Random.Range(0, 100);
			newObsTran.position.Set(newObsTran.position.x + Random.Range(-generateRange, generateRange), 
			                        newObsTran.position.y,  
			                        newObsTran.position.z + Random.Range(-generateRange, generateRange));
			Debug.Log("Position: " + newObsTran.position);
		}
	}

	public void ObstaclesFall(){

	}

	public void CastDelayStart(){
		if(delayBool){
			if(!delaying){
				Debug.Log("Endcast will be called");
				Invoke("EndCast", castTime);
				delaying = true;
			}
			else{
				owner.GetComponent<BossMovement>().disabled = true;
			}
		}
	}
	
	public void CastDelayEnd(){
		CreateObstacles();
		owner.GetComponent<BossMovement>().disabled = false;
		delaying = false;
	}

}
