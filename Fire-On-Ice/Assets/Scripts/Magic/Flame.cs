﻿using UnityEngine;
using System.Collections;

public class Flame : Ability, Cooldown, CasterEffect, Chargable {

	public GameObject onFireEffect;
	private Flame_Object flame_object;
	private Animation anim;

	// Use this for initialization
	void Start () {
		onFireEffect = owner.transform.Find ("onFireEffect").gameObject;
		triggerOnce = false;
		handledEndCast = false;
		anim = owner.GetComponent<Animation> ();
		SetupAbility ();
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

	public void SetupCooldown(){
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
		ability_object = Instantiate(ability_object);
		//ability_object.transform.parent = transform;
		ability_object.transform.position = owner.transform.Find("magicPoint").position;
		ability_object.transform.rotation = owner.transform.Find("magicPoint").rotation;
		flame_object = ability_object.GetComponent<Flame_Object> ();
		flame_object.ability = this;
		ability_object.SetActive (false);
	}

	public void CauseEffect (){
		Debug.Log ("Onfire");
		owner.GetComponent<PlayerHealth> ().onFire = true;
		owner.GetComponent<PlayerMovement> ().canMove = false;
		//owner.GetComponent<PlayerMovement> ().canTurn = false;
	}

	public void EndEffect (){
		Debug.Log ("Ceasefire");
		owner.GetComponent<PlayerHealth> ().onFire = false;
		owner.GetComponent<PlayerMovement> ().canMove = true;
		//owner.GetComponent<PlayerMovement> ().canTurn = true;
	}

	public void Charge (){
		triggerOnce = false;
		ability_object.transform.position = ability_point.position;
		ability_object.transform.rotation = owner.transform.rotation;
		ability_object.SetActive (true);
		anim.CrossFade ("Cast", 0.1f);
		anim.CrossFadeQueued ("Idle", 0.25f);
		onFireEffect.SetActive (true);
		//ability_object.GetComponent<ParticleSystem> ().Play();
		CauseEffect ();
	}
	
	public float EndCharge(){
		//ability_object.GetComponent<ParticleSystem> ().Clear ();
		onFireEffect.SetActive (false);
		ability_object.SetActive (false);
		anim.CrossFade ("Idle", 0.25f);
		EndEffect ();
		return 0f;
	}
	
}
