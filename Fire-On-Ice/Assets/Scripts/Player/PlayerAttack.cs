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

    private bool[] castings = new bool[4];

	//private bool casting1;
	//private bool casting2;
	//private bool casting3;
	//private bool casting4;
 //   private bool casting_Key;

	private Animation anim;
	private GameManager gm;

	public float damageDealt;
	public bool disabled;

    public int currentAbilityNum_Keyboard;

	void Start ()
	{
		anim = GetComponent<Animation> ();
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
		enchantEffect = transform.Find ("enchantEffect").gameObject;
		SetupAbilities ();
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
                HandleControl(joystickNum != 0);
			}
			
		}
	}

    void HandleControl(bool isController)
    {
        if (isController)
        {
            //Controller
            if (ControllerManager.GetTrigger(ControllerInputWrapper.Triggers.LeftTrigger, joystickNum, true) > 0)
            {
                if (!castings[1] && !castings[2] && !castings[3])
                {
                    if (abilities[0].abilityReady)
                    {
                        castings[0] = true;
                        abilities[0].Cast();
                        if (abilities[0].triggerOnce)
                        {
                            castings[0] = false;
                            StartCoroutine(EndAbility(0, 0.2f));
                        }
                    }
                }
            }
            else if (castings[0])
            {
                castings[0] = false;
                if (!abilities[0].handledEndCast)
                {
                    abilities[0].EndCast();
                }
            }



            if (ControllerManager.GetButton(ControllerInputWrapper.Buttons.LeftBumper, joystickNum))
            {
                if (!castings[0] && !castings[2] && !castings[3])
                {
                    if (abilities[0].abilityReady)
                    {
                        castings[1] = true;
                        abilities[1].Cast();
                        if (abilities[1].triggerOnce)
                        {
                            castings[1] = false;
                            StartCoroutine(EndAbility(1, 0.1f));
                        }
                    }
                }
            }
            else if (castings[1])
            {
                castings[1] = false;
                if (!abilities[1].handledEndCast)
                {
                    abilities[1].EndCast();
                }
            }

            //Debug.Log (Input.GetAxisRaw ("PS4_R1_" + playerString));

            if (ControllerManager.GetButton(ControllerInputWrapper.Buttons.RightBumper, joystickNum))
            {
                if (!castings[0] && !castings[1] && !castings[3])
                {
                    if (abilities[2].abilityReady)
                    {
                        //Debug.Log(joystickNum + " is casting");
                        castings[2] = true;
                        abilities[2].Cast();
                        if (abilities[2].triggerOnce)
                        {
                            castings[2] = false;
                            StartCoroutine(EndAbility(2, 0.1f));
                        }
                    }
                }
            }
            else if (castings[2])
            {
                //Debug.Log("End Cast 3rd ability");
                castings[2] = false;
                if (!abilities[2].handledEndCast)
                {
                    abilities[2].EndCast();
                }
            }

            if (ControllerManager.GetTrigger(ControllerInputWrapper.Triggers.RightTrigger, joystickNum, true) > 0)
            {
                if (!castings[0] && !castings[1] && !castings[2])
                {
                    if (abilities[3].abilityReady)
                    {
                        //Debug.Log(joystickNum + " is casting");
                        castings[3] = true;
                        abilities[3].Cast();
                        if (abilities[3].triggerOnce)
                        {
                            castings[3] = false;
                            StartCoroutine(EndAbility(3, 0.1f));
                        }
                    }
                }
            }
            else if (castings[3])
            {
                //Debug.Log("End Cast 3rd ability");
                castings[3] = false;
                if (!abilities[3].handledEndCast)
                {
                    abilities[3].EndCast();
                }
            }

            if (enchanting)
            {
                enchantEffect.SetActive(true);
            }
            else
            {
                enchantEffect.SetActive(false);
            }
        }
        else
        {
            //Keyboard

            //Handle fire
            if (ControllerManager.GetButton(ControllerInputWrapper.Buttons.A, joystickNum, false))
            {
                if (CkeckCasting_Key())
                {
                    if (abilities[currentAbilityNum_Keyboard].abilityReady)
                    {
                        //Debug.Log(joystickNum + " is casting");
                        //casting_Key = true;
                        abilities[currentAbilityNum_Keyboard].Cast();
                        if (abilities[currentAbilityNum_Keyboard].triggerOnce)
                        {
                            castings[currentAbilityNum_Keyboard] = false;
                            StartCoroutine(EndAbility(currentAbilityNum_Keyboard, 0.1f));
                        }
                    }
                }
            }
            else if(castings[currentAbilityNum_Keyboard])
            {
                //Debug.Log("End Cast 3rd ability");
                castings[currentAbilityNum_Keyboard] = false;
                if (!abilities[currentAbilityNum_Keyboard].handledEndCast)
                {
                    abilities[currentAbilityNum_Keyboard].EndCast();
                }
            }

            if (ControllerManager.GetTrigger(ControllerInputWrapper.Triggers.LeftTrigger, joystickNum, true) > 0)
            {
                currentAbilityNum_Keyboard = 0;
            }
            else if (ControllerManager.GetButton(ControllerInputWrapper.Buttons.LeftBumper, joystickNum))
            {
                currentAbilityNum_Keyboard = 1;
            }
            else if (ControllerManager.GetButton(ControllerInputWrapper.Buttons.RightBumper, joystickNum))
            {
                currentAbilityNum_Keyboard = 2;
            }
            else if (ControllerManager.GetTrigger(ControllerInputWrapper.Triggers.RightTrigger, joystickNum, true) > 0)
            {
                currentAbilityNum_Keyboard = 3;
            }

            if (enchanting)
            {
                enchantEffect.SetActive(true);
            }
            else
            {
                enchantEffect.SetActive(false);
            }

        }
    }

    bool CkeckCasting_Key()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i != currentAbilityNum_Keyboard)
            {
                if (castings[i])
                {
                    return false;
                }
            }

        }
        return true;
    }

}
