using UnityEngine;
using System.Collections;

public class Fireball : Ability, Chargable, Shootable, CasterEffect {

	public GameObject enchantEffect;
	public GameObject onFireEffect;
	public float maxChargeT;
	public readonly float onFireTime_max = 3f;
	public float cooldown;

	private Fireball_Object fireball_object;

	private float chargedTime;
	private float chargeTimer;
	private float onFireTimer;



	void Start (){

		enchantEffect = owner.transform.Find ("enchantEffect").gameObject;
		onFireEffect = owner.transform.Find ("onFireEffect").gameObject;
		abilityReady = false;
		fireball_object = ability_object.GetComponent<Fireball_Object> ();
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
		Debug.Log ("Casting");
		if(abilityReady){
			Charge ();
		}
		else{
			Debug.Log("AbilityReady_cast: " + abilityReady);
			Debug.Log("Ability not ready.");
		}
	}

	public override void EndCast(){
		Debug.Log ("Endcast");
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
		CauseEffect ();
	}

	public override void SetupObj(){
		Debug.Log (ability_point);
		fireball_object = ability_object.GetComponent<Fireball_Object> ();
		fireball_object.size = chargedTime;
		fireball_object.ability = this;
		ability_object.transform.position = ability_point.position;
		ability_object.transform.rotation = owner.transform.rotation;
	}

	public void Charge(){
		triggerOnce = false;
		enchantEffect.SetActive (true);
		if(chargeTimer < maxChargeT){
			chargeTimer += Time.deltaTime;
		}
	}

	public float EndCharge(){
		float result = chargeTimer;
		chargeTimer = 0f;
		enchantEffect.SetActive (false);
		return result;
	}

	public void CauseEffect(){
		Debug.Log ("Onfire");
		CancelInvoke ();
		onFireEffect.SetActive (true);
		owner.GetComponent<PlayerAttack> ().onFire = true;
		Invoke ("EndEffect", onFireTime_max);
	}

	public void EndEffect(){
		Debug.Log ("Ceasefire");
		onFireEffect.SetActive (false);
		owner.GetComponent<PlayerAttack> ().onFire = false;
	}

}
