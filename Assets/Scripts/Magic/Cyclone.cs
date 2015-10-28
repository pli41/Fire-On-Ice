using UnityEngine;
using System.Collections;

public class Cyclone : Ability, Cooldown, CastDelay, Shootable {
	
	public float castTime;
	private Cyclone_Object cyclone_object;
	
	//For CastDelay
	private float delayTimer;
	public bool delayBool = true;
	private bool delaying;
	
	void Start (){
		handledEndCast = true;;
		abilityReady = true;
		SetupCooldown ();
	}
	
	public override void SetupAbility(){
		triggerOnce = true;
	}
	
	public void SetupCooldown(){
		cdTimer = 0f;
	}
	
	public void ResetCooldown(){
		cdTimer = 0f;
		abilityReady = false;
	}
	
	void Update (){
		timeUntilReset = (int)(cooldown - cdTimer + 1f);
		CooldownUpdate ();
	}
	
	public void CooldownUpdate(){
		if(cdTimer < cooldown){
			cdTimer += Time.deltaTime;
		}
		else{
			abilityReady = true;
		}
	}
	
	public override void Cast(){
		//Debug.Log ("Casting");
		if(abilityReady){
			CastDelayStart();
		}
		else{
			//Debug.Log("AbilityReady_cast: " + abilityReady);
			//Debug.Log("Ability not ready.");
		}
	}
	
	public override void EndCast(){
		//Debug.Log ("Endcast");
		if(abilityReady){
			CancelInvoke();
			Shoot ();
			CastDelayEnd();
		}
	}
	
	public void Shoot(){
		SetupObj ();
	}
	
	public override void SetupObj(){
		Debug.Log (ability_point);

		cyclone_object = ability_object.GetComponent<Cyclone_Object> ();
		cyclone_object.ability = this;

		ability_object.transform.position = ability_point.position;
		ability_object.transform.rotation = owner.transform.rotation;
		Instantiate (ability_object); 
	}
	
	public void CastDelayStart(){
		if(delayBool){
			if(!delaying){
				//Debug.Log("Endcast will be called");
				owner.GetComponent<PlayerAttack>().enchanting = true;
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
		owner.GetComponent<PlayerAttack>().enchanting = false;
		owner.GetComponent<PlayerMovement>().disabled = false;
		delaying = false;
	}
}
