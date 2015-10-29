using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectionScreenManager : MonoBehaviour {
    public float timeDelay = .7f;
    public Text readyTimer;
    public Ability[] allAbilities;

    public Transform[] enterPanel;
    public Transform[] selectAbility;

    public Image[] currentAbility = new Image[4];
    public Image[] aAbility = new Image[4];
    public Image[] xAbility = new Image[4];
    public Image[] bAbility = new Image[4];
    public Text[] readyText = new Text[4];
    public float timer;


    int[] playerControllers = new int[4];
    int[] currentAbilitySelected = new int[4];
    int numPlayers;
    bool[] accepted = new bool[4];
    bool[] ready = new bool[4];
    float[] selectTimer = new float[4];
    Ability[,] playerAbilities = new Ability[4, 3];



    void Update()
    {
        
        checkSelectAbility();
        checkAbilityInput();
        checkPlayerAccept();
        updateReady();
        updateImages();
        checkReadyStart();
        beginMatchCountDown();
    }

    void beginMatchCountDown()
    {
        if (numPlayers < 1)
        {
            return;
        }
        for (int i = 0; i < numPlayers; i++)
        {
            if (!ready[i])
            {
                timer = 4;
                readyTimer.enabled = false;
                return;
            }
        }
        timer -= Time.deltaTime;
        int t = (int)timer;
        readyTimer.enabled = true;
        readyTimer.text = t.ToString();
        if (timer < 1)
        {
			setGameSettings();
            Application.LoadLevel("level3");
        }

    }

	void setGameSettings() {
		GameSettings.numPlayers = numPlayers;
		GameSettings.playerAbilitites = playerAbilities;
		GameSettings.playerController = playerControllers;
	}

    void updateReady()
    {

        for (int i = 0; i < numPlayers; i++)
        {
            if (ready[i])
            {
                selectAbility[i].gameObject.SetActive(false);
                readyText[i].enabled = true;
            }
            else
            {
                selectAbility[i].gameObject.SetActive(true);
                readyText[i].enabled = false;
            }
        }
    }

    void checkReadyStart()
    {
        for (int i = 0; i < numPlayers; i++)
        {
            if (ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.Start, playerControllers[i], true))
            //if (Input.GetKeyDown(KeyCode.KeypadEnter)) 
            {
                
                if (ready[i])
                {
                    ready[i] = false;
                }
                else if (checkAbilityFilled(i))
                {
                    print("Hello");
                    ready[i] = true;
                }
            }
        }
    }

    bool checkAbilityFilled(int i)
    {
        for (int j = 0; j < playerAbilities.GetLength(1); j++)
        {
            if (playerAbilities[i, j] == null)
            {
                return false;
            }
        }
        return true;
    }

    bool checkAbilityFilled()
    {
        if (numPlayers < 1)
        {
            return false;
        }
        for (int i = 0; i < numPlayers; i++)
        {
            if (!checkAbilityFilled(i))
            {
                return false;
            }
        }
        return true;
    }

    void updateImages()
    {
        for (int i = 0; i < numPlayers; i++)
        {
            currentAbility[i].sprite = allAbilities[currentAbilitySelected[i]].icon;
            if (playerAbilities[i, 0] != null)
            {
                xAbility[i].sprite = playerAbilities[i, 0].icon;
            }
            if (playerAbilities[i, 1] != null)
            {
                aAbility[i].sprite = playerAbilities[i, 1].icon;
            }
            if (playerAbilities[i, 2] != null)
            {
                bAbility[i].sprite = playerAbilities[i, 2].icon;
            }
        }
    }

    void checkAbilityInput()
    {
        for (int i = 0; i < numPlayers; i++)
        {
            if  (ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.X, playerControllers[i], true))
            {
                playerAbilities[i, 0] = allAbilities[currentAbilitySelected[i]];
                //xAbility[i].sprite = allAbilities[currentAbilitySelected[i]].icon;
            }
            else if (ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.A, playerControllers[i], true))
            {
                playerAbilities[i, 1] = allAbilities[currentAbilitySelected[i]];
                //aAbility[i].sprite = allAbilities[currentAbilitySelected[i]].icon;
            }else if (ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.B, playerControllers[i], true))
            {
                playerAbilities[i, 2] = allAbilities[currentAbilitySelected[i]];
                //bAbility[i].sprite = allAbilities[currentAbilitySelected[i]].icon;
            }
        }
    }

    void checkSelectAbility()
    {
        for (int i = 0; i < numPlayers; i++)
        {
            float direct = ControllerInputWrapper.GetAxisRaw(ControllerInputWrapper.Axis.LeftStickX, playerControllers[i]);
            selectTimer[i] -= Time.deltaTime;
            //print(direct);
            if (direct < -0.05f && selectTimer[i] <= 0)
            {
                currentAbilitySelected[i] = (--currentAbilitySelected[i]) % allAbilities.Length;
                if (currentAbilitySelected[i] < 0)
                {
                    currentAbilitySelected[i] += allAbilities.Length;
                }
                selectTimer[i] = timeDelay;
            } else if (direct > 0.05f && selectTimer[i] <= 0)
            {
                currentAbilitySelected[i] = (++currentAbilitySelected[i]) % allAbilities.Length;
                selectTimer[i] = timeDelay;
            }
            else if (Mathf.Abs(direct) < .05f)
            {
               selectTimer[i] = 0;
            }

        }
    }

    void Start()
    {
        ControllerInputWrapper.setControlTypes();
        ControllerInputWrapper.setPlatform();
    }

    void checkPlayerAccept()
    {
        for (int i = 0; i < playerControllers.Length; i++)
        {
            if (ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.A, i + 1, true) && !accepted[i])
            {
                playerControllers[numPlayers] = i + 1;
                accepted[i] = true;
                enterPanel[numPlayers].gameObject.SetActive(false);
                selectAbility[numPlayers].gameObject.SetActive(true);
                numPlayers++;
            }
        }
    }
}


