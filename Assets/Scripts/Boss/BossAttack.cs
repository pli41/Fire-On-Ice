using UnityEngine;
using System.Collections;

public class BossAttack : MonoBehaviour {

	public Ability[] abilities = new Ability[4];

	public Transform magicPoint;

	private Ability currentAbility;
	private BossMovement bossMove;
	private bool casting1;
	private bool casting2;
	private bool casting3;

	// Use this for initialization
	void Start () {
		SetupAbilities ();
		bossMove = GetComponent<BossMovement> ();
	}

	void SetupAbilities(){
		for(int i = 0; i < abilities.Length; i++){
			if(abilities[i]){
				Ability ability_new = Instantiate(abilities[i]);
				ability_new.transform.parent = gameObject.transform;
				ability_new.owner = gameObject;
				ability_new.ability_point = magicPoint;
				abilities[i] = ability_new;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if(CheckMelee()){
			Debug.Log ("In melee range");
			currentAbility.Cast ();
		}

		Debug.Log ("Boss attack Activated");
		if (currentAbility) {
			Debug.Log("Current ability is not empty");
		}
		else{
			Debug.Log("Current ability is empty");
			ChangeAbility();
		}
	}

	bool CheckMelee(){
		Vector3 bossPos = transform.position;
		Vector3 targetPos = bossMove.currentTarget.transform.position;
		bossPos.Set (bossPos.x, 0, bossPos.z);
		targetPos.Set (targetPos.x, 0, targetPos.z);

		if (Vector3.Distance (bossPos, targetPos) < bossMove.meleeRange) {
			currentAbility = ChangeAbility (0);
			return true;
		} else {
			return false;
		}
	}

	Ability ChangeAbility(){
		int num = 0;

		return abilities[num];
	}

	Ability ChangeAbility(int num){

		return abilities [num];
	}


}
