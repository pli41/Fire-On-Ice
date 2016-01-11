using UnityEngine;
using System.Collections;

public class Fireball : Ability, Chargable, Shootable, CasterEffect {

	public GameObject enchantEffect;
	public GameObject onFireEffect;
	public float maxChargeT;
	public float onFireTime_max;

	private Fireball_Object fireball_object;


	//private float oldSpeed;
	private float chargedTime;
	private float chargeTimer;
	private float onFireTimer;
	private Animation anim;


	void Start (){
		anim = owner.GetComponent<Animation> ();
		//oldSpeed = owner.GetComponent<PlayerMovement> ().speed;
		//enchantEffect = owner.transform.Find ("enchantEffect").gameObject;
		onFireEffect = owner.transform.Find ("onFireEffect").gameObject;
		abilityReady = false;
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
		timeUntilReset = cooldown - cdTimer;
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

			if(chargedTime < maxChargeT){
				anim.Play ("Attack1");
				anim.CrossFadeQueued ("Idle", 0.25f);
				Invoke("Shoot", anim.GetClip("Attack1").length/2f);
			}
			else{
				anim.Play ("AttackCritical");
				anim.CrossFadeQueued ("Idle", 0.25f);
				Invoke("Shoot", anim.GetClip("AttackCritical").length/2f);
			}

			ResetCooldown();
		}
	}

	public void Shoot(){

		SetupObj ();
		Invoke ("EndEffect", anim.GetClip("Attack1").length/2f);
		Instantiate (ability_object);
	}

	public override void SetupObj(){
		//Debug.Log (ability_point);
		fireball_object = ability_object.GetComponent<Fireball_Object> ();
		fireball_object.size = chargedTime;
		fireball_object.ability = this;
		ability_object.transform.position = ability_point.position;
		ability_object.transform.rotation = owner.transform.rotation;
	}

	//Abilities with charging need to set triggerOnce to be false
	public void Charge(){
		anim.CrossFade ("Cast", 0.1f);
		anim.CrossFadeQueued ("Attack1", 0.1f);
		triggerOnce = false;
		owner.GetComponent<PlayerAttack>().enchanting = true;
		CauseEffect ();
		if(chargeTimer < maxChargeT){
			chargeTimer += Time.deltaTime;
		}
	}

	public float EndCharge(){
		owner.GetComponent<PlayerAttack>().enchanting = false;
		float result = chargeTimer;
		chargeTimer = 0f;
		return result;
	}

	public void CauseEffect(){
		owner.GetComponent<PlayerMovement> ().canMove = false;
	}

	public void EndEffect(){
		owner.GetComponent<PlayerMovement> ().canMove = true;
	}

}
