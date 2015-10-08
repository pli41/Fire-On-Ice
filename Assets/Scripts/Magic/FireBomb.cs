using UnityEngine;
using System.Collections;

public class FireBomb : Ability, Cooldown, Shootable {

	public float cooldown;
	public float bombSpeed;

	private FireBomb_Object fireBomb_object;

	// Use this for initialization
	void Start () {
		abilityReady = false;
		SetupCooldown ();
	}
	
	// Update is called once per frame
	void Update () {
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
			Shoot();
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
		SetupObj ();
		Instantiate (ability_object);
	}
	
	public override void SetupObj(){
		Debug.Log (ability_point);
		fireBomb_object = ability_object.GetComponent<FireBomb_Object> ();

		ability_object.transform.position = ability_point.position;
		ability_object.transform.rotation = owner.transform.rotation;
	}


}
