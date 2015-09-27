using UnityEngine;
using System.Collections;

public class Smash : Ability, Cooldown{

	public float cooldown;

	public Smash_Object smash_Object;

	public bool endCastHandled;

	private float particleEffectTime;
	// Use this for initialization
	void Start () {
		endCastHandled = true;
		SetupAbility ();
		particleEffectTime = ability_object.GetComponent<ParticleSystem> ().duration;
	}
	
	// Update is called once per frame
	void Update () {
		CooldownUpdate ();
	}

	public override void Cast(){
		if(abilityReady){
			ability_object.transform.position = ability_point.position;
			ability_object.transform.rotation = owner.transform.rotation;
			ability_object.SetActive (true);
			//ability_object.GetComponent<ParticleSystem> ().Play();
			Invoke("EndCast", particleEffectTime-2f);
			abilityReady = false;
		}
	}
	
	public override void EndCast(){
		CancelInvoke ();
		ability_object.SetActive (false);
	}
	
	public override void SetupObj(){}
	
	public override void SetupAbility(){
		ability_object = Instantiate(ability_object);
		//ability_object.transform.parent = transform;
		smash_Object = ability_object.GetComponent<Smash_Object> ();
		smash_Object.ability = this;
		ability_object.SetActive (false);
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


}
