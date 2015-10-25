using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
	//start from 1
	public int joystickNum;
	public Transform magicPoint;

	public Ability[] abilities = new Ability[3];

	public bool enchanting;
	public GameObject enchantEffect;

	private bool casting1;
	private bool casting2;
	private bool casting3;

	void Start ()
	{
		enchantEffect = transform.Find ("enchantEffect").gameObject;
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
		//Handle enchant Effect
		if(enchanting){
			enchantEffect.SetActive(true);
		}
		else{
			enchantEffect.SetActive(false);
		}

		//This part can be structrually changed too. Will do it after having a lot of abilities.
		if(ControllerInputWrapper.GetTriggerRaw(ControllerInputWrapper.Triggers.RightTrigger, joystickNum) > 0)
		{	
			//Debug.Log("Get PS4_R2_" + joystickNum);
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
			if(!abilities[0].handledEndCast){
				abilities[0].EndCast();
			}
		}



		if(ControllerInputWrapper.GetTriggerRaw (ControllerInputWrapper.Triggers.LeftTrigger, joystickNum) > 0)
		{
			//Debug.Log("Get PS4_L2_" + joystickNum);
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
			if(!abilities[1].handledEndCast){
				abilities[1].EndCast();
			}
		}

		//Debug.Log (Input.GetAxisRaw ("PS4_R1_" + playerString));

		if(ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.RightBumper, joystickNum))
		{
			if(abilities[2].abilityReady && !casting1){
				//Debug.Log(joystickNum + " is casting");
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
