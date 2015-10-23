using UnityEngine;
using System.Collections;

public class FireSword : Ability, Cooldown, CastDelay {

	public enum STATE
	{
		NotSummoned,
		Summoning,
		Summoned,
		Disappear
	};

	public float castTime;
	public float cooldown;
	public float duration;
	public STATE state;


	private FireSword_Object firesword_Object;


	//For ability object
	public float forceMagnitude;
	public int damage;

	//For CastDelay
	private float delayTimer;
	public bool delayBool = true;
	private bool delaying;


	void Start () {
		SetupAbility ();
		state = STATE.NotSummoned;
		handledEndCast = true;
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

		if(abilityReady){
			if(state == STATE.NotSummoned){
				CastDelayStart();
			}
			else if (state == STATE.Summoned){
				ability_object.GetComponent<FireSword_Object>().Slash();
			}
		}


	}
	
	public override void EndCast(){
		Debug.Log ("End-Cast sword");
		if(state == STATE.Summoning){
			CastDelayEnd();
		}
	}

	//Called before the sword is summoned
	public override void SetupObj(){
		ability_object.transform.position = ability_point.position;
		ability_object.SetActive (true);
		ability_object.GetComponent<FireSword_Object> ().active = true;
	}

	//Called in start
	public override void SetupAbility(){
		abilityReady = true;
		ability_object = Instantiate(ability_object);
		ability_object.transform.parent = owner.transform.Find("magicPoint");
		firesword_Object = ability_object.GetComponent<FireSword_Object> ();
		firesword_Object.damage = damage;
		firesword_Object.ability = this;
		firesword_Object.forceMagnitude = forceMagnitude;
		ability_object.SetActive (false);
	}

	public void CastDelayStart(){
		if(delayBool){
			if(!delaying){
				Debug.Log("Endcast will be called");
				owner.GetComponent<PlayerAttack>().enchanting = true;
				owner.GetComponent<PlayerMovement>().disabled = true;
				Invoke("EndCast", castTime);
				delaying = true;
			}
			else{
				state = STATE.Summoning;
			}
		}
	}
	
	public void CastDelayEnd(){
		owner.GetComponent<PlayerAttack>().enchanting = false;
		owner.GetComponent<PlayerMovement>().disabled = false;
		SetupObj ();
		delaying = false;
		state = STATE.Summoned;
		Invoke ("Reset", duration);
	}

	public void Reset(){
		ability_object.GetComponent<FireSword_Object> ().active = true;
		ability_object.SetActive (false);
		ResetCooldown ();
		state = STATE.NotSummoned;
	}
}
