using UnityEngine;
using System.Collections;

public class BossAttack : MonoBehaviour {

	public Ability[] abilities = new Ability[4];

	public Transform magicPoint;

	private Ability currentAbility;
	private BossMovement bossMove;

	public bool casting;

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
		if(bossMove.currentTarget != null){
			if(!casting){
				if(CheckMelee()){
					//Debug.Log ("In melee range");
					currentAbility = ChangeAbility();
				}
				else{
					//Debug.Log("not in melee");
				}

				if(Input.GetKeyDown(KeyCode.T)){
					currentAbility = ChangeAbility(1);
				}

				if(Input.GetKeyDown(KeyCode.Y)){
					currentAbility = ChangeAbility(2);
				}


				//Debug.Log ("Boss attack Activated");

				if (currentAbility) {
					if(currentAbility.abilityReady){
						//Debug.Log("Current ability is ready");
						currentAbility.Cast();
						casting = true;
					}
					else{

						//Debug.Log("Current ability is not ready");
					}

					//Debug.Log("Current ability is not empty");
				}
			}
			else{
				//Debug.Log("Already casting");
			}
		}
		else{
			//Debug.Log("No target found");
			//Debug.Log(bossMove.currentTarget);
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
		} 
		else {
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
