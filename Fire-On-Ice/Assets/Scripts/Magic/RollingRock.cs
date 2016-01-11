using UnityEngine;
using System.Collections;

public class RollingRock : Ability, Shootable, CastDelay, Cooldown {

	public GameObject enchantEffect;

	public float castTime;
	

	private RollingRock_Object rollingRock_object;

	//For CastDelay
	private float delayTimer;
	public bool delayBool = true;
	private bool delaying;
	private Animation anim;

	void Start (){
		anim = owner.GetComponent<Animation> ();
		handledEndCast = true;
		enchantEffect = owner.transform.Find ("enchantEffect").gameObject;
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
			anim.Play ("AttackCritical");
			anim.CrossFadeQueued ("Idle", 0.25f);
			Invoke("Shoot", anim.GetClip("AttackCritical").length/2f);
		}
	}
	
	public void Shoot(){
		SetupObj ();
		Instantiate (ability_object);
		Invoke("CastDelayEnd", anim.GetClip("AttackCritical").length/2f);
	}
	
	public override void SetupObj(){
		//Debug.Log (ability_point);
		rollingRock_object = ability_object.GetComponent<RollingRock_Object> ();
		rollingRock_object.ability = this;
		ability_object.transform.position = ability_point.position + transform.forward * 1f;
		ability_object.transform.rotation = owner.transform.rotation;
	}

	public void CastDelayStart(){
		if(delayBool){
			if(!delaying){
				//Debug.Log("Endcast will be called");

				//anim.CrossFadeQueued ("AttackCritical", 0.1f);
				owner.GetComponent<PlayerAttack>().enchanting = true;
				Invoke("EndCast", castTime);
				delaying = true;
			}
			else{
				anim.CrossFade ("Cast", 0.1f);
				owner.GetComponent<PlayerMovement>().canMove = false;
			}
		}
	}

	public void CastDelayEnd(){
		ResetCooldown();
		owner.GetComponent<PlayerAttack>().enchanting = false;
		owner.GetComponent<PlayerMovement>().canMove = true;
		delaying = false;
	}
}
