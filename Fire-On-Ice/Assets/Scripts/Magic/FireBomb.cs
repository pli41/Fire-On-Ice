using UnityEngine;
using System.Collections;

public class FireBomb : Ability, Cooldown, Shootable, CasterEffect {
	
	public float bombSpeed;

	private FireBomb_Object fireBomb_object;
	private Animation anim;


	// Use this for initialization
	void Start () {
		anim = owner.GetComponent<Animation> ();
		abilityReady = false;
		SetupCooldown ();
	}
	
	// Update is called once per frame
	void Update () {
		timeUntilReset = (int)(cooldown - cdTimer + 1f);
		CooldownUpdate ();
	}

	//Cooldown interface
	public void SetupCooldown(){
		cdTimer = 0f;
	}
	
	public void ResetCooldown(){
		cdTimer = 0f;
		abilityReady = false;
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
			CauseEffect();
			anim.Play ("Attack2");
			anim.CrossFadeQueued ("Idle", 0.25f);
			Invoke("Shoot", anim.GetClip("Attack2").length/2f);
			ResetCooldown();
		}
		else{
			Debug.Log("Ability not ready.");
		}
	}

	public override void EndCast(){
		//Debug.Log ("Endcast");
	}
	
	public void Shoot(){
		Debug.Log ("Shoot");
		Invoke("EndEffect", anim.GetClip("Attack2").length/2f);
		SetupObj ();
		Instantiate (ability_object);
	}
	
	public override void SetupObj(){
		Debug.Log (ability_point);
		fireBomb_object = ability_object.GetComponent<FireBomb_Object> ();
		fireBomb_object.ability = this;
		ability_object.transform.position = ability_point.position;
		ability_object.transform.rotation = owner.transform.rotation;
	}

	public void CauseEffect(){
		owner.GetComponent<PlayerMovement> ().canMove = false;
	}
	
	public void EndEffect(){
		owner.GetComponent<PlayerMovement> ().canMove = true;
	}
}
