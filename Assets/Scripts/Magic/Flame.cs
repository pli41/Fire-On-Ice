using UnityEngine;
using System.Collections;

public class Flame : Ability, Cooldown, CasterEffect, Chargable {
	
	public float cooldown;
	public float new_speed;

	public GameObject onFireEffect;
	private Flame_Object flame_object;
	private float owner_old_speed;


	// Use this for initialization
	void Start () {
		SetupAbility ();

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

	public void SetupCooldown(){
		cooldown_new = cooldown;
		cdTimer = 0f;
	}
	
	public void ResetCooldown(){
		cdTimer = 0f;
		abilityReady = false;
	}

	public override void Cast(){
		//Debug.Log ("Casting flame");
		Charge ();
	}
	
	public override void EndCast(){
		//Debug.Log ("End-Cast flame");
		EndCharge ();
	}
	
	public override void SetupObj(){}
	
	public override void SetupAbility(){
		owner_old_speed = owner.GetComponent<PlayerMovement> ().speed;
		ability_object = Instantiate(ability_object);
		ability_object.transform.parent = transform;
		flame_object = ability_object.GetComponent<Flame_Object> ();
		flame_object.ability = this;
		ability_object.SetActive (false);
		onFireEffect = owner.transform.Find ("onFireEffect").gameObject;
	}

	public void CauseEffect (){
		//Debug.Log ("Onfire");
		owner.GetComponent<PlayerHealth> ().onFire = true;
		owner.GetComponent<PlayerMovement> ().speed = new_speed;
	}

	public void EndEffect (){
		//Debug.Log ("Ceasefire");
		owner.GetComponent<PlayerHealth> ().onFire = false;
		owner.GetComponent<PlayerMovement> ().speed = owner_old_speed;
	}

	public void Charge (){
		triggerOnce = false;
		ability_object.transform.position = ability_point.position;
		ability_object.transform.rotation = owner.transform.rotation;
		ability_object.SetActive (true);
		//ability_object.GetComponent<ParticleSystem> ().Play();
		CauseEffect ();
	}
	
	public float EndCharge(){
		//ability_object.GetComponent<ParticleSystem> ().Clear ();
		ability_object.SetActive (false);
		EndEffect ();
		return 0f;
	}
	
}
