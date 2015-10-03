using UnityEngine;
using System.Collections;

public class Fireball : Ability, Chargable, Shootable, CasterEffect {

	public GameObject enchantEffect;
	public GameObject onFireEffect;
	public float maxChargeT;
	public float onFireTime_max;
	public float cooldown;
	public float chargedSpeed;

	private Fireball_Object fireball_object;


	private float oldSpeed;
	private float chargedTime;
	private float chargeTimer;
	private float onFireTimer;

	void Start (){
		oldSpeed = owner.GetComponent<PlayerMovement> ().speed;
		enchantEffect = owner.transform.Find ("enchantEffect").gameObject;
		onFireEffect = owner.transform.Find ("onFireEffect").gameObject;
		abilityReady = false;
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
		if(cdTimer < cooldown_new){
			cdTimer += Time.deltaTime;
		}
		else{
			abilityReady = true;
		}
	}

	public override void Cast(){
		//Debug.Log ("Casting");
		if(abilityReady){
			Charge ();
		}
		else{
			Debug.Log("AbilityReady_cast: " + abilityReady);
			Debug.Log("Ability not ready.");
		}
	}

	public override void EndCast(){
		//Debug.Log ("Endcast");
		if(abilityReady){
			chargedTime = EndCharge ();
			Shoot ();
			ResetCooldown();
		}
	}

	public void Shoot(){
		Debug.Log ("Shoot");
		SetupObj ();
		Instantiate (ability_object);
		cooldown_new = cooldown * chargedTime / 3 + 0.5f; 
	}

	public override void SetupObj(){
		Debug.Log (ability_point);
		fireball_object = ability_object.GetComponent<Fireball_Object> ();
		fireball_object.size = chargedTime;
		fireball_object.ability = this;
		ability_object.transform.position = ability_point.position;
		ability_object.transform.rotation = owner.transform.rotation;
	}

	//Abilities with charging need to set triggerOnce to be false
	public void Charge(){
		triggerOnce = false;
		enchantEffect.SetActive (true);
		CauseEffect ();
		if(chargeTimer < maxChargeT){
			chargeTimer += Time.deltaTime;
		}
	}

	public float EndCharge(){
		float result = chargeTimer;
		chargeTimer = 0f;
		EndEffect ();
		enchantEffect.SetActive (false);
		return result;
	}

	public void CauseEffect(){
		//Debug.Log ("Onfire");
		owner.GetComponent<PlayerMovement> ().speed = chargedSpeed;
	}

	public void EndEffect(){
		Debug.Log ("Ceasefire");
		owner.GetComponent<PlayerMovement> ().speed = oldSpeed;
	}

}
