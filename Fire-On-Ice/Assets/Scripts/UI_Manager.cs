using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour {

	public GameManager gm;

	private List<GameObject> playerList;
	public List<GameObject> playerUIs = new List<GameObject>();
	public List<GameObject> abilityUIs = new List<GameObject>();

	public GameObject winUI;
	public Text survivorText;
	public Text damageText;
	public Text treasureText;

	public GameObject readyText;
	public GameObject goText;
	public float readyTextDuration;
	public float goTextDuration;

	public Animator whiteCoverAnim;
	public Animator tutorialAnim;

    public Camera camera;

    public Image Player_Keyboard_CurrentAbility;

    public List<GameObject> arrows = new List<GameObject>();

    private bool arrowsShowed;
	private bool inWinUI;
	private string[] colorsForPlayerNum = {"<color=red>RED</color>", "<color=blue>BLUE</color>"
		, "<color=green>GREEN/colors", "<color=yellow>YELLOW</color>"};
	private GameObject[] cooldownUIs;
	
	// Use this for initialization
	public void Start () {
        arrowsShowed = false;
		playerList = gm.playerList;
		cooldownUIs = GameObject.FindGameObjectsWithTag ("Cooldown");
		SetupAbilityPanels ();
		SetupAbilityIcons ();
	}

	public void Update(){
		if(inWinUI){
			if(ControllerManager.GetButton(ControllerInputWrapper.Buttons.A, 1) ||
			   ControllerManager.GetButton(ControllerInputWrapper.Buttons.A, 2) ||
			   ControllerManager.GetButton(ControllerInputWrapper.Buttons.A, 3) ||
			   ControllerManager.GetButton(ControllerInputWrapper.Buttons.A, 4)){
                SceneManager.LoadScene("Level3");
            }
			else if(ControllerManager.GetButton(ControllerInputWrapper.Buttons.B, 1) ||
			        ControllerManager.GetButton(ControllerInputWrapper.Buttons.B, 2) ||
			        ControllerManager.GetButton(ControllerInputWrapper.Buttons.B, 3) ||
			        ControllerManager.GetButton(ControllerInputWrapper.Buttons.B, 4)){
                SceneManager.LoadScene("Main_Menu");
            }
		}
	}
	
	public void HideTutorial(){
		whiteCoverAnim.SetTrigger ("transparencyInc");
		tutorialAnim.SetTrigger ("transparencyInc");
	}

    public void ShowArrows()
    {
        if (!arrowsShowed)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                arrows[i].SetActive(true);
            }
            arrowsShowed = true;
        }
        
        Invoke("HideArrows", 1.5f);
    }

    public void HideArrows()
    {
        for (int i = 0; i < arrows.Count; i++)
        {
            arrows[i].SetActive(false);
        }
        Invoke("ReadyTextEnable", 0.5f);
    }

	public void ReadyTextEnable(){
		if(readyText){
			readyText.SetActive (true);
		}
		Destroy (readyText, readyTextDuration);
		Invoke ("GoTextEnable", readyTextDuration);
	}

	public void GoTextEnable(){
		if(goText){
			goText.SetActive (true);
		}
		Destroy (goText, goTextDuration);
		Invoke ("UIReady", goTextDuration);
	}

	public void UIReady(){
		CancelInvoke ();
		gm.StartGame ();
	}

	void SetupAbilityPanels(){
		//set ability UIs
		for(int j = 0; j < playerList.Count; j++){
			playerUIs[j].SetActive(true);
		}

	}

	void SetupAbilityIcons(){
		//Debug.Log ("SetupAbilityIcons");
		for (int i = 0; i < playerList.Count; i++){
			//Get sprites from playerAttack

			List<Sprite> sprites = new List<Sprite>();
			for (int j = 0; j < 3; j++){
				sprites.Add(playerList[i].GetComponent<PlayerAttack>().abilities[j].icon);
			}
			//Debug.Log(sprites.Count);
			//Assign sprites
			//int playerNum = playerList[i].GetComponent<PlayerAttack>().joystickNum-1;

			for (int k = 0; k < 3; k++){

				Image image = abilityUIs[i].GetComponent<RectTransform>().Find("Ability_" + (k+1))
					.GetComponent<Image>();
				image.sprite = sprites[k];
			}

		}
	}


	void OnGUI(){
		//update cooldown UI
		//i is player number; j is ability number.
		if(gm.GameInProgress){
			for (int i = 0; i < playerList.Count; i++) {
				for (int j = 0; j < 3; j++){
					int playerNum = playerList[i].GetComponent<PlayerAttack>().playerNum;
					
					bool abilityReady = playerList[i].GetComponent<PlayerAttack>().abilities[j].abilityReady;
					
					GameObject cooldownUI = abilityUIs[playerNum-1].GetComponent<RectTransform>().Find("Ability_" + (j+1) 
					                                                                                   + "/Cooldown").gameObject;
					cooldownUI.SetActive(!abilityReady);
					if(!abilityReady){
						Text text = cooldownUI.GetComponent<RectTransform>().Find("Text").GetComponent<Text>();
                        Image image = cooldownUI.GetComponent<RectTransform>().Find("Image").GetComponent<Image>();
                        float shadePercent = playerList[i].GetComponent<PlayerAttack>().abilities[j].timeUntilReset / playerList[i].GetComponent<PlayerAttack>().abilities[j].cooldown;
                        text.text = ((int)playerList[i].GetComponent<PlayerAttack>().abilities[j].timeUntilReset).ToString();
                        //Debug.Log(shadePercent);
                        image.fillAmount = shadePercent;
					}
				}
			}

            HandleKeyboardPlayerUI();
		}
	}

    public void HandleKeyboardPlayerUI()
    {
        GameObject keyboardPlayer = FindKeyboardPlayer();
        if (keyboardPlayer)
        {
            
            Vector3 pos = keyboardPlayer.transform.position;
            Vector2 ViewportPosition = camera.WorldToViewportPoint(pos);
            //Debug.Log(ViewportPosition);
            
            Player_Keyboard_CurrentAbility.sprite = keyboardPlayer.GetComponent<PlayerAttack>().abilities[keyboardPlayer.GetComponent<PlayerAttack>().currentAbilityNum_Keyboard].icon;
            Player_Keyboard_CurrentAbility.GetComponent<RectTransform>().anchoredPosition = new Vector2(ViewportPosition.x * Screen.width - Screen.width/2f , ViewportPosition.y * Screen.height - Screen.height /2f + 55f);
            Player_Keyboard_CurrentAbility.GetComponent<RectTransform>().sizeDelta = new Vector2(25f, 25f);
            Player_Keyboard_CurrentAbility.enabled = true;
        }
        else
        {
            Player_Keyboard_CurrentAbility.enabled = false;
        }
    }

    public GameObject FindKeyboardPlayer()
    {
        foreach (GameObject player in playerList)
        {
            if (player.GetComponent<PlayerAttack>().joystickNum == 0)
            {
                return player;
            }
        }


        return null;
    }

	public void SetPlayerDeathByNum(int playerNum){
		playerUIs [playerNum-1].GetComponent<RectTransform>().Find ("Avatar").GetComponent<AnimateGif>().start = true;
		//Debug.Break();
	}

	public void ShowWinScreen(int winPlayerNum, int mostDamagePlayerNum, int chestHunterNum){
		survivorText.text = "Survivor: " + colorsForPlayerNum[winPlayerNum-1] + " Player!";

		if(mostDamagePlayerNum == 0){
			damageText.text = "WTH! You all did 0 damage!";
		}
		else{
			damageText.text = "Damage Dealer: "+  colorsForPlayerNum[mostDamagePlayerNum-1] + " Player!";
		}

		if(chestHunterNum == 0){
			treasureText.text = "Well, what a bunch of fighting nerds!";
		}
		else{
			treasureText.text = "Treasure Hunter: " + colorsForPlayerNum[chestHunterNum-1] + " Player!";
		}
		inWinUI = true;
		winUI.SetActive (true);
	}

	public void Rematch(){
        SceneManager.LoadScene("Level3");

    }

	public void BackToMainMenu(){
        SceneManager.LoadScene("Main_Menu");
    }

}
