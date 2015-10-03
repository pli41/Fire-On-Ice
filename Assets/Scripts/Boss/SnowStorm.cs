using UnityEngine;
using System.Collections;

public class SnowStorm : Ability, Cooldown, CasterEffect{

	public SnowStorm_Object snowstorm_Object;
	public bool endCastHandled;

	public float cooldown;
	public float duration;



	// Use this for initialization
	void Start () {
		endCastHandled = true;
		SetupAbility ();
	}
	
	// Update is called once per frame
	void Update () {
		CooldownUpdate ();
	}
	
	public override void Cast(){
		if(abilityReady){
			ability_object.SetActive (true);
			//ability_object.GetComponent<ParticleSystem> ().Play();
			abilityReady = false;
			CauseEffect();
			Invoke("EndCast", duration);
		}
	}
	
	public override void EndCast(){
		Debug.Log ("Snowstorm endcast called");
		CancelInvoke ();
		EndEffect ();
		ability_object.SetActive (false);
		ResetCooldown ();
		owner.GetComponent<BossAttack> ().casting = false;
	}
	
	public override void SetupObj(){}
	
	public override void SetupAbility(){
		ability_object = Instantiate(ability_object);
		//ability_object.transform.parent = transform;
		snowstorm_Object = ability_object.GetComponent<SnowStorm_Object> ();
		snowstorm_Object.ability = this;
		ability_object.SetActive (false);
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
		cooldown_new = cooldown;
		cdTimer = 0f;
	}
	
	public void ResetCooldown(){
		cdTimer = 0f;
		abilityReady = false;
	}

	public void CauseEffect (){
		Debug.Log ("Cause effect starts");
		owner.GetComponent<BossMovement> ().disabled = true;
	}

	public void EndEffect (){
		Debug.Log ("Cause effect ends");
		owner.GetComponent<BossMovement> ().disabled = false;
	}
}
