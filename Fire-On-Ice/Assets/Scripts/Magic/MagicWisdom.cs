using UnityEngine;
using System.Collections;

public class MagicWisdom : Ability, Cooldown {

	public GameObject magicWisdom_Object;
	public float effectLength;
	public float YOffset;
	private GameObject createdEffect;

	// Use this for initialization
	void Start () {
		handledEndCast = true;;
		abilityReady = true;
		SetupCooldown ();
		SetupObject ();
	}

	void SetupObject(){
		createdEffect = (GameObject)Instantiate (magicWisdom_Object, owner.transform.position, owner.transform.rotation);
		createdEffect.transform.parent = owner.transform;
		createdEffect.transform.localPosition = new Vector3 (0, YOffset, 0);;
		if(createdEffect.activeInHierarchy){
			createdEffect.SetActive (false);
		}
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
		if(!abilityReady){
			if(cdTimer < cooldown){
				cdTimer += Time.deltaTime;
			}
			else{
				abilityReady = true;
			}
		}
	}

	public override void Cast(){
		//Debug.Log ("Casting");
		if(abilityReady){
			ResetAbilityCooldowns();
			EnableEffect();
			EndCast();
		}
	}

	/// <summary>
	/// Resets other abilities cooldowns
	/// </summary>
	public void ResetAbilityCooldowns(){
		Ability[] abilities = owner.GetComponent<PlayerAttack> ().abilities;
		foreach(Ability ability in abilities){
			if(!(ability is MagicWisdom)){
				ability.cdTimer = ability.cooldown;
			}
		}
	}

	public override void EndCast(){
		//Debug.Log ("Endcast");
		if(abilityReady){
			Invoke ("DisableEffect", effectLength);
			ResetCooldown();
		}
	}

	void EnableEffect(){
		createdEffect.SetActive (true);
	}

	void DisableEffect(){
		createdEffect.SetActive (false);
	}
}
