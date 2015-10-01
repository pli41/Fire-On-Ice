using UnityEngine;
using System.Collections;

public class Rage : Ability, CastDelay {

	public float cooldown;
	public float castTime;

	private Camera main;
	private bool delaying;
	private bool delayBool;
	// Use this for initialization
	void Start () {
	
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
				owner.GetComponent<PlayerMovement>().disabled = true;
			}
		}
	}
	
	public void CastDelayEnd(){
		ResetCooldown();
		owner.GetComponent<PlayerMovement>().disabled = false;
		delaying = false;
	}

}
