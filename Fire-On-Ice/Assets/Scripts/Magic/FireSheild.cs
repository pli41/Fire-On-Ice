
using UnityEngine;
using System.Collections;

public class FireSheild : Ability, Cooldown, CasterEffect, Shootable {
	public float newMass;
	public float newSpeed;
	public float damageReduction;
	public float duration;

	private bool created;
	private float oldSpeed;
	private float oldMass;
	private GameObject shield;
	private Animation anim;

	void Start () {
		anim = owner.GetComponent<Animation> ();
		oldMass = owner.GetComponent<Rigidbody> ().mass;
		oldSpeed = gameObject.transform.parent.GetComponent<PlayerMovement> ().maxSpeed;
		SetupCooldown ();
	}
	
	// Update is called once per frame
	void Update () {
        timeUntilReset = cooldown - cdTimer;
        CooldownUpdate ();
	}

	public void Shoot(){
		if(!created){
			shield = Instantiate(ability_object) as GameObject;
			SetupObj();
			shield.transform.parent = ability_point.parent;
			CauseEffect();
			Invoke("EnableMove", anim.GetClip("Attack2").length/2f);
			created = true;
		}
	}

	public override void Cast()
	{
		if (abilityReady) {
			anim.Play ("Attack2");
			anim.CrossFadeQueued ("Idle", 0.25f);
			DisableMove();
			Invoke("Shoot", anim.GetClip("Attack2").length/2f);
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

	public void DisableMove(){
		owner.GetComponent<PlayerMovement> ().canMove = false;
	}

	public void EnableMove(){
		owner.GetComponent<PlayerMovement> ().canMove = true;
	}

	public void CauseEffect (){
		owner.GetComponent<Rigidbody> ().mass = newMass;
		owner.GetComponent<PlayerMovement> ().maxSpeed = newSpeed;
		owner.GetComponent<PlayerHealth> ().damageReduction = damageReduction;
		ResetCooldown();
		Invoke ("EndEffect", duration);
	}

	public void EndEffect (){
		created = false;
		endCasted = true;
		owner.GetComponent<Rigidbody> ().mass = oldMass;
		owner.GetComponent<PlayerMovement> ().maxSpeed = oldSpeed;
		owner.GetComponent<PlayerHealth> ().damageReduction = 1;
		Destroy (shield);
	}
}
