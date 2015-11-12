
using UnityEngine;
using System.Collections;

public class FireSheild : Ability,Cooldown, CasterEffect {
	public float newMass;
	public float newSpeed;
	public float damageReduction;
	public float duration;

	private float oldSpeed;
	private float oldMass;
	private GameObject shield;
	void Start () {
		oldMass = owner.GetComponent<Rigidbody> ().mass;
		oldSpeed = gameObject.transform.parent.GetComponent<PlayerMovement> ().maxSpeed;
		SetupCooldown ();
	}
	
	// Update is called once per frame
	void Update () {
		timeUntilReset = (int)(cooldown - cdTimer + 1f);
		CooldownUpdate ();
	}
	
	public override void Cast()
	{
		if (abilityReady) {
			shield = Instantiate(ability_object) as GameObject;
			SetupObj();
			shield.transform.parent = ability_point.parent;
			CauseEffect();
			ResetCooldown();
		}
	}
	public override void SetupObj(){
		//Debug.Log (ability_point);
		shield.transform.rotation = owner.transform.rotation;
	}
	
	public  void SetupCooldown(){
		cdTimer = 0;
	}

	public  void CooldownUpdate(){
		if(cdTimer < cooldown){
			cdTimer += Time.deltaTime;
		}
		else{
			abilityReady = true;
		}
	}

	public  void ResetCooldown(){
		cdTimer = 0f;
		abilityReady = false;
	}

	public void CauseEffect (){
		owner.GetComponent<Rigidbody> ().mass = newMass;
		owner.GetComponent<PlayerMovement> ().maxSpeed = newSpeed;
		owner.GetComponent<PlayerHealth> ().damageReduction = damageReduction;
		Invoke ("EndEffect", duration);
	}

	public void EndEffect (){

		owner.GetComponent<Rigidbody> ().mass = oldMass;
		owner.GetComponent<PlayerMovement> ().maxSpeed = oldSpeed;
		owner.GetComponent<PlayerHealth> ().damageReduction = 1;
		Destroy (shield);
	}
}
