using UnityEngine;
using System.Collections;

public class RollingRock : Ability, Shootable, CastDelay, Cooldown {

	public GameObject enchantEffect;

	public float castTime;

	public float cooldown;
	
	private RollingRock_Object rollingRock_object;

	//For CastDelay
	private float delayTimer;
	public bool delayBool = true;
	private bool delaying;

	void Start (){
		handledEndCast = true;
		enchantEffect = owner.transform.Find ("enchantEffect").gameObject;
		abilityReady = true;
		SetupCooldown ();
	}
	
	public override void SetupAbility(){
		triggerOnce = true;
	}
	
	public void SetupCooldown(){
		cooldown_new = cooldown;
		cdTimer = 0f;
	}
	
	public void ResetCooldown(){
		cdTimer = 0f;
		abilityReady = false;
	}
	
	void Update (){
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
			Debug.Log("AbilityReady_cast: " + abilityReady);
			Debug.Log("Ability not ready.");
		}
	}
	
	public override void EndCast(){
		Debug.Log ("Endcast");
		if(abilityReady){
			CancelInvoke();
			Shoot ();
			CastDelayEnd();
		}
	}
	
	public void Shoot(){
		SetupObj ();
		Instantiate (ability_object); 
	}
	
	public override void SetupObj(){
		Debug.Log (ability_point);
		rollingRock_object = ability_object.GetComponent<RollingRock_Object> ();
		rollingRock_object.ability = this;
		ability_object.transform.position = ability_point.position;
		ability_object.transform.rotation = owner.transform.rotation;
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
