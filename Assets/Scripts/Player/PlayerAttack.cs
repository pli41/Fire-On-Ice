using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
	public string playerString;
	public Transform magicPoint;
	public Ability[] abilities = new Ability[3];

	private bool casting1;
	private bool casting2;
	private bool casting3;

	void Start ()
	{
		SetupAbilities ();
		casting1 = false;
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
	

	IEnumerator EndAbility(int abilityNum, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);

		abilities[abilityNum].EndCast();
	}


	void Update ()
	{
		//This part can be structrually changed too. Will do it after having a lot of abilities.
		if(Input.GetAxisRaw ("PS4_R2_" + playerString) > 0)
		{	
			Debug.Log("Get PS4_R2_" + playerString);
			if(abilities[0].abilityReady){
				casting1 = true;
				abilities[0].Cast();
				
				if(abilities[0].triggerOnce){
					casting1 = false;
					StartCoroutine(EndAbility(0, 0.2f));
				}
			}
		}
		else if(casting1){
			casting1 = false;
			abilities[0].EndCast();
		}



		if(Input.GetAxisRaw ("PS4_L2_" + playerString) > 0)
		{
			Debug.Log("Get PS4_L2_" + playerString);
			if(abilities[1].abilityReady){
				casting2 = true;
				abilities[1].Cast();

				if(abilities[1].triggerOnce){
					casting2 = false;
					StartCoroutine(EndAbility(1, 0.2f));
				}
			}


		}
		else if(casting2){
			casting2 = false;
			abilities[1].EndCast();
		}

		//Debug.Log (Input.GetAxisRaw ("PS4_R1_" + playerString));

		if(Input.GetAxisRaw ("PS4_R1_" + playerString) > 0)
		{
			if(abilities[2].abilityReady && !casting1){
				Debug.Log(playerString + " is casting");
				casting3 = true;
				abilities[2].Cast();
				if(abilities[2].triggerOnce){
					casting3 = false;
					StartCoroutine(EndAbility(2, 0.2f));
				}
			}
		}
		else if(casting3){
			//Debug.Log("End Cast 3rd ability");
			casting3 = false;
			if(!abilities[2].handledEndCast){
				abilities[2].EndCast();
			}
		}
	}
}
