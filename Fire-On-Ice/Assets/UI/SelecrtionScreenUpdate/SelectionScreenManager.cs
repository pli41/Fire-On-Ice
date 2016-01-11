using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionScreenManager : MonoBehaviour {
	public float timeDelay = .7f;
	public Text readyTimer;
	public Ability[] allAbilities;
	public Ability mainAbility;
	
	public Animator[] tvScreenAnimations;
	
	public Transform[] enterPanel;
	public Transform[] selectAbility;

	private AsyncOperation async;
	//public Image[] panels = new Image[4];
	public Image[] arrows = new Image[8];
	public Image[] currentAbility = new Image[4];
	public Image[] aAbility = new Image[4];
	public Image[] xAbility = new Image[4];
	public Image[] bAbility = new Image[4];
	public Image[] yAbility = new Image[4];
	public Image[] blockImage = new Image[4];
    public Sprite[] keyboardSprites = new Sprite[4];
    public Sprite[] controllerSprites = new Sprite[4];
    public Text[] readyText = new Text[4];
	public Text[] currentAbilityName = new Text[4];
	public Text[] currentAbilityDescription = new Text[4];
	public float timer;
	public Animator countDownAnim;
	public Animator whiteCoverAnim;


	public AudioSource scrollAbilitySounds;
	public AudioSource selectAbilitySounds;
	public AudioSource TVonSounds;
	public AudioSource countDownSounds;
	
	public AudioClip TwoMenClip;
	public AudioClip ThreeMenClip;
	public AudioClip FourMenClip;
	public AudioClip ReadyToGoClip;
	
	int[] playerControllers = { -1, -1, -1, -1};
	int[] currentAbilitySelected = new int[4];
	int numPlayers;
	bool[] accepted = new bool[5];
	bool[] ready = new bool[4];
	float[] selectTimer = new float[4];
	Ability[,] playerAbilities = new Ability[4, 4];
	
	AudioSource audioS;
	bool ReadyToGoClipPlayed;
	
	
	void Update()
	{ 
		if(async.isDone){
			Debug.Log("Scene loaded");
		}

		checkSelectAbility();
		checkAbilityInput();
		checkPlayerAccept();
		updateReady();
		updateImages();
		checkBlockedImage ();
		updateCurrentAbilityName();
		checkReadyStart();
		updateTVScreen();
		beginMatchCountDown();
	}
	
	void updateTVScreen()
	{
		for (int i = 0; i < numPlayers; i++)
		{
			tvScreenAnimations[i].SetBool("PlayerReady", ready[i]);
		}
		
	}
	
	void checkBlockedImage() {
		for (int i = 0; i < numPlayers; i++) {
			if (checkAbilitySelected(i)) {
				blockImage[i].enabled = true;
			}
			else {
				blockImage[i].enabled = false;
			}
		}
	}
	
	void updateCurrentAbilityName()
	{
		for(int i = 0; i < numPlayers; i++)
		{
			currentAbilityName[i].text = allAbilities[currentAbilitySelected[i]].name;
			currentAbilityDescription[i].text = allAbilities[currentAbilitySelected[i]].description;
		}
	}
	
	bool getAllPlayersReady()
	{
		for (int i = 0; i < numPlayers; i++)
		{
			if (!ready[i])
			{
				timer = 4;
				readyTimer.enabled = false;
				return false;
			}
		}
		return true;
	}
	
	void beginMatchCountDown()
	{
		if (numPlayers < 2)
		{
			return;
		}
		if (!getAllPlayersReady ()) {
			
			countDownAnim.SetBool ("PlayerStart", false);
			
			return;
		} else {
			countDownAnim.SetBool("PlayerStart", true);
		}
		timer -= Time.deltaTime;
		if (timer < 0)
		{
			Debug.Log("Play Audio");
			setGameSettings();
			if(!ReadyToGoClipPlayed){
				audioS.Stop();
				audioS.clip = ReadyToGoClip;
				audioS.Play();
				ReadyToGoClipPlayed = true;
			}
			whiteCoverAnim.SetTrigger("whiteOutStart");
			Invoke("StartGame", 2f);
		}
	}
	
	void StartGame(){
		async.allowSceneActivation = true;
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
			if (ControllerManager.GetButton(ControllerInputWrapper.Buttons.Start, playerControllers[i], true))
				//if (Input.GetKeyDown(KeyCode.KeypadEnter)) 
			{
                
				if (ready[i] && timer > 0)
				{
					ready[i] = false;
					//countDownAnim.SetTrigger("CancelCD");
				}
				else if (checkAbilityFilled(i))
				{
					ready[i] = true;
					if (getAllPlayersReady())
					{
						//countDownAnim.SetTrigger("BeginCD");
					}
					
				}
			}
        }


       

	}

	/// <summary>
	/// Checks the ability filled.
	/// </summary>
	/// <returns><c>true</c>, if ability filled was checked, <c>false</c> otherwise.</returns>
	/// <param name="i">The index.</param>
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
				aAbility[i].sprite = playerAbilities[i, 0].icon;
			}
			if (playerAbilities[i, 1] != null)
			{
				xAbility[i].sprite = playerAbilities[i, 1].icon;
			}
			if (playerAbilities[i, 2] != null)
			{
				bAbility[i].sprite = playerAbilities[i, 2].icon;
			}
			if (playerAbilities[i, 3] != null)
			{
				yAbility[i].sprite = playerAbilities[i, 3].icon;
			}
		}
	}
	
	void checkAbilityInput()
	{
		for (int i = 0; i < numPlayers; i++)
		{
			if (ControllerManager.GetTrigger(ControllerInputWrapper.Triggers.LeftTrigger, playerControllers[i], true) > .01f)
			{
				if (checkAbilitySelected(i)) {
					return;
				}
				selectAbilitySounds.Stop();
				selectAbilitySounds.Play();
				playerAbilities[i, 0] = allAbilities[currentAbilitySelected[i]];
				//aAbility[i].sprite = allAbilities[currentAbilitySelected[i]].icon;
			}else if  (ControllerManager.GetButton(ControllerInputWrapper.Buttons.LeftBumper, playerControllers[i], true))
			{
				if (checkAbilitySelected(i)) {
					return;
				}
				playerAbilities[i, 1] = allAbilities[currentAbilitySelected[i]];
				selectAbilitySounds.Stop();
				selectAbilitySounds.Play();
				//xAbility[i].sprite = allAbilities[currentAbilitySelected[i]].icon;
			}
		else if (ControllerManager.GetButton(ControllerInputWrapper.Buttons.RightBumper, playerControllers[i], true))
			{
				if (checkAbilitySelected(i)) {
					return;
				}
				selectAbilitySounds.Stop();
				selectAbilitySounds.Play();
				playerAbilities[i, 2] = allAbilities[currentAbilitySelected[i]];
				//bAbility[i].sprite = allAbilities[currentAbilitySelected[i]].icon;
			}
		}
	}

	/// <summary>
	/// Checks the ability selected.
	/// </summary>
	/// <returns><c>true</c>, if ability selected was checked, <c>false</c> otherwise.</returns>
	/// <param name="i">The index.</param>
	bool checkAbilitySelected(int i) {
		for (int j = 0; j < 3; j++) {
			if (playerAbilities[i, j] != null && playerAbilities[i, j].name == allAbilities[currentAbilitySelected[i]].name) {
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// Checks the select ability.
	/// </summary>
	void checkSelectAbility()
	{
		for (int i = 0; i < numPlayers; i++)
		{
			float direct = ControllerManager.GetAxis(ControllerInputWrapper.Axis.LeftStickX, playerControllers[i], true);
			selectTimer[i] -= Time.deltaTime;
			//print(direct);
			if (direct < -0.05f && selectTimer[i] <= 0)
			{
				currentAbilitySelected[i] = (--currentAbilitySelected[i]) % allAbilities.Length;
				scrollAbilitySounds.Stop();
				scrollAbilitySounds.Play();
				arrows[i*2].GetComponent<Animator>().SetTrigger("arrowChange");
				if (currentAbilitySelected[i] < 0)
				{
					currentAbilitySelected[i] += allAbilities.Length;
				}
				selectTimer[i] = timeDelay;
			} else if (direct > 0.05f && selectTimer[i] <= 0)
			{
				currentAbilitySelected[i] = (++currentAbilitySelected[i]) % allAbilities.Length;
				scrollAbilitySounds.Stop();
				scrollAbilitySounds.Play();
				arrows[i*2+1].GetComponent<Animator>().SetTrigger("arrowChange");
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
		audioS = GetComponent<AudioSource>();
        ControllerManager.setUpControls();
		async = SceneManager.LoadSceneAsync ("level3");
		async.allowSceneActivation = false;
	}

	/// <summary>
	/// Checks the player accept.
	/// </summary>
	void checkPlayerAccept()
	{
		for (int i = -1; i < playerControllers.Length; i++)
		{
			if (ControllerManager.GetButton(ControllerInputWrapper.Buttons.Start, i + 1, true) && !accepted[i+1])
			{
                //Debug.Log("i = " + i );

                //Set key icons

                int targetPanelNum = FindSelectPanelNum(i);
                ChangeButtonIcons(i==0, targetPanelNum);

				TVonSounds.Stop();
				TVonSounds.Play();
				playerAbilities[numPlayers, 3] = mainAbility;
				tvScreenAnimations[numPlayers].GetComponent<Image>().enabled = true;
				tvScreenAnimations[numPlayers].SetTrigger("PlayerStart");
				playerControllers[numPlayers] = i + 1;
				accepted[i+1] = true;
				//panels[numPlayers].enabled = false;
				enterPanel[numPlayers].gameObject.SetActive(false);
				selectAbility[numPlayers].gameObject.SetActive(true);
				numPlayers++;
				if(numPlayers == 2){
					//Debug.Log("2 men audio");
					audioS.Stop();
					audioS.clip = TwoMenClip;
					audioS.Play();
				}
				else if (numPlayers == 3){
					//Debug.Log("3 men audio");
					audioS.Stop();
					audioS.clip = ThreeMenClip;
					audioS.Play();
				}
				else if (numPlayers == 4){
					//Debug.Log("4 men audio");
					audioS.Stop();
					audioS.clip = FourMenClip;
					audioS.Play();
				}
			}
		}
	}

    int FindSelectPanelNum(int target)
    {
        for (int i = 0; i < playerControllers.Length; i++)
        {
            if (playerControllers[i] == target)
            {
                return i;
            }
        }
        return -1;
    }

    void ChangeButtonIcons(bool isController, int num)
    {
        if (isController)
        {
            Image button_1 = selectAbility[num].GetComponent<RectTransform>().Find("L2").GetComponent<RectTransform>().Find("Image").GetComponent<Image>();
            button_1.sprite = controllerSprites[0];
            button_1.GetComponent<RectTransform>().sizeDelta.Set(105f, 57f);
            Image button_2 = selectAbility[num].GetComponent<RectTransform>().Find("L1").GetComponent<RectTransform>().Find("Image").GetComponent<Image>();
            button_2.sprite = controllerSprites[1];
            button_2.GetComponent<RectTransform>().sizeDelta.Set(105f, 57f);
            Image button_3 = selectAbility[num].GetComponent<RectTransform>().Find("R1").GetComponent<RectTransform>().Find("Image").GetComponent<Image>();
            button_3.sprite = controllerSprites[2];
            button_3.GetComponent<RectTransform>().sizeDelta.Set(105f, 57f);
            Image button_4 = selectAbility[num].GetComponent<RectTransform>().Find("R2").GetComponent<RectTransform>().Find("Image").GetComponent<Image>();
            button_4.sprite = controllerSprites[3];
            button_4.GetComponent<RectTransform>().sizeDelta.Set(105f, 57f);
        }
        else
        {
            Image button_1 = selectAbility[num].GetComponent<RectTransform>().Find("L2").GetComponent<RectTransform>().Find("Image").GetComponent<Image>();
            button_1.sprite = keyboardSprites[3];
            button_1.GetComponent<RectTransform>().sizeDelta = new Vector2(57f, 57f); button_1.GetComponent<RectTransform>().sizeDelta = new Vector2(57f, 57f);
            Image button_2 = selectAbility[num].GetComponent<RectTransform>().Find("L1").GetComponent<RectTransform>().Find("Image").GetComponent<Image>();
            button_2.sprite = keyboardSprites[2];
            button_2.GetComponent<RectTransform>().sizeDelta = new Vector2(57f, 57f);
            Image button_3 = selectAbility[num].GetComponent<RectTransform>().Find("R1").GetComponent<RectTransform>().Find("Image").GetComponent<Image>();
            button_3.sprite = keyboardSprites[1];
            button_3.GetComponent<RectTransform>().sizeDelta = new Vector2(57f, 57f);
            Image button_4 = selectAbility[num].GetComponent<RectTransform>().Find("R2").GetComponent<RectTransform>().Find("Image").GetComponent<Image>();
            button_4.sprite = keyboardSprites[0];
            button_4.GetComponent<RectTransform>().sizeDelta = new Vector2(57f, 57f);
        }
        
    }
}


