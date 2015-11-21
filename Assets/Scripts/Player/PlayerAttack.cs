using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
	//start from 1
	public int joystickNum;

	public int playerNum;

	public Transform magicPoint;

	public Ability[] abilities = new Ability[4];

	public bool enchanting;
	public GameObject enchantEffect;

	private bool casting1;
	private bool casting2;
	private bool casting3;
	private bool casting4;

	private Animation anim;
	private GameManager gm;

	public float damageDealt;
	public bool disabled;

	void Start ()
	{
		anim = GetComponent<Animation> ();
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
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
		if (gm.GameInProgress) {
			//Handle enchant Effect
			if (!disabled) {
				//This part can be structrually changed too. Will do it after having a lot of abilities.

				if (ControllerInputWrapper.GetTriggerRaw (ControllerInputWrapper.Triggers.LeftTrigger, joystickNum) > 0) {	
					if (!casting2 && !casting3 && !casting4) {
						if (abilities [0].abilityReady) {
							casting1 = true;
							abilities [0].Cast ();
							if (abilities [0].triggerOnce) {
								casting1 = false;
								StartCoroutine (EndAbility (0, 0.2f));
							}
						}
					}
				} else if (casting1) {
					casting1 = false;
					if (!abilities [0].handledEndCast) {
						abilities [0].EndCast ();
					}
				}
				
				
				
				if (ControllerInputWrapper.GetButton (ControllerInputWrapper.Buttons.LeftBumper, joystickNum)) {
					if (!casting1 && !casting3 && !casting4) {
						if (abilities [1].abilityReady) {
							casting2 = true;
							abilities [1].Cast ();
							if (abilities [1].triggerOnce) {
								casting2 = false;
								StartCoroutine (EndAbility (1, 0.1f));
							}
						}
					}
				} else if (casting2) {
					casting2 = false;
					if (!abilities [1].handledEndCast) {
						abilities [1].EndCast ();
					}
				}
				
				//Debug.Log (Input.GetAxisRaw ("PS4_R1_" + playerString));
				
				if (ControllerInputWrapper.GetButton (ControllerInputWrapper.Buttons.RightBumper, joystickNum)) {
					if (!casting1 && !casting2 && !casting4) {
						if (abilities [2].abilityReady) {
							//Debug.Log(joystickNum + " is casting");
							casting3 = true;
							abilities [2].Cast ();
							if (abilities [2].triggerOnce) {
								casting3 = false;
								StartCoroutine (EndAbility (2, 0.1f));
							}
						}
					}
				} else if (casting3) {
					//Debug.Log("End Cast 3rd ability");
					casting3 = false;
					if (!abilities [2].handledEndCast) {
						abilities [2].EndCast ();
					}
				}

				if (ControllerInputWrapper.GetTriggerRaw (ControllerInputWrapper.Triggers.RightTrigger, joystickNum) > 0) {
					if (!casting1 && !casting2 && !casting3) {
						if (abilities [3].abilityReady) {
							//Debug.Log(joystickNum + " is casting");
							casting4 = true;
							abilities [3].Cast ();
							if (abilities [3].triggerOnce) {
								casting4 = false;
								StartCoroutine (EndAbility (3, 0.1f));
							}
						}
					}
				} else if (casting4) {
					//Debug.Log("End Cast 3rd ability");
					casting4 = false;
					if (!abilities [3].handledEndCast) {
						abilities [3].EndCast ();
					}
				}
				
				if (enchanting) {
					enchantEffect.SetActive (true);
				} else {
					enchantEffect.SetActive (false);
				}

			}
			
		}
	}

}
